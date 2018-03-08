using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem m_MuzzleFlash;
    private Camera m_Camera;
    private Animator m_Anim;
    private int m_Damage;

    public static int m_MaxAmmo;
    public static int m_CurrentAmmo;

    private AudioSource m_GunSoundSource;  // Not included yet (GOLD)
    public AudioClip m_Shoot;

	private void Start ()
    {

        m_GunSoundSource = GetComponent<AudioSource>();
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
