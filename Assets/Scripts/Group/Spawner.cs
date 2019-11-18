using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tag;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Group;

    [SerializeField]
    private GameObject[] staticGroup;

    // Use queue to create group pool
    private Queue<GameObject> WaitGroupList = new Queue<GameObject>(); 

    public static GameObject NextGroup;

    private Vector3 NextGroupPosition;

    private static GameObject staticNextGroup;

    void Awake()
    {
        GroupPoolCreate();


        NextGroup = WaitGroupList.Dequeue();

        NextGroupPosition = GameObject.FindWithTag(Tag.NextGroup.Next).transform.position;


        CreateGroup();
    }


    public void CreateGroup()
    {
        GameObject currentObject = NextGroup;

        NextGroup = WaitGroupList.Dequeue();

        //next group show
        if (NextGroup.CompareTag(Tag.Group.GroupI))
        {
            Destroy(staticNextGroup);
            staticNextGroup = Instantiate(staticGroup[0], NextGroupPosition, Quaternion.identity);
            
        }
        if (NextGroup.CompareTag(Tag.Group.GroupJ))
        {
            Destroy(staticNextGroup);
            staticNextGroup = Instantiate(staticGroup[1], NextGroupPosition, Quaternion.identity);
        }
        if (NextGroup.CompareTag(Tag.Group.GroupL))
        {
            Destroy(staticNextGroup);
            staticNextGroup = Instantiate(staticGroup[2], NextGroupPosition, Quaternion.identity);
        }
        if (NextGroup.CompareTag(Tag.Group.GroupS))
        {
            Destroy(staticNextGroup);
            staticNextGroup = Instantiate(staticGroup[3], NextGroupPosition, Quaternion.identity);
        }
        if (NextGroup.CompareTag(Tag.Group.GroupT))
        {
            Destroy(staticNextGroup);
            staticNextGroup = Instantiate(staticGroup[4], NextGroupPosition, Quaternion.identity);
        }
        if (NextGroup.CompareTag(Tag.Group.GroupZ))
        {
            Destroy(staticNextGroup);
            staticNextGroup = Instantiate(staticGroup[5], NextGroupPosition, Quaternion.identity);
        }
        if (NextGroup.CompareTag(Tag.Group.GroupO))
        {
            Destroy(staticNextGroup);
            staticNextGroup = Instantiate(staticGroup[6], NextGroupPosition, Quaternion.identity);
        }


        Vector3 OriginPosition = transform.position;

        if (currentObject.CompareTag(Tag.Group.GroupO)|| currentObject.CompareTag(Tag.Group.GroupI))
        {
            transform.position += new Vector3(-0.5f, 0.5f, 0);
            Instantiate(currentObject, transform.position, Quaternion.identity);
            transform.position = OriginPosition;
        }
        else
        {
            Instantiate(currentObject, transform.position, Quaternion.identity);
        }

        WaitGroupList.Enqueue(Group[Random.Range(0, Group.Length)]);


    }

    void GroupPoolCreate()
    {
        for (int i = 0; i < 5; i++)
        {
            WaitGroupList.Enqueue(Group[Random.Range(0, Group.Length)]);
        }
    }



}
