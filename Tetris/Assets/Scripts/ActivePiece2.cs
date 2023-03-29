
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

        this.lockTime += Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rotate(-1);
        }

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


        Board.Set(this);


    }



}

