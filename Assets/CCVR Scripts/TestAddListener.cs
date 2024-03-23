using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAddListener : MonoBehaviour
{
    public Button increase;
    int textNum;

    // Start is called before the first frame update
    void Start()
    {
        increase.onClick.AddListener(IncreasValue);
    }

    private void IncreasValue()
    {
        textNum++;
        Debug.Log(textNum);
    }
}
