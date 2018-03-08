using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    private float m_Timer;
    private float m_Speed;
    private float m_BobbingAmount;
    private const float MIDPOINT = 1.44f; // The normal y position of my camera

	private void Start ()
    {
        m_Timer = 0f;
        m_Speed = 0.25f;
        m_BobbingAmount = 0.08f;
	}
	
	private void Update ()
    {
        if (!Player_Controller.m_IsGamePaused)
        {
            float waveslice = 0f;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (Mathf.Abs(horizontal) == 0f && Mathf.Abs(vertical) == 0f)
            {
                m_Timer = 0f;
            }
            else
            {
                waveslice = Mathf.Sin(m_Timer);
                m_Timer = m_Timer + m_Speed;

                if (m_Timer > Mathf.PI * 2)
                {
                    m_Timer = m_Timer - (Mathf.PI * 2);
                }
            }

            if (waveslice != 0f)
            {
                float translateChange = waveslice * m_BobbingAmount;
                float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
                totalAxes = Mathf.Clamp(totalAxes, 0f, 1f);
                translateChange = totalAxes * translateChange;
                Vector3 newPos = new Vector3(transform.localPosition.x, MIDPOINT + translateChange, transform.localPosition.z);
                transform.localPosition = newPos;
            }
            else
            {
                Vector3 newPos = new Vector3(transform.localPosition.x, MIDPOINT, transform.localPosition.z);
                transform.localPosition = newPos;
            }

        }
        
	}
}
