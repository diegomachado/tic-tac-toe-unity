using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour 
{
	private Grid _grid;

	void Start()
	{
		_grid = Grid.instance;
	}

	public Piece GetMovePiece()
	{
		var movePiece = RandomEmptySpaceStrategy();
		movePiece.SetPiece();
		return movePiece;
	}

	public Piece RandomEmptySpaceStrategy()
	{
		var emptyPieces = _grid.EmptyPieces();
		var randomId = Random.Range(0, emptyPieces.Length);

		return emptyPieces[randomId];
	}
}
