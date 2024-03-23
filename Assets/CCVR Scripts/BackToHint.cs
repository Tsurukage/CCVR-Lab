using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToHint : MonoBehaviour
{
    [SerializeField]
    private Transform confirmPanel, hintPanel;
    [SerializeField]
    private Button hintBtn, confirmBtn;

    private bool hintIsEnabled, panelIsEnabled;

    void Start()
    {
        SetButton();
    }
    private void SetButton()
    {

        Button btn1 = hintBtn;
        Button btn2 = confirmBtn;

        btn1.onClick.AddListener(GetHintPanel);
        btn2.onClick.AddListener(ConfirmPanel);

        hintIsEnabled = false;
        hintPanel.gameObject.SetActive(hintIsEnabled);
        panelIsEnabled = false;
        confirmPanel.gameObject.SetActive(panelIsEnabled);
    }
    private void GetHintPanel()
    {
        hintIsEnabled ^= true;
        hintPanel.gameObject.SetActive(hintIsEnabled);
    }
    private void ConfirmPanel()
    {
        panelIsEnabled ^= true;
        confirmPanel.gameObject.SetActive(panelIsEnabled);
    }
}
