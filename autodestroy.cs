using UnityEngine;
using System.Collections;

public class autodestroy : MonoBehaviour {

    public float DestroyAfterTime = 1;
    private float timeSinceStart = 0;

	void Update ()
    {
        timeSinceStart += Time.deltaTime;

        if(timeSinceStart >= DestroyAfterTime)
        {
            Destroy(this.gameObject);
        }
	}
}