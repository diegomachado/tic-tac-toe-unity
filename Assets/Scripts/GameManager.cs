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
	private Board _board;
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
		_board = Board.instance;
		_ai = GetComponent<AI>();
	}

	public void PlayerMove(Piece piece)
	{
		Move(piece);
	}

	public void Move(Piece piece)
	{
		_board.SetPiece(piece.gridPosition);
		SoundManager.instance.PlayRandomSetPieceSFX();

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
				StartCoroutine(AIMove());
		}

	}

	private bool CheckForWinner()
	{
		var hasMinTurnsToCheckForWinner = (turnCount >= MIN_TURNS_TO_WIN);
		return hasMinTurnsToCheckForWinner && _board.CheckForWinner();
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
		_board.DisableAllPieces();
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
		
	IEnumerator AIMove(float delayTime = 1f)
	{
		yield return new WaitForSeconds(delayTime);
		Piece aiPiece = _ai.MovePiece();
		Move(aiPiece);
	}

	public void RestartGame()
	{
		turnCount = 0;
		_hud.Restart();
		_board.RemoveAllPieces();

		if(!isPlayerTurn)
			StartCoroutine(AIMove(.4f));
	}

	public void BackToMenu()
	{
		turnCount = 0;
		_hud.Restart();
		_board.RemoveAllPieces();

		playerScore = 0;
		tieScore = 0;
		computerScore = 0;

		SceneManager.LoadScene("Menu");
	}
}
