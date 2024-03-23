using UnityEngine.EventSystems;
using UnityEngine;

public class CartToBSC : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Transform handPos;
    [SerializeField]
    private string patchName;
    [SerializeField]
    private Transform patchOnCart;
    [SerializeField]
    private Vector3 posValue;
    [SerializeField]
    private Vector3 rotValue;
    [SerializeField]
    private string patchInCabName;
    [SerializeField]
    private Transform patchInCab;
    [SerializeField]
    [TextArea]
    private string rightPosInCab;

    private Animation spraying;
    private bool isSprayed;
    private bool isPicked;
    private AudioManager playSFX;
    private PutDown patchesPos;

    private playerHUDui shMessage;
    private WorldSpaceMessageBox messageBox;

    public ItemStatus itemState;

    // Start is called before the first frame update
    void Start()
    {
        shMessage = FindObjectOfType<playerHUDui>();
        messageBox = FindObjectOfType<WorldSpaceMessageBox>();
        playSFX = FindObjectOfType<AudioManager>();
        handPos = transform.Find("/Player/Main Camera/HandPos2");
        patchesPos = FindObjectOfType<PutDown>();
        patchOnCart.GetChild(0).transform.localScale = Vector3.zero;
        isPicked = false;
        if (this.gameObject.name.Contains("Spray"))
            this.itemState = ItemStatus.isSprayed;
        else
            this.itemState = ItemStatus.isDirty;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(PlayerPrefs.GetInt("SprayHasPicked") == 0)
        {
            if (handPos.childCount > 0)
            {
                shMessage.ShowMessage("You can only pick an item at a time");
            }
            else
            {
                this.gameObject.transform.parent = handPos.transform;
                this.gameObject.transform.localPosition = Vector3.zero;
                this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                patchOnCart.GetChild(0).transform.localScale = new Vector3(0.1f, 0.001f, 0.1f);
                isPicked = true;
            }
        }

        if (PlayerPrefs.GetInt("SprayHasPicked") == 1)
        {
            if(PlayerPrefs.GetInt("CleaningTaskDone") < 6)
            {
                shMessage.ShowMessage("Please switch on the fan before transferring the apparatus into the biosafety cabinet.");
            }
            else if(PlayerPrefs.GetInt("CleaningTaskDone") == 6 || PlayerPrefs.GetInt("CleaningTaskDone") > 6)
            {
                if (!isSprayed)
                {
                    if (handPos.childCount == 0)
                    {
                        shMessage.ShowMessage("You must spray the apparatus with 70% ethanol before transferring them into the BSC.");
                    }
                    else if (handPos.childCount > 0 && handPos.GetChild(0).name.Contains("Spray"))
                    {
                        SprayAnimation();
                    }
                }
                else
                {
                    if (handPos.childCount > 0)
                    {
                        shMessage.ShowMessage("You can only pick an item at a time.");
                    }
                    else
                    {
                        this.gameObject.transform.parent = handPos.transform;
                        this.gameObject.transform.localPosition = Vector3.zero;
                        this.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                        patchOnCart.GetChild(0).localScale = new Vector3(0.1f, 0.001f, 0.1f);
                        isPicked = true;
                        //handState = HandStatus.handIsOccupied;
                    }
                }
            }
            
        }
    }

    public void PutDownItem()
    {
        transform.parent = patchOnCart;
        transform.localPosition = posValue;
        transform.localEulerAngles = rotValue;
        patchOnCart.GetChild(0).localScale = Vector3.zero;
        isPicked = false;
        //handState = HandStatus.handIsEmpty;
    }
    public void PutHereInCab()
    {
        if (handPos.childCount == 0)
        {
            shMessage.ShowMessage("You have nothing in your hand.");
        }
        else if (handPos.childCount > 0)
        {
            if (handPos.GetChild(0).name.Contains(this.name))
            {
                if (this.patchInCab.name == this.patchInCabName)
                {
                    transform.parent = patchInCab;
                    transform.localPosition = posValue;
                    transform.localEulerAngles = rotValue;
                    this.enabled = false;
                    isPicked = false;
                    shMessage.IncreaseScore(5);
                    //handState = HandStatus.handIsEmpty;
                    patchOnCart.GetChild(0).localScale = Vector3.zero;
                    patchesPos.TaskIncrease(1);
                    if (rightPosInCab == "")
                    {
                        //Do nothing here.
                    }
                    else if(rightPosInCab != "")
                    {
                        messageBox.MessagePopUp(rightPosInCab);
                    }
                }
            }
            else
            {
                shMessage.ShowMessage("Hmm, try again.");
                shMessage.DeductScore(1);
            }
        }
    }
    public void SprayAnimation()
    {
        if (!isSprayed)
        {
            if (handPos.GetChild(0).name == "Spray Bottle")
            {
                spraying = handPos.GetChild(0).GetComponent<Animation>();
                spraying.Play("Spraying");
                playSFX.Spraying();
                shMessage.ShowMessage("You have sprayed the apparatus.");
                //OptionFadeOut();
                isSprayed = true;
                this.itemState = ItemStatus.isSprayed;
            }
            else if (handPos.GetChild(0).name != "Spray Bottle")
            {
                shMessage.ShowMessage("You did not pick up the <color=green>70% ethanol</color>.");
            }
            else
            {
                shMessage.ShowMessage("You did not pick up the <color=green>70% ethanol</color>.");
            }
        }
        else
        {
            shMessage.ShowMessage("You have sprayed the apparatus.");
        }
    }
    public void OutSpray()
    {
        this.transform.localEulerAngles = new Vector3(0, -90, 0);
        spraying = handPos.GetChild(0).GetComponent<Animation>();
        spraying.Play("Spraying");
        playSFX.Spraying();
    }
    // Update is called once per frame
    void Update()
    {
        switch (itemState)
        {
            case ItemStatus.isDirty:
                isSprayed = false;
                break;
            case ItemStatus.isSprayed:
                isSprayed = true;
                break;
        }
    }
}

public enum ItemStatus
{
    isDirty,
    isSprayed
}