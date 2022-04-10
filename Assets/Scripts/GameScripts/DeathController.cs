using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour, IOnEventCallback
{
    Dictionary<string, float> currentCahracteristics;
    // Start is called before the first frame update
    void Start()
    {
        currentCahracteristics = gameObject.GetComponent<ParametersController>().CurrentCharacteristics;
    }


    //public void TakeDamage(float damage)
    //{
    //    currentCahracteristics["Health"] -= damage - currentCahracteristics["Defence"];
    //}

    // Update is called once per frame
    void Update()
    {
        if (currentCahracteristics["Hunger"] >= 1 || currentCahracteristics["Health"] <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    string[] CustomDataArray = new string[2];
    public void OnEvent(EventData photonEvent)
    {
        CustomDataArray = photonEvent.CustomData.ToString().Split(" ");
        if (CustomDataArray[1] == name)
        {
            currentCahracteristics["Health"] -= float.Parse(CustomDataArray[0]) - currentCahracteristics["Defence"];
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


}
