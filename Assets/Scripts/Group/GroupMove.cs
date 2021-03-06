﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tag;

public class GroupMove : MonoBehaviour
{

    public Vector3 rotationPoint;

    private float fallTime = 0.8f;

    private Text ScoreText;
    private Text LoseMsg;
    private Spawner spawner;

    private float Timer = 0f;

    private static int height = 20;
    private static int width = 10;
    private float Margin =0.5f;

    private static Transform[,] Grid = new Transform[height, width];

    private static int LineCount=0;

    private void Awake()
    {
        spawner = GameObject.FindWithTag(Tag.Spawner.__Spawner).GetComponent<Spawner>();
        ScoreText= GameObject.FindWithTag(Tag.UI.Score).GetComponent<Text>();
        LoseMsg = GameObject.FindWithTag(Tag.UI.LoseMsg).GetComponent<Text>();

    }

    private void Update()
    {
        groupFall(fallTime);
        Move();
        checkLines();
        LoseMessage();
    }

    private void groupFall(float TheFallTime)
    {
        Timer += Input.GetKey(KeyCode.DownArrow)? Time.deltaTime+0.7f : Time.deltaTime;
        if (Timer > TheFallTime)
        {
            if (FallBorder())
            {
                Vector3 fall = new Vector3(0, -1, 0);
                transform.position += fall;
                Timer = 0f;
            }
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (FallBorder())
            {
                if (isValidMove(true))
                {
                    Vector3 right = new Vector3(1, 0, 0);
                    transform.position += right;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (FallBorder())
            {
                if (isValidMove(false))
                {
                    Vector3 left = new Vector3(-1, 0, 0);
                    transform.position += left;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (FallBorder())
            {
                if (isValidRotate(true) && isValidRotate(false) )
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                }     
            }
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (FallBorder())
            {
                if (isValidRotate(false) && isValidRotate(true))
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, -1), 90);
                }
            }
        }
    }


    // direct: left:false ; right:true  ; down:false ; up:true

       // 起始為 0 ;
    private bool isValidMove(bool direction)
    {
        if (direction)
        {
            foreach (Transform children in transform)
            {
                int Yposition = Mathf.RoundToInt(children.transform.position.y + height/2 - Margin);
                int Xposition = Mathf.RoundToInt(children.transform.position.x +width/2 - Margin +1);
  
                if (Xposition < 0 || Xposition > width - 1)
                {
                    return false;
                }

                if (Grid[Yposition, Xposition] != null)
                {
                    return false;
                } 
            }
            return true;
        }
        else
        {
            foreach (Transform children in transform)
            {
                int Yposition = Mathf.RoundToInt(children.transform.position.y + height/2 - Margin);
                int Xposition = Mathf.RoundToInt(children.transform.position.x  + width/2 - Margin - 1);
                if (Xposition < 0 || Xposition > width-1)
                {
                       return false;
                    
                }

                if (Grid[Yposition, Xposition] != null)
                {
                    return false;
                } 
            }
            return true;
        }
    }

    private bool isValidRotate(bool direction)
    {
        foreach (Transform children in transform)
        {
            int Yposition = Mathf.RoundToInt(children.transform.position.y + height / 2 - Margin);
            int Xposition;
            if (direction)
            {
                Xposition = Mathf.RoundToInt(children.transform.position.x - Margin + width / 2 + 1);
            }
            else
            {
                Xposition = Mathf.RoundToInt(children.transform.position.x - Margin + width / 2 - 1);
            }

            if (Xposition < 0 || Xposition > width - 1)
            {
                return false;
            }

            if (Grid[Yposition, Xposition] != null)
            {
                return false;
            }
        }
        return true;
    }


    public bool FallBorder()
    {
        foreach (Transform children in transform)
        {
            int Yposition = Mathf.RoundToInt(children.transform.position.y - Margin + height / 2 - 1);
            int Xposition = Mathf.RoundToInt(children.transform.position.x - Margin + width / 2);

            if (Yposition < 0 || Yposition > height - 1)
            {
                addToGrid();
                spawner.CreateGroup();
                enabled = false;
                return false;
            }

           if (Grid[Yposition, Xposition] != null)
            {

          
                addToGrid();
                spawner.CreateGroup();
                enabled = false;
                return false;
                } 
        }
        return true;
    }


    private void addToGrid()
    {
        foreach (Transform children in transform)
        {
            int Yposition = Mathf.RoundToInt(children.transform.position.y + 10 - 0.5f);
            int Xposition = Mathf.RoundToInt(children.transform.position.x - 0.5f + 5);
 
            Grid[Yposition, Xposition]=children;
        }
    }

    private void checkLines()
    {
        for (int i = 0; i <height; i++)
        {
            if (oneLine(i))
            {
                deleteLine(i);
                rowDown(i);
                GetScore();
            }
        }
    }


    private bool oneLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
   
            if (Grid[i,j] == null)
            {
                return false;
            }
        }
        return true;
    }

    private void deleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(Grid[i, j].gameObject);
            Grid[i, j] = null;
        }
    }

    private void rowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
              if (Grid[y,j] != null)
                {
                    Grid[y - 1, j] = Grid[y, j];
                    Grid[y, j] = null;
               
                    Grid[y - 1, j].transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    private void GetScore()
    {
        LineCount++;
        string Line = LineCount.ToString();
        ScoreText.text = Line;
    }

    private bool isFailure()
    {
        
        for (int i =0; i < width; i++)
        {
           if (Grid[19,i] != null)
            {
                return true;
            }
        }

        return false;
    }

    private void LoseMessage()
    {
        if (isFailure())
        {
            LoseMsg.text = "You Lose!";
            spawner.enabled = false;
            enabled = false;
        }
    }
}
