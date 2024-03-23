using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransferFlask : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Transform handPos, handLeft;
    [SerializeField]
    private Transform incubator;
    [SerializeField]
    private Transform flaskIncubator;
    [SerializeField]
    private Transform incubatorDoor;
    bool openIncuDoor;
    [SerializeField]
    private Transform incubatorQuiz;
    private static int totalFlask = 3;
    private static int currentNumFlask = 0;

    private playerHUDui showMessage;
    private CartToBSC sprayAnim;
    private WorldSpaceMessageBox popMsj;
    private bool sprayed, passTheQuiz;

    // Start is called before the first frame update
    void Start()
    {
        sprayed = false;
        passTheQuiz = false;
        showMessage = FindObjectOfType<playerHUDui>();
        sprayAnim = FindObjectOfType<CartToBSC>();
        popMsj = FindObjectOfType<WorldSpaceMessageBox>();
        openIncuDoor = false;
        handPos = transform.Find("/Player/Main Camera/HandPos2");
        handLeft = transform.Find("/Player/Main Camera/HandLeft");
        incubatorDoor = transform.Find("/Incubator/Hinge");
        incubator.localScale = Vector3.zero;
        flaskIncubator.localScale = Vector3.zero;
        incubatorQuiz = transform.Find("/Incubator_Quiz");
        PlayerPrefs.SetInt("allTransfer", 0);
        PlayerPrefs.SetInt("PassTheQuiz", 0);
    }

    void Update()
    {
        if(handLeft.childCount > 0 && handPos.childCount > 0)
        {
            if (handPos.GetChild(0).name.Contains("Spray"))
            {
                if (!sprayed)
                {
                    sprayAnim.OutSpray();
                    sprayed = true;
                    popMsj.MessagePopUp("Great! Now, click on the green spot on the floor to move towards the incubator.");
                }
            }
        }
        if (incubatorQuiz != null)
        {

        }
        else if (incubatorQuiz == null)
        {
            showMessage.ShowMessage("Well done! You’ve adjusted the incubator to the appropriate conditions for the cells. You may now proceed to store them in the incubator.");
            passTheQuiz = true;
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(handPos.childCount > 0)
        {
            showMessage.ShowMessage("You can only pick up one flask at a time.");
        }
        else if(handPos.childCount == 0)
        {
            //this.gameObject.transform.parent = handPos.transform;
            //this.gameObject.transform.localPosition = Vector3.zero;
            /*if (handPos.GetChild(0).name == "Flask1")
            {
                incubator[0].transform.gameObject.SetActive(true);
            }
            else if (handPos.GetChild(0).name == "Flask2")
            {
                incubator[1].transform.gameObject.SetActive(true);
            }
            else if (handPos.GetChild(0).name == "Flask3")
            {
                incubator[2].transform.gameObject.SetActive(true);
            }*/
            incubator.localScale = Vector3.one;
        }

        if(this.transform.name == "Tray")
        {
            this.transform.parent = handLeft.transform;
            this.transform.localPosition = Vector3.zero;
            popMsj.MessagePopUp("Before transferring the flasks into the incubator, it is a good practice to spray them with 70% ethanol to avoid introducing contamination into the incubator. Please click on the 70% ethanol on the service cart to sterilise the flasks.");
        }
    }
    public void PutInIncubator()
    {
        #region Unused
        /*if (handPos.childCount > 0)
        {

            if (handPos.GetChild(0).name == "Flask1")
            {
                transform.parent = incubator[0];
                //transform.localScale = new Vector3(0.7f, 36.6f, 0.7f);
                transform.localPosition = Vector3.zero;
                transform.eulerAngles = new Vector3(0, -90, 0);
                currentNumFlask++;
            }
            else if (handPos.GetChild(0).name == "Flask2")
            {
                transform.parent = incubator[1];
                //transform.localScale = new Vector3(0.7f, 36.6f, 0.7f);
                transform.localPosition = Vector3.zero;
                transform.eulerAngles = new Vector3(0, -90, 0);
                currentNumFlask++;
            }
            else if (handPos.GetChild(0).name == "Flask3")
            {
                transform.parent = incubator[2];
                //transform.localScale = new Vector3(0.7f, 36.6f, 0.7f);
                transform.localPosition = Vector3.zero;
                transform.eulerAngles = new Vector3(0, -90, 0);
                currentNumFlask++;
            }
        }
        else
        {
            showMessage.ShowMessage("You did not pick up any flask");
            Debug.Log("No flask in your hand");
        }*/
        #endregion Unused
        if(handLeft.childCount > 0 && handLeft.GetChild(0).name.Contains("Tray"))
        {
            flaskIncubator.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            incubator.localScale = Vector3.zero;
            handLeft.GetChild(0).localScale = Vector3.zero;
        }
    }
    public void OpenIncubator()
    {
        if (!passTheQuiz)
        {
            showMessage.ShowMessage("Answer the quiz on the right before proceeding.");
            OpenQuiz();
        }
        else
        {
            if (!openIncuDoor)
            {
                incubatorDoor.transform.eulerAngles = new Vector3(0, -25, 0);
                openIncuDoor = true;
            }
            else
            {
                incubatorDoor.transform.eulerAngles = new Vector3(0, 90, 0);
                openIncuDoor = false;
                #region Unused1
                /*if (currentNumFlask == totalFlask)
                {
                    showMessage.ShowMessage("All three flasks have been placed in the CO2 incubator after proper labelling.");
                    StartCoroutine(NextMessage());
                    Debug.Log("Flask properly keep in incubator");
                    PlayerPrefs.SetInt("allTransfer", 1); //RemoveUsedApparatus.cs to clear the cabinet.
                }
                else
                {
                    //showMessage.ShowMessage("There are flask still not keep in here");
                    Debug.Log("There are flask still not keep in here");
                } */
                #endregion Unused1
                if (flaskIncubator.localScale == new Vector3(0.5f, 0.5f, 0.5f))
                {
                    PlayerPrefs.SetInt("allTransfer", 1); //RemoveUsedApparatus.cs to clear the cabinet.
                                                          //popMsj.MessagePopUp("You may return to the biosafety cabinet to wrap up your work");
                    ActivityFour aFour = FindObjectOfType<ActivityFour>();
                    aFour.EndActivityButton();
                }
            }
        }
    }
    IEnumerator NextMessage()
    {
        yield return new WaitForSeconds(4f);
        showMessage.ShowMessage("Now, remove all the items in the biosafety cabinet by clicking on the items, one by one.");
    }

    void OpenQuiz()
    {
        incubatorQuiz.localScale = new Vector3(0.003f, 0.003f);
    }

    public void IndependetDoor()
    {
        if (!openIncuDoor)
        {
            incubatorDoor.transform.eulerAngles = new Vector3(0, -25, 0);
            openIncuDoor = true;
        }
        else
        {
            incubatorDoor.transform.eulerAngles = new Vector3(0, 90, 0);
            openIncuDoor = false;
        }
    }
}
