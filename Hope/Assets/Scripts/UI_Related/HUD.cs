using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text m_PlayerHp, m_WaveNb, m_Ammo, m_EnemiesLeft;        // Represents all the texts on the HUD
    public Image m_SelectedWeaponIcon;                              // Used to show an icon of the selected weapon
    public Sprite[] m_Icons;                                        // The list of icons used
    public GameObject m_TimerBetweenWaves;                          // Holds the timer's text

    public Image m_BloodScreen;                                     // Used to show the player the amount of damage by flooding the screen with blood
    Color m_color;                                                  // Used to change the alpha color of the blood (can't do it directly on the image)

    private void Start()
    {
        m_color = m_BloodScreen.color;
    }

    private void Update ()
    {
        m_PlayerHp.text = Player_Controller.m_Hp.ToString();
        m_WaveNb.text = EnemySpawner.Instance.m_WaveNb.ToString() + " / 10";
        m_EnemiesLeft.text = EnemySpawner.Instance.m_NoofEnemiesKilled.ToString() + " / " + EnemySpawner.Instance.m_EnemyCount.ToString();

        switch (WeaponSwitch.m_SelectedWeapon)
        {
            case 0:
                m_Ammo.text = Gun.m_CurrentAmmo.ToString() + " / " + Gun.m_MaxAmmo.ToString();
                break;
            case 1:
                m_Ammo.text = Mp7.m_CurrentAmmo.ToString() + " / " + Mp7.m_MaxAmmo.ToString();
                break;

        }

        m_SelectedWeaponIcon.sprite = m_Icons[WeaponSwitch.m_SelectedWeapon];

        m_color.a = (float)((Player_Controller.m_MaxHp - Player_Controller.m_Hp)) / (float)Player_Controller.m_MaxHp;

        m_BloodScreen.color = m_color;

    }

    public void PlayTimerBetweenWaves()
    {
        GameObject timer = Instantiate(m_TimerBetweenWaves);
    }
}
