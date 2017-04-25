using System.Collections;
using UnityEngine;

class GridMovement : MonoBehaviour
{
	public bool isMoving = false;
	public float moveSpeed = 3f;
	public float gridSize = 54.5f;
	public bool allowDiagonals = true;
	[HideInInspector]
	public Vector2 numberOfMoves;

	private GameObject chessboard;
	private ChessMovement chessMovement;
	private ChessMovement.PGNMoveData chessPieceOfList;

	private enum Orientation { Horizontal, Vertical };
	private Orientation gridOrientation = Orientation.Horizontal;
	private bool correctDiagonalSpeed = true;
	private Vector2 input;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;
	private float factor;

	void Start()
	{
		numberOfMoves = new Vector2 (0f, 0f);
		chessboard = GameObject.FindGameObjectWithTag ("Ground");
		chessMovement = chessboard.GetComponent<ChessMovement> ();
		chessPieceOfList = chessMovement.chessPiecesOnBoard.Find ((ChessMovement.PGNMoveData obj) => { return (obj.chessPiece == this.gameObject); });
	}
	
	public void Update() 
	{
		if (!isMoving) 
		{
			numberOfMoves = chessPieceOfList.moveVector;
			chessPieceOfList.sourceVector += chessPieceOfList.moveVector;
//			if(chessPieceOfList.chessPiece.name=="WhitePawn4")
//				Debug.Log(chessPieceOfList.moveVector);
			//chessPieceOfList.moveVector = Vector2.zero;
			input = numberOfMoves;// here, we want the values to come from the PGN Parser into numberOfMoves
			if (!allowDiagonals) 
			{
				if (Mathf.Abs(input.x) > Mathf.Abs(input.y)) 
				{
					input.y = 0;
				} 
				else 
				{
					input.x = 0;
				}
			}
			
			if (input != Vector2.zero) 
			{
				//Debug.Log(chessPieceOfList.sourceVector.ToString());
				StartCoroutine(move(transform));
			}
		}
	}
	
	public IEnumerator move(Transform transform) 
	{
		isMoving = true;
		startPosition = transform.position;
		t = 0;
		
		if(gridOrientation == Orientation.Horizontal) 
		{
			//need to change this line, change Sys.math.sign to no. of moves for continuous path
			endPosition = new Vector3(startPosition.x + input.x * gridSize, startPosition.y, startPosition.z + input.y * gridSize);
		} 
		else 
		{
			endPosition = new Vector3(startPosition.x + input.x * gridSize, startPosition.y + input.y * gridSize, startPosition.z);
		}
		
		if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) 
		{
			factor = 3f * 0.7071f;
		} 
		else 
		{
			factor = 3f;
		}
		
		while (t < 1f) 
		{
			t += Time.deltaTime * (moveSpeed/gridSize) * factor;
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
			yield return null;
		}
		chessPieceOfList.moveVector = Vector2.zero;
		numberOfMoves = Vector2.zero;
		isMoving = false;
		yield return 0;
	}
}