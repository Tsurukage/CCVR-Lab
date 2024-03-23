using System;
using UnityEngine;
using UnityEngine.UI;

public class DressCodeQuizEvent : MonoBehaviour
{
    [SerializeField]
    private Text questionTextElement;
    [SerializeField]
    private Text responseTextElement;
    //added
    [SerializeField]
    private Image imagePanel;
    [SerializeField]
    private Image largeImage;
    [SerializeField]
    private Button[] buttons;
    [SerializeField]
    private Transform[] parts;
    [SerializeField]
    private Transform[] partsScale;

    [SerializeField]
    private DCQuestion[] questions;
    [SerializeField]
    private int currenAnsweredQuestion;

    [SerializeField]
    private Button nextBtn;

    private int currentQuestionIndex = 0;
    //private int answerIndex = 0;

    private playerHUDui updateScores;
    private AudioManager playSFX;
    private bool quizSessionDone = false;

    #region DCQuestion Variables
    public DCQuestion GetCurrentQuestion()
    {
        var question = questions[currentQuestionIndex];
        return question;
    }
    public DCQuestion GetCurrentImageQuestion()
    {
        var imageq = questions[currentQuestionIndex];
        return imageq;
    }
    public DCQuestion GetCurrentQuestionCorrectAttire()
    {
        var correctDC = questions[currentQuestionIndex];
        return correctDC;
    }
    public DCQuestion GetCurrentQuestionWrongAttire()
    {
        var wrongDC = questions[currentQuestionIndex];
        return wrongDC;
    }
    #endregion DCQuestion Variables

    // Start is called before the first frame update
    public void Start()
    {
        updateScores = FindObjectOfType<playerHUDui>();
        playSFX = FindObjectOfType<AudioManager>();
        currenAnsweredQuestion = 0;
        largeImage.transform.localScale = Vector3.zero;
        InitializeButtons();
        PresentCurrentQuestion();
        nextBtn.onClick.AddListener(MovetoNextQuestionAfterDelay);
        for(int i = 0; i < partsScale.Length; i++)
        {
            partsScale[i].transform.localScale = Vector3.zero;
        }
    }

    //Enlarge Image
    public void EnlargeImage()
    {
        largeImage.transform.localScale = Vector3.one;
    }
    //Diminish Image
    public void DiminishImage()
    {
        largeImage.transform.localScale = Vector3.zero;
    }

