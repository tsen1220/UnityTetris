using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tag;

public class GroupMove : MonoBehaviour
{

    public Vector3 rotationPoint;

    private float fallTime = 0.8f;

    private float Timer = 0f;

    // Update is called once per frame
    void Update()
    {
     
       


          groupFall(fallTime);
          Move();
          
        
        
    }


    void groupFall(float TheFallTime)
    {
        Timer += Time.deltaTime;
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



    void Move()
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

          
                    if (isValidMove(true) && isValidMove(false))
                    {


                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);


                    }
                


     
            }
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (FallBorder())
            {

          
                    if (isValidMove(true) && isValidMove(false))
                    {


                        transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);


                    }
                



            }
        }

    }


    // direct: left:false ; right:true  ; down:false ; up:true
    bool isValidMove(bool direction)
    {
        if (direction)
        {
            foreach (Transform children in transform)
            {
                int Xposition = Mathf.RoundToInt(children.transform.position.x+1);
                if (Xposition < -5 || Xposition > 5 )
                {
                    
                    return false;
                }
            }
        }
        else
        {
            foreach (Transform children in transform)
            {
                int Xposition = Mathf.RoundToInt(children.transform.position.x-1);
                if (Xposition < -5 || Xposition > 5 )
                {
                   
                    return false;
                    
                }
            }
        }

        return true;

    }






    public bool FallBorder()
    {
        foreach (Transform children in transform)
        {
            int Yposition = Mathf.RoundToInt(children.transform.position.y);
            if (Yposition < -9 || Yposition > 10)
            {

                return false;

            }
        }
        return true;

    }
}
