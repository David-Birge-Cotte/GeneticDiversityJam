using UnityEngine;
using System.Collections;

public class loltemp : MonoBehaviour {

    Rigidbody2D rb;
    public float speed = 10;

	// Use this for initialization
	void Start () {
        rb = GetComponent <Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * Time.fixedDeltaTime * speed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddForce(-Vector2.right * Time.fixedDeltaTime * speed);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            rb.AddForce(Vector2.up * Time.fixedDeltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-Vector2.up * Time.fixedDeltaTime * speed);
        }

    }
}
