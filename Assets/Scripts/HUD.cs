using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour 
{
	public static HUD instance = null;    

	public Text playerScoreText;
	public Text tiesScoreText;
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
		playerScoreText.text = tiesScoreText.text = computerScoreText.text = 0.ToString();
	}

	public void PassTurn()
	{
		statusText.PassTurn();
	}

	public void PlayerWin()
	{
		statusText.PlayerWin();
	}

	public void ComputerWin()
	{
		statusText.ComputerWin();
	}

	public void Tie()
	{
		statusText.Tie();
	}
}
