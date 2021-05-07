using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
 
    //Declaring health variables
    private float m_CurrentHealth;
    public float m_MaxHealth;
    private float m_Damage;
    private float m_OriginalAirTimeToDamage;
    private float m_AirTimeToDamage = 2;
    private bool m_ToDamage;

    public float currentHealth { get { return m_CurrentHealth; } }

    // Declaring a public float for turning left to right
    public float m_HorizontalTurn;

    // Declaring a public float for the speed of the left to right
    public float m_SpeedX;

    //Start of Movement Variables

    //Declaring a public move speed float variable
    public float m_Speed;

    private float m_Horizontal_Movement;
    private float m_Vertical_Movement;
    private float CollisionDistanceCheck = 0.7f;

    private Vector3 m_Move;
    [SerializeField] bool m_Colliding, m_CollidingRight, m_CollidingLeft, m_CollidingFront, m_CollidingBack, m_CollidingAny;

    private Transform m_Front;
    private Transform m_Back;
    private Transform m_Left;
    private Transform m_Right;

    //End of Movement Variables

    private float m_VerticalVelocity;
    public float m_JumpForce = 100f;

    private float m_Gravity = 14f;
    private float m_JumpSuccession = 0f;
    private int m_JumpCounter = 0;
    private bool m_IsGrounded;

    private void Awake()
    {
        m_Front = this.gameObject.transform.GetChild(1);
        m_Back = this.gameObject.transform.GetChild(2);
        m_Left = this.gameObject.transform.GetChild(3);
        m_Right = this.gameObject.transform.GetChild(4);
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_CurrentHealth = m_MaxHealth;
        m_OriginalAirTimeToDamage = m_AirTimeToDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentHealth > m_MaxHealth)
        {
            m_CurrentHealth = m_MaxHealth;
        }
        else if (m_CurrentHealth < 0f)
        {
            m_CurrentHealth = 0f;
        }

        if (m_ToDamage)
        {
            m_AirTimeToDamage -= Time.deltaTime;
        }
        Debug.Log(m_AirTimeToDamage);
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
            
            if (Input.GetKey(KeyCode.Space))
            {
                m_VerticalVelocity = m_JumpForce;
                m_JumpCounter = 1;
                m_JumpSuccession = Time.time;
                
            }
            else
            {
                m_VerticalVelocity = 0f;
                m_JumpCounter = 0;
            }
        }
        else if (!m_IsGrounded && Input.GetKeyDown(KeyCode.Space) && m_JumpCounter == 1)
        {
            if (Time.time - m_JumpSuccession < 3f)
            {
                m_VerticalVelocity += m_JumpForce / 2f;
                m_JumpCounter = 0;
                m_JumpSuccession = 0f;
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
        

        //if (!m_IsGrounded)
        //{
        //    if (m_JumpCounter == 1)
        //    {
        //        if (Input.GetKey(KeyCode.Space))
        //        {
        //            m_VerticalVelocity = m_JumpForce;
        //            m_JumpCounter = 0;
        //        }
        //    }
        //}
    }

    private void FixedUpdate()
    {
        // Player movement section
        m_Vertical_Movement = Input.GetAxis("Vertical") /* m_Speed*/;
        m_Horizontal_Movement = Input.GetAxis("Horizontal") /* m_Speed*/;

        Vector3 theVerticalVelocity = new Vector3(0f, m_VerticalVelocity, 0f);
        transform.Translate(theVerticalVelocity * Time.deltaTime);

        Move(m_Horizontal_Movement, m_Vertical_Movement);

        //Player jump physics section
        RaycastHit hit;

        LayerMask mask = LayerMask.GetMask("Player");
        mask = ~mask;

        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, mask);

        if (m_AirTimeToDamage <= 0f && hit.distance > 30f)
        {
            m_Damage = hit.distance;
        }

        if (m_CollidingAny)
        {
            if (hit.distance <= 2f || hit.distance == 2f)
            {
                m_IsGrounded = true;
            }
            else if (hit.distance < 2f)
            {
                float difference;
                Vector3 playerPosition;
                difference = 2f - hit.distance;
                playerPosition = new Vector3(0f, hit.distance + difference, 0f);
                transform.position += playerPosition;
                m_IsGrounded = true;
            }
            else
            {
                m_IsGrounded = false;
            }
        }
        else if (!m_CollidingAny)
        {
            if (hit.distance <= 1f || hit.distance == 1f)
            {
                m_IsGrounded = true;
            }
            else if (hit.distance < 1f)
            {
                float difference;
                Vector3 playerPosition;
                difference = 1f - hit.distance;
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

    private void OnCollisionEnter(Collision collision)
    {
        m_ToDamage = false;
        // Debug.Log("I collided with " + collision.gameObject.name);
        m_CurrentHealth -= m_Damage;
        m_AirTimeToDamage = m_OriginalAirTimeToDamage;

        if (collision.collider.tag == "StoppingCollidable")
        {
            m_Colliding = true;
        }

        if (LayerMask.LayerToName(collision.gameObject.layer) == "Terrain")
        {
           // Debug.Log("We hit the terrain");
        }

        m_CollidingAny = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "StoppingCollidable")
        {
            ContactPoint[] contacts = new ContactPoint[5];

            int numContacts = collision.GetContacts(contacts);
            for (int i = 0; i < numContacts; i++)
            {
                if (Vector3.Distance(contacts[i].point, m_Front.position) < CollisionDistanceCheck)
                {
                    m_CollidingFront = true;
                }
                else if (Vector3.Distance(contacts[i].point, m_Front.position) > CollisionDistanceCheck)
                {
                    m_CollidingFront = false;
                }

                if (Vector3.Distance(contacts[i].point, m_Back.position) < CollisionDistanceCheck)
                {
                    m_CollidingBack = true;
                }
                else if (Vector3.Distance(contacts[i].point, m_Back.position) > CollisionDistanceCheck)
                {
                    m_CollidingBack = false;
                }

                if (Vector3.Distance(contacts[i].point, m_Left.position) < CollisionDistanceCheck)
                {
                    m_CollidingLeft = true;
                }
                else if (Vector3.Distance(contacts[i].point, m_Left.position) > CollisionDistanceCheck)
                {
                    m_CollidingLeft = false;
                }

                if (Vector3.Distance(contacts[i].point, m_Right.position) < CollisionDistanceCheck)
                {
                    m_CollidingRight = true;
                }
                else if (Vector3.Distance(contacts[i].point, m_Right.position) > CollisionDistanceCheck)
                {
                    m_CollidingRight = false;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        m_Damage = 0f;
        m_ToDamage = true;

        if (collision.collider.tag == "StoppingCollidable")
        {

            m_Colliding = false;
            m_CollidingFront = false;
            m_CollidingBack = false;
            m_CollidingLeft = false;
            m_CollidingRight = false;
        }

        m_CollidingAny = false;
    }

    private void Move(float Hori, float Vert)
    {

        // check collision if moving
        if (m_Colliding)
        {
            float clampedVert = Vert;
            float clampedHori = Hori;
            if (m_CollidingFront && m_CollidingBack)
            {
                clampedVert = Mathf.Clamp(Vert, 0f, 0f);
            }
            else if (m_CollidingFront)
            {
                clampedVert = Mathf.Clamp(Vert, -1f, 0f);
            }
            else if (m_CollidingBack)
            {
                clampedVert = Mathf.Clamp(Vert, 0f, 1f);
            }

            if (m_CollidingLeft && m_CollidingRight)
            {
                clampedHori = Mathf.Clamp(Hori, 0f, 0f);
            }
            else if (m_CollidingLeft)
            {
                clampedHori = Mathf.Clamp(Hori, 0f, 1f);
            }
            else if (m_CollidingRight)
            {
                clampedHori = Mathf.Clamp(Hori, -1f, 0f);
            }

            m_Move = new Vector3(clampedHori, 0f, clampedVert) * m_Speed;
            Vector3.Normalize(m_Move);
            m_Move *= Time.deltaTime;

            transform.Translate(m_Move);

        }
        else
        {
            m_Move = new Vector3(Hori, 0f, Vert) * m_Speed;
            Vector3.Normalize(m_Move);
            m_Move *= Time.deltaTime;

            transform.Translate(m_Move);
        }
    }


}
