using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    List<GameObject> food;
    List<GameObject> Carnivores;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider other)
    {
        if (other.gameObject.tag == "VegetableFood")
        {
            food.Add(other.gameObject);
        }
        else
        {
            if (other.gameObject.tag == "Carnivore")
            {
                Carnivores.Add(other.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
