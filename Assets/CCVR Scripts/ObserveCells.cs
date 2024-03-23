using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObserveCells : MonoBehaviour, IPointerClickHandler
{
    private WorldSpaceMessageBox popMessage;
    [SerializeField]
    private Sprite cellsInFlask;
    [SerializeField]
    private Transform imageHolderForCell;
    [SerializeField]
    private Image imageHolder;
    [SerializeField]
    private Button closeBtn;
    [SerializeField]
    private GameObject tray;

    private static int openedOnce = 0;
    [SerializeField]
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        time = 2;
        popMessage = FindObjectOfType<WorldSpaceMessageBox>();
        imageHolderForCell = transform.Find("/ImageHolder");
        imageHolderForCell.localScale = Vector3.zero;
        imageHolder = GameObject.Find("/ImageHolder/Canvas/Panel/Image").GetComponent<Image>();
        closeBtn = GameObject.Find("/ImageHolder/Canvas/Panel/ClsBtn").GetComponent<Button>();
        closeBtn.onClick.AddListener(CloseTransform);
        tray = GameObject.Find("/SubcultureDone/Tray");
        tray.GetComponent<BoxCollider>().enabled = false;
        tray.GetComponent<NvrInteractiveObject>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (openedOnce == 1)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                popMessage.MessagePopUp("Great, the cells in <color=green>" + this.name + "</color> have been successfully subcultured and the confluency is now reduced. Before transferring the flasks to the incubator, turn left to answer the quiz.");
                openedOnce++;
                print(openedOnce);
                print(time);
                time = 0;
            }
            
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        openedOnce++;
        if (cellsInFlask != null)
        {
            OpenTransform();
            imageHolder.sprite = cellsInFlask;
        }
        
    }
    public void ClickTheFlask()
    {
        openedOnce++;
        if (cellsInFlask != null)
        {
            OpenTransform();
            imageHolder.sprite = cellsInFlask;
        }
    }
    void OpenTransform()
    {
        iTween.StopByName(imageHolderForCell.gameObject, "ScaleImageBox");
        iTween.ScaleTo(imageHolderForCell.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "ScaleImageBox"));
    }

    void CloseTransform()
    {
        iTween.StopByName(imageHolderForCell.gameObject, "ScaleImageMBox");
        iTween.ScaleTo(imageHolderForCell.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "ScaleImageBox"));
    }
    public void ActiveCollider()
    {
        tray.GetComponent<BoxCollider>().enabled = true;
        tray.GetComponent<NvrInteractiveObject>().enabled = true;
    }
}
