using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rise_platform : MonoBehaviour
{
    Rigidbody2D rb;
    private float originalYPos;
    public bool isUP = false;
    public bool isOn = false;
    public float speed;

    public Transform pointA;
    public Transform pointB;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;


    // Use this for initialization
    void Start()
    {
        originalYPos = transform.position.y;

        pointAPosition = new Vector3(0, pointA.position.y, 0);
        pointBPosition = new Vector3(0, pointB.position.y, 0);
    }


    private void Update()
    {
        pointAPosition = new Vector3(pointA.position.x, transform.position.y, transform.position.z);
        pointBPosition = new Vector3(pointB.position.x, transform.position.y, transform.position.z);

        if (isUP && isOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointAPosition, speed);
            if (transform.position.y <= (pointAPosition.y))
            {
                isUP = false;
            }
        }

        else if ((!isUP && isOn))
        {
            transform.position = Vector3.MoveTowards(transform.position, pointBPosition, speed);
            if (transform.position.y >= (pointBPosition.y))
            {
                isUP = true;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D col)
    {

    

        if (col.gameObject.name.Equals("player") && !isUP)
        {

            isOn = true;

        }
        if (col.gameObject.name.Equals("player") && isUP)
        {

            isOn = true;
        }

        else
        {
            isOn = false;
        }

    }





}
