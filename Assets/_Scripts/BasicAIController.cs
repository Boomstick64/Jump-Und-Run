using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAIController : MonoBehaviour
{
    private GameObject m_Player;
    private Vector3 m_StartingPosition;
    private float m_AttackTimer = 2f;
    private bool m_IsAttacking;

    PlayerController m_PlayerController;

    private bool m_InBattleZone;

    public bool m_inbattlezone 
    {
        get { return m_InBattleZone; }
        set { m_InBattleZone = value; }
    }

    NavMeshAgent m_Navemesh;

    private void Awake()
    {
        m_Player = GameObject.Find("Player");
        m_PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        m_Navemesh = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        m_StartingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (m_InBattleZone)
        {
            m_Navemesh.SetDestination(m_Player.transform.position);
           // Debug.Log("It's true");
        }

        if (m_IsAttacking)
        {
            m_AttackTimer -= Time.deltaTime;
            
            if (m_AttackTimer <= 0f)
            {
                m_PlayerController.currentHealth -= 5f;
                m_AttackTimer = 2f;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            m_IsAttacking = true;
            Debug.Log("IS ATTACKING");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            m_IsAttacking = false;
            m_AttackTimer = 2f;
        }

    }

}
