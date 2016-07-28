using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour 
{
	private Board _grid;

	private int[,] winConfigs = new int[,]
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
		
	void Start()
	{
		_grid = Board.instance;
	}

	public Piece MovePiece()
	{
		var movePiece = 
			OffensePiece() ?? 
			DefensePiece() ?? 
			CenterPiece() ?? 
			OppositeCornerPiece() ?? 
			CornerPiece() ?? 
			RandomEmptyPiece();
		
		movePiece.SetPiece();
		return movePiece;
	}

	public Piece RandomEmptyPiece()
	{
		var emptyPieces = _grid.EmptyPieces();
		var randomId = Random.Range(0, emptyPieces.Length);

		return emptyPieces[randomId];
	}

	public Piece CenterPiece()
	{
		var centerPosition = 4;

		if(_grid.cells[centerPosition] == Board.EMPTY)
			return _grid.pieces[centerPosition];
		
		return null;
	}

	public Piece OppositeCornerPiece()
	{
		var cornersPositions = new int[] {0, 2, 6, 8};
		var oppositeCornersPositions = new int[] {8, 6, 2, 0};
		var spaces = _grid.cells;

		for (int i = 0; i < cornersPositions.Length; ++i) 
		{
			var cornerSpace = spaces[cornersPositions[i]];
			var oppositeCornerSpace = spaces[oppositeCornersPositions[i]];

			if(cornerSpace == Board.PLAYER_PIECE && oppositeCornerSpace == Board.EMPTY)
				return _grid.pieces[oppositeCornersPositions[i]];
		}

		return null;
	}

	public Piece CornerPiece()
	{
		var cornersPositions = new int[] {0, 2, 6, 8};

		for (int i = 0; i < cornersPositions.Length; i++) 
		{
			if(_grid.cells[cornersPositions[i]] == Board.EMPTY)
				return _grid.pieces[cornersPositions[i]];
		}

		return null;
	}

	public Piece DefensePiece()
	{
		return WinPiece(Board.PLAYER_PIECE);
	}

	public Piece OffensePiece()
	{
		return WinPiece(Board.COMPUTER_PIECE);
	}

	public Piece WinPiece(int pieceType)
	{
		for (int i = 0; i < winConfigs.GetLength(0); i++)
		{
			var space0 = _grid.cells[winConfigs[i, 0]];
			var space1 = _grid.cells[winConfigs[i ,1]];
			var space2 = _grid.cells[winConfigs[i, 2]];

			if(space0 == Board.EMPTY && space1 == pieceType && space2 == pieceType) 
				return _grid.pieces[winConfigs[i, 0]];
			else if(space0 == pieceType && space1 == Board.EMPTY && space2 == pieceType) 
				return _grid.pieces[winConfigs[i, 1]];
			else if(space0 == pieceType && space1 == pieceType && space2 == Board.EMPTY) 
				return _grid.pieces[winConfigs[i, 2]];
		}

		return null;
	}
}
