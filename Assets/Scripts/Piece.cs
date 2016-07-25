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
		var grid = Grid.instance;

		image.sprite = grid.isPlayerTurn ? grid.playerPiece : grid.computerPiece;
		var color = image.color;
		color.a = 1;
		image.color = color;
		button.enabled = false;

		grid.SetSpacePiece(gridPosition);
	}
}
