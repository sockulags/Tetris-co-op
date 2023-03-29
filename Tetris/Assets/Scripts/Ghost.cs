using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Ghost : MonoBehaviour
{

    public Tile tile;
    public Board board;
    public ActivePiece1 trackingPiece;
    

    public Tilemap tilemap { get; private set; }
    public Vector3Int Position { get; private set; }
    public Vector3Int[] cells { get; private set; }

    private void Awake()
    {
        this.tilemap= GetComponentInChildren<Tilemap>();
        this.cells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        Clear();
        Copy();
        Drop();
        Set();
    }

    private void Clear()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.Position;
            this.tilemap.SetTile(tilePosition, null);
        }

    }

    private void Copy()
    {
        for ( int i = 0; i < this.cells.Length;i++)
        { this.cells[i] = this.trackingPiece.cells[i]; }
    }

    private void Drop()
    {
        Vector3Int position = this.trackingPiece.Position;
        int currentRow = position.y;
        int bottom = (-this.board.boardSize.y / 2) -1;

        this.board.Clear(this.trackingPiece);
        for(int row = currentRow; row >= bottom; row--)
        {
            position.y = row;
            if(this.board.IsValidPosition(this.trackingPiece, position))
            {
                this.Position = position;
            } else { break; }
        }
        this.board.Set(this.trackingPiece);
    }

    private void Set()
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            Vector3Int tilePosition = this.cells[i] + this.Position;
            this.tilemap.SetTile(tilePosition, this.tile);
        }
    }
}
