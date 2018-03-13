using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Represents the button actions of the Main Menu, each action calls a method in the GameManager

    public void NewGame()
    {
        GameManager.Instance.LoadDocsScene();
    }

    public void Options()
    {
        GameManager.Instance.ShowOptions();
    }

    public void Credits()
    {
        GameManager.Instance.ShowCredits();
    }

    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }
}
