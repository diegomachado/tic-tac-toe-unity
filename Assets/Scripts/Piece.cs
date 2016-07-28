using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Piece : MonoBehaviour 
{
	private Image _image;
	private Button _button;
	private ButtonHelper _buttonHelper;
	public int gridPosition;

	void Start () 
	{
		_image = GetComponent<Image>();
		_button = GetComponent<Button>();
		_buttonHelper = GetComponent<ButtonHelper>();

		_button.onClick.AddListener(PlayerSetPiece);
	}

	public void PlayerSetPiece()
	{
		SetPiece();
		GameManager.instance.PlayerMove(this);
	}

	public void SetPiece() 
	{
		SetImage();
		Disable();
	}
		
	void SetImage()
	{
		var grid = Grid.instance;
		var gameManager = GameManager.instance;

		_image.sprite = gameManager.isPlayerTurn ? grid.playerPiece : grid.computerPiece;
		_buttonHelper.SetImageAlpha(1);
	}

	public void RemoveImage()
	{
		_image.sprite = null;
		_buttonHelper.SetImageAlpha(0);
	}

	public void Enable()
	{
		_button.enabled = true;
	}

	public void Disable()
	{
		_button.enabled = false;
	}
}
