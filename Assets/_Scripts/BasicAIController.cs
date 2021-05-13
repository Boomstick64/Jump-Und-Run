using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAIController : MonoBehaviour
{
    private GameObject m_Player;
    private Vector3 m_StartingPosition;

    private bool m_InBattleZone;

    public bool m_inbattlezone 
    {
        get { return m_InBattleZone; }
        set { m_InBattleZone = value; }
    }

    NavMeshAgent m_Navemesh;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.Find("Player");
        m_Navemesh = GetComponent<NavMeshAgent>();
        m_StartingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_InBattleZone)
        {
            m_Navemesh.SetDestination(m_Player.transform.position);
        }
    }


}
