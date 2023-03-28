
//using Assets.Scripts;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UIElements;

//public class Piece2 : Blocks
//{
//    public Board Board { get; internal set; }
//    public TetrominoData data { get; internal set; }
//    public Vector3Int Position { get; internal set; }
//    public Vector3Int[] cells { get; internal set; }
//    public int RotationIndex { get; internal set; }
 
//    public float stepDelay = 1f;
//    public float lockDelay = 0.1f;

//    internal float stepTime;
//    internal float lockTime;

//    internal virtual void Initialize(Board board, Vector3Int spawnPosition, TetrominoData data)
//    {
//        this.Board = board;
//        this.Position = spawnPosition;
//        this.data = data;
//        this.stepTime = Time.time + this.stepDelay;
//        this.lockTime = 0f;
//        if (this.cells == null)
//        {
//            this.cells = new Vector3Int[data.cells.Length];
//        }

//        for (int i = 0; i < data.cells.Length; i++)
//        {
//            this.cells[i] = (Vector3Int)data.cells[i];
//        }
//    }

//    internal void Step()
//    {
//        this.stepTime = Time.time + this.stepDelay;

//        Move(Vector2Int.down);

//        if (this.lockTime >= this.lockDelay)
//        {
//            Lock();
//        }
//    }

//    internal void Lock()
//    {
//        this.Board.Set(this);
//        this.Board.ClearLines();
//        this.Board.SpawnPiece2();
//    }

//    internal void HardDrop()
//    {
//        while (Move(Vector2Int.down))
//            continue;
//    }

//    internal bool Move(Vector2Int translation)
//    {
//        Vector3Int newPosition = this.Position;
//        newPosition.x += translation.x;
//        newPosition.y += translation.y;
//        bool valid = this.Board.IsValidPosition(this, newPosition);

//        if (valid)
//        {
//            this.Position = newPosition;
//            this.lockTime = 0f;
//        }

//        return valid;
//    }

//    internal void Rotate(int direction)
//    {
//        int originalRotationIndex = this.RotationIndex;
//        this.RotationIndex += Wrap(this.RotationIndex + direction, 0, 4);

//        ApplyRotationMatrix(direction);

//        if (!TestWallKicks(this.RotationIndex, direction))
//        {
//            this.RotationIndex = originalRotationIndex;
//            ApplyRotationMatrix(-direction);
//        }
//    }

//    internal void ApplyRotationMatrix(int direction)
//    {
//        for (int i = 0; i < this.cells.Length; i++)
//        {
//            int x = 0, y = 0;
//            Vector3 cell = this.cells[i];

//            switch (this.data.tetromino)
//            {
//                case Tetromino.I:
//                case Tetromino.O:
//                    cell.x -= 0.5f;
//                    cell.y -= 0.5f;
//                    x = Mathf.CeilToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
//                    y = Mathf.CeilToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
//                    break;


//                default:
//                    x = Mathf.RoundToInt((cell.x * Data.RotationMatrix[0] * direction) + (cell.y * Data.RotationMatrix[1] * direction));
//                    y = Mathf.RoundToInt((cell.x * Data.RotationMatrix[2] * direction) + (cell.y * Data.RotationMatrix[3] * direction));
//                    break;
//            }

//            this.cells[i] = new Vector3Int(x, y, 0);
//        }
//    }

//    internal bool TestWallKicks(int rotationIndex, int direction)
//    {
//        int wallKickIndex = GetWallKickIndex(rotationIndex, direction);

//        for (int i = 0; i < this.data.wallKicks.GetLength(1); i++)
//        {
//            Vector2Int translation = this.data.wallKicks[wallKickIndex, i];

//            if (Move(translation))
//            {
//                return true;
//            }

//        }

//        return false;
//    }

//    internal int GetWallKickIndex(int rotationIndex, int direction)
//    {
//        int wallKickIndex = rotationIndex * 2;

//        if (direction < 0)
//        {
//            wallKickIndex--;
//        }
//        return Wrap(wallKickIndex, 0, this.data.wallKicks.GetLength(0));
//    }



//    internal int Wrap(int input, int min, int max)
//    {
//        if (input < min)
//        {
//            return max - (min - input) % (max - min);
//        }
//        else
//        {
//            return min + (input - min) % (max - min);
//        }
//    }


//    public void Update()
//    {
//        this.Board.Clear(this);

//        this.lockTime += Time.deltaTime;

//        if (Input.GetKeyDown(KeyCode.UpArrow))
//        {
//            Rotate(-1);
//        }
//        //else if (Input.GetKeyDown(KeyCode.Ri))
//        //    Rotate(1);

//        if (Input.GetKeyDown(KeyCode.LeftArrow))
//        {
//            Move(Vector2Int.left);
//        }
//        else if (Input.GetKeyDown(KeyCode.RightArrow))
//        {
//            Move(Vector2Int.right);
//        }

//        if (Input.GetKeyDown(KeyCode.DownArrow))
//        {
//            Move(Vector2Int.down);
//        }

//        if (Input.GetKeyDown(KeyCode.RightShift))
//        {
//            HardDrop();
//        }
//        if (Time.time >= this.stepTime)
//        {
//            Step();
//        }
//        this.Board.Set(this);

//    }

//}



   


