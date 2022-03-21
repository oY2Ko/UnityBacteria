using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Vector3 ClosestFoodDirection;
    Vector3 ClosestPartnerDirection;
    Dictionary<string, float> characteristics;
    float movementSpeed;
    GameObject[] VegetableFood;
    GameObject[] VegetableEaters;
    float ClosestFoodDistance;
    float ClosestPartnerDistance;
    float angle = 0;
    GameObject closestPartner;
    string FoodTag;
    string PartnerTag;
    // Start is called before the first frame update
    void Start()
    {
        characteristics = gameObject.GetComponent<ParametersController>().CurrentCharacteristics;
        movementSpeed = characteristics["MovementSpeed"];
        switch (gameObject.tag)
        {
            case "VegetableEater":
                FoodTag = "VegetableFood";
                PartnerTag = "VegetableEater";
                break;
            case "Carnivore":
                FoodTag = "VegetableEater";
                PartnerTag = "Carnivore";
                break;
            default:
                break;
        }
    }

    private GameObject GetClosestFood()
    {
        Vector3 direction;
        VegetableFood = GameObject.FindGameObjectsWithTag(FoodTag);
        ClosestFoodDistance = Mathf.Infinity;
        float sqrMagnitude;
        GameObject ClosestFood = null;
        foreach (var food in VegetableFood)
        {
            direction = (food.transform.position - transform.position);
            sqrMagnitude = direction.sqrMagnitude;
            if (ClosestFoodDistance > sqrMagnitude)
            {
                ClosestFoodDirection = direction;
                ClosestFoodDistance = sqrMagnitude;
                ClosestFood = food;
            }
        }
        return ClosestFood;
    }

    private GameObject GetClosestPartner()
    {
        Vector3 direction;
        VegetableEaters = GameObject.FindGameObjectsWithTag(PartnerTag);
        ClosestPartnerDistance = Mathf.Infinity;
        float sqrMagnitude;
        GameObject ClosestEater = null;
        foreach (var eater in VegetableEaters)
        {
            if (eater == gameObject)
            {
                continue;
            }
            direction = (eater.transform.position - transform.position);
            sqrMagnitude = direction.sqrMagnitude;
            if (ClosestPartnerDistance > sqrMagnitude)
            {
                ClosestPartnerDirection = direction;
                ClosestPartnerDistance = sqrMagnitude;
                ClosestEater = eater;
            }
        }
        return ClosestEater;
    }
    //float ClampBetweenZeroOn(float value)
    //{
    //    if (value > 1)
    //    {
    //        value = 1;
    //    }
    //    else
    //    {
    //        if (value < 0)
    //        {
    //            value = 0;
    //        }
    //    }
    //    return value;
    //}
    private void OnCollisionEnter2D(Collision2D foodCollision) // Перенести в отдельный скрипт 
    {
        
        if (foodCollision.gameObject.tag == FoodTag)
        {
            gameObject.GetComponent<Genome>().GetCharacteristics()["Hunger"] -= 0.1f;
            gameObject.GetComponent<Genome>().GetCharacteristics()["Hunger"] = Mathf.Clamp(gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"], 0, 1);
            if (foodCollision.gameObject.tag == "VegetableFood" && FoodTag == "VegetableFood")
            {
                Destroy(foodCollision.gameObject);
            }
            else
            {
                foodCollision.gameObject.GetComponent<DeathController>().TakeDamage(characteristics["Attack"]);
                print(foodCollision.gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Health"]);
                
                StartCoroutine(Waiter());
                
            }
        }

    }
    IEnumerator Waiter()
    {
        var temp = characteristics["MovementSpeed"];
        movementSpeed = 0;

        yield return new WaitForSecondsRealtime(0.2f); 

        movementSpeed = temp;
    }
    public Vector3 MovementVector;
    private void Move()
    {
        var a = GetClosestFood();
        closestPartner = GetClosestPartner();
        if (ClosestFoodDistance <= ClosestPartnerDistance || gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"] > 0.6f)
        {
            angle = Mathf.Atan2(ClosestFoodDirection.y, ClosestFoodDirection.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(ClosestPartnerDirection.y, ClosestPartnerDirection.x) * Mathf.Rad2Deg;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90f), 0.1f);

        MovementVector = movementSpeed * Time.deltaTime * transform.InverseTransformDirection(transform.up);
        gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"] += MovementVector.magnitude * 0.03f;
        transform.Translate(MovementVector);
        //if (gameObject.GetComponent<ParametersController>().CurrentCharacteristics["Hunger"] >= 0.6f)
        //{
        //    gameObject.GetComponent<Renderer>().material.color = Color.gray;
        //}
        //else
        //{
        //    gameObject.GetComponent<Renderer>().material.color = Color.red;

        //}
    }

    public GameObject GetPartner()
    {
        return closestPartner;
    }

    // Update is called once per frame
    void Update()
    {

        Move();
    }
}
