using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayPveGameMode()
    {
        SceneManager.LoadScene(1);

    }
    
    public void PlayPvpGameMode()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackHome()
    {
        SceneManager.LoadScene(0);
    }
}
