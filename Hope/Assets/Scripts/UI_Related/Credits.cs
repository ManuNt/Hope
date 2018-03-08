using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    private string m_Inspired, m_Music;
    public Text m_TxtHope, m_TxtMusic;

    private void Start()
    {
        m_Inspired = "Inspired by Killing Floor 2 \n by Tripwire Interactive";
        m_Music = "Music by : bensound.com";

        StartCoroutine(ShowCredits());
    }

    private IEnumerator ShowCredits()
    {
        for (int i = 0; i < m_Inspired.Length; ++i)
        {
            m_TxtHope.text += m_Inspired[i];
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < m_Music.Length; ++i)
        {
            m_TxtMusic.text += m_Music[i];
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void CloseCredits()
    {
        Destroy(gameObject);
    }
}
