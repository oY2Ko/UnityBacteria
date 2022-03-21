using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    Dictionary<string, float> currentCahracteristics;
    // Start is called before the first frame update
    void Start()
    {
        currentCahracteristics = gameObject.GetComponent<ParametersController>().CurrentCharacteristics;
    }

    public void TakeDamage(float damage)
    {
        currentCahracteristics["Health"] -= damage - currentCahracteristics["Defence"];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCahracteristics["Hunger"] >= 1 || currentCahracteristics["Health"] <= 0)
        {
            Destroy(gameObject);
        }
    }
}
