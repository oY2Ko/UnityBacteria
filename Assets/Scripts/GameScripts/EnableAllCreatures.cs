using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAllCreatures : MonoBehaviour
{
    GameObject[] VegetableEaters;
    GameObject[] Carnivores;
    [SerializeField] public GameObject VegetableEater;
    [SerializeField] public GameObject Carnivore;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < 10; i++)
        {
            if (PhotonNetwork.PlayerList.Length == 1)
            {
                PhotonNetwork.Instantiate(VegetableEater.name, new Vector3(Random.Range(-40f, 40f), Random.Range(-20f, 20f)), new Quaternion());
                VegetableEaters = GameObject.FindGameObjectsWithTag("VegetableEater");
                foreach (var item in VegetableEaters)
                {
                    item.GetComponent<Genome>().SetParents(item, item);
                    item.GetComponent<Genome>().enabled = true;
                    item.GetComponent<Genome>().Mutate();
        }
            }
            if (PhotonNetwork.PlayerList.Length == 2)
            {
                PhotonNetwork.Instantiate(Carnivore.name, new Vector3(Random.Range(-40f, 40f), Random.Range(-20f, 20f)), new Quaternion());
                Carnivores = GameObject.FindGameObjectsWithTag("Carnivore");
                foreach (var item in Carnivores)
                {
                    item.GetComponent<Genome>().SetParents(item, item);
                    item.GetComponent<Genome>().enabled = true;
                    item.GetComponent<Genome>().Mutate();
                }

            }

        }

    }

}
