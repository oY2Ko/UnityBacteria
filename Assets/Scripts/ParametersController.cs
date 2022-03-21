using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParametersController : MonoBehaviour
{
    public Dictionary<string, float> CurrentCharacteristics;

    void Start()
    {
        CurrentCharacteristics = gameObject.GetComponent<Genome>().GetCharacteristics();
        if (gameObject.tag == "VegetableEater")
        {
            CurrentCharacteristics["Hunger"] = 0.6f;
        }
        if (gameObject.tag == "Carnivore")
        {
            CurrentCharacteristics["Hunger"] = 0.6f;
        }
        gameObject.GetComponent<VisualsController>().enabled = true;
        gameObject.GetComponent<MovementController>().enabled = true;
        gameObject.GetComponent<Copulate>().enabled = true;
        gameObject.GetComponent<DeathController>().enabled = true;
    }
    string item;
    private void Update()
    {

        for (int i = 0; i < CurrentCharacteristics.Keys.Count; i++)
        {
            item = CurrentCharacteristics.Keys.ElementAt(i);
            CurrentCharacteristics[item] = Mathf.Clamp(CurrentCharacteristics[item], 0, gameObject.GetComponent<Genome>().GetCharacteristics()[item]);
            //print(CurrentCharacteristics[item] + "    ");

        }
    }

}
