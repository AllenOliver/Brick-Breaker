using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void beginNew()

    {
        SceneManager.LoadScene("Level_One");
    }

    public void beginLevelSelect(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }


    public void viewCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void endGame()

    {
        //Pc version only
        //xbox don't need it 
        Application.Quit();
    }
} 
