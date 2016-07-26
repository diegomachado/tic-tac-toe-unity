using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Piece : MonoBehaviour 
{
	private Image image;
	private Button button;
	public int gridPosition;

	void Start () 
	{
		image = GetComponent<Image>();
		button = GetComponent<Button>();
		button.onClick.AddListener(SetPiece);
	}

	public void SetPiece() 
	{
		var grid = Grid.instance;
		var gameManager = GameManager.instance;

		image.sprite = gameManager.isPlayerTurn ? grid.playerPiece : grid.computerPiece;
		var color = image.color;
		color.a = 1;
		image.color = color;
		button.enabled = false;

		grid.SetSpacePiece(gridPosition);
	}
}
