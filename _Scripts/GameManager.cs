using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text sliderText;

	// Use this for initialization
	void Start () {
	    if(sliderText == null)
        {
            Debug.LogError("Slider Text missing in script !");
        }
	}

    public void ChangeSpeed(float speed)
    {
        Time.timeScale = speed;
        sliderText.text = "Time Scale : x" + speed.ToString("F01");
    }
}
