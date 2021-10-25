// Content modified from Game Engine Design Tutorials
// Author: Parisa Sargolzaei

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    static List<Transform> cubes;
    public static void PlaceCube(Vector3 position, Color color, Transform cube)
    {
        Transform newCube = Instantiate(cube, position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        newCube.gameObject.AddComponent<EditPrefabTransform>();
        //newCube.GetComponentInChildren<MeshRenderer>().material.color = color;
        if (cubes == null){
            cubes = new List<Transform>();
        }
        cubes.Add(newCube);
    }

    public static void RemoveCube(Vector3 position, Color color)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            if (cubes[i].position == position) 
            {
                GameObject.Destroy(cubes[i].gameObject);
                cubes.RemoveAt(i);
                break;
            }
        }
    }
}
