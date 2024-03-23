using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveUsedApprts : MonoBehaviour
{
    [SerializeField]
    private Transform trashcan;
    [SerializeField]
    private StatusEnd state;

    private static int boolS;

    [SerializeField]
    private GameObject emptyParent;
    private bool done;

    private playerHUDui showMessage;
    private WorldSpaceMessageBox popMessage;

    private ActivityFour switchSpray;

    // Start is called before the first frame update
    void Start()
    {
        trashcan = transform.Find("/Environment/trashcan/Trashcan");
        showMessage = FindObjectOfType<playerHUDui>();
        popMessage = FindObjectOfType<WorldSpaceMessageBox>();
        switchSpray = FindObjectOfType<ActivityFour>();
        emptyParent = GameObject.Find("SubcultureDone");
        done = false;
        PlayerPrefs.SetInt("AllClear", 0);
    }

    void Update()
    {
        if(emptyParent.transform.childCount == 0)
        {
            if (!done)
            {
                emptyParent.SetActive(false);
                StartCoroutine(PopBox());
                //switchSpray.SwitchSprayBottle(); //Switch spray bottle to pick up in ActivityFour.cs
                done = true;
                PlayerPrefs.SetInt("AllClear", 1); //For LastCorrectChoice.cs
            }
        }
    }
    public void ClearTable()
    {
        switch (state)
        {
            case StatusEnd.usedFlaskA:
                this.transform.parent = trashcan;
                this.transform.localPosition = trashcan.localPosition;
                popMessage.MessagePopUp("The used flask will be discarded.\nIt should be sterilized before disposal by placing it in unsealed autoclavable bags and autoclaved.");
                return;
            case StatusEnd.usedBeaker:
                this.transform.parent = trashcan;
                this.transform.localPosition = trashcan.localPosition;
                popMessage.MessagePopUp("Cell culture waste may contain cells and other potentially biohazardous materials.\nIt must be sterilized by sterilizing agents such as hypochlorite or bleach before disposal.");
                return;
            case StatusEnd.usedMediaBottle:
                this.transform.parent = trashcan;
                this.transform.localPosition = trashcan.localPosition;
                popMessage.MessagePopUp("The lid of the media bottle should be closed tightly.\nCulture media should be stored at 4°C immediately after use as they normally contain heat labile components such as growth factors and amino acids.");
                return;
            case StatusEnd.usedScrapper:
                this.transform.parent = trashcan;
                this.transform.localPosition = trashcan.localPosition;
                popMessage.MessagePopUp("Used cell scrapper should be disposed.\nIt should be sterilized before disposal by placing it in unsealed autoclavable bags and autoclaved.");
                return;
            /*case StatusEnd.tray:
                this.transform.parent = trashcan;
                this.transform.localPosition = trashcan.localPosition;
                popMessage.MessagePopUp("The tray should be disinfected properly prior to re-use.");
                break;*/
        }
        /*boolS = PlayerPrefs.GetInt("allTransfer");
        if (boolS == 1)
        {
            
        }
        else
        {
            showMessage.ShowMessage("Please store the T25 flasks into the incubator first.");
        }*/
    }
    IEnumerator PopBox()
    {
        yield return new WaitForSeconds(3f);
        popMessage.MessagePopUp("Phew, the biosafety cabinet finally looks clean now. However, before the biosafety cabinet is switched off, there is one important step that MUST be performed.\nCheck the service cart and click on the item to perform this critical step.");
    }
}
public enum StatusEnd
{
    usedFlaskA,
    usedMediaBottle,
    usedBeaker,
    usedScrapper,
    //tray
}
