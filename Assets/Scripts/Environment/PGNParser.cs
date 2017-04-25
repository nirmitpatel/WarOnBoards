using UnityEngine;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PGNParser : MonoBehaviour 
{
	//only takes files that do not instantiate new bishops
	private List<Move> database = new List<Move> ();
	private ChessMovement chessMovement;
	private GameObject chessPiece;
	private ChessMovement.PGNMoveData pieceFromList;
	private JsonData data;

	[SerializeField]
	private List<GameObject> pieceToInstantiate;
	private int []newlyInstantiatedPieces;//white=0 to 3 black=4 to 7
	private float timeBetweenMoves;

	void Start () 
	{
		data = JsonMapper.ToObject(File.ReadAllText(Application.dataPath+"/StreamingAssets/pgnfile.json"));
		chessMovement = GetComponent<ChessMovement> ();
		newlyInstantiatedPieces = new int[8] {0, 0, 0, 0, 0, 0, 0, 0};
		ConstructDatabase ();
	}
	void ConstructDatabase()
	{
		for (int i=0; i<data.Count; i++) 
		{
			database.Add(new Move((int)data[i]["movenumber"],data[i]["whitemove"].ToString(),data[i]["blackmove"].ToString()));
		}
		FindPieceDetails ();
	}
	/*public Move FetchbyMoveNumber(int no)
	{
		for (int i=0; i<database.Count; i++) 
		{
			if(database[i].MoveNumber == no)
				return database[i];
		}
		return null;
	}*/
	ChessMovement.PGNMoveData FindPieceFromList(string PieceName, Vector2 destination, char Type, bool conflict, Vector2 currentLocation)
	{
		int flag = 0;
		ChessMovement.PGNMoveData temp = new ChessMovement.PGNMoveData ();
		//Debug.Log (PieceName+" "+destination.ToString());
		//Debug.Log (chessMovement.chessPiecesOnBoard.FindAll ((ChessMovement.PGNMoveData obj) => { return (obj.chessPiece.name.Contains(PieceName)); }).Count);
		foreach (ChessMovement.PGNMoveData o in chessMovement.chessPiecesOnBoard.FindAll ((ChessMovement.PGNMoveData obj) => { return (obj.chessPiece.name.Contains(PieceName)); }))
		{
			//Debug.Log(o.chessPiece.name + " " + o.sourceVector);
			if(flag==0)
			{
				switch(Type)
				{
				case 'R':
					if(conflict)
					{
						if(currentLocation.x==0)
						{
							if(o.sourceVector.y==currentLocation.y)
							{
								temp.chessPiece=o.chessPiece;
								temp.moveVector=destination-o.sourceVector;
								flag=1;
								o.moveVector=temp.moveVector;
							}
						}
						else
						{
							if(o.sourceVector.x==currentLocation.x)
							{
								temp.chessPiece=o.chessPiece;
								temp.moveVector=destination-o.sourceVector;
								flag=1;
								o.moveVector=temp.moveVector;
							}                                      
						}
					}
					else
					{
						if(o.sourceVector.x==destination.x||o.sourceVector.y==destination.y)
						{
							temp.chessPiece=o.chessPiece;
							temp.moveVector=destination-o.sourceVector;
							flag=1;
							o.moveVector=temp.moveVector;
						}
					}
					break;
				case 'N':
					if(conflict)
					{
						if(currentLocation.x==0)
						{
							if(o.sourceVector.y==currentLocation.y)
							{
								temp.chessPiece=o.chessPiece;
								temp.moveVector=destination-o.sourceVector;
								flag=1;
								o.moveVector=temp.moveVector;
							}
						}
						else
						{
							if(o.sourceVector.x==currentLocation.x)
							{
								temp.chessPiece=o.chessPiece;
								temp.moveVector=destination-o.sourceVector;
								flag=1;
								o.moveVector=temp.moveVector;
							}
						}
					}
					else
					{
						if(Mathf.Abs((o.sourceVector.x-destination.x)+Mathf.Abs(o.sourceVector.y-destination.y))==3&&Mathf.Abs(o.sourceVector.x-destination.x)!=0&&Mathf.Abs(o.sourceVector.y-destination.y)!=0)
						{
							temp.chessPiece=o.chessPiece;
							temp.moveVector=destination-o.sourceVector;
							flag=1;
							o.moveVector=temp.moveVector;
						}
					}
					break;
				case 'B':
					int a = Mathf.RoundToInt(destination.x+destination.y);
					if(a%2==0)//black one
					{
						if(o.chessPiece.name.Contains("Left"))
						{
							temp.chessPiece=o.chessPiece;
							temp.moveVector=destination-o.sourceVector;
							flag=1;
							o.moveVector=temp.moveVector;
						}
					}
					else//white one
					{
						if(o.chessPiece.name.Contains("Right"))
						{
							temp.chessPiece=o.chessPiece;
							temp.moveVector=destination-o.sourceVector;
							flag=1;
							o.moveVector=temp.moveVector;
						}
					}
					break;
				case 'P':
					if(conflict)
					{
						if(o.sourceVector.x ==currentLocation.x)
						{
							temp.chessPiece=o.chessPiece;
							temp.moveVector=destination-o.sourceVector;
							flag=1;
							o.moveVector=temp.moveVector;
						}
					}
					else
					{
						if((o.sourceVector.x == destination.x))
						{
							temp.chessPiece=o.chessPiece;
							temp.moveVector=destination-o.sourceVector;
							flag=1;
							o.moveVector=temp.moveVector;
						}
					}
					break;
				case 'K':
					temp.chessPiece=o.chessPiece;
					temp.moveVector=destination-o.sourceVector;
					flag=1;
					o.moveVector=temp.moveVector;
					break;
				case 'Q':
					temp.chessPiece=o.chessPiece;
					temp.moveVector=destination-o.sourceVector;
					flag=1;
					o.moveVector=temp.moveVector;
					break;
				}
			}
			else
			{
				break;
			}
		}
		//Debug.Log (temp.chessPiece.ToString () + " " + temp.moveVector.ToString () + "  " + temp.sourceVector.ToString ());
		return temp;
	}
	void FindPieceDetails()
	{
		StartCoroutine (makeAMove());
	}

	IEnumerator makeAMove()
	{
		string [] move=new string[2];
		for (int j=0; j<database.Count; j++)
		{
			move[0]=database[j].WhiteMove;
			move[1]=database[j].BlackMove;
			//delay here between two sets of moves
			StartCoroutine(moveChessPiece(move));
			yield return new WaitForSeconds(20);
		}
	}

	IEnumerator moveChessPiece(string[] move)
	{
		for(int i=0;i<2;i++)
		{
			timeBetweenMoves=0f;
			//Debug.Log(move[i].ToString());
			Vector2 x = movePieceLogic(move, i).moveVector;
			//timeBetweenMoves += x.magnitude * 3f + 1f;
			yield return new WaitForSeconds(x.magnitude * 3f + 1f);
			//delay here between white move and black move
		}
	}
	ChessMovement.PGNMoveData movePieceLogic(string[] move, int i)
	{
		string [] color = new string[2];
		color[0]="White";
		color[1]="Black";
		string pieceToMove;
		Vector2 destination = new Vector2 ();
		bool pieceCaptured;
		bool pieceConflict;
		ChessMovement.PGNMoveData piece = null;
		Vector2 source = new Vector2 ();

		source.x=0;
		source.y=0;
		pieceConflict=false;
		switch(move[i][0])
		{
		case 'R':
			pieceToMove=color[i]+"Rook";
			if(move[i].Length==3)
			{
				pieceCaptured=false;
				destination.x=move[i][1]-96;
				destination.y=move[i][2]-48;
			}
			else if(move[i].Length==4)
			{
				if(move[i][1]=='x')
					pieceCaptured=true;
				else
				{
					pieceCaptured=false;
					pieceConflict=true;
					if(move[i][1]>48&&move[i][1]<57)
					{
						source.y=move[i][1]-48;
					}
					else
					{
						source.x=move[i][1]-96;
					}
				}
				destination.x=move[i][2]-96;
				destination.y=move[i][3]-48;
			}
			else
			{
				pieceCaptured=true;
				pieceConflict=true;
				destination.x=move[i][3]-96;
				destination.y=move[i][4]-48;
				if(move[i][1]>48&&move[i][1]<57)
				{
					source.y=move[i][1]-48;
				}
				else
				{
					source.x=move[i][1]-96;
				}
			}
			piece=FindPieceFromList(pieceToMove,destination,'R',pieceConflict,source);
			break;
		case 'N':
			pieceToMove=color[i]+"Knight";
			if(move[i].Length==3)
			{
				pieceCaptured=false;
				destination.x=move[i][1]-96;
				destination.y=move[i][2]-48;
			}
			else if(move[i].Length==4)
			{
				if(move[i][1]=='x')
					pieceCaptured=true;
				else
				{
					pieceCaptured=false;
					pieceConflict=true;
					if(move[i][1]>48&&move[i][1]<57)
					{
						source.y=move[i][1]-48;
					}
					else
					{
						source.x=move[i][1]-96;
					}
				}
				destination.x=move[i][2]-96;
				destination.y=move[i][3]-48;
			}
			else
			{
				pieceCaptured=true;
				pieceConflict=true;
				destination.x=move[i][3]-96;
				destination.y=move[i][4]-48;
				if(move[i][1]>48&&move[i][1]<57)
				{
					source.y=move[i][1]-48;
				}
				else
				{
					source.x=move[i][1]-96;
				}
			}
			piece=FindPieceFromList(pieceToMove,destination,'N',pieceConflict,source);
			break;
		case 'B':
			pieceToMove=color[i]+"Bishop";
			if(move[i].Length==3)
			{
				pieceCaptured=false;
				destination.x=move[i][1]-96;
				destination.y=move[i][2]-48;
			}
			else
			{
				pieceCaptured=true;
				destination.x=move[i][2]-96;
				destination.y=move[i][3]-48;
			}
			piece=FindPieceFromList(pieceToMove,destination,'B',pieceConflict,source);
			break;
		case 'K':
			pieceToMove=color[i]+"King";
			if(move[i].Length==3)
			{
				pieceCaptured=false;
				destination.x=move[i][1]-96;
				destination.y=move[i][2]-48;
			}
			else
			{
				pieceCaptured=true;
				destination.x=move[i][2]-96;
				destination.y=move[i][3]-48;
			}
			piece=FindPieceFromList(pieceToMove,destination,'K',pieceConflict,source);
			break;
		case 'Q':
			pieceToMove=color[i]+"Queen";
			if(move[i].Length==3)
			{
				pieceCaptured=false;
				destination.x=move[i][1]-96;
				destination.y=move[i][2]-48;
			}
			else
			{
				pieceCaptured=true;
				destination.x=move[i][2]-96;
				destination.y=move[i][3]-48;
			}
			piece=FindPieceFromList(pieceToMove,destination,'Q',pieceConflict,source);
			break;
		case 'O':
			//need to move two chess pieces rook and king
			pieceCaptured=false;
			if(move[i].Length==3)
			{
				if(i==0)
				{
					pieceToMove=color[i]+"Rook";
					//rook destination
					pieceConflict=true;
					source.x=8;
					destination.x=6;
					destination.y=1;
					piece=FindPieceFromList(pieceToMove,destination,'R',pieceConflict,source);
					pieceToMove=color[i]+"King";
					//king destination
					pieceConflict=false;
					source.x=0;
					destination.x=7;
					destination.y=1;
					piece=FindPieceFromList(pieceToMove,destination,'K',pieceConflict,source);
				}
				
				else
				{
					pieceToMove=color[i]+"Rook";
					//rook destination
					pieceConflict=true;
					source.x=8;
					destination.x=6;
					destination.y=8;
					piece=FindPieceFromList(pieceToMove,destination,'R',pieceConflict,source);
					pieceToMove=color[i]+"King";
					//king destination
					pieceConflict=false;
					source.x=0;
					destination.x=7;
					destination.y=8;
					piece=FindPieceFromList(pieceToMove,destination,'K',pieceConflict,source);
				}
			}
			else 
			{
				if(i==0)
				{
					pieceToMove=color[i]+"Rook";
					//rook destination
					pieceConflict=true;
					source.x=1;
					destination.x=4;
					destination.y=1;
					piece=FindPieceFromList(pieceToMove,destination,'R',pieceConflict,source);
					pieceToMove=color[i]+"King";
					//king destination
					pieceConflict=false;
					source.x=0;
					destination.x=3;
					destination.y=1;
					piece=FindPieceFromList(pieceToMove,destination,'K',pieceConflict,source);
				}
				
				else
				{
					pieceToMove=color[i]+"Rook";
					//rook destination
					pieceConflict=true;
					source.x=1;
					destination.x=4;
					destination.y=8;
					piece=FindPieceFromList(pieceToMove,destination,'R',pieceConflict,source);
					pieceToMove=color[i]+"King";
					pieceConflict=false;
					source.x=0;
					//king destination
					destination.x=3;
					destination.y=8;
					piece=FindPieceFromList(pieceToMove,destination,'K',pieceConflict,source);
				}
			}
			break;
		case '1':
			//stop
			break;
		case '0':
			// stop
			break;
		default:
			pieceToMove=color[i]+"Pawn";
			if(move[i].Length==2)
			{
				pieceCaptured=false;
				destination.x=move[i][0]-96;
				destination.y=move[i][1]-48;
			}
			else if(move[i].Length==4)
			{	
				if(move[i][1]=='x')
				{	//piece is captured
					pieceCaptured=true;
					pieceConflict=true;
					destination.x=move[i][2]-96;
					destination.y=move[i][3]-48;
					if(move[i][0]>48&&move[i][0]<57)
					{
						source.y=move[i][0]-48;
					}
					else
					{
						source.x=move[i][0]-96;
					}
				}
				else if(move[i][2]=='=')
				{
					//piece is promoted but not capturing
					// You need to instantiate the piece and destroy the pawn
					pieceCaptured=false;
					destination.x=move[i][2]-96;
					destination.y=move[i][3]-48;
				}
			}
			else if(move[i].Length==6)
			{
				//piece is promoted and piece is also capturing
				// You need to instantiate the piece and destroy the pawn
				pieceCaptured=true;
				pieceConflict=true;
				destination.x=move[i][2]-96;
				destination.y=move[i][3]-48;
				if(move[i][0]>48&&move[i][0]<57)
				{
					source.y=move[i][0]-48;
				}
				else
				{
					source.x=move[i][0]-96;
				}
			}
			piece=FindPieceFromList(pieceToMove,destination,'P',pieceConflict,source);
			if(move[i].Length==4||move[i].Length==6)
			{
				InstantiatePieceFromPawn(move[i][move[i].Length - 1], i, piece);
			}
			break;
		}
		return piece;
	}
	void InstantiatePieceFromPawn(char c, int color, ChessMovement.PGNMoveData pawn)
	{
		GameObject newPiece;
		Vector3 tempPosition;
		ChessMovement.PGNMoveData tempPiece;
		switch(c)
		{
		case 'Q':
			//instantiate queen and destroy pawn
			newPiece = Instantiate<GameObject> (pieceToInstantiate[color * 4  + 3]);//queen=3
			tempPosition = newPiece.transform.position;
			tempPosition.x = pawn.chessPiece.transform.position.x;
			tempPosition.y = pawn.chessPiece.transform.position.y + 46.5f;
			tempPosition.z = pawn.chessPiece.transform.position.z;
			newPiece.transform.position = tempPosition;
			newPiece.name = (color > 0 ? "Black" : "White") + "Queen" + (++newlyInstantiatedPieces[color * 4  + 3]).ToString();
			newPiece.tag = (color > 0 ? "Black" : "White") + "Queen";

			tempPiece=new ChessMovement.PGNMoveData();
			tempPiece.chessPiece=newPiece;
			tempPiece.sourceVector=pawn.sourceVector;
			Destroy(pawn.chessPiece);
			chessMovement.chessPiecesOnBoard.Remove(pawn);
			chessMovement.chessPiecesOnBoard.Add(tempPiece);
			break;
		case 'R':
			//instantiate rook and destroy pawn
			newPiece = Instantiate<GameObject> (pieceToInstantiate[color * 4]);//rook=0
			tempPosition = newPiece.transform.position;
			tempPosition.x = pawn.chessPiece.transform.position.x;
			tempPosition.y = pawn.chessPiece.transform.position.y + 31f;
			tempPosition.z = pawn.chessPiece.transform.position.z;
			newPiece.transform.position = tempPosition;
			newPiece.name = (color > 0 ? "Black" : "White") + "Rook" + (++newlyInstantiatedPieces[color * 4]).ToString();
			newPiece.tag = (color > 0 ? "Black" : "White") + "Rook";

			tempPiece=new ChessMovement.PGNMoveData();
			tempPiece.chessPiece=newPiece;
			tempPiece.sourceVector=pawn.sourceVector;
			Destroy(pawn.chessPiece);
			chessMovement.chessPiecesOnBoard.Remove(pawn);
			chessMovement.chessPiecesOnBoard.Add(tempPiece);
			break;
		case 'N':
			//instantiate Knight and destroy pawn
			newPiece = Instantiate<GameObject> (pieceToInstantiate[color * 4 + 1]);//knight=1
			tempPosition = newPiece.transform.position;
			tempPosition.x = pawn.chessPiece.transform.position.x;
			tempPosition.y = pawn.chessPiece.transform.position.y + 21f;
			tempPosition.z = pawn.chessPiece.transform.position.z;
			newPiece.transform.position = tempPosition;
			newPiece.name = (color > 0 ? "Black" : "White") + "Knight" + (++newlyInstantiatedPieces[color * 4 + 1]).ToString();
			newPiece.tag = (color > 0 ? "Black" : "White") + "Knight";
			
			tempPiece=new ChessMovement.PGNMoveData();
			tempPiece.chessPiece=newPiece;
			tempPiece.sourceVector=pawn.sourceVector;
			Destroy(pawn.chessPiece);
			chessMovement.chessPiecesOnBoard.Remove(pawn);
			chessMovement.chessPiecesOnBoard.Add(tempPiece);
			break;
		case 'B':
			//instantiate bishop and destroy pawn
			newPiece = Instantiate<GameObject> (pieceToInstantiate[color * 4 + 2]);//bishop=2
			tempPosition = newPiece.transform.position;
			tempPosition.x = pawn.chessPiece.transform.position.x;
			tempPosition.y = pawn.chessPiece.transform.position.y - 2f;
			tempPosition.z = pawn.chessPiece.transform.position.z;
			newPiece.transform.position = tempPosition;
			newPiece.name = (color > 0 ? "Black" : "White") + "Bishop" + (++newlyInstantiatedPieces[color * 4 + 2]).ToString();
			newPiece.tag = (color > 0 ? "Black" : "White") + "Bishop";
			
			tempPiece=new ChessMovement.PGNMoveData();
			tempPiece.chessPiece=newPiece;
			tempPiece.sourceVector=pawn.sourceVector;
			Destroy(pawn.chessPiece);
			chessMovement.chessPiecesOnBoard.Remove(pawn);
			chessMovement.chessPiecesOnBoard.Add(tempPiece);
			break;
		}
	}
}

public class Move
{
	public int MoveNumber{ get; set;}
	public string WhiteMove{ get; set;}
	public string BlackMove{ get; set;}
	public Move(int movenumber,string whitemove,string blackmove)
	{
		this.MoveNumber = movenumber;
		this.WhiteMove = whitemove;
		this.BlackMove = blackmove;
	}
}
