using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour 
{
	public const int EMPTY = 0;
	public const int PLAYER_PIECE = 1;
	public const int COMPUTER_PIECE = 2;

	public static Board instance = null;    

	public Sprite playerPiece;
	public Sprite computerPiece;

	public Piece[] pieces;

	public int[] cells = new int[9];

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
		var rowWin = CheckRowWin(piece);
		var columnWin = CheckColumnWin(piece);
		var diagonalWin = CheckDiagonalWin(piece);

		return rowWin || columnWin || diagonalWin;
	}

	private bool CheckRowWin(int piece)
	{
		for (int i = 0; i < 3; ++i) 
		{
			if(cells [i * 3] == cells [i * 3 + 1] && cells [i * 3 + 1] == cells [i * 3 + 2] && cells [i * 3 + 2] == piece)
				return true;
		}

		return false;
	}

	private bool CheckColumnWin(int piece)
	{
		for (int i = 0; i < 3; ++i) 
		{
			if(cells [i] == cells [i + 3] && cells [i + 3] == cells [i + 6] && cells [i + 6] == piece)
				return true;
		}

		return false;
	}

	private bool CheckDiagonalWin(int piece)
	{
		var firstDiagonal = cells[0] == cells[4] && cells[4] == cells[8] && cells[8] == piece;
		var secondDiagonal = cells[2] == cells[4] && cells[4] == cells[6] && cells[6] == piece;

		return firstDiagonal || secondDiagonal;
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
