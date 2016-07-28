using UnityEngine;
using System.Collections;

public enum Difficulty
{
	Noob,
	Meh,
	Skilled,
	NotWinning
};

public class AI : MonoBehaviour 
{
	static public Difficulty difficultyLevel = Difficulty.Noob;

	private Board _grid;
		
	void Start()
	{
		_grid = Board.instance;
	}

	public Piece MovePiece()
	{
		Piece movePiece;

		movePiece = GetDifficultyPiece ();
			
		movePiece.SetPiece();
		return movePiece;
	}

	private Piece GetDifficultyPiece()
	{
		Piece movePiece;

		if (difficultyLevel == Difficulty.Noob) 
		{
			movePiece = RandomEmptyPiece ();
		}
		else if (difficultyLevel == Difficulty.Meh) 
		{
			movePiece = DefensePiece () ?? RandomEmptyPiece ();
		}
		else if (difficultyLevel == Difficulty.Skilled) 
		{
			movePiece = DefensePiece () ?? CenterPiece () ?? OppositeCornerPiece () ?? CornerPiece () ?? RandomEmptyPiece ();
		}
		else if (difficultyLevel == Difficulty.NotWinning) 
		{
			movePiece = OffensePiece () ?? DefensePiece () ?? CenterPiece () ?? OppositeCornerPiece () ?? CornerPiece () ?? RandomEmptyPiece ();
		}
		else 
		{
			movePiece = RandomEmptyPiece ();
		}
		
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

	public Piece OppositeCornerPiece()
	{
		var cornersPositions = new int[] {0, 2, 6, 8};
		var oppositeCornersPositions = new int[] {8, 6, 2, 0};
		var cells = _grid.cells;

		for (int i = 0; i < cornersPositions.Length; ++i) 
		{
			var cornerCell = cells[cornersPositions[i]];
			var oppositeCornerCell = cells[oppositeCornersPositions[i]];

			if(cornerCell == Board.PLAYER_PIECE && oppositeCornerCell == Board.EMPTY)
				return _grid.pieces[oppositeCornersPositions[i]];
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
		var winConfigs = _grid.winConfigs;

		for (int i = 0; i < winConfigs.GetLength(0); i++)
		{
			var cell0 = _grid.cells[winConfigs[i, 0]];
			var cell1 = _grid.cells[winConfigs[i ,1]];
			var cell2 = _grid.cells[winConfigs[i, 2]];

			if(cell0 == Board.EMPTY && cell1 == pieceType && cell2 == pieceType) 
				return _grid.pieces[winConfigs[i, 0]];
			else if(cell0 == pieceType && cell1 == Board.EMPTY && cell2 == pieceType) 
				return _grid.pieces[winConfigs[i, 1]];
			else if(cell0 == pieceType && cell1 == pieceType && cell2 == Board.EMPTY) 
				return _grid.pieces[winConfigs[i, 2]];
		}

		return null;
	}
}
