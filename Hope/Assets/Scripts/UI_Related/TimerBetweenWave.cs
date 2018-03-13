using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBetweenWave : MonoBehaviour
{
    private const int TIME_BETWEEN_WAVES = 30;              // The amount of time between the waves
    public Text m_TxtTimer;                                 // The actual timer text on screen
	
	private void Start ()
    {
        StartCoroutine(StartTimer());
	}
	
	private IEnumerator StartTimer()
    {
        for (int i = 0; i <= TIME_BETWEEN_WAVES; ++i)
        {
            m_TxtTimer.text = (TIME_BETWEEN_WAVES - i).ToString();
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);

        m_TxtTimer.text = "New wave!";

        yield return new WaitForSeconds(2f);

        EnemySpawner.m_IsTimerDone = true;

        Destroy(gameObject);
    }
}
