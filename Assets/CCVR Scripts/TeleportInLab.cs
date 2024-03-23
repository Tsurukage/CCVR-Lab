using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInLab : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform cabinetSpot, incubatorSpot;
    [SerializeField]
    private BoxCollider incubatorDoor;
    // Start is called before the first frame update

    void Start()
    {
        cabinetSpot.localScale = Vector3.zero;
        incubatorSpot.localScale = new Vector3(1f, 0.025f, 1f);
        incubatorDoor = GameObject.Find("/Incubator/Hinge/Door").GetComponent<BoxCollider>();
        incubatorDoor.enabled = false;
    }

    public void ToCabinet()
    {
        player.transform.position = new Vector3(cabinetSpot.position.x, 1.6f, cabinetSpot.position.z);
        cabinetSpot.transform.localScale = Vector3.zero;
        incubatorSpot.transform.localScale = new Vector3(1f, 0.025f, 1f);
        incubatorDoor.enabled = false;
        
    }
    public void ToIncubator()
    {
        player.transform.position = new Vector3(incubatorSpot.position.x, 1.6f, incubatorSpot.position.z);
        incubatorSpot.transform.localScale = Vector3.zero;
        cabinetSpot.transform.localScale = new Vector3(1f, 0.025f, 1f);
        incubatorDoor.enabled = true;
    }
}
