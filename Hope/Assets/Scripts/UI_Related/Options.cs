using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider m_VolumeSlider;           // Represent the volume slider

    private void Start()
    {
        m_VolumeSlider.value = AudioManager.Instance.m_MySource.volume;
    }

    private void Update ()
    {
        AudioManager.Instance.m_MySource.volume = m_VolumeSlider.value;
    }

    public void ShowControls()
    {
        GameManager.Instance.ShowControls();
    }

    public void CloseOption()
    {
        Destroy(gameObject);
    }
}
