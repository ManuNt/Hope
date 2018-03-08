using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // Scriptable object - player's info
    public Player_Data m_Data;

    // Movement
    private float m_MoveSpeed, m_JumpForce;
    private static Rigidbody m_Rb;     // Using physics
    private Vector3 m_MoveDir;  // Used to process the movement direction
    private bool m_IsGrounded;  // Used to check if the player is on a surface
    private bool m_IsMoving;    // Used for the crosshair animation

    // Life
    public static int m_MaxHp;
    public static int m_Hp;

    // Random spawn points
    public Transform[] m_Spawners;

    private static float m_KnockbackForce, m_KnockTime, m_KnockCounter; // Not fully Implemented

    public static bool m_IsGamePaused;


    private void Start()
    {
        m_MoveSpeed = m_Data.GetRunSpeed();
        m_JumpForce = m_Data.GetJumpForce();

        int rand = Random.Range(0, m_Spawners.Length);

        transform.position = m_Spawners[rand].position;

        // In case the component is missing
        m_Rb = GetComponent<Rigidbody>();
        if (m_Rb == null)
        {
            m_Rb = gameObject.AddComponent<Rigidbody>();
            m_Rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        m_MoveDir = new Vector3();

        m_IsGrounded = false;
        m_IsMoving = false;

        m_MaxHp = 100;
        m_Hp = m_MaxHp;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        m_KnockTime = 0.3f;
        m_KnockCounter = 0f;
        m_KnockbackForce = 40f;

        m_IsGamePaused = false;
    }

    private void FixedUpdate()
    {
        if (!m_IsGamePaused)
        {
            m_MoveDir.y = m_Rb.velocity.y; // Keeping the right Y value
            m_Rb.velocity = m_MoveDir;
        }
    }


    private void Update()
    {
        if (!m_IsGamePaused)
        {
            if (m_Hp <= 0)
            {
                GameManager.Instance.GameOver();
            }

            if (m_KnockCounter <= 0f)
            {
                if (m_IsGrounded)
                {
                    CheckMovementInput();

                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Keypad0))
                    {
                        Jump();
                    }

                }
                else
                {
                    //Debug.Log(m_Rb.velocity.y);
                }

                transform.rotation = Quaternion.Euler(0f, MouseLook.GetCurrentYRot(), 0f); // Making sure that the player is rotated on the Y axis just like its head (camera)

                if (m_IsMoving)
                {
                    CrosshairManager.Instance.m_IsMoving = true;
                }
                else
                {
                    CrosshairManager.Instance.m_IsMoving = false;
                }

                if (Input.GetKeyDown(KeyCode.Escape) && !m_IsGamePaused)
                {
                    m_IsGamePaused = true;
                    GameManager.Instance.PauseGame();
                }
            }
            else
            {
                m_KnockCounter -= Time.deltaTime;
            }
            
        }
        

    }

    private void CheckMovementInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            m_IsMoving = true;
            m_MoveDir = -transform.right * m_MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            m_IsMoving = true;
            m_MoveDir = transform.right * m_MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            m_IsMoving = true;
            m_MoveDir = transform.forward * m_MoveSpeed;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            m_IsMoving = true;
            m_MoveDir = -transform.forward * (m_MoveSpeed / 1.5f);
        }
        else
        {
            m_IsMoving = false;
            m_MoveDir = Vector3.zero;
        }
    }



    private void Jump()
    {
        m_Rb.AddForce(Vector3.up * m_JumpForce);
    }
    

    private void OnTriggerStay(Collider aCol)
    {
        if (!m_IsGamePaused)
        {
            if (aCol.gameObject.tag == "Ground")
            {
                m_IsGrounded = true;

            }
        }
    }

    private void OnTriggerExit(Collider aCol)
    {
        if (!m_IsGamePaused)
        {
            if (aCol.gameObject.tag == "Ground")
            {
                m_IsGrounded = false;
            }
        }
    }

    public int GetHp() { return m_Hp; }
    public void GetHit(int aDamageAmount)
    {
        m_Hp -= aDamageAmount; 
    }

    public static void Knockback(float aZDir)
    {
        m_Rb.velocity -= new Vector3(m_Rb.velocity.x, m_Rb.velocity.y, aZDir * m_KnockbackForce);
        m_KnockCounter = m_KnockTime;
    }

}
