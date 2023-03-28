using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public TetrominoData[] tetrominos;
    public Piece activePiece1 { get; private set; }
    public Piece activePiece2 { get; private set; }
    public Vector3Int spawnPosition1 = new Vector3Int(-6, 8, 0);
    public Vector3Int spawnPosition2 = new Vector3Int(4, 8, 0);
    public Vector2Int boardSize = new Vector2Int(20, 20);
    public int clearedLines { get; private set; }


    public RectInt Bounds
    {
       get {Vector2Int position = new Vector2Int(-this.boardSize.x/2, -this.boardSize.y/2);
            return new RectInt(position, this.boardSize);
        }
    }

    private void OnGUI()
    {
        var score = clearedLines.ToString();
        GUI.TextArea(new Rect(4,4,score.Length,4), score, 50);
    }
    private void Awake()
    {
        this.activePiece2 = GetComponentInChildren<Piece>();
        this.activePiece1 = GetComponentInChildren<Piece>();
        activePiece1.isPlayer1 = true;

        this.tilemap = GetComponentInChildren<Tilemap>();
        for (int i = 0; i < this.tetrominos.Length; i++)
        {
            this.tetrominos[i].Initalize();
        }
    }

    private void Start()
    {
        SpawnPiece(true);
        SpawnPiece(false);
    }


    public void SpawnPiece(bool isPlayer1 )
    {
        var activePiece = isPlayer1 ? activePiece1 : activePiece2;
        activePiece.isPlayer1 = isPlayer1;
        var spawnPosition = isPlayer1 ? spawnPosition1 : spawnPosition2;
        int random = Random.Range(0, this.tetrominos.Length);
        TetrominoData data = this.tetrominos[random];
      
        activePiece.Initialize(this, spawnPosition, data);

            if (IsValidPosition(activePiece, spawnPosition))
            {
                Set(activePiece);
            }
            else
            {
                GameOver();
            }
    }

    //public void SpawnPiece2()
    //{
        
    //    int random = Random.Range(0, this.tetrominos.Length);
    //    TetrominoData data = this.tetrominos[random];

    //    this.activePiece2.Initialize(this, spawnPosition2, data);

    //    if (IsValidPosition(this.activePiece2, spawnPosition2))
    //    {

    //        Set(this.activePiece2);
            
    //    }
    //    else
    //    {

    //        GameOver();
    //    }
    //}

    private void GameOver()
    {
        this.tilemap.ClearAllTiles();
    }

    public void Set(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.Position;
            this.tilemap.SetTile(tilePosition, piece.data.tile);
        }
       
    }

    public void Clear(Piece piece)
    {
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + piece.Position;
            this.tilemap.SetTile(tilePosition, null);
        }
    }

    public bool IsValidPosition(Piece piece, Vector3Int position)
    {
        RectInt bounds = this.Bounds;
        for (int i = 0; i < piece.cells.Length; i++)
        {
            Vector3Int tilePosition = piece.cells[i] + position;

            if(!bounds.Contains((Vector2Int)tilePosition)) 
             return false; 

            if (this.tilemap.HasTile(tilePosition))
                return false;
            //if (tilePosition == activePiece2.Position)
            //{
            //    return false;
            //}

        }
        return true;
    }

   
    public void ClearLines()
    {
        RectInt bounds = this.Bounds;
        int row = bounds.yMin;

        while (row < bounds.yMax)
        {
            if (IsLineFull(row))
            {
                clearedLines++;
                LineClear(row);
            }
            else
                row++;
        }


    }

    private void LineClear(int row)
    {
        //var activePiece = activePiece1 ?? activePiece2;
        RectInt bounds = this.Bounds;
        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);
            this.tilemap.SetTile(position, null);
        }

        while (row < bounds.yMax)
        {
            for (int col = bounds.xMin; col < bounds.xMax; col++)
            {
             
                Vector3Int position = new Vector3Int(col, row + 1, 0);
                //Vector3Int currentPosition = new Vector3Int(col, row, 0);
                TileBase above = this.tilemap.GetTile(position);
                //TileBase current = this.tilemap.GetTile(currentPosition);
                

                position = new Vector3Int(col, row, 0);
                
                this.tilemap.SetTile(position,/* (position == this.activePiece1.Position || position == this.activePiece2.Position) ? current :*/ above);
                //this.activePiece1 = ap1;
                //this.activePiece2 = ap2;
            }
            row++;
        }
    }

    private bool IsLineFull(int row)
    {
        RectInt bounds = this.Bounds;

        for (int col = bounds.xMin; col < bounds.xMax; col++)
        {
            Vector3Int position = new Vector3Int(col, row, 0);

            if (!this.tilemap.HasTile(position))
            {
                return false;
            }
        }
        return true;
    }


}
