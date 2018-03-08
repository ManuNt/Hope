// Ambient Music - https://www.bensound.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager m_Instance;
    
    public static AudioManager Instance { get { return m_Instance; } }
    
    [HideInInspector]
    public AudioSource m_MySource;
    public AudioClip m_DocsClip, m_MainMenuClip;

    public float m_GameVolume;

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
