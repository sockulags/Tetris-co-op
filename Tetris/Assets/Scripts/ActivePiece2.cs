
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ActivePiece2 : Blocks
{




    public void Update()
    {

        Board.Clear(this);
        //Board.Clear(Board.activePiece2);
this.lockTime += Time.deltaTime;
        if (this.isPlayer1)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Rotate(-1);
            }
            else if (Input.GetKeyDown(KeyCode.E))
                Rotate(1);

            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                Move(Vector2Int.right);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Move(Vector2Int.down);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                HardDrop();
            }
            if (Time.time >= this.stepTime)
            {
                Step();
            }
        }
        if (!this.isPlayer1)
        {

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Rotate(-1);
            }
            //else if (Input.GetKeyDown(KeyCode.Ri))
            //    Rotate(1);

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Move(Vector2Int.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Move(Vector2Int.right);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Move(Vector2Int.down);
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                HardDrop();
            }
            if (Time.time >= this.stepTime)
            {
                Step();
            }
        }

        Board.Set(this);
       
        
    }



}

