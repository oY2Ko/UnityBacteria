using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegetableFoodSpawnController : MonoBehaviour
{
    [SerializeField] public GameObject VegetableFoodPrefab;
    GameObject[] vegetableFood;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vegetableFood = GameObject.FindGameObjectsWithTag("VegetableFood");
        if (vegetableFood.Length < 50 && PhotonNetwork.PlayerList[0].IsLocal)
        {

            PhotonNetwork.Instantiate(VegetableFoodPrefab.name, new Vector3(Random.Range(-40f, 40f), Random.Range(-20f, 20f)), new Quaternion());
        }
    }
}
