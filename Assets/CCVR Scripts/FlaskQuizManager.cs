using UnityEngine;

public class FlaskQuizManager : MonoBehaviour
{
    [SerializeField]
    private Transform[] quizPanel;

    private playerHUDui showMsg;
    private int answeredQuiz;
    private bool allDone;

    // Start is called before the first frame update
    void Start()
    {
        showMsg = FindObjectOfType<playerHUDui>();
        answeredQuiz = 0;
        allDone = false;
    }
    void Update()
    {
        if (quizPanel[0] == null)
            answeredQuiz = 1;
        if (quizPanel[0] == null && quizPanel[1] == null)
            answeredQuiz = 2;
        if (!allDone)
        {
            if (quizPanel[0] == null && quizPanel[1] == null && quizPanel[2] == null)
                answeredQuiz = 3;
            allDone = true;
        }
        
    }
    public void FlaskAQuiz()
    {
        if(answeredQuiz == 0)
        {
            showMsg.ShowMessage("Turn to your left to answer the quiz");
            iTween.ScaleFrom(quizPanel[0].gameObject, Vector3.zero, 1f);
            iTween.ScaleTo(quizPanel[0].gameObject, new Vector3(0.003f, 0.003f, 1), 1f);
        }
        else if(answeredQuiz == 1)
        {
            showMsg.ShowMessage("You have answered the quiz. Click Flask B to observe closely to answer the quiz");
        }
    }
    public void FlaskBQuiz()
    {
        if(answeredQuiz == 0)
        {
            showMsg.ShowMessage("Please click Flask A to answer the quiz first");
        }
        else if(answeredQuiz == 1)
        {
            showMsg.ShowMessage("Turn to your left to answer the quiz");
            iTween.ScaleFrom(quizPanel[1].gameObject, Vector3.zero, 1f);
            iTween.ScaleTo(quizPanel[1].gameObject, new Vector3(0.003f, 0.003f, 1), 1f);
        }
    }
    public void FlaskA2Quiz()
    {
        if(answeredQuiz == 2)
        {
            showMsg.ShowMessage("Turn to your left to answer the quiz");
            iTween.ScaleFrom(quizPanel[2].gameObject, Vector3.zero, 1f);
            iTween.ScaleTo(quizPanel[2].gameObject, new Vector3(0.003f, 0.003f, 1), 1f);
        }
    }
}
