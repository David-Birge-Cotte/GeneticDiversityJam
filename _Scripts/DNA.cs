using UnityEngine;
using System.Collections;

public class DNA
{  
    public byte[] geneticCode;
    public string[] genesList;
    public int nbGenesFirstGeneration = 100;

    public void GenerateNewGeneticCode()
    {
        //string geneticCodeString = "";
        geneticCode = new byte[nbGenesFirstGeneration * 4];
        for(int i = 0; i < geneticCode.Length; i++)
        {
            geneticCode[i] = (byte)(int)Random.Range(0, 2); // vieux Cast de merde
            //geneticCodeString += geneticCode[i].ToString();
        }
        
        genesList = new string[nbGenesFirstGeneration];
        for (int i = 0; i < genesList.Length; i++)
        {
            int k = i * 4;
            string gene = geneticCode[k].ToString();
            gene += geneticCode[k + 1].ToString();
            gene += geneticCode[k + 2].ToString();
            gene += geneticCode[k + 3].ToString();

            genesList[i] = gene;
        }
    }

    public void Mutate()
    {
        int nbRng = Random.Range(0, 10);
        for(int i = 0; i < nbRng; i++)
        {
            int rng = Random.Range(0, geneticCode.Length);
            geneticCode[rng] = (byte)(int)Random.Range(0, 2);
        }

        genesList = new string[nbGenesFirstGeneration];
        for (int i = 0; i < genesList.Length; i++)
        {
            int k = i * 4;
            string gene = geneticCode[k].ToString();
            gene += geneticCode[k + 1].ToString();
            gene += geneticCode[k + 2].ToString();
            gene += geneticCode[k + 3].ToString();

            genesList[i] = gene;
        }
    }
}