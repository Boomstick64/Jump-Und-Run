using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAIController : MonoBehaviour
{
    private GameObject m_Player = GameObject.Find("Player");

    NavMeshAgent m_Navemesh;
    // Start is called before the first frame update
    void Start()
    {
        m_Navemesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
