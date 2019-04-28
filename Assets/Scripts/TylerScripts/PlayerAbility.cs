using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

//TODO: Circle damage, instaheal?
//TODO: A way to pass colors in would be nice

public abstract class PlayerAbility : MonoBehaviour {
    
    public float countDown;
    public bool isCountingDown;
    public bool isHoldAbility;
    public bool isUsing;
   
    public abstract void useAbility(KeyCode buttonPressed);

    public abstract void finishAbility();

    public IEnumerator startCountdown(float time, float countdownTime) {

        isCountingDown = true;
        isUsing = false;
        var timeToGet = time + countdownTime;

        while (Time.time < timeToGet) {
            yield return 0;
        }
        
        isCountingDown = false;
    }
    
    public IEnumerator startUse(float time, float length) {

        isCountingDown = true;
        isUsing = true;
        var timeToGet = time + length;

        while (Time.time < timeToGet) {
            yield return 0;
        }
        
        finishAbility();
    }
    
}


public class Dash : PlayerAbility {
    private Transform playerTransform;
    private Vector2 playerPos;
    private LineRenderer lineRenderer;

    private IEnumerator coroutine;

    private bool startedToDash;

    public void Start() {
        playerTransform = GetComponent<Transform>();
        isHoldAbility = true;
        countDown = 6f;
        startedToDash = false;
        if (!lineRenderer) {
            gameObject.AddComponent<LineRenderer>();
            lineRenderer = GetComponent<LineRenderer>();
        }


        lineRenderer.material = new Material(Shader.Find("Unlit/Texture"));
        lineRenderer.SetColors(Color.blue, Color.cyan);
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.numCapVertices = 20;

    }
    
    
    
    public override void useAbility(KeyCode buttonPressed) {
        
        var temp = buttonPressed;

     
        lineRenderer.enabled = true;

        if (Input.GetKey(temp)) {
            
            Cursor.visible = true;
            playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);

            Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            //lookPos -= playerPos;


            var points = new Vector3[2];
            points[0] = playerPos;
            points[1] = lookPos;
            
            lineRenderer.SetPositions(points);
        }

    }





    public override void finishAbility() {
        
        Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        lineRenderer.enabled = false;
        startedToDash = true;


        bool facingRight = GetComponent<PlayerMovement>().facingRight;

        if (facingRight && lookPos.x < transform.position.x) {
            //GetComponent<PlayerMovement>().facingRight = false;
            GetComponent<PlayerMovement>().Flip();
        }
        
        if (!facingRight && lookPos.x > transform.position.x) {
            //GetComponent<PlayerMovement>().facingRight = true;
            GetComponent<PlayerMovement>().Flip();
        }
        

        var body = GetComponent<Rigidbody2D>();
        
        //body.AddForce(vector);
        coroutine = doMovement(lookPos, body);
        StartCoroutine(coroutine);
        
        coroutine = startCountdown(Time.time, countDown);        
        StartCoroutine(coroutine);

    }


    private IEnumerator doMovement(Vector2 lookPos, Rigidbody2D body) {

        
        //TODO: Maybe we can limit the magnitude of the vector? Having trouble calculating it.


        GetComponent<Animator>().SetBool("isDashing", true);

        GetComponent<Rigidbody2D>().gravityScale = 0;
        
        float speed = .1f;


        while (Vector2.Distance(playerTransform.position, lookPos) > 1) {
            
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, lookPos, speed);
            //Debug.Log("Runnin");
            yield return 0;


        }

        playerTransform.position = lookPos;
        GetComponent<Rigidbody2D>().gravityScale = 1;

        GetComponent<Animator>().SetBool("isDashing", false);


    }
    
    
    void OnCollisionEnter2D(Collision2D collision){

        if (!startedToDash) {
            return;
        }
        
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy")) {
            Debug.Log("Hit while dashing!");
            StopCoroutine("doMovement");
            GetComponent<Rigidbody2D>().gravityScale = 1;

            GetComponent<Animator>().SetBool("isDashing", false);
        }
    
    
    }
    
}

public class Protect : PlayerAbility {
    
    
    private IEnumerator coroutine;
    public float length;

    
    public void Start() {
        length = 2f;
        countDown = 10f;
        isCountingDown = false;
        isHoldAbility = false;
    }

    public override void useAbility(KeyCode buttonPressed) {

        GetComponent<SpriteRenderer>().color = new Color(55/255.0f, 237/255.0f, 57/255.0f, 100/255.0f);
        
        GetComponent<PlayerMovement>().isProtected = true;
        var time = Time.time;
        coroutine = startUse(time, length);        
        StartCoroutine(coroutine);

    }


    public override void finishAbility() {

        var time = Time.time;
        GetComponent<SpriteRenderer>().color = Color.white;//new Color(255, 255, 255, 255);
        coroutine = startCountdown(time, countDown);        
        StartCoroutine(coroutine);
        GetComponent<PlayerMovement>().isProtected = false;

    }


    

    


}

public class powerAttack : PlayerAbility {


    public void Start() {

        countDown = 5f;
        isCountingDown = false;
        isHoldAbility = false;


    }


    public override void useAbility(KeyCode buttonPressed) {

        GetComponent<PlayerMovement>().damage *= 2;
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 0, 0.75f, 1);
        var coroutine = startUse(Time.time, 4);
        StartCoroutine(coroutine);

    }

    public override void finishAbility() {

        var coroutine = startCountdown(Time.time, countDown);
        StartCoroutine(coroutine);
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<PlayerMovement>().damage /= 2;


    }
}
