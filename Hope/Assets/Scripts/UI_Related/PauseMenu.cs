using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{


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
