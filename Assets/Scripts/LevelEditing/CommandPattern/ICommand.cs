// Content modified from Game Engine Design Tutorials
// Author: Parisa Sargolzaei

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}
