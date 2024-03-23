using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BSCSwitchForLast : MonoBehaviour
{
    [SerializeField]
    private Button lightSwitch, uvSwitch, fanSwitch;
    [SerializeField]
    private GameObject spotLight;
    [SerializeField]
    [Tooltip("Swith on when true, off when false.")]
    private bool onLight = false;
    [SerializeField]
    [Tooltip("Blue when true, white when false")]
    private bool uvOn = false;
    [SerializeField]
    private Sprite lightOnSpt;
    [SerializeField]
    private Text dateSystem;
    [SerializeField]
    private Text menuStatus;
    [SerializeField]
    private string[] status;
    [SerializeField]
    private Animation anim1;
    [SerializeField]
    private bool sashOpened;
    [SerializeField]
    private AudioSource cabinetSound;
    [SerializeField]
    private bool cbntSoundOn;

    private playerHUDui shMsj;
    private WorldSpaceMessageBox popMsg;

    // Start is called before the first frame update
    void Start()
    {
        popMsg = FindObjectOfType<WorldSpaceMessageBox>();
        shMsj = FindObjectOfType<playerHUDui>();

        lightSwitch = GameObject.Find("Light").GetComponent<Button>();
        lightSwitch.onClick.AddListener(() => LightOnOff());

        uvSwitch = GameObject.Find("UV").GetComponent<Button>();
        uvSwitch.onClick.AddListener(() => UVOnOff());

        fanSwitch = GameObject.Find("Fan").GetComponent<Button>();
        fanSwitch.onClick.AddListener(() => CabinetFanSound());

        spotLight = GameObject.Find("Lights");
        dateSystem = GameObject.Find("LCD_TimeSystem").GetComponent<Text>();
        menuStatus = GameObject.Find("Status").GetComponent<Text>();

        anim1 = GameObject.Find("Glass3Front").GetComponent<Animation>();

        if(menuStatus != null)
        {
            menuStatus.text = status[2];
        }
        if(cabinetSound != null)
        {
            SoundOn();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onLight)
        {
            spotLight.SetActive(true);
            lightSwitch.GetComponent<Image>().color = Color.white;
        }
        else
        {
            spotLight.SetActive(false);
            lightSwitch.GetComponent<Image>().color = Color.gray;
        }

        if(dateSystem != null)
        {
            dateSystem.text = System.DateTime.Now.ToString("HH:mm");
        }
        
    }

    public void LightOnOff()
    {
        onLight ^= true;
        //spotLight.SetActive(onLight);
    }
    public void UVOnOff()
    {
        if (PlayerPrefs.GetInt("LastStep") == 0)
        {
            if (sashOpened)
            {
                shMsj.ShowMessage("UV light is dangerous! Please pull down the sash before switching on the UV light.");
            }
            else
            {
                if (!uvOn)
                {
                    if (!spotLight.activeSelf)
                        shMsj.ShowMessage("Light is off");
                    else
                        UVOn();
                }
                else
                {
                    UVOff();
                }
            }
        }
        else if(PlayerPrefs.GetInt("LastStep") == 1)
        {
            shMsj.ShowMessage("Let the air purge for a while");
        }
        else if(PlayerPrefs.GetInt("LastStep") == 2)
        {
            shMsj.ShowMessage("Try to remember again the correct order to close the biosafety cabinet.");
        }
        else if(PlayerPrefs.GetInt("Laststep") == 3)
        {
            shMsj.ShowMessage("Please <b>pull down the sash first</b>.");
        }
        else if(PlayerPrefs.GetInt("LastStep") == 4)
        {
            UVOn();
            shMsj.IncreaseScore(1);
            popMsg.MessagePopUp("Turn on the UV light for at least 15 minutes to decontaminate the interior of the biosafety cabinet.");
            StartCoroutine(FinalMessage());
            PlayerPrefs.SetInt("LastStep", 5);
        }
        else if(PlayerPrefs.GetInt("LastStep") == 5)
        {
            UVOff();
            PlayerPrefs.SetInt("LastStep", 6);
        }
    }
    void UVOn()
    {
        spotLight.transform.GetChild(0).gameObject.GetComponent<Light>().color = Color.blue;
        spotLight.transform.GetChild(1).gameObject.GetComponent<Light>().color = Color.blue;
        uvSwitch.GetComponent<Image>().color = Color.white;
        uvOn = true;
    }
    void UVOff()
    {
        spotLight.transform.GetChild(0).gameObject.GetComponent<Light>().color = Color.white;
        spotLight.transform.GetChild(1).gameObject.GetComponent<Light>().color = Color.white;
        uvSwitch.GetComponent<Image>().color = Color.gray;
        uvOn = false;
    }
    IEnumerator FinalMessage()
    {
        yield return new WaitForSeconds(3f);
        popMsg.MessagePopUp("Congratulations! You’ve successfully completed the task given. Proper aseptic techniques are crucial for cell and tissue culture and that’s a very valuable skill that you had acquired from SBP3410 Cell and Tissue Culture. Do have fun and enjoy cell and tissue culture!\n<color=green>Remember to turn off the UV light and the light before exiting the laboratory.</color>\nLook to the door to return to the main menu.");
    }

    public void CabinetSash()
    {
        if(PlayerPrefs.GetInt("LastStep") == 0)
        {
            if (!onLight)
            {
                shMsj.ShowMessage("The light is off");
            }
            else
            {
                if (!sashOpened)
                {
                    if (uvOn)
                    {
                        shMsj.ShowMessage("It's dangerouse to pull up the sash when the UV light is on. Please turn the UV light off first.");
                    }
                    else
                    {
                        PullUpSash();
                    }
                }
                else
                {
                    PullDownSash();
                }
                
            }
        }
        else if(PlayerPrefs.GetInt("LastStep") == 1)
        {
            shMsj.ShowMessage("Let the air purge for a while");
        }
        else if(PlayerPrefs.GetInt("LastStep") == 2)
        {
            shMsj.ShowMessage("Please switch off the fan before pulling down the sash.");
        }
        else if(PlayerPrefs.GetInt("LastStep") == 3)
        {
            PullDownSash();
            shMsj.IncreaseScore(1);
            PlayerPrefs.SetInt("LastStep", 4);
            shMsj.ShowMessage("The sash is pulled down.");
        }
        else if(PlayerPrefs.GetInt("LastStep") == 4)
        {
            shMsj.ShowMessage("You have pulled down the sash. Please continue the next step.");
        }
    }
    void PullUpSash()
    {
        anim1.Play("cabinet_glass_up");
        sashOpened = true;
    }
    void PullDownSash()
    {
        anim1.Play("cabinet_glass_down");
        anim1["cabinet_glass_down"].wrapMode = WrapMode.Once;
        sashOpened = false;
    }

    public void CabinetFanSound()
    {
        if(PlayerPrefs.GetInt("LastStep") < 1)
        {
            shMsj.ShowMessage("Please do not switch off the fan in the middle of cell culture work.");
        }
        else if(PlayerPrefs.GetInt("LastStep") == 1)
        {
            shMsj.ShowMessage("Let the air purge for a while");
        }
        else if(PlayerPrefs.GetInt("LastStep") == 2)
        {
            SoundOff();
            PlayerPrefs.SetInt("LastStep", 3);
        }
        
    }
    void SoundOn()
    {
        fanSwitch.GetComponent<Image>().color = Color.white;
        cabinetSound.volume = 0.05f;
        cbntSoundOn = true;
    }
    void SoundOff()
    {
        fanSwitch.GetComponent<Image>().color = Color.grey;
        cabinetSound.volume = 0f;
        cbntSoundOn = false;
    }
}
