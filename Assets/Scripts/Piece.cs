using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Piece : MonoBehaviour 
{
	private Image image;
	public Button button;
	public int gridPosition;

	void Start () 
	{
		image = GetComponent<Image>();
		button = GetComponent<Button>();
		button.onClick.AddListener(SetPiece);
	}

	public void SetPiece() 
	{
		SetImage();
		Grid.instance.MakeMove(this);
		button.enabled = false;
	}

	void SetImage()
	{
		var grid = Grid.instance;
		var gameManager = GameManager.instance;

		image.sprite = gameManager.isPlayerTurn ? grid.playerPiece : grid.computerPiece;
		var color = image.color;
		color.a = 1;
		image.color = color;
	}

	public void RemoveImage()
	{
		image.sprite = null;
		var color = image.color;
		color.a = 0;
		image.color = color;
	}

	public void Enable()
	{
		button.enabled = true;
	}

	public void Disable()
	{
		button.enabled = false;
	}
}
