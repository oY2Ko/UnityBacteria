using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualsController : MonoBehaviour
{
    Dictionary<string, float> characteristics;
    string MaxCharecteristic;
    float MaxValue;
    List<string> Pairs;
    Renderer Renderer;
    // Start is called before the first frame update
    void Start()
    {
        characteristics = gameObject.GetComponent<ParametersController>().CurrentCharacteristics;
        Renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MaxValue = characteristics.Max(s => s.Value);
        Pairs = characteristics.Where(s => s.Value.Equals(MaxValue)).Select(s => s.Key).ToList();
        switch (Pairs[0])
        {
            case "MovementSpeed":
                Renderer.material.color = Color.green;
                break;
            case "Health":
                Renderer.material.color = Color.red;
                break;
            case "Defence":
                Renderer.material.color = Color.blue;
                break;
            case "Attack":
                Renderer.material.color = Color.black;
                break;
            default:
                break;
        }
    }
}
