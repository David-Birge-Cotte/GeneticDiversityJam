using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
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

    [SerializeField]
	private float _energy = 10;
	public float Energy
	{
		get
		{
			return _energy;
		}
		set
		{
			_energy = value;
			if (_energy <= 0)
			{
                foreach(GameObject limb in limbs)
                {
                    Destroy(limb);
                }

                GameObject part = Resources.Load<GameObject>("Death");
                Instantiate(part, transform.position, Quaternion.identity);
                WorldBehaviour.individus.Remove(this);

                Destroy(this.gameObject);
            }
		}
	}

	public float EnergyConsumptionPerSecond;
	public float EnergyConsumedPerUnityUnit;

	static float SizeEnergyRatio = 1;
	public DNA dna;

	public float VisionRange = 100;
    public GameObject closeFood;

    public GameObject[] limbs;
    public GameObject limbPrefab;

    public float findFoodTime;

    public bool hasMutated = false;

	// Monobehavior //
	void Start()
	{
        EnergyConsumedPerUnityUnit = 0;
		EnergyConsumptionPerSecond = 0.05f;

        if(hasMutated == false)
        {
            dna = new DNA();
            dna.GenerateNewGeneticCode();
            CompileDNA();
        }
   
    }

    void FixedUpdate()
    {
        if(closeFood == null || findFoodTime > 10) //si il n'y a plus de bouffe ou si il est vraiment trop con pour y aller
        {
            ScanEnv();
            findFoodTime = 0;
        }

        findFoodTime += Time.fixedDeltaTime;
    }

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<Food>() != null)
		{
			Energy += col.gameObject.GetComponent<Food>().Amount;
            WorldBehaviour.foodObjects.Remove(col.gameObject);
			Destroy(col.gameObject);

            GameObject offspring = Instantiate(this.gameObject, new Vector3(transform.position.x, transform.position.y+2), Quaternion.identity) as GameObject;
            offspring.GetComponent<Agent>().dna = dna;
            offspring.GetComponent<Agent>().dna.Mutate();
            offspring.GetComponent<Agent>().hasMutated = true;
            offspring.GetComponent<Agent>().CompileDNA();
            offspring.GetComponent<Agent>().Energy = 3;

            WorldBehaviour.individus.Add(offspring.GetComponent<Agent>());
        }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "DeathWall")
        {
            Energy = 0;
        }
    }

    void CompileDNA()
    {
        List<int> limbPos = FindlimbBegin();
        if(limbPos.Count != 0)
        {
            limbs = new GameObject[limbPos.Count - 1];
        }
        
        for(int i = 0; i < limbPos.Count; i++)
        {
            if(i != limbPos.Count - 1)
            {
                Createlimb(i, limbPos[i], limbPos[i + 1]);
            }      
        }
    }

    /// <summary>
    /// Finds the position in the Gene sequence of the limb Begin (0000 or 1111)
    /// </summary>
    /// <returns></returns>
    List<int> FindlimbBegin()
    {
        List<int> limbPosInGeneList = new List<int>();
        for(int i = 0; i < dna.genesList.Length; i++)
        {
            if(dna.genesList[i] == "0000" || dna.genesList[i] == "1111")
            {
                limbPosInGeneList.Add(i);
            }
        }

        return limbPosInGeneList;
    }

    void Createlimb(int i, int beginPos, int endPos)
    {
        limbs[i] = Instantiate<GameObject>(limbPrefab); 
        limbs[i].transform.position = new Vector3(transform.position.x + i + 1, Position2D.y, transform.position.z);
        limbs[i].GetComponent<Limb>()._myAgent = this;

        if(i > 0)
        {
            limbs[i].GetComponent<HingeJoint2D>().connectedBody = limbs[i - 1].GetComponent<Rigidbody2D>();
        }
        else
        {
            limbs[i].GetComponent<HingeJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
        }

        int nbGenes = endPos - beginPos;
        for(int k = 0; k < nbGenes; k++)
        {
            switch (dna.genesList[i+k])
            {
                case "0001":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.R_F;
                    break;
                case "0010":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.L_F;
                    break;
                case "0011":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.F_F;
                    break;
                case "0100":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.R_R;
                    break;
                case "0101":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.L_R;
                    break;
                case "0110":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.F_R;
                    break;
                case "0111":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.R_L;
                    break;
                case "1000":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.L_L;
                    break;
                case "1001":
                    limbs[i].GetComponent<Limb>().forceType = ForceType.F_L;
                    break;
                case "1010":
                    limbs[i].GetComponent<Limb>().forcePower += 1;
                    break;
                case "1011":
                    limbs[i].GetComponent<Limb>().forcePower -= 1;
                    break;
                case "1100":
                    limbs[i].GetComponent<Limb>().forcePower += 1;
                    break;
                case "1101":
                    limbs[i].GetComponent<Limb>().forcePower -= 1;
                    break;
                case "1110":
                    limbs[i].GetComponent<Limb>().forcePower += 1;
                    break;
                default:
                    break;
            }
        }    
    }

    private void ScanEnv()
    {
        GameObject target = null;
        float scanRegion;
        scanRegion = VisionRange;
        foreach (GameObject i in WorldBehaviour.foodObjects)
        {
            float dist = Vector2.Distance(Position2D, new Vector2(i.transform.position.x, transform.position.y));
            if (dist < scanRegion)
            {
                scanRegion = dist;
                target = i;
            }
        }
        closeFood = target;
    }

    public void Mutate()
    {
        foreach (GameObject limb in limbs)
        {
            Destroy(limb);
        }

        dna.Mutate();
        CompileDNA();
    }
}