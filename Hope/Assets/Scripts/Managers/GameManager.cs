using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Used as a singleton to ease the acces to the data from multiple GameObjects and scripts

    private static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }

    public GameObject m_OptionMenu; // Options Canvas
    public GameObject m_PauseMenu, m_EndGameUIPrefab, m_Credits, m_Controls; // The different menus prefabs
    public bool m_IsGameOver;       // Checks if the game is over in order to trigger other actions

	private void Awake ()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }

    }


    public void LoadDocsScene()
    {
        m_IsGameOver = false;
        SceneManager.LoadScene(1);
        AudioManager.Instance.PlayTheDocs();
    }

    public void LoadMainMenu()
    {
        EnableMouse();
        AudioManager.Instance.PlayMainMenu();
        SceneManager.LoadScene(0);
    }

    public void ShowOptions()
    {
        GameObject option = Instantiate(m_OptionMenu);
    }

    public void ShowCredits()
    {
        GameObject credits = Instantiate(m_Credits);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void LoadGame()
    {
        // TODO - Add Save/Load feature
    }

    public void PauseGame()
    {
        GameObject pauseMenu = Instantiate(m_PauseMenu);
        EnableMouse();

        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
        }

    }

    public void UnpauseGame()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }

        DisableMouse();

        Player_Controller.m_IsGamePaused = false;


    }

    private void EnableMouse()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void DisableMouse()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameOver()
    {
        Player_Controller.m_IsGamePaused = true;
        m_IsGameOver = true;
        EnableMouse();
        GameObject endScreen = Instantiate(m_EndGameUIPrefab);
    }

    public void ShowControls()
    {
        GameObject controls = Instantiate(m_Controls);
    }
}
