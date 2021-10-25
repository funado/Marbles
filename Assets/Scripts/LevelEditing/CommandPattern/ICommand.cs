// Content modified from Game Engine Design Tutorials
// Author: Parisa Sargolzaei
// Modified By: Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}
