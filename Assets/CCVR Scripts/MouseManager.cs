using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public GameObject selectedObj;
    playerHUDui showMessage;

    // Start is called before the first frame update
    void Start()
    {
        showMessage = FindObjectOfType<playerHUDui>();
        Debug.Log(Screen.height);
        if(selectedObj != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        //Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.height / 2, Screen.width / 2));
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo))
        {
            GameObject hitObj = hitInfo.transform.gameObject;

            SelectedObj(hitObj);
        }
        else
        {
            ClearSelection();
        }

    }
    void SelectedObj(GameObject hitObj)
    {
        if(selectedObj != null)
        {
            if (hitObj == selectedObj)
                return;
            ClearSelection();
        }
        selectedObj = hitObj;
        Renderer[] rs = selectedObj.GetComponents<Renderer>();
        foreach(Renderer r in rs)
        {
            Material m = r.material;
            m.color = Color.green;
            r.material = m;
        }
        showMessage.ShowMessage(hitObj.name);
    }
    void ClearSelection()
    {
        if (selectedObj == null)
            return;

        Renderer[] rs = selectedObj.GetComponents<Renderer>();
        foreach(Renderer r in rs)
        {
            Material m = r.material;
            m.color = Color.white;
            r.material = m;
        }
        selectedObj = null;
        showMessage.ShowMessage("Revert to white");
    }
}
