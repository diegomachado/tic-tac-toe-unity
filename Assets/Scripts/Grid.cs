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
	public Piece[] pieces;

	public int[] spaces = new int[9];

	void Awake()
	{
		if(instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    
	}

	void Start()
	{
		pieces = transform.Find("Spaces").GetComponentsInChildren<Piece>();
	}

	public void MakeMove(Piece piece)
	{
		SetPieceSpace(piece.gridPosition);

		var gameManager = GameManager.instance;

		if(gameManager.HasMinTurnsToCheckForWinner() && CheckForWinner())
		{
			gameManager.Win();
			DisableAllPieces();
		}
		else
		{
			gameManager.PassTurn();
		}
	}
		
	public void SetPieceSpace(int piecePosition) 
	{
		spaces[piecePosition] = GameManager.instance.isPlayerTurn ? PLAYER_PIECE : COMPUTER_PIECE;
	}
		
	private bool CheckForWinner()
	{
		return GameManager.instance.isPlayerTurn ? CheckPlayerWin() : CheckComputerWin();
	}

	private bool CheckPlayerWin()
	{
		return CheckWin(PLAYER_PIECE);
	}

	private bool CheckComputerWin()
	{
		return CheckWin(COMPUTER_PIECE);
	}

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
			if(spaces [i * 3] == spaces [i * 3 + 1] && spaces [i * 3 + 1] == spaces [i * 3 + 2] && spaces [i * 3 + 2] == piece)
				return true;
		}

		return false;
	}

	private bool CheckColumnWin(int piece)
	{
		for (int i = 0; i < 3; ++i) 
		{
			if(spaces [i] == spaces [i + 3] && spaces [i + 3] == spaces [i + 6] && spaces [i + 6] == piece)
				return true;
		}

		return false;
	}

	private bool CheckDiagonalWin(int piece)
	{
		var firstDiagonal = spaces[0] == spaces[4] && spaces[4] == spaces[8] && spaces[8] == piece;
		var secondDiagonal = spaces[2] == spaces[4] && spaces[4] == spaces[6] && spaces[6] == piece;

		return firstDiagonal || secondDiagonal;
	}

	private void DisableAllPieces()
	{
		foreach(var piece in pieces)
			piece.Disable();
	}
		
	public void RemoveAllPieces()
	{
		spaces = new int[9];

		foreach(var piece in pieces)
		{
			piece.Enable();
			piece.RemoveImage();
		}
	}
}
