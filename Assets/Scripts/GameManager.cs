using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour 
{
	public const int MIN_TURNS_TO_WIN = 4;
	public const int MAX_TURNS = 9;

	public static GameManager instance = null;    
	
	public int turnCount = 0;
	public bool isPlayerTurn = true;

	public int playerScore = 0;
	public int tieScore = 0;
	public int computerScore = 0;

	private HUD _hud;
	private Grid _grid;
	private AI _ai;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

	void Start()
	{
		_hud = HUD.instance;
		_grid = Grid.instance;
		_ai = GetComponent<AI>();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
			SceneManager.LoadScene("Gameplay");
	}

	public void PlayerMove(Piece piece)
	{
		Move(piece);
	}

	public void Move(Piece piece)
	{
		_grid.SetPiece(piece.gridPosition);

		if(CheckForWinner())
		{
			Win();
			return;
		}

		PassTurn();

		if(TurnsEnded())
		{
			Tie();
			return;
		}
		else
		{
			ToggleTurnPlayer();

			if(!isPlayerTurn)
				AIMove();
		}

	}

	private bool CheckForWinner()
	{
		var hasMinTurnsToCheckForWinner = (turnCount >= MIN_TURNS_TO_WIN);
		return hasMinTurnsToCheckForWinner && _grid.CheckForWinner();
	}

	private void Win()
	{
		if(isPlayerTurn) 
			PlayerWin();
		else
			AIWin();

		EndMatch();
	}

	private void PlayerWin()
	{
		playerScore++;
		_hud.PlayerWin();
	}

	private void AIWin()
	{
		computerScore++;
		_hud.ComputerWin();
	}
	
	private void EndMatch()
	{
		_grid.DisableAllPieces();
		_hud.EnableRestartButton();
	}
	
	private void PassTurn()
	{
		turnCount++;
	}

	private bool TurnsEnded() 
	{
		return turnCount == MAX_TURNS;
	}

	private void Tie()
	{
		ToggleTurnPlayer();
		tieScore++;
		_hud.Tie();
		EndMatch();
	}

	private void ToggleTurnPlayer()
	{
		isPlayerTurn = !isPlayerTurn;
		_hud.UpdateTurnText();
	}
		
	private void AIMove()
	{
		Piece aiPiece = _ai.GetMovePiece();
		Move(aiPiece);
	}

	public void RestartGame()
	{
		turnCount = 0;
		_hud.Restart();
		_grid.RemoveAllPieces();

		if(!isPlayerTurn)
			AIMove();
	}
}