    private void PresentCurrentQuestion()
    {
        //DCQuestion Text
        var question = GetCurrentQuestion();
        questionTextElement.text = question.QuestionText;
        //DCQuestion Image
        var imageq = GetCurrentImageQuestion();
        if (imageq.imageQ != null)
        {
            imagePanel.sprite = imageq.imageQ;
            largeImage.sprite = imageq.imageQ;
        }
        else
        {
            imagePanel.gameObject.SetActive(false);
        }
        //DCQuestion Correct Attire
        var correctDC = GetCurrentQuestionCorrectAttire();
        if(correctDC.correctAttire != null)
            correctDC.correctAttire.transform.localScale = Vector3.zero;
        //DCQuestion Wrong Attire
        var wrongDC = GetCurrentQuestionWrongAttire();
        if (wrongDC.wrongAttire != null)
            wrongDC.wrongAttire.transform.localScale = Vector3.one;
        //DCQuestion Response test
        responseTextElement.text = "";
        for (int i = 0; i < parts.Length; i++)
        {
            if(i != currentQuestionIndex)
                parts[i].GetComponent<Outline>().enabled = false;
            else if(i == currentQuestionIndex)
                parts[i].GetComponent<Outline>().enabled = true;
        }
        

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i >= question.answersTex.Length)
            {
                buttons[i].gameObject.SetActive(false);
                buttons[i].transform.parent.gameObject.SetActive(false);
                continue;
            }
            if (i < question.answersTex.Length)
            {
                buttons[i].GetComponentInChildren<Text>().text = question.answersTex[i].Answers;
            }
            buttons[i].gameObject.SetActive(true);
            buttons[i].transform.parent.gameObject.SetActive(true);
        }
    }
    private void InitializeButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];

            int buttonIndex = i;
            button.onClick.AddListener(() => ShowResponses(buttonIndex));
            button.GetComponent<Image>().color = Color.white;
        }
    }
    private void ShowResponses(int buttonIndex)
    {
        var question = GetCurrentQuestion();

        if (question.answersTex[buttonIndex].isCorrectAns == false)
        {
            WrongChoice(buttonIndex);
            CurrentQuizCheck();
        }
        else
        {
            CorrectChoice(buttonIndex);
            CurrentQuizCheck();
            //nextBtn.GetComponent<Button>().interactable = true;
        }

    }
    void CorrectChoice(int buttonIndex)
    {
        var question = GetCurrentQuestion();

        updateScores.IncreaseScore(2);
        responseTextElement.text = question.answersTex[buttonIndex].Responses;
        playSFX.CorrectChoices();
        buttons[buttonIndex].GetComponent<Image>().color = Color.green;
        question.wrongAttire.localScale = Vector3.zero;
        question.correctAttire.localScale = Vector3.one;
    }
    void WrongChoice(int buttonIndex)
    {
        var question = GetCurrentQuestion();

        updateScores.DeductScore(1);
        responseTextElement.text = question.answersTex[buttonIndex].Responses;
        buttons[buttonIndex].GetComponent<Image>().color = Color.red;
        playSFX.WrongChoices();
        //buttons[buttonIndex].GetComponent<Button>().interactable = false;

        question.wrongAttire.localScale = Vector3.zero;
        question.correctAttire.localScale = Vector3.one;
        //nextBtn.GetComponent<Button>().interactable = false;
    }
    void CurrentQuizCheck()
    {
        var question = GetCurrentQuestion();

        if (question.totalAnswerNum > 0)
        {
            question.currentAnswerNum++;
            if (question.currentAnswerNum == question.totalAnswerNum)
            {
                AnsweredNum(question.currentAnswerNum);
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].GetComponent<Button>().interactable = false;
                }
            }
        }
    }
    private void AnsweredNum(int numAnswered)
    {
        var question = GetCurrentQuestion();
        question.currentAnswerNum = +numAnswered;
        if (question.currentAnswerNum == question.totalAnswerNum)
        {
            AnsweredQuestion(1);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    private void AnsweredQuestion(int questAnswered)
    {
        currenAnsweredQuestion += questAnswered;
        Debug.Log(currenAnsweredQuestion + " / " + questions.Length);
        parts[currenAnsweredQuestion - 1].GetComponent<Image>().color = Color.green;
        if (currenAnsweredQuestion == questions.Length)
        {
            quizSessionDone = true;
        }
        else if (currenAnsweredQuestion < questions.Length)
        {
            quizSessionDone = false;
            currentQuestionIndex++;
        }
    }
    private void ResetButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];

            int buttonIndex = i;
            button.GetComponent<Image>().color = Color.white;
            button.GetComponent<Button>().interactable = true;
        }
    }
    private void MovetoNextQuestionAfterDelay()
    {
        if (!quizSessionDone)
        {
            //Debug.Log(currentQuestionIndex);
            PresentCurrentQuestion();
            ResetButtons();
        }
        else
        {
            AvatarActivity quizDone = GameObject.Find("SceneManager").GetComponent<AvatarActivity>();
            quizDone.EndDCQuiz();
            //Destroy(gameObject, 1f);
        }
    }
}

[Serializable]
public class DCQuestion
{
    [TextArea]
    public string QuestionText;
    public Sprite imageQ;
    public DCAnswer[] answersTex;
    public int totalAnswerNum;
    public int currentAnswerNum = 0;
    public Transform correctAttire;
    public Transform wrongAttire;
}
[Serializable]
public class DCAnswer
{
    [TextArea]
    public string Answers;
    public bool isCorrectAns = false;
    [TextArea(2, 10)]
    public string Responses;
}
