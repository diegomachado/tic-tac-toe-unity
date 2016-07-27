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

	// TODO: Extract to Player Class
	public void MakeMove(Piece piece)
	{
		_grid.SetPiece(piece.gridPosition);

		if(HasMinTurnsToCheckForWinner() && _grid.CheckForWinner())
		{
			Win();
		}
		else
		{
			PassTurn();

			if(GameEnded()) 
			{
				Tie();
			}
			else 
			{
				ToggleTurnPlayer();
				AIMove();
			}
		}
	}

	public bool HasMinTurnsToCheckForWinner()
	{
		return turnCount >= MIN_TURNS_TO_WIN;
	}

	public void Win()
	{
		if(isPlayerTurn) 
			PlayerWin();
		else 
			ComputerWin();
		
		EndMatch();
	}

	private void PlayerWin()
	{
		playerScore++;
		_hud.PlayerWin();
	}

	private void ComputerWin()
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

	private bool GameEnded() 
	{
		return turnCount == MAX_TURNS;
	}

	private void ToggleTurnPlayer()
	{
		isPlayerTurn = !isPlayerTurn;
		_hud.UpdateTurnText();
	}

	private void Tie()
	{
		ToggleTurnPlayer();
		tieScore++;
		_hud.Tie();
		EndMatch();
	}

	private void AIMove()
	{
		_ai.Play();

		if(HasMinTurnsToCheckForWinner() && _grid.CheckForWinner())
		{
			Win();
		}
		else
		{
			PassTurn();

			if(GameEnded()) 
			{
				Tie();
			}
			else 
			{
				ToggleTurnPlayer();
			}
		}
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
