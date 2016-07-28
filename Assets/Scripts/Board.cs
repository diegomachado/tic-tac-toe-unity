using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour 
{
	public const int EMPTY = 0;
	public const int PLAYER_PIECE = 1;
	public const int COMPUTER_PIECE = 2;

	public static Board instance = null;    

	public int[,] winConfigs = new int[,]
	{
		{ 0, 1, 2 },
		{ 3, 4, 5 },
		{ 6, 7, 8 },
		{ 0, 3, 6 },
		{ 1, 4, 7 },
		{ 2, 5, 8 },
		{ 0, 4, 8 },
		{ 2, 4, 6 }
	};

	[HideInInspector]
	public Piece[] pieces;

	[HideInInspector]
	public int[] cells = new int[9];

	public Sprite playerPiece;
	public Sprite computerPiece;

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

	void Start()
	{
		pieces = transform.Find("Cells").GetComponentsInChildren<Piece>();
	}
		
	public void SetPiece(int piecePosition) 
	{
		cells[piecePosition] = GameManager.instance.isPlayerTurn ? PLAYER_PIECE : COMPUTER_PIECE;
	}
		
	public bool CheckForWinner()
	{
		return GameManager.instance.isPlayerTurn ? CheckPlayerWin() : CheckComputerWin();
	}

	public bool CheckPlayerWin() { return CheckWin(PLAYER_PIECE); }

	public bool CheckComputerWin()	{ return CheckWin(COMPUTER_PIECE); }

	private bool CheckWin(int piece)
	{
		for (int i = 0; i < winConfigs.GetLength(0); i++)
		{
			if(cells[winConfigs[i, 0]] == piece && cells[winConfigs[i, 1]] == piece && cells[winConfigs[i, 2]] == piece)
				return true;
		}

		return false;
	}
		
	public void DisableAllPieces()
	{
		foreach(var piece in pieces)
			piece.Disable();
	}
		
	public void RemoveAllPieces()
	{
		cells = new int[9];

		foreach(var piece in pieces)
		{
			piece.Enable();
			piece.RemoveImage();
		}
	}

	public Piece[] EmptyPieces()
	{
		var emptyPieces = new List<Piece>();

		for (int i = 0; i < cells.Length; ++i) 
		{
			if(cells[i] == EMPTY)
				emptyPieces.Add(pieces[i]);
		}

		return emptyPieces.ToArray();
	}
}
