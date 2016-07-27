using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusText : MonoBehaviour 
{
	public string playerTurnText = "Player's Turn";
	public string computerTurnText = "Computer's Turn";

	public string playerWinText = "Player Wins!";
	public string tieText = "It's a Tie!";
	public string computerWinText = "Computer Wins :(";

	private Text text;

	void Start() 
	{
		text = GetComponent<Text>();
		SetTurnText();
	}

	public void SetTurnText()
	{
		text.text = GameManager.instance.isPlayerTurn ? playerTurnText : computerTurnText;
	}

	public void PlayerWin()
	{
		text.text = playerWinText;
	}

	public void ComputerWin()
	{
		text.text = computerWinText;
	}
	
	public void Tie()
	{
		text.text = tieText;
	}
}
