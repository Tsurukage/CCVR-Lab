using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Button[] sequenceObj;
    public Transform[] quizPanel;
    private playerHUDui showMsg;

    private int quizAnswered;

    // Start is called before the first frame update
    void Start()
    {
        showMsg = FindObjectOfType<playerHUDui>();
        quizAnswered = 0;
        InitializedGO();
    }

    private void InitializedGO()
    {
        for(int i = 0; i < sequenceObj.Length; i++)
        {
            Button goInteractive = sequenceObj[i];

            int goIndex = i;
            goInteractive.onClick.AddListener(() => ResponsePanel(goIndex));
        }
    }
    private void ResponsePanel(int goIndex)
    {
        showMsg.ShowMessage("Turn to your left to answer the quiz.");
        for(int i = 0; i < quizPanel.Length; i++)
        {
            if(quizPanel[i] != null)
            {
                if (i == goIndex)
                {
                    iTween.ScaleFrom(quizPanel[i].gameObject, Vector3.zero, 1f);
                    iTween.ScaleTo(quizPanel[i].gameObject, new Vector3(0.003f, 0.003f, 1), 1f);
                }
                else if (i != goIndex)
                {
                    //iTween.ScaleFrom(quizPanel[i].gameObject, new Vector3(0.003f, 0.003f, 1), 1f);
                    iTween.ScaleTo(quizPanel[i].gameObject, Vector3.zero, 1f);
                }
            }
        }
    }
    public void GetQuizIndex(int indexNum)
    {
        ResponsePanel(indexNum);
    }
}
