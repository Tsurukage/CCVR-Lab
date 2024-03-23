using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceMessageBox : MonoBehaviour
{
    public Transform messageBox;
    public Text messageText;
    public Image imageHolder;

    private AudioManager notificationSFX;
    private bool isOne;

    // Start is called before the first frame update
    void Start()
    {
        isOne = false;
        messageBox.transform.localScale = Vector3.zero;
        notificationSFX = GameObject.Find("AudioSource").GetComponent<AudioManager>();
    }

    public void MessagePopUp(string messageTxt)
    {
        messageText.text = messageTxt;
        imageHolder.sprite = null;
        imageHolder.rectTransform.localScale = Vector3.zero;
        int wordcount = messageTxt.Split(' ').Length;
        OpenMessageBox();
    }

    public void ImagePopUp(Sprite incomingImage)
    {
        imageHolder.sprite = incomingImage;
        imageHolder.rectTransform.localScale = Vector3.one;
        messageText.text = "";
        OpenMessageBox();
    }

    void OpenMessageBox()
    {
        iTween.StopByName(messageBox.gameObject, "ScaleOneMBox");
        iTween.ScaleTo(messageBox.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "ScaleOneMBox"));
        notificationSFX.PopNitification();
        isOne = true;
    }

    public void CloseMessageBox()
    {
        iTween.StopByName(messageBox.gameObject, "ScaleZeroMBox");
        iTween.ScaleTo(messageBox.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "ScaleZeroMBox"));
        isOne = false;
    }
}
