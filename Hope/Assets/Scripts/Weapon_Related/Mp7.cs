using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mp7 : MonoBehaviour
{
    public ParticleSystem m_MuzzleFlash;                // Used for the muzzle flash when shooting

    private Camera m_Camera;                            // Used for the raycast going from the camera when shooting


    public static int m_MaxAmmo, m_CurrentAmmo;         // Used to regulate the amount of amunition. Static because used by the HUD and the ammo manager
    private const float FIRE_SPEED = 13;                // The number of bullet per second
    private float m_WaitTilNextFire;                    // Used as a timer between the shots
    private int m_Damage;                               // The amount of hit points the rifle does

    private Animator m_Anim;                            // The animation of the rifle when shooting

    private AudioSource m_Mp7SoundSource;               // The audio source from where the shot will be fired
    public AudioClip m_Shoot;                           // The actual rifle shot sound

    private void Start ()
    {
        m_Camera = Camera.main;

        m_MaxAmmo = 120;
        m_CurrentAmmo = m_MaxAmmo;
        m_Damage = 15;

        m_Anim = GetComponent<Animator>();

        m_Mp7SoundSource = GetComponent<AudioSource>();
        m_Mp7SoundSource.clip = m_Shoot;

    }
	
	private void Update ()
    {
        if (!Player_Controller.m_IsGamePaused)
        {
            if (Input.GetButton("Fire1") && m_CurrentAmmo > 0)
            {
                m_MuzzleFlash.Play();
                //AudioManager.Instance.PlayMe(m_Mp7SoundSource, m_Shoot);  Commente pour les oreilles lol
                Shoot();
            }
            m_WaitTilNextFire -= Time.deltaTime * FIRE_SPEED;
        }
	}

    private void Shoot()
    {

        if (m_WaitTilNextFire <= 0)
        {
            m_CurrentAmmo--;
            m_Anim.SetTrigger("Shoot");
            m_Mp7SoundSource.Play();

            RaycastHit hit;


            CrosshairManager.Instance.Shoot();
            if (Physics.Raycast(m_Camera.transform.position, m_Camera.transform.forward, out hit, 500f))
            {
                if (hit.collider.tag == "Enemy")
                {
                    hit.collider.GetComponent<StarkieAI>().GetHit(m_Damage);
                }
            }

            m_WaitTilNextFire = 1f;
        }
    }
}
