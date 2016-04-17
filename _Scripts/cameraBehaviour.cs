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

	private Vector3 Pivot;
	private bool Dragging = false;

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
		Vector3 MouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 cameraStartPosition = new Vector3();
		//transform.Translate((Input.GetAxis("Horizontal") * Vector3.right + Input.GetAxis("Vertical") * Vector3.up) * speed * Time.deltaTime / Time.timeScale);
		OrthoSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime * 10 / Time.timeScale * ZoomSensibility.Evaluate((OrthoSize-MinOrthoSize)/ (MaxOrthoSize-MinOrthoSize));
		if (Input.GetMouseButtonDown(1))
		{
			Pivot = MouseWorldPosition;
			cameraStartPosition = transform.position;
			Dragging = true;
			//Debug.Log("Pivot Position : " + Pivot);
		}
		if (Input.GetMouseButtonUp(1))
		{
			Dragging = false;
		}
		if (Dragging == true && Input.GetMouseButton(1))
		{
			Vector2 temporare = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y),
											 new Vector2(cameraStartPosition.x, cameraStartPosition.y) +
											 new Vector2(Pivot.x, Pivot.y) -
											 new Vector2 (MouseWorldPosition.x, MouseWorldPosition.y), 0.5f);
			//Debug.Log("Target : " + new Vector3(temporare.x, temporare.y, transform.position.z));
			transform.position = new Vector3(temporare.x, temporare.y, transform.position.z);

		}
	}
}
