using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Agent : MonoBehaviour 
{
	// PROPERTIES //
	public Vector2 Position2D
	{
		get
		{
			return new Vector2(transform.position.x, transform.position.y);
		}
		set
		{
			transform.position = new Vector3(value.x, value.y, transform.position.z);
		}
	}

	private float _energy;
	public float Energy
	{
		get
		{
			return _energy;
		}
		set
		{
			_energy = value;
			if (_energy < 0)
			{
				//TODO kill Agent
			}
		}
	}

	public float EnergyConsumptionPerSecond;
	public float EnergyConsumedPerUnityUnit;

	static float SizeEnergyRatio = 1;

	// Monobehavior //
	void Awake()
	{
		EnergyConsumedPerUnityUnit = GetComponent<SpriteRenderer>().bounds.size.x * SizeEnergyRatio;
		EnergyConsumptionPerSecond = GetComponent<SpriteRenderer>().bounds.size.x * SizeEnergyRatio;
	}

}
