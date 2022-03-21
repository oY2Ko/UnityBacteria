using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copulate : MonoBehaviour
{
    GameObject partner;
    bool flag = false;
    float startTime;
    MovementController movementController;
    // Start is called before the first frame update
    void Start()
    {
        movementController = gameObject.GetComponent<MovementController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"] <= 0.6f)
        {
            partner = movementController.GetPartner();
            if (collision.gameObject == partner && partner.GetComponent<MovementController>().GetPartner() == gameObject)
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

            var child = Instantiate(partner);
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
