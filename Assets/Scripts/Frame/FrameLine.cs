using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tag;

public class FrameLine : MonoBehaviour
{
    [SerializeField]
    private GameObject HorizontalLine, VerticalLine;

    private GameObject fLine;



    void Awake()
    {


    
        for (int i = 0; i <= 20; i++)
        {
            Instantiate(HorizontalLine, transform.position, Quaternion.identity);
            transform.position = transform.position + new Vector3(0, 1, 0);
            
        }

        transform.position = new Vector3(-5, 0, 0);

        for (int i = 0; i <= 10; i++)
        {
            Instantiate(VerticalLine, transform.position, Quaternion.identity);
            transform.position = transform.position + new Vector3(1, 0, 0);
           
        }

       

        
    }
}
