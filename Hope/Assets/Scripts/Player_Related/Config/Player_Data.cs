using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class Player_Data : ScriptableObject
{   
    [SerializeField] private float m_RunSpeed = 10f;
    [SerializeField] private float m_JumpForce = 350f;
    [SerializeField] private float m_RotationSpeed = 3f;

    // Getters
    public float GetRunSpeed() { return m_RunSpeed; }
    public float GetJumpForce() { return m_JumpForce; }
    public float GetRotationSpeed() { return m_RotationSpeed; }

}
