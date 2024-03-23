using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameObjectName : MonoBehaviour, IPointerEnterHandler
{
    private SubcultureAtvt getGO;

    void Start()
    {
        if(GameObject.Find("Atvt3_Subculture") != null)
        {
            getGO = GameObject.Find("Atvt3_Subculture").GetComponent<SubcultureAtvt>();
        }
        
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameObject.Find("Atvt3_Subculture") != null)
            getGO.ReceivedGameObject(this.transform.gameObject);//Atvt3 game object
        //Debug.Log(gameObject);
    }    
}
