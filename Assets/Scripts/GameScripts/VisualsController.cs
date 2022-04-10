using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualsController : MonoBehaviour, IPunObservable
{
    Dictionary<string, float> characteristics;
    float MaxValue;
    List<string> Pairs;
    Renderer Renderer;
    string ColorToSend;
    string RecievedColor;
    private PhotonView photonView;
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ColorToSend);
        }
        else
        {
            RecievedColor = (string)stream.ReceiveNext();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        characteristics = gameObject.GetComponent<ParametersController>().CurrentCharacteristics;
        Renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            print($"{ gameObject.tag} ::  {RecievedColor}");
            switch (RecievedColor)
            {
                case "Green":
                    Renderer.material.color = Color.green;
                    break;
                case "Red":
                    Renderer.material.color = Color.red;
                    break;
                case "Blue":
                    Renderer.material.color = Color.blue;
                    break;
                case "Black":
                    Renderer.material.color = Color.black;
                    break;
                default:
                    print("DEFAULT");
                    break;
            }
            return;
        }

        MaxValue = characteristics.Max(s => s.Value);
        Pairs = characteristics.Where(s => s.Value.Equals(MaxValue)).Select(s => s.Key).ToList();
        switch (Pairs[0])
        {
            case "MovementSpeed":
                Renderer.material.color = Color.green;
                ColorToSend = "Green";
                break;
            case "Health":
                Renderer.material.color = Color.red;
                ColorToSend = "Red";
                break;
            case "Defence":
                Renderer.material.color = Color.blue;
                ColorToSend = "Blue";
                break;
            case "Attack":
                Renderer.material.color = Color.black;
                ColorToSend = "Black";
                break;
            default:
                break;
        }
    }
}
