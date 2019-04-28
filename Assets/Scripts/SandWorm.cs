using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWorm : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public bool isOut = true;
    public float speed;
    private bool freeze;
    private Vector3 pointAPosition;
    private Vector3 pointBPosition;

    // Start is called before the first frame update
    void Start()
    {

        //GetComponent<Rigidbody2D>().drag = 1;

        pointAPosition = new Vector3(pointA.position.x, 0, 0);
        pointBPosition = new Vector3(pointB.position.x, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        pointAPosition = new Vector3(pointA.position.x, transform.position.y, transform.position.z);
        pointBPosition = new Vector3(pointB.position.x, transform.position.y, transform.position.z);

        if (isOut)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointBPosition, speed);
            if (transform.position.x >= (pointBPosition.x))
            {
                isOut = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointAPosition, speed);
            if (transform.position.x <= (pointAPosition.x))
            {
                isOut = true;
            }
        }
    }
}
