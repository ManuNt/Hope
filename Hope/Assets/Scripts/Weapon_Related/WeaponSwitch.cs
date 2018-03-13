using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{

    public GameObject[] m_Weapons;              // The array of weapon GameObjects
    public static int m_SelectedWeapon;         // Will be used to control the weapon selection

    private void Start ()
    {
        HideAllWeapons();
        m_SelectedWeapon = 0;
        m_Weapons[m_SelectedWeapon].SetActive(true);
        CrosshairManager.Instance.SwitchWeapon("Gun");
    }
	
	private void Update ()
    {
        if (!Player_Controller.m_IsGamePaused)
        {
            CheckSwitching();
        }

    }

    private void CheckSwitching()
    {
        

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Scroll up
        {
            m_Weapons[m_SelectedWeapon].SetActive(false);
            m_SelectedWeapon++;
            if (m_SelectedWeapon == m_Weapons.Length)
            {
                m_SelectedWeapon = 0;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            m_Weapons[m_SelectedWeapon].SetActive(false);
            m_SelectedWeapon--;
            if (m_SelectedWeapon < 0)
            {
                m_SelectedWeapon = m_Weapons.Length - 1;
            }
        }

        m_Weapons[m_SelectedWeapon].SetActive(true);

        switch(m_SelectedWeapon)
        {
            case 0:
                CrosshairManager.Instance.SwitchWeapon("Gun");
                break;
            case 1:
                CrosshairManager.Instance.SwitchWeapon("Mp7");
                break;
        }
    }

    private void HideAllWeapons()
    {
        foreach (GameObject weapon in m_Weapons)
        {
            weapon.SetActive(false);
        }
    }
}
