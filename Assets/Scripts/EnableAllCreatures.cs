using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAllCreatures : MonoBehaviour
{
    GameObject[] VegetableEaters;
    GameObject[] Carnivores;
    // Start is called before the first frame update
    void Start()
    {
        VegetableEaters = GameObject.FindGameObjectsWithTag("VegetableEater");
        foreach (var item in VegetableEaters)
        {
            item.GetComponent<Genome>().SetParents(item, item);
            item.GetComponent<Genome>().enabled = true;
            item.GetComponent<Genome>().Mutate();
        }
        Carnivores = GameObject.FindGameObjectsWithTag("Carnivore");
        foreach (var item in Carnivores)
        {
            item.GetComponent<Genome>().SetParents(item, item);
            item.GetComponent<Genome>().enabled = true;
            item.GetComponent<Genome>().Mutate();
        }
    }

}
