using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PickObject : MonoBehaviour {

	// Use this for initialization
	public Transform hand, original, posInCab;
    private bool isPickedUp, optionOn;
    public bool isSprayed;
    
    public Text task2;
    private playerHUDui showMessage;
    private ActivityOne panelTween;
    private ActivityTwo toggleApparatus;
    private WorldSpaceMessageBox popMessage;

    public GameObject objOnCart, objInCab, thisPrefab; //Object holding each of the apparatus
    private GameObject newInstance;
    private Animation spraying;
    private AudioManager playSFX;

    private static int totalTask = 5;
    private static int currentTask = 0;

    bool once = false;

    void Start ()
    {
        //Initialized
        currentTask = 0;
        isSprayed = false;
        hand = transform.Find("/Player/Main Camera/HandPos");
        task2 = GameObject.Find("/Player/Main Camera/UICamera/Canvas/TaskBox2/NumTaskText").GetComponent<Text>();
        showMessage = FindObjectOfType<playerHUDui>();
        panelTween = FindObjectOfType<ActivityOne>();
        toggleApparatus = FindObjectOfType<ActivityTwo>();
        playSFX = FindObjectOfType<AudioManager>();
        popMessage = FindObjectOfType<WorldSpaceMessageBox>();

        //If gameObject is not null
        if (objOnCart != null)
        {
            objOnCart.transform.localScale = Vector3.one;
            if(this.gameObject.name == "SprayBottle2")
            {
                objOnCart.transform.localScale = Vector3.zero;
            }
        }
        if (original != null)
        {
            original.transform.localScale = Vector3.zero;
        }
        if (objInCab != null)
        {
            objInCab.transform.localScale = Vector3.zero;
        }
        if(posInCab != null)
        {
            posInCab.transform.localScale = Vector3.zero;
        }

        if (this.gameObject.name == "SprayBottle" || this.gameObject.name == "SprayBottle2")
        {
            isSprayed = true;
        }
        task2.text = currentTask.ToString() + "/" + totalTask.ToString();
        PlayerPrefs.SetInt("LastStep", 0);
    }

    #region Apparatus transfer
    void Update_Task(int increaseNum)
    {
        currentTask += increaseNum;
        Debug.Log(currentTask.ToString() + "/" + totalTask.ToString());
        task2.text = currentTask.ToString() + "/" + totalTask.ToString();
        PlayerPrefs.SetInt("Transfered", 1);
        if (currentTask == totalTask)
        { 
            showMessage.ShowMessage("All items have been placed at their proper locations inside the biosafety cabinet. Great job!");
            panelTween.NextActvtMessage(); 
            //toggleApparatus.ChangeAprtsGroup();
            panelTween.FadeOutPanel();
            panelTween.CloseTask2();
        }
    }
    #endregion Apparatus transfer
    #region Apparatus on utility cart
    public void TogglePickItem()
    {
        if (isPickedUp)
        {
            PutDownObject();
        }
        else
        {
            PickupObject();
        }
    }
    public void PickupObject()
    {
        if (!isSprayed)
        {
            if(hand.childCount == 0)
            {
                showMessage.ShowMessage("You have not sanitized the apparatus.");
            }
            else if(hand.childCount > 0 && hand.GetChild(0).name == "Spray Bottle")
            {
                //if countdown has not done
                //Message: Please wait for the cabinet to finish
                //if countdown has finished
                SprayAnimation();
            }
        }
        else
        {
            if (hand.childCount > 0)
            {
                showMessage.ShowMessage("You have an appratus in your hand.");
            }
            else
            {
                newInstance = Instantiate(thisPrefab, hand.position, thisPrefab.transform.rotation, hand);
                newInstance.name = newInstance.name.Replace("(Clone)", "");
                showMessage.ShowMessage(newInstance.name);
                objOnCart.transform.localScale = Vector3.zero;
                posInCab.transform.localScale = new Vector3(0.1f, 0.01f, 0.1f);
                original.transform.localScale = new Vector3(0.1f, 0.01f, 0.1f);
                isPickedUp = true;

                if (this.gameObject.name == "SprayBottle2")
                {
                    if (!once)
                    {
                        popMessage.MessagePopUp("You are absolutely right! Do not forget to swab the work surface liberally with 70% ethanol in an inward to outward direction after using the biosafety cabinet.");
                        playSFX.CorrectChoices();
                        showMessage.IncreaseScore(1);
                        PlayerPrefs.SetInt("LastStep", 1);
                        //After spray bottle is picked up
                        //LastStep playerpref goes to PlayAnimation.cs and CabinetSwitchManager.cs
                        StartCoroutine(MessageForLastTask());
                        once = true;
                    }
                }
            }
        }
    }
    
    public void PutDownObject()
    {
        Destroy(newInstance);
        objOnCart.transform.localScale = Vector3.one;
        original.transform.localScale = Vector3.zero;
        posInCab.transform.localScale = Vector3.zero;
        //Invoke("OptionFadeOut", 0f);
        isPickedUp = false;
    }
    #endregion Apparatus on utility cart
    #region Apparatus inside cabinet
    public void TogglePickItem2()
    {
        if (isPickedUp)
        {
            PutHere();
        }
        else
        {
            TakeAway();
        }
    }
    public void PutHere()
    {
        if(hand.childCount == 0)
        {
            showMessage.ShowMessage("You did not pick up any apparatus.");
        }
        else
        {
            Destroy(newInstance);
            objInCab.transform.localScale = Vector3.one;
            posInCab.transform.localScale = Vector3.zero;
            original.transform.localScale = Vector3.zero;
            //Invoke("Option2FadeOut", 0f);
            Update_Task(1);
        }

        isPickedUp = false;
    }
    public void TakeAway()
    {
        if (hand.childCount > 0)
        {
            showMessage.ShowMessage("You have picked up an appratus.");
        }
        else
        {
            newInstance = Instantiate(thisPrefab, hand.position, thisPrefab.transform.rotation, hand);
            newInstance.name = newInstance.name.Replace("(Clone)", "");
            objInCab.SetActive(false);
            original.gameObject.SetActive(true);
            posInCab.gameObject.SetActive(true);
            Invoke("Option2FadeOut", 0f);
        }

        isPickedUp = true;
    }
    #endregion Apparatus inside cabinet
    #region SprayOption
    public void SprayAnimation()
    {
        if (!isSprayed)
        {
            if (hand.childCount > 0 && hand.GetChild(0).name == "Spray Bottle")
            {
                spraying = hand.GetChild(0).GetComponent<Animation>();
                spraying.Play("Spraying");
                playSFX.Spraying();
                showMessage.ShowMessage("You have sprayed the apparatus.");
                //OptionFadeOut();
                isSprayed = true;
            }
            else if (hand.childCount > 0 && hand.GetChild(0).name != "Spray Bottle")
            {
                showMessage.ShowMessage("You did not pick up the <color=green>70% ethanol</color>.");
            }
            else
            {
                showMessage.ShowMessage("You did not pick up the <color=green>70% ethanol</color>.");
            }
        }
        else
        {
            showMessage.ShowMessage("You have sprayed the apparatus.");
        }
    }
    #endregion SprayOption
    IEnumerator MessageForLastTask()
    {
        yield return new WaitForSeconds(6f);
        popMessage.MessagePopUp("You are almost finished! The final step is to switch off the biosafety cabinet in the same manner it was switched on earlier. The steps are similar but in a reversed order. You may do so by clicking on a specific location on the biosafety cabinet.");
    }
}

public enum ActivityStatus
{
    ACTIVITY_SYANDBY,
    ACTIVITY_IN_PROCESS,
    ACTIVITT_COMPLETE
}
