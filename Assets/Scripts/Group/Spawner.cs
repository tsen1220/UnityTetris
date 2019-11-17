using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tag;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Group;

   


    void Awake()
    {
        CreateGroup();

    }

    void CreateGroup()
    {
        GameObject currentObject = Group[Random.Range(0, Group.Length)];

        Vector3 OriginPosition = transform.position;

        if (currentObject.CompareTag(Tag.Group.GroupO)|| currentObject.CompareTag(Tag.Group.GroupI))
        {
            transform.position += new Vector3(-0.5f, -0.5f, 0);
            Instantiate(currentObject, transform.position, Quaternion.identity);
            transform.position = OriginPosition;
        }
        else
        {
            Instantiate(currentObject, transform.position, Quaternion.identity);
        }

     

    }




}
