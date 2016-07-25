using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Grid : MonoBehaviour 
{
	public const int EMPTY = 0;
	public const int CIRCLE = 1;
	public const int SQUARE = 2;

	public bool isPlayerTurn = true;

	public Sprite playerPiece;
	public Sprite computerPiece;

	public int[] spaces = new int[9];
	public Button[] spacesImages = new Button[9];

	void Start()
	{
		spacesImages = transform.Find("Spaces").GetComponentsInChildren<Button>();
	}
		
	public void SetSpacePiece(Button piece) 
	{
		Debug.Log(piece.image.sprite);

		if(isPlayerTurn)
		{
			piece.image.sprite = playerPiece;
			var color = piece.image.color;
			color.a = 1;
			piece.image.color = color;
			piece.enabled = false;
		} 
		else
		{
			piece.image.sprite = computerPiece;
			var color = piece.image.color;
			color.a = 1;
			piece.image.color = color;
			piece.enabled = false;
		}
	}
}
