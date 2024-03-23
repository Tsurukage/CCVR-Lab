using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoDisplay : MonoBehaviour
{
    [SerializeField]
    private string titleText;
    [SerializeField]
    private Text titleHolder, descriptionHolder;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Information[] informations;
    
    // Start is called before the first frame update
    void Start()
    {
        titleHolder.text = titleText;
        PresentInformation();
        ButtonsInitialized();
        descriptionHolder.text = "Click the tab above to view the content.";
        descriptionHolder.fontStyle = FontStyle.Italic;
        descriptionHolder.color = Color.grey;
        descriptionHolder.resizeTextForBestFit = false;
    }

    private void PresentInformation()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            if(i >= informations.Length)
            {
                buttons[i].gameObject.SetActive(false);
                continue;
            }
            if(i < informations.Length)
            {
                buttons[i].GetComponentInChildren<Text>().text = informations[i].subtitle;
            }
            buttons[i].gameObject.SetActive(true);
        }
    }
    private void ButtonsInitialized()
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];

            int buttonIndex = i;
            button.onClick.AddListener(() => ShowResponsesContent(buttonIndex));
        }
    }
    private void ShowResponsesContent(int buttonIndex)
    {
        descriptionHolder.text = informations[buttonIndex].content;
        int wordCount = informations[buttonIndex].content.Split(' ').Length;
        Debug.Log(wordCount);
        descriptionHolder.color = Color.black;
        descriptionHolder.fontStyle = FontStyle.Normal;
        if(wordCount > 20)
        {
            descriptionHolder.resizeTextForBestFit = true;
        }
        else
        {
            descriptionHolder.resizeTextForBestFit = false;
            descriptionHolder.fontSize = 12;
        }
        
    }
}
[Serializable]
public class Information
{
    [TextArea]
    public string subtitle;
    [TextArea]
    public string content;
}
