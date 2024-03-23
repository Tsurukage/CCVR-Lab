using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayAnimation : MonoBehaviour
{
    private Animation anim1;
    [SerializeField] private GameObject cabinetLight;
    [SerializeField] private GameObject onCartSet;
    public string name1, firstSent, secondSent;
    private playerHUDui showMessage;
    private CabinetSwitchManager uvLight;
    private WorldSpaceMessageBox popMsg;
    private bool windowIsOpen, executeOnce;
    [SerializeField]
    private GameObject messyCounter;

    private static int boolLast;

    // Start is called before the first frame update
    void Start()
    {
        cabinetLight = GameObject.Find("Lights");
        onCartSet = GameObject.Find("/OnCart");
        anim1 = gameObject.GetComponent<Animation>();
        showMessage = FindObjectOfType<playerHUDui>();
        uvLight = FindObjectOfType<CabinetSwitchManager>();
        popMsg = FindObjectOfType<WorldSpaceMessageBox>();
        if(messyCounter != null)
        {
            messyCounter.transform.localScale = Vector3.zero;
        }
        PlayerPrefs.SetInt("SashIsOpen", 0);
        PlayerPrefs.SetInt("LastStep", 0); //Has to declare here for LabLvl1 scene
    }
    public void playCabinetAnim()
    {
        boolLast = PlayerPrefs.GetInt("CleaningTaskDone");
        if(PlayerPrefs.GetInt("LastStep") == 0)
        {
            if (boolLast == 0)
            {
                if (!cabinetLight.activeSelf)
                {
                    showMessage.ShowMessage("The light is not switched on.");
                }
                else
                {
                    if (!windowIsOpen)
                    {
                        if (PlayerPrefs.GetInt("UVIsOn") == 1)
                        {
                            showMessage.ShowMessage("UV light is dangerous! Please switch it off before opening the sash.");
                        }
                        else if (PlayerPrefs.GetInt("UVIsOn") == 0)
                        {
                            OpenGlass();
                            if (messyCounter != null)
                                messyCounter.transform.localScale = Vector3.one;
                            if (!onCartSet.activeSelf)
                            {
                                onCartSet.SetActive(true);
                            }
                            if (!executeOnce)
                            {
                                showMessage.ShowTaskBox();
                                showMessage.ShowMessage(firstSent);
                                StartCoroutine(Delay());
                                executeOnce = true;
                            }
                        }
                    }
                    else
                    {
                        CloseGlass();
                    }
                }
            }
        }
        else if(PlayerPrefs.GetInt("LastStep") == 1)
        {
            CloseGlass();
            showMessage.IncreaseScore(1);
            PlayerPrefs.SetInt("LastStep", 2);
        }
        else if(PlayerPrefs.GetInt("LastStep") == 2)
        {
            showMessage.ShowMessage("You have pulled down the sash");
            //popMsg.MessagePopUp("Turn on the UV light for at least 15 min to decontaminate the interior of the biosafety cabinet.");
        }

        if(PlayerPrefs.GetInt("CleaningTaskDone") == 1)
        {
            CloseGlass();
            showMessage.ShowMessage("Next, switch on the UV to decontaminate the cabinet.");
            PlayerPrefs.SetInt("CleaningTaskDone", 2);
        }
        else if(PlayerPrefs.GetInt("CleaningTaskDone") == 2)
        {
            showMessage.ShowMessage("Please decontaminate the biosafety cabinet first.");
        }
        else if (PlayerPrefs.GetInt("CleaningTaskDone") == 3)
        {
            showMessage.ShowMessage("Please switch off the UV light.");
        }
        else if(PlayerPrefs.GetInt("CleaningTaskDone") == 4)
        {
            OpenGlass();
            popMsg.MessagePopUp("Now, switch on the fan to establish an airflow in the biosafety cabinet.");
            //popMsg.MessagePopUp("You can now transfer the apparatus from the cart into the biosafety cabinet.\n Remember to spray the apparatus before transferring it into the biosafety cabinet!");
            PlayerPrefs.SetInt("CleaningTaskDone", 5);
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5f);
        showMessage.ShowMessage(secondSent);
    }

    public void OpenGlass()
    {
        anim1.Play(name1);
        windowIsOpen = true;
        PlayerPrefs.SetInt("SashIsOpen", 1); //1 = Sash is opened
    }
    public void CloseGlass()
    {
        anim1["cabinet_glass_down"].wrapMode = WrapMode.Once;
        anim1.Play("cabinet_glass_down");
        windowIsOpen = false;
        PlayerPrefs.SetInt("SashIsOpen", 0); //0 = Sash is closed
    }
}
