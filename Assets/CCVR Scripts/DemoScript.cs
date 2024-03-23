using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{ 
    private playerHUDui showMessage;
    public Sprite onSp, offSpt;
    public Button testBtn;
    private bool isOn;
    public GameObject cube;

    bool isRed;
    #region

    void Start()
    {
        showMessage = GameObject.FindObjectOfType<playerHUDui>();
        testBtn.GetComponent<Image>().sprite = offSpt;
        cube.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isRed = !isRed;
        if (isRed)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }

        //showMessage.ShowMessage(this.name);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isRed = !isRed;
        if (isRed)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        //Destroy(this);
    }
    public void OnMouseUp()
    {
        //isOn = !isOn;
        if (!cube.activeSelf)
        {
            cube.SetActive(true);
            testBtn.GetComponent<Image>().sprite = offSpt;
        }
        else
        {
            cube.SetActive(false);
            testBtn.GetComponent<Image>().sprite = onSp;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        showMessage.ShowMessage(this.name);
    }

    #endregion*/

}

