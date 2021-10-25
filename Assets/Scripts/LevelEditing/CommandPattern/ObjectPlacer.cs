// Content modified from Game Engine Design Tutorials
// Author: Parisa Sargolzaei
// Modified By: Arthiran Sivarajah - 100660300, Aaron Chan - 100657311


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    static List<Transform> objects;
    public static void PlaceCube(Vector3 position, Color color, Transform cube)
    {
        Transform newObject = Instantiate(cube, position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        newObject.gameObject.AddComponent<EditPrefabTransform>();
        if (objects == null){
            objects = new List<Transform>();
        }
        objects.Add(newObject);
    }

    public static void RemoveCube(Vector3 position, Color color)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].position == position) 
            {
                GameObject.Destroy(objects[i].gameObject);
                objects.RemoveAt(i);
                break;
            }
        }
    }
}
