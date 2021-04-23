using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declaring a public float for turning left to right
    public float m_HorizontalTurn;

    // Declaring a public float for the speed of the left to right
    public float m_SpeedX;

    //Declaring a public move speed float variable
    public float m_Speed;

    public float m_VerticalVelocity;
    public float m_JumpForce = 100f;

    private float m_Gravity = 14f;
    public bool m_IsGrounded;

    private float m_Vertical_Movement;
    private float m_Horizontal_Movement;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //Player rotation section
        // Setting m_HorizontalTurn to the mouse moving along the X axis * by the speed
        m_HorizontalTurn = m_SpeedX * Input.GetAxis("Mouse X");

        // multiplying that by delta time and setting it so that the turn is frame independent
        // m_HorizontalTurn *= Time.deltaTime;

        // then rotating the player for however much is needed
        transform.Rotate(0f, m_HorizontalTurn, 0f);

        // Player Jump section
        if (m_IsGrounded)
        {
            // m_VerticalVelocity = -m_Gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                m_VerticalVelocity = m_JumpForce;
            }
            else
            {
                m_VerticalVelocity = 0f;
            }
        }
        else if (!m_IsGrounded && m_VerticalVelocity <= 0f)
        {
            m_VerticalVelocity -= m_Gravity * 2 * Time.deltaTime;
        }
        else if (!m_IsGrounded && m_VerticalVelocity > 0f)
        {
            m_VerticalVelocity -= m_Gravity * Time.deltaTime;
        }

        Vector3 theVerticalVelocity = new Vector3(0f, m_VerticalVelocity, 0f);
        transform.Translate(theVerticalVelocity * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Player movement section
        m_Vertical_Movement = Input.GetAxis("Vertical") * m_Speed;
        m_Horizontal_Movement = Input.GetAxis("Horizontal") * m_Speed;

        Vector3 move = new Vector3(m_Horizontal_Movement, 0f, m_Vertical_Movement);
        Vector3.Normalize(move);
        move *= Time.deltaTime;

        transform.Translate(move);

        //Player jump physics section
        RaycastHit hit;

        LayerMask mask = LayerMask.GetMask("Player");
        mask = ~mask;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, mask))
        {
      
        }

        if (hit.distance <= 1f || hit.distance == 1f)
        {
            m_IsGrounded = true;
        }
        else if (hit.distance < 1)
        {
            float difference;
            Vector3 playerPosition;
            difference = 1 - hit.distance;
            playerPosition = new Vector3(0f, hit.distance + difference, 0f);
            transform.position += playerPosition;
            m_IsGrounded = true;
        }
        else
        {
            m_IsGrounded = false;
        }
    }

   
}
