using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour {
    Rigidbody2D rb;
	
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            Invoke("DropPlatform", 0.2f);
            Destroy(gameObject, 1F);
        }
    }
    void DropPlatform()
    {
        rb.isKinematic = false;
    }
}
