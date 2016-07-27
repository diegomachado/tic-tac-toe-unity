using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	public static HUD instance = null;    

	public Text playerScoreText;
	public Text tieScoreText;
	public Text computerScoreText;

	public StatusText statusText;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

	void Start()
	{
		playerScoreText.text = tieScoreText.text = computerScoreText.text = 0.ToString();
	}

	public void PassTurn()
	{
		statusText.SetTurnText();
	}

	public void PlayerWin()
	{
		playerScoreText.text = GameManager.instance.playerScore.ToString();
		statusText.PlayerWin();
	}

	public void Tie()
	{
		tieScoreText.text = GameManager.instance.tieScore.ToString();
		statusText.Tie();
	}

	public void ComputerWin()
	{
		computerScoreText.text = GameManager.instance.computerScore.ToString();
		statusText.ComputerWin();
	}

	public void UpdateTurnText()
	{
		statusText.SetTurnText();
	}
}
