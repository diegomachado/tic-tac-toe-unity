using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
	public static GameManager instance = null;    
	
	public bool isPlayerTurn = true;
	public int turnCount = 0;

	public int playerScore;
	public int tiesScore;
	public int computerScore;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}
		
	public void PassTurn()
	{
		turnCount++;

		if(turnCount == 9)
		{
			Tie();
		}
		else
		{
			isPlayerTurn = !isPlayerTurn;
			HUD.instance.PassTurn();
		}
	}

	public void Win()
	{
		if(isPlayerTurn) PlayerWin();
		else ComputerWin();
	}

	private void PlayerWin()
	{
		HUD.instance.PlayerWin();
	}

	private void ComputerWin()
	{
		HUD.instance.ComputerWin();
	}

	private void Tie()
	{
		HUD.instance.Tie();
	}
}
