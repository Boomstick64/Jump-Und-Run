using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleZoneTrigger : MonoBehaviour
{
    private GameObject m_Player;
    private BasicAIController m_InZone;
    // Start is called before the first frame update
    void Start()
    {
        m_Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == m_Player)
        {
            m_InZone.m_inbattlezone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == m_Player)
        {
            m_InZone.m_inbattlezone = false;
        }
    }
}
