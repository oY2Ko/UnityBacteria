using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class Copulate : MonoBehaviour
{
    [SerializeField] GameObject Prefab;
    GameObject partner;
    bool flag = false;
    float startTime;
    MovementController movementController;
    // Start is called before the first frame update
    void Start()
    {
        movementController = gameObject.GetComponent<MovementController>();
        partner = movementController.GetPartner();
        if (gameObject.tag == "VegetableEater")
        {
            Prefab = Resources.Load<GameObject>("Prefabs/VegetableEater");
        }
        if (gameObject.tag == "Carnivore")
        {
            Prefab = Resources.Load<GameObject>("Prefabs/Carnivore");
        }
    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        movementController = gameObject.GetComponent<MovementController>();
        partner = movementController.GetPartner();
        if (collision.gameObject == partner && partner.GetComponent<MovementController>().GetPartner() == gameObject)
        {
            if (gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"] <= 0.6f)
            {
                startTime = Time.time;

            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == partner && gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"] <= 0.6f)
        {
            if (Time.time - startTime >= 2f && flag == false)
            {
                flag = true;
                GiveBirth();
            }
        }
    }

    private GameObject GiveBirth()
    {
        if (gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"] < 0.6f)
        {
            var partnerTransform = partner.GetComponent<Transform>();
            var child = PhotonNetwork.Instantiate(Prefab.name,partnerTransform.position,partnerTransform.rotation);
            gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"] += 0.1f;
            flag = false;
            child.GetComponent<Genome>().SetParents(gameObject, partner);
            child.GetComponent<Genome>().enabled = true;

            return child;
        }
        else
        {
            return null;
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
       

    //}
}
