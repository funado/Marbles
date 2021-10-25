// Content modified from Game Engine Design Tutorials
// Author: Parisa Sargolzaei

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectCommand : ICommand
{
    Vector3 position;
    Color color;
    Transform cube;

    public PlaceObjectCommand(Vector3 position, Color color, Transform cube)
    {
        this.position = position;
        this.color = color;
        this.cube = cube;
    }
    
    public void Execute()
    {
        ObjectPlacer.PlaceCube(position, color, cube);
    }

    public void Undo()
    {
        ObjectPlacer.RemoveCube(position, color);
    }
}
