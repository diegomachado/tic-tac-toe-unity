using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Grid : MonoBehaviour 
{
	private const int EMPTY = 0;
	private const int PLAYER_PIECE = 1;
	private const int COMPUTER_PIECE = 2;

	public static Grid instance = null;    

	public Sprite playerPiece;
	public Sprite computerPiece;

	public bool isPlayerTurn = true;

	public int[] spaces = new int[9];

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}
		
	public void SetSpacePiece(int spacePosition) 
	{
		spaces[spacePosition] = isPlayerTurn ? PLAYER_PIECE : COMPUTER_PIECE;
		PassTurn();
	}

	private void PassTurn()
	{
		isPlayerTurn = !isPlayerTurn;
	}
}
