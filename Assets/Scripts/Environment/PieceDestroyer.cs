using UnityEngine;
using System.Collections;

public class PieceDestroyer : MonoBehaviour 
{
	private ChessMovement chessMovement;
	private GridMovement gridMovement;

	void Start()
	{
		chessMovement = GameObject.FindGameObjectWithTag ("Ground").GetComponent<ChessMovement> ();
		gridMovement = GetComponent<GridMovement> ();
        if (chessMovement == null)
            Debug.Log("Chess Movement Script not found.");
	}

	void OnTriggerEnter(Collider other)
	{
		//Debug.Log (other.gameObject.tag + " " + other.gameObject.name);
		if ((other.gameObject.tag.Contains("White") || other.gameObject.tag.Contains ("Black")) && gridMovement.isMoving) 
		{
			ChessMovement.PGNMoveData pieceToDestroy = chessMovement.chessPiecesOnBoard.Find ((ChessMovement.PGNMoveData obj) => { return (obj.chessPiece == other.gameObject); });
			ChessMovement.PGNMoveData destroyerPiece = chessMovement.chessPiecesOnBoard.Find ((ChessMovement.PGNMoveData obj) => { return (obj.chessPiece == this.gameObject); });
			if (pieceToDestroy != null && destroyerPiece != null)
			{
				if (destroyerPiece.sourceVector == pieceToDestroy.sourceVector)
				{
                    pieceToDestroy.chessPiece.SetActive(false);
					Destroy (pieceToDestroy.chessPiece);
					chessMovement.chessPiecesOnBoard.Remove (pieceToDestroy);
				}
			}
		}
	}
}
