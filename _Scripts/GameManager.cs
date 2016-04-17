using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class GameManager : MonoBehaviour {

    public Text sliderText;
    public Text individualText;

    public float timePressingEsc = 0;

    private float vignettingStart;
    private VignetteAndChromaticAberration vignette;

    void Start () {
	    if(sliderText == null)
        {
            Debug.LogError("Slider Text missing in script !");
        }

        vignette = GameObject.Find("Main Camera").GetComponent<VignetteAndChromaticAberration>();
        vignettingStart = vignette.intensity;

    }

    void Update()
    {
        individualText.text = "Number of Individuals : " + WorldBehaviour.individus.Count.ToString();


        if (Input.GetKey(KeyCode.Escape))
        {
            timePressingEsc += Time.deltaTime;

            if(vignette.intensity < 1)
            {
                vignette.intensity += Time.deltaTime;

                if (vignette.intensity > 1)
                {
                    vignette.intensity = 1;
                }
            }          
        }
        else
        {
            if(timePressingEsc != 0)
            {
                vignette.intensity = vignettingStart;
            }

            timePressingEsc = 0;
        }

        if(timePressingEsc > 1)
        {
            Application.Quit();
        }
    }

    public void ChangeSpeed(float speed)
    {
        Time.timeScale = speed;
        sliderText.text = "Time Scale : x" + speed.ToString("F01");
    }
}