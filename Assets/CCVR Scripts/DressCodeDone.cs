using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressCodeDone : MonoBehaviour
{
    [SerializeField]
    private GameObject[] correctParts;
    //Hair, correctTop, correctPants, correctShoes, GearOn, GlovesOn 
    [SerializeField]
    private GameObject[] takeOffParts;
    //wirstOff, necklaceOff

    private playerHUDui showScore;
    [SerializeField]
    private Transform ToNextScenePnl, confirmPnl;

    private static int correctChoice = 0;
    private static int wrongChoice = 0;
    private static int totalChoice;

    private AvatarDressCodePart initializeTransform;

    // Start is called before the first frame update
    void Start()
    {
        totalChoice = correctParts.Length + takeOffParts.Length;
        showScore = FindObjectOfType<playerHUDui>();
        ToNextScenePnl.localScale = Vector3.zero;
        initializeTransform = FindObjectOfType<AvatarDressCodePart>();
    }

    public void DressCode()
    {
        //LocalScale has to be one
        for(int i = 0; i < correctParts.Length; i++)
        {
            if(correctParts[i].transform.localScale == Vector3.one)
            {
                correctChoice++;
            }
            else if(correctParts[i].transform.localScale == Vector3.zero)
            {
                wrongChoice++;
            }
        }
        //LocalScale has to be zero
        for (int i = 0; i < takeOffParts.Length; i++)
        {
            if(takeOffParts[i].transform.localScale == Vector3.zero)
            {
                correctChoice++;
            }
            else if(takeOffParts[i].transform.localScale == Vector3.one)
            {
                wrongChoice++;
            }
        }

        Debug.Log(correctChoice + "(C)/(W)" + wrongChoice);
        string show = "Hint: <color=red> " + wrongChoice + " </color> wrong answer(s)";

        #region Check Result
        if (correctChoice == totalChoice)
        {
            ToNextScenePnl.localScale = Vector3.one;
            showScore.IncreaseScore(correctChoice * 2);
            showScore.ShowMessage("Good job!");
        }
        else if (wrongChoice > 0)
        {
            showScore.DeductScore(wrongChoice);
            ToNextScenePnl.localScale = Vector3.zero;
            confirmPnl.localScale = Vector3.zero;
            initializeTransform.PresentCurrentTransform();
            correctChoice = 0;
            wrongChoice = 0;
            showScore.ShowMessage("You have not identify all the correct dress code. Please try again.\n" + show);
        }
        #endregion Check Result
    }
}