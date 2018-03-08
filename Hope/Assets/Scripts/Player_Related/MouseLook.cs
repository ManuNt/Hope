using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    // Limits within which the camera can rotate up and down on the X axis
    public float m_TopLimit, m_BtmLimit;

    // Used to make the right feeling movement betweent the mouse and the camera movement in 3D space
    private float m_LookSensitivity;

    // These are to save the target rotation
    private float m_XRotation, m_YRotation;

    // The amount of degrees the camera is rotated. Used to smothly catch up with the target rotation
    private static float m_CurrentYRotation, m_CurrentXRotation;

    // Using it because the function SmoothDamp needs it
    private float m_YRotV, m_XRotV;

    // It's the amount of time the catching up between the current rotations and the target rotations
    private float m_LookSmoothDamp;

	private void Start ()
    {
        m_LookSensitivity = 2.5f;
        m_LookSmoothDamp = 0.07f;
        m_BtmLimit = 50f;
        m_TopLimit = -85f;

    }
	
	private void Update ()
    {
        if (!Player_Controller.m_IsGamePaused)
        {
            m_YRotation += Input.GetAxis("Mouse X") * m_LookSensitivity; // Updating the horizontal rotation target
            m_XRotation -= Input.GetAxis("Mouse Y") * m_LookSensitivity; // Updating the vertical rotation target

            if (m_XRotation > m_BtmLimit)
            {
                m_XRotation = m_BtmLimit;
            }
            else if (m_XRotation < m_TopLimit)
            {
                m_XRotation = m_TopLimit;
            }

            m_CurrentXRotation = Mathf.SmoothDamp(m_CurrentXRotation, m_XRotation, ref m_XRotV, m_LookSmoothDamp);
            m_CurrentYRotation = Mathf.SmoothDamp(m_CurrentYRotation, m_YRotation, ref m_YRotV, m_LookSmoothDamp);

            transform.rotation = Quaternion.Euler(m_CurrentXRotation, m_CurrentYRotation, 0f);
        }

    }

    public static float GetCurrentYRot() { return m_CurrentYRotation; }
}
