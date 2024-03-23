using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuizTest : MonoBehaviour// IPointerClickHandler
{

    //public GameObject objName;

    void Start()
    {
        //objName = gameObject.name;
        //RayCastTest();
    }

    /*public void OnPointerClick(PointerEventData eventData)
    {
        if (objName == "Cube")
        {
            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Wrong");
        }
    }*/

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.height / 2, Screen.width / 2));
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject hitObj = hitInfo.transform.gameObject;

                if(hitInfo.transform != null)
                {
                    Debug.Log(hitObj.name);
                }
                
            }
        }
        
    }
}
