using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem m_MuzzleFlash;        // Used for the muzzle flash when shooting
    private Camera m_Camera;                    // Used for the raycast going from the camera when shooting
    private Animator m_Anim;                    // The animation of the gun when shooting
    private int m_Damage;                       // The amount of hit points the gun does

    public static int m_MaxAmmo, m_CurrentAmmo; // Used to regulate the amount of amunition. Static because used by the HUD and the ammo manager

    private AudioSource m_GunSoundSource;       // The audio source from where the shot will be fired
    public AudioClip m_Shoot;                   // The actual gun shot sound

    private void Start ()
    {

        m_GunSoundSource = GetComponent<AudioSource>();
        m_GunSoundSource.clip = m_Shoot;
        m_Camera = Camera.main;

        m_Anim = GetComponent<Animator>();
        m_Damage = 10;
        m_MaxAmmo = 50;
        m_CurrentAmmo = m_MaxAmmo;
    }
	
	private void Update ()
    {
        if (!Player_Controller.m_IsGamePaused)
        {
            if (Input.GetButtonDown("Fire1") && m_CurrentAmmo > 0)
            {
                m_MuzzleFlash.Play();
                // AudioManager.Instance.PlayMe(m_GunSoundSource, m_Shoot); En commentaire pour les oreilles :)
                Shoot();
            }
        }
	}


    private void Shoot()
    {
        m_CurrentAmmo--;
        m_Anim.SetTrigger("Shoot");
        m_GunSoundSource.Play();

        RaycastHit hit;


        CrosshairManager.Instance.Shoot();
        if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, 500f))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<StarkieAI>().GetHit(m_Damage);
            }
        }
    }

}
