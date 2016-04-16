using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Agent))]
public class HuntBehavior : MonoBehaviour 
{
	public enum AgentState
	{
		HuntingFood,
		HuntingAgent,
		LookingForFood
	}

	private Agent _agent;
	private AgentState _huntState;
	float _scanRange;
	private GameObject target;

	// Use this for initialization
	void Start () 
	{
		_agent = GetComponent<Agent>();
		_huntState = AgentState.LookingForFood;
		_scanRange = _agent.VisionRange;
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch (_huntState)
		{
			case AgentState.HuntingFood:
				HuntFood();
				break;

			case AgentState.HuntingAgent:
				break;

			case AgentState.LookingForFood:
				ScanEnv();
				break;

			default:
				break;
		}
	}

	private void ScanEnv()
	{
		GameObject target = null;
		float scanRegion;
		scanRegion = _scanRange;
		foreach(GameObject i in WorldBehaviour.foodObjects)
		{
			float dist = Vector2.Distance(_agent.Position2D, new Vector2(i.transform.position.x, transform.position.y));
			if (dist < scanRegion)
			{
				scanRegion = dist;
				target = i;
			}
		}
		if (target != null)
		{
			_huntState = AgentState.LookingForFood;
		} else
		{
			this.target = target;
		}
	}

	private void HuntFood()
	{
		//TODO move to target
	}
}
