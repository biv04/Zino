using UnityEngine;
using System.Collections;

public class PlatformFall : MonoBehaviour
{
    public Rigidbody platform; private bool hasPlayerExited; float timer = 0f;

    void Start() {
        platform = gameObject.GetComponent<Rigidbody>();
        if (platform == null)
        {
            platform = gameObject.AddComponent<Rigidbody>();
        }
        platform.isKinematic = true;
        platform.useGravity = false;
    }

    void OnCollisionEnter() { hasPlayerExited = false; }

    void OnCollisionExit() { hasPlayerExited = true; }

    void Update() { if (hasPlayerExited == true) { timer += Time.deltaTime; if (timer > 0.5) { platform.isKinematic = false; platform.useGravity = true; } } }
}