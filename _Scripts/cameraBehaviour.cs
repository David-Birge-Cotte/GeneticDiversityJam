using UnityEngine;
using System.Collections;

public class cameraBehaviour : MonoBehaviour {

    public float speed = 2;
    public float zoomSpeed = 20;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }

        float molAx = Input.GetAxis("Mouse ScrollWheel");
        if(molAx != 0)
        {
            GetComponent<Camera>().orthographicSize -= molAx * zoomSpeed * Time.deltaTime * 10;
        } 

    }
}
