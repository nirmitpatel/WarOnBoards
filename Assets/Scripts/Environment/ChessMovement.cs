using UnityEngine;
using System.Collections.Generic;

public class ChessMovement : MonoBehaviour
{
	public Vector3 topLeftCenter;//the value is (58.5,0,58.5) according to me
	public float sideLength;//i have computed it to be 54.5
	public List<GameObject> whitePieces;
	public List<GameObject> blackPieces;
	public class PGNMoveData
	{
		public Vector2 sourceVector { get; set; }
		public Vector2 moveVector { get; set; }
		public GameObject chessPiece { get; set; }
	}
	public List<PGNMoveData> chessPiecesOnBoard;
	
	void Start ()
	{
		chessPiecesOnBoard = new List<PGNMoveData> ();
		putPiecesOnBoard ();
	}
	
	void Update ()
	{
		
	}
	
	void putPiecesOnBoard()
	{
		GameObject chessPiece;
		PGNMoveData tempPiece;
		for (int i=0; i<8; i++) 
		{
			chessPiece=Instantiate<GameObject>(whitePieces[0]);//pawn=0
			chessPiece.transform.position = topLeftCenter + new Vector3(i * sideLength, 2f, sideLength);
			chessPiece.name="WhitePawn"+(i+1).ToString();
			chessPiece.tag="WhitePawn";
			tempPiece=new PGNMoveData();
			tempPiece.chessPiece=chessPiece;
			tempPiece.sourceVector=new Vector2(i+1,2);
			chessPiecesOnBoard.Add(tempPiece);
		}
		
		for (int i=0; i<2; i++) 
		{
			chessPiece = Instantiate<GameObject> (whitePieces [1]);//rook=1
			chessPiece.transform.position = topLeftCenter + new Vector3 (i * sideLength * 7, 33f, 0f);
			chessPiece.name = "WhiteRook" + ((i>0)?"Right":"Left");
			chessPiece.tag = "WhiteRook";
			tempPiece=new PGNMoveData();
			tempPiece.chessPiece=chessPiece;
			tempPiece.sourceVector=new Vector2(i * 7 + 1, 1);
			chessPiecesOnBoard.Add(tempPiece);
		}
		
		for (int i=0; i<2; i++) 
		{
			chessPiece = Instantiate<GameObject> (whitePieces [2]);//knight=2
			chessPiece.transform.position = topLeftCenter + new Vector3 (i * sideLength * 5 + sideLength, 23f, 0f);
			chessPiece.name = "WhiteKnight" + ((i>0)?"Right":"Left");
			chessPiece.tag = "WhiteKnight";
			tempPiece=new PGNMoveData();
			tempPiece.chessPiece=chessPiece;
			tempPiece.sourceVector=new Vector2(i*5+2,1);
			chessPiecesOnBoard.Add(tempPiece);
		}
		
		for (int i=0; i<2; i++) 
		{
			chessPiece = Instantiate<GameObject> (whitePieces [3]);//bishop=3
			chessPiece.transform.position = topLeftCenter + new Vector3 (i * sideLength * 3 + sideLength * 2, 0f, 0f);
			chessPiece.name = "WhiteBishop" + ((i>0)?"Right":"Left");
			chessPiece.tag = "WhiteBishop";
			tempPiece=new PGNMoveData();
			tempPiece.chessPiece=chessPiece;
			tempPiece.sourceVector=new Vector2(i*3+3,1);
			chessPiecesOnBoard.Add(tempPiece);
		}
		
		chessPiece = Instantiate<GameObject> (whitePieces [4]);//queen=4
		chessPiece.transform.position = topLeftCenter + new Vector3 (sideLength * 3, 48.5f, 0f);
		chessPiece.name = "WhiteQueen";
		chessPiece.tag = "WhiteQueen";
		tempPiece=new PGNMoveData();
		tempPiece.chessPiece=chessPiece;
		tempPiece.sourceVector=new Vector2(4, 1);
		chessPiecesOnBoard.Add(tempPiece);
		
		chessPiece = Instantiate<GameObject> (whitePieces [5]);//king=5
		chessPiece.transform.position = topLeftCenter + new Vector3 (sideLength * 4, 55f, 0f);
		chessPiece.name = "WhiteKing";
		chessPiece.tag = "WhiteKing";
		tempPiece=new PGNMoveData();
		tempPiece.chessPiece=chessPiece;
		tempPiece.sourceVector=new Vector2(5, 1);
		chessPiecesOnBoard.Add(tempPiece);
		
		//black placement starts
		for (int i=0; i<8; i++) 
		{
			chessPiece=Instantiate<GameObject>(blackPieces[0]);
			chessPiece.transform.position = topLeftCenter + new Vector3(i * sideLength, 2f, 6 * sideLength);
			chessPiece.name = "BlackPawn" + (i+1).ToString();
			chessPiece.tag="BlackPawn";
			tempPiece=new PGNMoveData();
			tempPiece.chessPiece=chessPiece;
			tempPiece.sourceVector=new Vector2(i+1,7);
			chessPiecesOnBoard.Add(tempPiece);
		}
		for (int i=0; i<2; i++) 
		{
			chessPiece = Instantiate<GameObject> (blackPieces [1]);//rook=1
			chessPiece.transform.position = topLeftCenter + new Vector3 (i * sideLength * 7, 33f, sideLength * 7);
			chessPiece.name = "BlackRook" + ((i>0)?"Right":"Left");
			chessPiece.tag = "BlackRook";
			tempPiece=new PGNMoveData();
			tempPiece.chessPiece=chessPiece;
			tempPiece.sourceVector=new Vector2(i*7+1,8);
			chessPiecesOnBoard.Add(tempPiece);
		}
		
		for (int i=0; i<2; i++) 
		{
			chessPiece = Instantiate<GameObject> (blackPieces [2]);//knight=2
			chessPiece.transform.position = topLeftCenter + new Vector3 (i * sideLength * 5 + sideLength, 23f, 7 * sideLength);
			chessPiece.name = "BlackKnight" + ((i>0)?"Right":"Left");
			chessPiece.tag = "BlackKnight";
			tempPiece=new PGNMoveData();
			tempPiece.chessPiece=chessPiece;
			tempPiece.sourceVector=new Vector2(i*5+2,8);
			chessPiecesOnBoard.Add(tempPiece);
		}
		
		for (int i=0; i<2; i++) 
		{
			chessPiece = Instantiate<GameObject> (blackPieces [3]);//bishop=3
			chessPiece.transform.position = topLeftCenter + new Vector3 (i * sideLength * 3 + sideLength * 2, 0f, sideLength * 7);
			chessPiece.name = "BlackBishop" + ((i>0)?"Left":"Right");
			chessPiece.tag = "BlackBishop";
			tempPiece=new PGNMoveData();
			tempPiece.chessPiece=chessPiece;
			tempPiece.sourceVector=new Vector2(i*3+3,8);
			chessPiecesOnBoard.Add(tempPiece);
		}
		
		chessPiece = Instantiate<GameObject> (blackPieces [4]);//queen=4
		chessPiece.transform.position = topLeftCenter + new Vector3 (sideLength * 3, 48.5f, sideLength * 7);
		chessPiece.name = "BlackQueen";
		chessPiece.tag = "BlackQueen";
		tempPiece=new PGNMoveData();
		tempPiece.chessPiece=chessPiece;
		tempPiece.sourceVector=new Vector2(4,8);
		chessPiecesOnBoard.Add(tempPiece);
		
		chessPiece = Instantiate<GameObject> (blackPieces [5]);//king=5
		chessPiece.transform.position = topLeftCenter + new Vector3 (sideLength * 4, 55f, sideLength * 7);
		chessPiece.name = "BlackKing";
		chessPiece.tag = "BlackKing";
		tempPiece=new PGNMoveData();
		tempPiece.chessPiece=chessPiece;
		tempPiece.sourceVector=new Vector2(5,8);
		chessPiecesOnBoard.Add(tempPiece);
	}
}
