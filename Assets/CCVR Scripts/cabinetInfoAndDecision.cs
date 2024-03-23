using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cabinetInfoAndDecision : MonoBehaviour
{
    public Image infoCanvas;
    public Image ToNextPanel;
    private playerHUDui showMessage;

    // Start is called before the first frame update
    void Start()
    {
        showMessage = GameObject.FindObjectOfType<playerHUDui>();
        infoCanvas.transform.localScale = Vector3.zero;
        if (ToNextPanel != null)
        {
            ToNextPanel.transform.localScale = Vector3.zero;
        }
    }
    public void ShowCanvas()
    {
        iTween.StopByName(infoCanvas.gameObject, "fadeInCanvas");
        iTween.ScaleTo(infoCanvas.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInCanvas"));

        iTween.StopByName(ToNextPanel.gameObject, "fadeOutToNextPanel");
        iTween.ScaleTo(ToNextPanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "fadeOutToNextPanel"));
    }
    public void CloseCanvas()
    {
        iTween.StopByName(infoCanvas.gameObject, "fadeOutCanvas");
        iTween.ScaleTo(infoCanvas.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "fadeOutCanvas"));
    }
    public void CorrectCabinetPanel()
    {
        iTween.StopByName(ToNextPanel.gameObject, "fadeInToNextPanel");
        iTween.ScaleTo(ToNextPanel.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInToNextPanel"));

        iTween.StopByName(infoCanvas.gameObject, "fadeOutCanvas");
        iTween.ScaleTo(infoCanvas.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "fadeOutCanvas"));
    }
    public void CorrectCabinet()
    {
        showMessage.ShowMessage("Well Done!");
        showMessage.IncreaseScore(2);
        CorrectCabinetPanel();
    }
    public void WrongCabinet()
    {
        showMessage.ShowMessage("Oops, try again!");
        showMessage.DeductScore(1);
        CorrectCabinetPanel();
    }
}
