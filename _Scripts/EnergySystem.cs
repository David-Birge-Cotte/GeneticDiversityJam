using UnityEngine;
using System.Collections;

public class EnergySystem : MonoBehaviour 
{
	// PROPERTIES //
	private Agent _agent;
	private Vector2 _agentLastPosition;
	private bool _firstFramePassed = false;

	// MONOBEHAVIOR //
	void Awake()
	{
		try
		{
			_agent = GetComponent<Agent>();
		}
		catch
		{
			Debug.LogError("No Agent Found");
		}
	}

	void Start () 
	{
		StartCoroutine(IdleConsumption(_agent.EnergyConsumptionPerSecond));
	}

	void Update () 
	{
		if (_firstFramePassed && _agent.Position2D != _agentLastPosition)
		{
			_agent.Energy -= Vector2.Distance(_agent.Position2D, _agentLastPosition) * _agent.EnergyConsumedPerUnityUnit;
		}
		_agentLastPosition = _agent.Position2D;
		_firstFramePassed = true;
	}

	// METHODS //
	private IEnumerator IdleConsumption(float energyConsumedPerSecond)
	{
		float t;
		while (true)
		{
			t=1;
			while(t>0)
			{
				t -= Time.deltaTime * energyConsumedPerSecond;
				yield return new WaitForEndOfFrame();
			}
		}
	}
}
