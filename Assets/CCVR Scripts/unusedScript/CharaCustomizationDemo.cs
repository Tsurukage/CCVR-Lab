using System;
using UnityEngine;
using UnityEngine.UI;

public class CharaCustomizationDemo : MonoBehaviour
{
    [SerializeField]
    private Text moduleTitle;
    [SerializeField]
    private Text Q_Text;
    [SerializeField]
    private string m_Title;
    [SerializeField]
    [TextArea]
    private string Question;
    [SerializeField]
    private GameObject buttonSet;//casualLook, standarDressCode,//
    [SerializeField]
    private Button change1, change2, backToHint, confirmBtn;

    [SerializeField]
    private Button[] avatarParts;

    [SerializeField]
    private Attire[] attires;

    // Start is called before the first frame update
    void Start()
    {
        if (buttonSet != null)
        {
            buttonSet.gameObject.SetActive(false);
        }
        confirmBtn.gameObject.SetActive(true);
        /*if(change1 != null && change2 != null)
        {
            change1.gameObject.SetActive(false);
            change2.gameObject.SetActive(false);
        }*/
        //SetButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void InitializeButtonsForAvatar()
    {
        for(int i = 0; i < avatarParts.Length; i++)
        {
            Button button = avatarParts[i];
        }
    }
    /*private void SetButton()
    {
        Button button1 = change1;
        Button button2 = change2;

        button1.onClick.AddListener(changeToStandard);
        button2.onClick.AddListener(changeToCasual);
    }*/
    public void showHeadQuest()
    {
        //backToHint.gameObject.SetActive(true);
        //confirmBtn.gameObject.SetActive(t);
        moduleTitle.text = m_Title;
        moduleTitle.color = new Color(71f/255f, 243f/255f, 1, 1);
        Q_Text.text = Question;
        Q_Text.color = new Color(158f/255f, 255f/255f, 103f/255f, 255f/255f);
        buttonSet.SetActive(true);
        //change1.gameObject.SetActive(true);
        //change2.gameObject.SetActive(true);
    }
    /*public void changeToStandard()
    {
        if(casualLook != null || standarDressCode != null)
        {
            standarDressCode.gameObject.SetActive(true);
            casualLook.gameObject.SetActive(false);
        }
        
    }
    public void changeToCasual()
    {
        if (casualLook != null || standarDressCode != null)
        {
            standarDressCode.gameObject.SetActive(false);
            casualLook.gameObject.SetActive(true);
        }
    }*/
    public void CheckResult()
    {

    }
}
[Serializable]
public class Attire
{
    public GameObject attire;
    public bool correct;
}
