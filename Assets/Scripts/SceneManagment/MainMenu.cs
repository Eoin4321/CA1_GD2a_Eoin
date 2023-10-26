using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //Load first scene. 
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        //Closes the game.
        Application.Quit();
    }
}
