using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class HUD : MonoBehaviour 
{
	public static HUD instance = null;    

	public Text playerScoreText;
	public Text tieScoreText;
	public Text computerScoreText;

	public StatusText statusText;
	public Button restartButton;

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
		DisableRestartButton();
	}

	public void Restart()
	{
		UpdateTurnText();
		DisableRestartButton();
	}

	public void DisableRestartButton()
	{
		restartButton.GetComponent<ButtonHelper>().DisableHidingImage();
	}

	public void EnableRestartButton()
	{
		restartButton.GetComponent<ButtonHelper>().EnableShowingImage();
	}
		
	public void UpdateTurnText()
	{
		statusText.SetTurnText();
	}

	public void PlayerWin()
	{
		playerScoreText.text = GameManager.instance.playerScore.ToString();
		playerScoreText.transform.DOPunchScale(new Vector3(.5f, .5f), .8f);
		statusText.PlayerWin();
	}

	public void Tie()
	{
		tieScoreText.text = GameManager.instance.tieScore.ToString();
		tieScoreText.transform.DOPunchScale(new Vector3(.5f, .5f), .8f);
		statusText.Tie();
	}

	public void ComputerWin()
	{
		computerScoreText.text = GameManager.instance.computerScore.ToString();
		computerScoreText.transform.DOPunchScale(new Vector3(.5f, .5f), .8f);
		statusText.ComputerWin();
	}
}
