using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    // Used as a singleton to ease the acces to the data from multiple GameObjects and scripts
    private static AmmoManager m_Instance;
    public static AmmoManager Instance { get { return m_Instance; } }

    private const float RESPAWN_TIME = 180f;        // How long before the next respawn

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }
	
    public void ReloadAmmo(GameObject aItem)
    {
        Gun.m_CurrentAmmo += Gun.m_MaxAmmo;

        if (Gun.m_CurrentAmmo > Gun.m_MaxAmmo)
        {
            Gun.m_CurrentAmmo = Gun.m_MaxAmmo;
        }

        Mp7.m_CurrentAmmo += Mp7.m_MaxAmmo;

        if (Mp7.m_CurrentAmmo > Mp7.m_MaxAmmo)
        {
            Mp7.m_CurrentAmmo = Mp7.m_MaxAmmo;
        }
        
        StartCoroutine(HideBox(aItem));
    }

    IEnumerator HideBox(GameObject aItem)
    {
        aItem.SetActive(false);

        yield return new WaitForSeconds(RESPAWN_TIME);

        aItem.SetActive(true);
    }

    public void RestoreHealth(GameObject aItem)
    {
        Player_Controller.m_Hp = Player_Controller.m_MaxHp;
        StartCoroutine(HideBox(aItem));
    }

}
