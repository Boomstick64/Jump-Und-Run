using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEMYDIE : MonoBehaviour
{
    // declaring the trigger box variable
    BoxCollider m_TriggerBox;

    private void Awake()
    {
        //Setting the trigger box variable to the box collider
        m_TriggerBox = GetComponent<BoxCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // On entrance of the trigger with an object if the object is the player then destroy the enemy. Check the tag
        if (other.tag == "Player")
        {
            DestroyEnemy();                                     // Calling the destroy enemy function
        }
    }

    //decloration of the destroy enemy function
    private void DestroyEnemy()
    {
        //destroying the parent object as this is attached to the child object that has a trigger box
        Destroy(transform.parent.gameObject);
    }
}
