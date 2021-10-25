// Content modified from Game Engine Design Tutorials
// Author: Parisa Sargolzaei
// Modified By: Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    static Queue<ICommand> commandBuffer;

    static List<ICommand> commandHistory;
    static int counter;

    private void Awake() 
    {
        commandBuffer = new Queue<ICommand>();
        commandHistory = new List<ICommand>();
    }

    public static void AddCommand(ICommand command)
    {
        while(commandHistory.Count > counter)
        {
            commandHistory.RemoveAt(counter);
        }
        
        commandBuffer.Enqueue(command);
    }

    // Update is called once per frame
    void Update()
    {
        if (commandBuffer.Count > 0)
        {
            ICommand c = commandBuffer.Dequeue();
            c.Execute();
            
            //commandBuffer.Dequeue().Execute();

            commandHistory.Add(c);
            counter++;
            Debug.Log("Command history length: " + commandHistory.Count);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                UndoCommand();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                RedoCommand();
            }
        }
    }

    public void UndoCommand()
    {
        if (counter > 0)
        {
            counter--;
            commandHistory[counter].Undo();
        }
    }

    public void RedoCommand()
    {
        if (counter < commandHistory.Count)
        {
            commandHistory[counter].Execute();
            counter++;
        }
    }
}
