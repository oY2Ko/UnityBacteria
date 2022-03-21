using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Genome : MonoBehaviour
{

    private Dictionary<string, float> Characteristics = new Dictionary<string, float>();
    private float MutationRange = 0.5f;
    private float MutationStep;
    GameObject firstParent, secondParent;
    public Dictionary<string, float> GetCharacteristics()
    {
        return Characteristics;
    }

    public void Mutate()
    {
        for (int i = 0; i < Characteristics.Keys.Count; i++)
        {
            MutationStep = Random.Range(-MutationRange, MutationRange);
            var item = Characteristics.Keys.ElementAt(i);
            Characteristics[item] += MutationStep;
            MutationRange -= MutationStep;
            Mathf.Clamp(Characteristics[item], 0, Mathf.Infinity);
        }
        //foreach (var key in Characteristics.Keys)
        //{
        //    MutationStep = Random.Range(-MutationRange, MutationRange);
        //    Characteristics[key] += MutationStep;
        //    MutationRange -= MutationStep;
        //}
        gameObject.GetComponent<ParametersController>().enabled = true;

    }

    public void SetParents(GameObject first, GameObject second)
    {
        firstParent = first;
        secondParent = second;
    }
    public void SetGenome()
    {
        var firstParentCharacteristics = firstParent.GetComponent<Genome>().Characteristics;
        var secondParentCharacteristics = secondParent.GetComponent<Genome>().Characteristics;
        Characteristics["Attack"] = (firstParentCharacteristics["Attack"] + secondParentCharacteristics["Attack"]) / 2;
        Characteristics["Defence"] = (firstParentCharacteristics["Defence"] + secondParentCharacteristics["Defence"]) / 2;
        Characteristics["MovementSpeed"] = (firstParentCharacteristics["MovementSpeed"] + secondParentCharacteristics["MovementSpeed"]) / 2;
        Characteristics["Health"] = (firstParentCharacteristics["Health"] + secondParentCharacteristics["Health"]) / 2;
        Mutate();
    }

    private void Start()
    {
        
        SetGenome();
    }


    private void Awake()
    {
        switch (tag)
        {
            case "VegetableEater":
                Characteristics.Add("MovementSpeed", 1f);
                Characteristics.Add("Health", 1f);
                Characteristics.Add("Defence", 0f);
                Characteristics.Add("Attack", 0f);
                Characteristics.Add("Hunger", 0f);
                break;
            case "Carnivore":
                Characteristics.Add("MovementSpeed", 2.5f);
                Characteristics.Add("Health", 0.7f);
                Characteristics.Add("Defence", 0f);
                Characteristics.Add("Attack", 0.6f);
                Characteristics.Add("Hunger", 0f);
                break;
            default:
                break;

        }
    }
}
