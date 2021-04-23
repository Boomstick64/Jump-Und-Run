using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // creating a public float that will store the input from the mouse on the Y axis
    public float m_VerticalTurn;

    // the speed of the which will be times by the input
    public float m_SpeedY;

    // the 2 numbers which the rotation will HAVE to stay between and the private Vector 3 that is the rotation
    private float m_MinRotate = 26.311f, m_MaxRotate = 40f;
    private Vector3 rot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if it has set m_VerticalTurn to the input on the Y * the speed
        m_VerticalTurn = m_SpeedY * Input.GetAxis("Mouse Y");

        // times it by delta time then set it
        // m_VerticalTurn *= Time.deltaTime;

        // make the roation variable equal to the euler angles then plus it by the new vector 3 which is the negative vertical turn
        rot = transform.rotation.eulerAngles + new Vector3(-m_VerticalTurn, 0f, 0f);

        // clamp the roation on X axis with the ClampAngle function
        rot.x = ClampAngle(rot.x, m_MinRotate, m_MaxRotate);

        // then set the euler angles to the rotation variable
        transform.eulerAngles = rot;
    }

    public static float ClampAngle(float angle, float from, float to)
    {
        // if the angle is less then 0 subtract that from 360 so that it doesn't get bigger then 360
        if (angle < 26.311f)
        {
            angle = 360f + angle;
        }

        // then check if the angle is bigger then 360 , if it is return the eiter the max of 360f + from provided that from is a negative
        if (angle > 180f)
        {
            return Mathf.Max(angle, 360f + from);
        }

        // else just return the smallest out of the angle and the max
        return Mathf.Min(angle, to);
    }
}
