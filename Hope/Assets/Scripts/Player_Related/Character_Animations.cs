using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animations : MonoBehaviour
{
    private static Character_Animations m_Instance;
    public static Character_Animations Instance { get { return m_Instance; } }
    public Animator m_Anim;

    public enum IState
    {
        Idle_Gun,
        Idle_Rifle,
        Walk_Gun,
        Walk_Rifle,
    }

    public IState m_State;
	
	private void Awake ()
    {
		if (m_Instance == null)
        {
            m_Instance = this;
        }
	}
	
	private void Update ()
    {
		switch(m_State)
        {
            case IState.Idle_Gun:
                m_Anim.SetBool("IsWalking_Gun", false);
                //m_Anim.SetBool("IsIdle_Gun", true);
                //m_Anim.SetBool("IsWalking_Rifle", false);
                break;
            //case m_CharacterState.Idle_Rifle:
            //    m_Anim.SetBool("IsWalking_Gun", false);
            //    break;
            case IState.Walk_Gun:
                m_Anim.SetBool("IsWalking_Gun", true);
                break;
            //case m_CharacterState.Walk_Rifle:
            //    m_Anim.SetBool("IsWalking_Gun", false);
            //    break;
        }
	}

    public void SwitchState(IState aState)
    {
        m_State = aState;
    }

    public void TriggerGunShot()
    {
        m_Anim.SetTrigger("Shoot_Gun");
    }
}
