// Arthiran Sivarajah - 100660300, Aaron Chan - 100657311

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{
    public void Restart(string SceneName)
    {
        // Allows us to restart the scene on button press
        SceneManager.LoadScene(SceneName);
    }
}
