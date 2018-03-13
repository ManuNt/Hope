using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    // Used as a singleton to ease the acces to the data from multiple GameObjects and scripts
    private static EnemySpawner m_Instance;
    public static EnemySpawner Instance { get { return m_Instance; } }

    public GameObject m_StarkiePrefab;                                          // The actuel enemy's prefab
    public Transform[] m_Spawners = new Transform[10];                          // The different spawning spots
    private float m_SpawningSpeed, m_NextSpawn;                                 // Used to regulate the spawning process
        
    public GameObject m_Player;                                                 // Used to feed a target to the spawned agent

    private int m_Counter;                                                      // Used to regulate the amount spawned
    public int m_NoofEnemiesKilled, m_WaveNb, m_EnemyCount;                     // Used to know how many enemy have been kill, and the current wave number used to know how many enemy will be spawned
    private const int MAX_NOOF_ENEMIES_INCREMENTER = 10;                        // Represents the increase amount for every new wave
    private const int INITIAL_NOOF_ENEMIES = 30;                                // The initial number of enemies
    private bool m_CanSpawn, m_CanUpdateInfo, m_FirstSpawning, m_IsTimerOn;     // Used to regulate the spawning process and the information displayed

    public static bool m_IsTimerDone;                                           // Used to know if the next wave can spawn
    public HUD m_Timer;                                                         // Used to connect to the HUD's timer

    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }

    private void Start ()
    {
        m_SpawningSpeed = 2f;
        m_WaveNb = 0;
        m_NoofEnemiesKilled = 0;
        m_EnemyCount = 0;
        m_NextSpawn = 0;
        m_CanSpawn = true;
        m_CanUpdateInfo = true; // 
        m_IsTimerDone = true;   // To allow the player to have time in between waves
        m_FirstSpawning = true; // To avoid having the timer running before the first wave
        m_IsTimerOn = false;
    }
	
	private void Update ()
    {
        if (!GameManager.Instance.m_IsGameOver)
        {
            if (m_NoofEnemiesKilled == m_EnemyCount)
            {
                m_CanSpawn = true;
                m_CanUpdateInfo = true;
                if (!m_FirstSpawning && !m_IsTimerOn)
                {
                    m_Timer.PlayTimerBetweenWaves();
                    m_IsTimerOn = true;
                }
            }

            if (m_CanSpawn && m_IsTimerDone)
            {
                if (m_CanUpdateInfo)
                {
                    UpdateInfos();
                }
                if (m_Counter < m_EnemyCount)
                {
                    if (Time.time > m_NextSpawn)
                    {
                        m_NextSpawn = Time.time + m_SpawningSpeed;
                        SpawnEnemy();
                        m_Counter++;
                    }
                }
                else
                {
                    m_CanSpawn = false;
                    m_IsTimerDone = false;
                    m_IsTimerOn = false;

                    if (m_FirstSpawning)
                    {
                        m_FirstSpawning = false;
                    }
                }
            }

        }
		
	}

    private void SpawnEnemy()
    {
        int randSpawner = 0;
        randSpawner = Random.Range(0, m_Spawners.Length);
        GameObject enemy = Instantiate(m_StarkiePrefab);
        enemy.transform.position = m_Spawners[randSpawner].position;
    }

    private void UpdateInfos()
    {
        m_WaveNb++;

        if (m_WaveNb <= 10)
        {
            m_CanUpdateInfo = false;
            m_EnemyCount = INITIAL_NOOF_ENEMIES + (MAX_NOOF_ENEMIES_INCREMENTER * (m_WaveNb - 1));

            m_Counter = 0;
            m_NoofEnemiesKilled = 0;
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }
    
    public GameObject ProvideTarget() { return m_Player; }
}
