using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public const int MIN_TURNS_TO_WIN = 4;
	public const int MAX_TURNS = 9;

	public static GameManager instance = null;    
	
	public bool isPlayerTurn = true;
	public int turnCount = 0;

	public int playerScore = 0;
	public int tieScore = 0;
	public int computerScore = 0;

	public bool canRestartGame = false;

	private HUD _hud;
	private Grid _grid;

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
	}

	public bool HasMinTurnsToCheckForWinner()
	{
		return turnCount >= MIN_TURNS_TO_WIN;
	}
		
	public void PassTurn()
	{
		turnCount++;

		if(turnCount == MAX_TURNS)
		{
			Tie();
			canRestartGame = true;
		}
		else
		{
			isPlayerTurn = !isPlayerTurn;
			_hud.PassTurn();
		}
	}

	public void Win()
	{
		if(isPlayerTurn) PlayerWin();
		else ComputerWin();

		canRestartGame = true;
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

	private void Tie()
	{
		ChangeFirstToPlay();
		tieScore++;
		_hud.Tie();
	}

	private void ChangeFirstToPlay()
	{
		isPlayerTurn = !isPlayerTurn;
	}

	public void RestartGame()
	{
		turnCount = 0;
		_hud.UpdateTurnText();
		_grid.RemoveAllPieces();
	}
}
