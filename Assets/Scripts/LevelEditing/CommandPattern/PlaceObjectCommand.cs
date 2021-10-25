// Content modified from Game Engine Design Tutorials
// Author: Parisa Sargolzaei
// Modified By: Arthiran Sivarajah - 100660300, Aaron Chan - 100657311


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectCommand : ICommand
{
    Vector3 position;
    Color color;
    Transform newObject;

    public PlaceObjectCommand(Vector3 position, Color color, Transform _object)
    {
        this.position = position;
        this.color = color;
        this.newObject = _object;
    }
    
    public void Execute()
    {
        ObjectPlacer.PlaceCube(position, color, newObject);
    }

    public void Undo()
    {
        ObjectPlacer.RemoveCube(position, color);
    }
}
