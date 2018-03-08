using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StarkieAI : MonoBehaviour
{
    private int m_Hp;
    private float m_Speed;
    private int m_DamageAmount;
    private float m_AttackSpeed;
    private float m_NextAttack;
    private float m_StoppingDistance;

    private NavMeshAgent m_Agent;
    public Animator m_Anim;

    private GameObject m_Target;
    private Player_Controller m_PlayerData;


    public enum IState
    {
        Idle,
        Chase,
        Attack,
        Die,
    }


    public IState m_State;

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
