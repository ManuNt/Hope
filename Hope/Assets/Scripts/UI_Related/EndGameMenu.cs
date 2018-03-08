using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    public Text m_EndGameText;

    private void Start()
    {
        if (Player_Controller.m_Hp <= 0)
        {
            m_EndGameText.text = "You've Lost!";
        }
        else
        {
            m_EndGameText.text = "Congratulations!!";
        }
    }

    public void Retry()
    {
        GameManager.Instance.LoadDocsScene();
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
