// Ambient Music - https://www.bensound.com   Credit

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // Used as a singleton to ease the acces to the data from multiple GameObjects and scripts
    private static AudioManager m_Instance;
    
    public static AudioManager Instance { get { return m_Instance; } }
    
    [HideInInspector]
    public AudioSource m_MySource;                      // The main game audio source
    public AudioClip m_DocsClip, m_MainMenuClip;        // Holds the different scenes musics

    public float m_GameVolume;                          // Keeps the volume set in the options

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else if (m_Instance != this)
        {
            Destroy(gameObject);
        }

        m_MySource = GetComponent<AudioSource>();

        PlayMainMenu();
    

        // TODO - When Save/Load, Load volume saved
        m_MySource.volume = 1f;
    }

	
    public void PlayMe(AudioSource aSource, AudioClip aClip)
    {
        aSource.clip = aClip;
        aSource.Play();
    }

    public void PlayMainMenu()
    {
        m_MySource.clip = m_MainMenuClip;
        m_MySource.Play();
    }

    public void PlayTheDocs()
    {
        m_MySource.clip = m_DocsClip;
        m_MySource.Play();
    }

}
