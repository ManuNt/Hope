using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_Rotator : MonoBehaviour
{
    private float m_RotationSpeed;      // The GameObject's rotation speed


	private void Start ()
    {
        m_RotationSpeed = 60f;
	}
	
	private void Update ()
    {
        transform.Rotate(Vector3.right, m_RotationSpeed * Time.deltaTime);
	}
}
