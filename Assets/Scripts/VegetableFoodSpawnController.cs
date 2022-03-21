using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableFoodSpawnController : MonoBehaviour
{
    GameObject[] vegetableFood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vegetableFood = GameObject.FindGameObjectsWithTag("VegetableFood");
        if (vegetableFood.Length < 45)
        {

            Instantiate((Resources.Load<GameObject>("Prefabs/VegetableFood")), new Vector3(Random.Range(-12f, 12f), Random.Range(-12f, 12f)), new Quaternion());
        }
    }
}
