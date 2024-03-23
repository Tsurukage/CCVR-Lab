using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SimpleQuiz : MonoBehaviour, IPointerClickHandler
{
    public string question;
    public Text quesText;
    public GameObject objName;

    playerHUDui showMessage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (objName.name == "SprayBottle")
        {
            //Debug.Log("Correct!");
            showMessage.ShowMessage("Correct");
        }
        else
        {
            //Debug.Log("Wrong");
            showMessage.ShowMessage("Wrong");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        quesText.text = question;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
