using UnityEngine;
using UnityEngine.UI;

public class BSCMachineWork : MonoBehaviour
{
    [SerializeField]
    private Button lightSwitch, uvSwitch;
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

    // Start is called before the first frame update
    void Start()
    {
        shMsj = FindObjectOfType<playerHUDui>();

        lightSwitch = GameObject.Find("Light").GetComponent<Button>();
        lightSwitch.onClick.AddListener(() => LightOnOff());

        uvSwitch = GameObject.Find("UV").GetComponent<Button>();
        uvSwitch.onClick.AddListener(() => UVOnOff());

        spotLight = GameObject.Find("Lights");
        dateSystem = GameObject.Find("LCD_TimeSystem").GetComponent<Text>();
        menuStatus = GameObject.Find("Status").GetComponent<Text>();

        anim1 = GameObject.Find("Glass3Front").GetComponent<Animation>();

        if (menuStatus != null)
        {
            menuStatus.text = status[2];
        }
        if (cabinetSound != null)
        {
            cabinetSound.volume = 0.05f;
            cbntSoundOn = true;
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

        if (dateSystem != null)
        {
            dateSystem.text = System.DateTime.Now.ToString("HH:mm");
        }
    }

    public void LightOnOff()
    {
        onLight ^= true;
        //spotLight.SetActive(onLight);
    }
    #region UVSwitch
    public void UVOnOff()
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
    #endregion UVSwitch

    public void CabinetSash()
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
}
