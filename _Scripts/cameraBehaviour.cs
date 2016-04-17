using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class cameraBehaviour : MonoBehaviour {

	public AnimationCurve ZoomSensibility;

    public float speed = 2;
    public float zoomSpeed = 20;
	private Camera _camera;
	public float MaxOrthoSize;
	public float MinOrthoSize;
	private BlurOptimized _blurScript;

	private float OrthoSize
	{
		get
		{
			return _camera.orthographicSize;
		}
		set
		{
			if (value != _camera.orthographicSize)
			{
				_blurScript.downsample = (int)Mathf.Lerp(_blurScript.downsample, 1, 0.5f);
				_blurScript.blurSize = Mathf.Lerp(_blurScript.blurSize, 2, 0.5f);
				_blurScript.blurIterations = (int)Mathf.Lerp(_blurScript.blurIterations, 2, 0.5f);
			}
			else
			{
				_blurScript.downsample = (int)Mathf.Lerp(_blurScript.downsample, 0, 0.5f);
				_blurScript.blurSize = Mathf.Lerp(_blurScript.blurSize, 0, 0.5f);
				_blurScript.blurIterations = (int)Mathf.Lerp(_blurScript.blurIterations, 1, 0.5f);
			}
			_camera.orthographicSize = Mathf.Clamp(value, MinOrthoSize, MaxOrthoSize);
		}
	}

	void Awake()
	{
		_camera = GetComponent<Camera>();
		_blurScript = GetComponent<BlurOptimized>();
	}

    void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		ReadInputs();
    }

	void ReadInputs()
	{
		transform.Translate((Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up) * speed * Time.deltaTime / Time.timeScale);
		OrthoSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime * 10 / Time.timeScale * ZoomSensibility.Evaluate((OrthoSize-MinOrthoSize)/ (MaxOrthoSize-MinOrthoSize));
	}
}
