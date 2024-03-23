using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessyWorkStation : MonoBehaviour
{
    public GameObject messyWorkstationGO, trashesObj, returnedPlc;
    public objStatus ObjectDestination;
    
    [SerializeField]
    private string dLiquid, trashes, rotObject;
    public GameObject trashcan;

    private static int numOfTask = 12;
    private static int completedTask = 0;

    [SerializeField]
    private Text taskText;

    private playerHUDui showMessage;
    private CabinetSwitchManager activeBtn;
    private AudioManager playSFX;
    private WorldSpaceMessageBox popMsg;

    private bool taskDone;

    //public string contMsj;

    // Start is called before the first frame update
    void Start()
    {
        completedTask = 0;
        taskDone = false;
        messyWorkstationGO = GameObject.Find("MessyWorkStation");
        if (messyWorkstationGO != null)
        {
            messyWorkstationGO.transform.localScale = Vector3.zero;
        }
        showMessage = FindObjectOfType<playerHUDui>();
        activeBtn = FindObjectOfType<CabinetSwitchManager>();
        playSFX = FindObjectOfType<AudioManager>();
        popMsg = FindObjectOfType<WorldSpaceMessageBox>();

        trashcan = GameObject.Find("/Environment/trashcan/Trashcan");
        if(returnedPlc != null)
        {
            returnedPlc.SetActive(false);
        }
        taskText = GameObject.Find("/Player/Main Camera/UICamera/Canvas/TaskBox/NumTaskText").GetComponent<Text>();
        PlayerPrefs.SetInt("CleaningTaskDone", 0);


    }
    // Update is called once per frame
    void Update()
    {
        taskText.text = "Tasks: " + completedTask.ToString() + "/" + numOfTask.ToString();
        taskText.color = Color.cyan;
        if (completedTask == numOfTask)
        {
            if (!taskDone)
            {
                messyWorkstationGO.transform.localScale = Vector3.zero;
                popMsg.MessagePopUp("The biosafety cabinet is now ready for cell culture work!\nYou may now close the sash window and turn on the UV light for at least 15 minutes.\n You need to decontaminate the air before transferring any apparatus into the biosafety cabinet.\n <b><color=teal>To do this, pull down the the sash and switch on the UV Light.</color></b>");
                //Invoke("DelayMsj1", 3.0f);
                showMessage.ShowTaskBox();
                //activeBtn.SendMessage("OnNewBtn");
                PlayerPrefs.SetInt("CleaningTaskDone", 1); //Set for next task: Close sash and on UV Light
                taskDone = true;
            }
        }
    }
    public void ObjectDetect()
    {
        switch (ObjectDestination)
        {
            case objStatus.dirtyLiquid:
                showMessage.ShowMessage(dLiquid);
                playSFX.Wiping();
                trashesObj.SetActive(false);
                showMessage.IncreaseScore(1);
                completedTask++;
                break;
            case objStatus.throwAway:
                if(trashes != "")
                {
                    showMessage.ShowMessage(trashes);
                }
                trashesObj.transform.parent = trashcan.transform;
                trashesObj.transform.localPosition = transform.transform.position;
                showMessage.IncreaseScore(1);
                completedTask++;
                break;
            case objStatus.returnPlace:
                showMessage.ShowMessage(rotObject);
                trashesObj.SetActive(false);
                returnedPlc.SetActive(true);
                showMessage.IncreaseScore(1);
                completedTask++;
                break;
        }
    }
    void DelayMsj1()
    {
        popMsg.MessagePopUp("You need to clean the air in the biosafety cabinet first before transferring any apparatus into it.\n<b><color=teal>To do this, pull down the sash and switch on the UV Light.</color></b>");
    }
}
public enum objStatus
{
    dirtyLiquid,
    throwAway,
    returnPlace
}
