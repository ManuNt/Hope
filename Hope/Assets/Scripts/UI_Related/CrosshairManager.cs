using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    public Animator m_Anim;                             // The crosshair animation
    public Image m_CrosshairImg;                        // The actual crosshair image

    private Camera m_Camera;                            // The camera is used to for the raycast, that if it hits an enemy, the crosshair will turn red
    private const float MAX_DISTANCE = 1000f;           // How far can the raycast go

    public Sprite m_Gun, m_GunTarget, m_Rifle, m_RifleTarget, m_Melee, m_NotPossible;       // Represents the different sprites used depending on the selected weapon, and if it is targeting an enemy
    private enum ITargetSprite                          // The different sprite states
    {
        Gun,
        Rifle,
        Melee,
        Not,
    }

    private ITargetSprite m_TargetSprite;               // Used as a State Machine

    // Singleton
    private static CrosshairManager m_Instance;
    public static CrosshairManager Instance
    {
        get { return m_Instance; }
    }



    public bool m_IsEnemyTargeted, m_IsMoving;          // Used to regulate what is targeted for the State Machine, and if the player is moving (its precision won't be the same)

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

    private void Start ()
    {
        m_IsEnemyTargeted = false;
        m_IsMoving = false;
        m_Camera = Camera.main;
    }
	
	private void Update ()
    {
        if (m_IsEnemyTargeted)
        {
            TargetLocked();
        }
        else
        {
            m_CrosshairImg.color = Color.white;
            switch (m_TargetSprite)
            {
                case ITargetSprite.Gun:
                    m_CrosshairImg.sprite = m_Gun;
                    break;
                case ITargetSprite.Rifle:
                    m_CrosshairImg.sprite = m_Rifle;
                    break;
            }
        }

        if (m_IsMoving)
        {
            m_Anim.SetBool("isRunning", true);
        }
        else
        {
            m_Anim.SetBool("isRunning", false);
        }

        AmITargetingAnEnemy();
        
        
        

	}

    public void SwitchWeapon(string aName)
    {
        switch (aName)
        {
            case "Gun":
                m_CrosshairImg.sprite = m_Gun;
                m_TargetSprite = ITargetSprite.Gun;
                break;
            case "Mp7":
                m_CrosshairImg.sprite = m_Rifle;
                m_TargetSprite = ITargetSprite.Rifle;
                break;
            case "Melee":
                m_CrosshairImg.sprite = m_Melee;
                m_TargetSprite = ITargetSprite.Melee;
                break;
            case "Not":
                m_CrosshairImg.sprite = m_NotPossible;
                m_TargetSprite = ITargetSprite.Not;
                break;
        }
        
    }

    private void TargetLocked()
    {
        switch (m_TargetSprite)
        {
            case ITargetSprite.Gun:
                m_CrosshairImg.sprite = m_GunTarget;
                break;
            case ITargetSprite.Rifle:
                m_CrosshairImg.sprite = m_RifleTarget;
                break;
            case ITargetSprite.Melee:
                m_CrosshairImg.sprite = m_Melee;
                break;
            case ITargetSprite.Not:
                m_CrosshairImg.sprite = m_NotPossible;
                break;
        }
        m_CrosshairImg.color = Color.red;        
    }

    private void AmITargetingAnEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, MAX_DISTANCE))
        {
            if (hit.collider.tag == "Enemy")
            {
                TargetLocked();
            }
        }
    }

    public void Shoot()
    {
        m_Anim.SetTrigger("attack");
    }
}
