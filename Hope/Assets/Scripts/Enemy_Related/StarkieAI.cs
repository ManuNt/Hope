using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StarkieAI : MonoBehaviour
{
    private int m_Hp;                           // Hp
    private float m_Speed;                      // Movement speed that will be plugged in the NavMeshAgent's speed
    private int m_DamageAmount;                 // Damage amount for each attack
    private float m_AttackSpeed;                // Attack speed, will control the timer till the next attack
    private float m_NextAttack;                 // Will be uploaded before each attack and will run as a timer
    private float m_StoppingDistance;           // Will be used to tell the NavMeshAgent how far from the target the agent needs to stop before attacking

    private NavMeshAgent m_Agent;               // NavMeshAgent that will be used to handle every movement on the map
    public Animator m_Anim;                     // Will be used to control the animations linked to this GameObject

    private GameObject m_Target;                // Used to tell the NavMeshAgent its destination 
    private Player_Controller m_PlayerData;     // Used to get the player's Hp status


    public enum IState                          // The different states the enemy can be in
    {
        Idle,
        Chase,
        Attack,
        Die,
    }


    public IState m_State;                     // Will be used as a State Machine for the decision making

    private void Start ()
    {
        m_Target = EnemySpawner.Instance.ProvideTarget();

        m_PlayerData = m_Target.GetComponent<Player_Controller>();
        m_Agent = gameObject.AddComponent<NavMeshAgent>();

        m_Hp = 100;
        m_Speed = Random.Range(5f, 9f);
        m_DamageAmount = Random.Range(10, 15);
        m_AttackSpeed = Random.Range(1f, 1.5f); ;
        m_NextAttack = 0f;
        m_StoppingDistance = 5f;
        m_Agent.speed = m_Speed;
        m_Agent.stoppingDistance = m_StoppingDistance;
        m_Agent.angularSpeed = 300f;
        m_State = IState.Chase;


    }

    private void Update ()
    {
        if (!Player_Controller.m_IsGamePaused)
        {
            if (m_Hp > 0 && m_PlayerData.GetHp() > 0)
            {
                switch (m_State)
                {
                    case IState.Chase:
                        Chase();
                        break;
                    case IState.Attack:
                        m_Anim.SetBool("IsRunning", false);
                        Attack();
                        break;
                }
            }
            else if (m_Hp > 0 && m_PlayerData.GetHp() <= 0)
            {
                m_State = IState.Idle;
                m_Anim.SetBool("IsRunning", false);
            }

            if (m_Hp <= 0)
            {
                Die();
            }
        }

    }

    private void Chase()
    {
        m_Agent.SetDestination(m_Target.transform.position);
        m_Anim.SetBool("IsRunning", true);
    }

    private void Attack()
    {
        if (Time.time > m_NextAttack)
        {
            m_Anim.SetTrigger("AttackSwip");
            m_NextAttack = Time.time + m_AttackSpeed;
            m_PlayerData.GetHit(m_DamageAmount);

            Player_Controller.Knockback(transform.forward.z);
        }
    }

    private void Die()
    {
        EnemySpawner.Instance.m_NoofEnemiesKilled++;
        Destroy(gameObject);
    }

    public void GetHit(int aDamage)
    {
        ApplyDamage(aDamage);
        m_Anim.SetTrigger("GetHit");
    }

    private void ApplyDamage(int aAmout)
    {
        m_Hp -= aAmout;
    }

    private void OnTriggerStay(Collider aCol)
    {
        if (!Player_Controller.m_IsGamePaused)
        {
            if (aCol.tag == "Player" && m_PlayerData.GetHp() > 0 && m_State != IState.Attack)
            {
                m_State = IState.Attack;
            }
        }
    }

    private void OnTriggerExit(Collider aCol)
    {
        if (!Player_Controller.m_IsGamePaused)
        {
            if (aCol.tag == "Player" && m_PlayerData.GetHp() > 0)
            {
                m_State = IState.Chase;
            }
            else if (aCol.tag == "Player" && m_PlayerData.GetHp() <= 0)
            {
                m_State = IState.Idle;
            }
        }
    }

}
