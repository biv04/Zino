using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spash : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(spashScreen());
	}
	
	// Update is called once per frame
	IEnumerator spashScreen()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel(1);
    }
}
