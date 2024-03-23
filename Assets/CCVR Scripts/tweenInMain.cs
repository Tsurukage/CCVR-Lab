using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tweenInMain : MonoBehaviour
{
    [SerializeField]
    private Image warningPanel;
    [SerializeField]
    private Image mainPanel;
    [SerializeField]
    private Image genderPanel;

    [SerializeField]
    private Transform page1, page2, middle;
    [SerializeField]
    private Button p1, p2;

    // Start is called before the first frame update
    void Start()
    {
        if(warningPanel != null)
        {
            warningPanel.transform.localScale = Vector3.one;
        }
        if (mainPanel != null)
        {
            mainPanel.transform.localScale = Vector3.zero;
        }
        if(genderPanel != null)
        {
            genderPanel.transform.localScale = Vector3.zero;
        }
        if(page1 != null)
        {
            page1.localPosition = middle.localPosition;
        }
        if(page2 != null)
        {
            page2.localPosition = new Vector2(1020, 0);
        }
        if(p1 != null)
            p1.onClick.AddListener(OpenPageOne);
        if(p2 != null)
            p2.onClick.AddListener(OpenPageTwo);
    }

    /*
    public void ShowWarningPanel()
    {
        iTween.StopByName(warningPanel.gameObject, "fadeInPanel1");
        iTween.StopByName(genderPanel.gameObject, "fadeOutPanel2");
        iTween.ScaleTo(warningPanel.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInPanel1"));
        iTween.ScaleTo(genderPanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "fadeOutPanel2"));
    }
    */
    public void ShowGenderPanel()
    {
        if (warningPanel != null)
        {
            iTween.StopByName(warningPanel.gameObject, "warningPanelFadeOut");
            iTween.ScaleTo(warningPanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "warningPanelFadeOut"));
        }

        iTween.StopByName(mainPanel.gameObject, "fadeOutPanel1");
        iTween.ScaleTo(mainPanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "fadeOutPanel1"));

        iTween.StopByName(genderPanel.gameObject, "fadeInPanel2");
        iTween.ScaleTo(genderPanel.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInPanel2"));
    }
    public void ShowIntroductionPanel()
    {
        //Warning Panel
        if(warningPanel != null)
        {
            iTween.StopByName(warningPanel.gameObject, "warningPanelFadeOut");
            iTween.ScaleTo(warningPanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "warningPanelFadeOut"));
        }

        //MainPanel
        if(mainPanel != null)
        {
            iTween.StopByName(mainPanel.gameObject, "introPanelFadeIn");
            iTween.ScaleTo(mainPanel.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "introPanelFadeIn"));
        }
        
        //GenderPanel
        if(genderPanel != null)
        {
            iTween.StopByName(genderPanel.gameObject, "genderPanelFadeOut");
            iTween.ScaleTo(genderPanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "genderPanelFadeOut"));
        }
        
    }
    public void OpenPageOne()
    {
        iTween.MoveTo(page1.gameObject, iTween.Hash("position", middle.position, "time", 1f));
        iTween.MoveTo(page2.gameObject, iTween.Hash("position", new Vector3(1.020f, 1.330f, 0), "time", 1f));
    }
    public void OpenPageTwo()
    {
        iTween.MoveTo(page1.gameObject, iTween.Hash("position", new Vector3(-1.020f, 1.330f, 0), "time", 1f));
        iTween.MoveTo(page2.gameObject, iTween.Hash("position", middle.position, "time", 1f));
    }
}
