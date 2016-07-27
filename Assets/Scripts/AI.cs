using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour 
{
	private Grid _grid;

	void Start()
	{
		_grid = Grid.instance;
	}

	public void Play()
	{
		var movePiece = RandomEmptySpaceStrategy();
		movePiece.SetPiece();
		_grid.SetPiece(movePiece.gridPosition);
	}

	public Piece RandomEmptySpaceStrategy()
	{
		var emptyPieces = _grid.EmptyPieces();
		var randomId = Random.Range(0, emptyPieces.Length);

		return emptyPieces[randomId];
	}

	void Score()
	{
		
	}

	void MinMax()
	{
	}


}
