using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
