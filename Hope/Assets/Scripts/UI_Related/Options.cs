using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider m_VolumeSlider;

    private void Start()
    {
        m_VolumeSlider.value = AudioManager.Instance.m_MySource.volume; // Will have the same value when the DontDestroyOnLoad will be fixed!
    }

    private void Update ()
    {
        AudioManager.Instance.m_MySource.volume = m_VolumeSlider.value;
    }

    public void CloseOption()
    {
        Destroy(gameObject);
    }
}
