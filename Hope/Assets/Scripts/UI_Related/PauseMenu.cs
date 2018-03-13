using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Represents the different button actions on the pause menu. The actions are all in the GameManager script (singleton)

    public void Resume()
    {
        GameManager.Instance.UnpauseGame();
        Destroy(gameObject);
    }

    public void Options()
    {
        GameManager.Instance.ShowOptions();
    }

    public void MainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void Quit()
    {
        GameManager.Instance.QuitGame();
    }

}
