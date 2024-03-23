using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetSwitchManager : MonoBehaviour
{
    [SerializeField]
    private Button switchOnCLight;
    [SerializeField]
    private GameObject spotLight;
    [SerializeField]
    [Tooltip("Switch on the light when true, else off.")]
    private bool onLight = false;
    [SerializeField]
    private Sprite switchOnSpt;
    [SerializeField]
    private GameObject switchPanel;
    [SerializeField]
    private bool displaySwtPanel = false;
    [SerializeField]
    private GameObject act2Btn;

    private playerHUDui showMessage;
    private WorldSpaceMessageBox popMsg;
    private CabinetStandby cbntStnby;

    [SerializeField]
    private Button fanSwitch;
    [SerializeField]
    private AudioSource cabinetSound;
    [SerializeField]
    private bool cbntSoundOn;
    [SerializeField]
    private string firstMsj, secondMsj;

    [SerializeField]
    private Text dateSystem;
    [SerializeField]
    private Text menuStatus;
    [SerializeField]
    private string[] status;

    private bool textAfterLightOn, uvOn;

    // Start is called before the first frame update
    void Start()
    {
        textAfterLightOn = false;
        uvOn = false;
        showMessage = FindObjectOfType<playerHUDui>();
        popMsg = FindObjectOfType<WorldSpaceMessageBox>();
        cbntStnby = FindObjectOfType<CabinetStandby>();
        switchOnCLight.GetComponent<Image>().sprite = switchOnSpt;
        fanSwitch = GameObject.Find("Fan").GetComponent<Button>();

        if (spotLight != null)
        {
            if (!onLight)
            {
                spotLight.SetActive(false);
                switchOnCLight.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                spotLight.SetActive(true);
                switchOnCLight.GetComponent<Image>().color = Color.white;
            }
            
        }
        if (switchPanel != null)
        {
            switchPanel.transform.localScale = new Vector3(0.025f, 0.025f);
        }
        if (act2Btn != null)
        {
            act2Btn.SetActive(false);
        }
        if (menuStatus != null)
        {
            menuStatus.text = status[2];
        }
        if(cabinetSound != null)
        {
            cabinetSound.volume = 0f;
            cbntSoundOn = false;
        }
        PlayerPrefs.SetInt("UVIsOn", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (dateSystem != null)
        {
            dateSystem.text = System.DateTime.Now.ToString("HH:mm");
        }

        if (cbntSoundOn)
            fanSwitch.GetComponent<Image>().color = Color.white;
        else
            fanSwitch.GetComponent<Image>().color = Color.grey;
    }
    public void UpdateText(int textIndex)
    {
        menuStatus.text = status[textIndex];
    }
    public void SwictOnLight()
    {
        if (!spotLight.activeSelf)
        {
            switchOnCLight.GetComponent<Image>().sprite = switchOnSpt;
            switchOnCLight.GetComponent<Image>().color = Color.white;
            spotLight.SetActive(true);

            if (!textAfterLightOn)
            {
                popMsg.MessagePopUp("Now, pull up the sash by looking at the <color=blue>" + firstMsj + "</color> handles.");
                textAfterLightOn = true;
            }
        }
        else
        {
            if (!uvOn)
            {
                spotLight.SetActive(false);
                switchOnCLight.GetComponent<Image>().sprite = switchOnSpt;
                switchOnCLight.GetComponent<Image>().color = Color.grey;
            }
            else
            {
                showMessage.ShowMessage("The UV light is on");
            }
        }

    }
    IEnumerator delayMessage()
    {
        yield return new WaitForSeconds(5f);
        showMessage.ShowMessage(secondMsj);
    }
    public void OnCabinetSound()
    {
        if(PlayerPrefs.GetInt("CleaningTaskDone") < 5 && PlayerPrefs.GetInt("CleaningTaskDone") > 5)
        {
            if (!cbntSoundOn)
            {
                cabinetSound.volume = 0.05f;
                cbntSoundOn = true;
            }
            else
            {
                cabinetSound.volume = 0f;
                cbntSoundOn = false;
            }
        }
        else if(PlayerPrefs.GetInt("CleaningTaskDone") == 5)
        {
            if (!cbntSoundOn)
            {
                cabinetSound.volume = 0.05f;
                cbntSoundOn = true;
                PlayerPrefs.SetInt("CleaningTaskDone", 6);
            }
        }
    }
    #region Access by CabinetStandby.cs
    public void UVLightOn()
    {
        Button btn = GameObject.Find("UV").GetComponent<Button>();
        spotLight.transform.GetChild(0).gameObject.GetComponent<Light>().color = Color.blue;
        spotLight.transform.GetChild(1).gameObject.GetComponent<Light>().color = Color.blue;
        btn.GetComponent<Image>().color = Color.white;
        uvOn = true;
        PlayerPrefs.SetInt("UVIsOn", 1); //1 = On
    }
    public void UVLightOff()
    {
        Button btn = GameObject.Find("UV").GetComponent<Button>();
        spotLight.transform.GetChild(0).gameObject.GetComponent<Light>().color = Color.white;
        spotLight.transform.GetChild(1).gameObject.GetComponent<Light>().color = Color.white;
        btn.GetComponent<Image>().color = Color.gray;
        uvOn = false;
        PlayerPrefs.SetInt("UVIsOn", 0); //0 = Off
    }
    #endregion Access by CabinetStandb.cs
    public void UVLight()
    {
        if (PlayerPrefs.GetInt("CleaningTaskDone") == 0)
        {
            if(PlayerPrefs.GetInt("SashIsOpen") == 1)
            {
                showMessage.ShowMessage("UV light is dangerous! Please close the sash before switching on the UV light.");
            }
            else if(PlayerPrefs.GetInt("SashIsOpen") == 0)
            {
                if (!uvOn)
                {
                    if (!spotLight.activeSelf)
                    {
                        showMessage.ShowMessage("The light is off.");
                    }
                    else
                    {
                        UVLightOn();
                    }
                }
                else
                {
                    UVLightOff();
                }
            } 
        }

        if (PlayerPrefs.GetInt("CleaningTaskDone") == 1)
        {
            showMessage.ShowMessage("Please close the sash first before switching on the UV light.");
        }
        else if (PlayerPrefs.GetInt("CleaningTaskDone") == 2)
        {
            UVLightOn();
            cbntStnby.StartTimer();
            Invoke("DelayMsj", 1f);
            act2Btn.SetActive(true);
            //PlayerPrefs.SetInt("CleaningTaskDone", 3);
        }
        else if (PlayerPrefs.GetInt("CleaningTaskDone") == 3)
        {
            UVLightOff();
            PlayerPrefs.SetInt("CleaningTaskDone", 4);
        }

        if (PlayerPrefs.GetInt("LastStep") == 1)
        {
            showMessage.ShowMessage("Please <b>close the sash first</b>.");
        }
        else if (PlayerPrefs.GetInt("LastStep") == 2)
        {
            UVLightOn();
            showMessage.IncreaseScore(1);
            popMsg.MessagePopUp("Turn on the UV light for at least 15 minutes to decontaminate the interior of the biosafety cabinet.");
            StartCoroutine(FinalMessage());
            PlayerPrefs.SetInt("LastStep", 3);
        }
        else if (PlayerPrefs.GetInt("LastStep") == 3)
        {
            UVLightOff();
            PlayerPrefs.SetInt("CleaningTaskDone", 0);
            PlayerPrefs.SetInt("LastStep", 0);
        }
    }
    IEnumerator FinalMessage()
    {
        yield return new WaitForSeconds(5f);
        popMsg.MessagePopUp("Congratulations! You’ve successfully completed the task given. Proper aseptic techniques are crucial for cell and tissue culture and that’s a very valuable skill that you had acquired from SBP3410 Cell and Tissue Culture. Do have fun and enjoy cell and tissue culture! \n<color=green>Remember to turn off the light before exit the laboratory.</color>");
    }
    void DelayMsj()
    {
        showMessage.ShowMessage("Now, turn to your right.\n Begin the next activity by looking at the start button on the cart.");
    }
}
