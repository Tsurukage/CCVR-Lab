using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuizEvent : MonoBehaviour
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
    private Question[] questions;
    [SerializeField]
    private int currenAnsweredQuestion;
    [Tooltip("This question has multiple answers")]
    [SerializeField]
    private int currentAnsweredNumberIs;
    [Tooltip("The total answers of this question")]
    [SerializeField]
    private int cTotalAnsweredIs;

    [SerializeField]
    private Button nextBtn;

    private int currentQuestionIndex = 0;
    //private int answerIndex = 0;

    private playerHUDui updateScores;
    private AudioManager playSFX;
    private bool quizSessionDone = false;

    public Question GetCurrentQuestion()
    {
        var question = questions[currentQuestionIndex];
        return question;
    }
    //Added
    public Question GetCurrentImageQuestion()
    {
        var imageq = questions[currentQuestionIndex];
        return imageq;
    }
    public Question GetCurrentQuestionAnswerNumber()
    {
        var answerNum = questions[currentQuestionIndex];
        return answerNum;
    }

    // Start is called before the first frame update
    public void Start()
    {
        updateScores = FindObjectOfType<playerHUDui>();
        playSFX = FindObjectOfType<AudioManager>();
        currenAnsweredQuestion = 0;
        gameObject.transform.localScale = Vector3.zero;
        largeImage.transform.localScale = Vector3.zero;
        InitializeButtons();
        PresentCurrentQuestion();
        nextBtn.onClick.AddListener(MovetoNextQuestionAfterDelay);
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
    //Open Panel
    /*public void OpenPanel()
    {
        iTween.StopByName(gameObject, "QuizPanel");
        iTween.ScaleTo(gameObject, iTween.Hash("scale", Vector3.Scale(new Vector3(0.003f, 0.003f, 1f), new Vector3(1, 1, 1)), "time", 1f, "name", "QuizPanel"));
        //gameObject.transform.localScale = Vector3.Scale(new Vector3(0.002f, 0.002f, 1f), new Vector3(1, 1, 1));
    }*/
    private void PresentCurrentQuestion()
    {
        var question = GetCurrentQuestion();
        questionTextElement.text = question.QuestionText;
        var imageq = GetCurrentImageQuestion();
        if(imageq.imageQ != null)
        {
            imagePanel.sprite = imageq.imageQ;
            largeImage.sprite = imageq.imageQ;
        }
        else
        {
            imagePanel.gameObject.SetActive(false);
        }
        var answerNum = GetCurrentQuestionAnswerNumber();
        currentAnsweredNumberIs = answerNum.currentAnswerNum; //Latest
        cTotalAnsweredIs = answerNum.totalAnswerNum;
        for(int i = 0; i < buttons.Length; i++)
        {
            if(i >= question.answersTex.Length)
            {
                buttons[i].gameObject.SetActive(false);
                buttons[i].transform.parent.gameObject.SetActive(false);
                continue;
            }
            if(i < question.answersTex.Length)
            {
                buttons[i].GetComponentInChildren<Text>().text = question.answersTex[i].Answers;
            }
            buttons[i].gameObject.SetActive(true);
            buttons[i].transform.parent.gameObject.SetActive(true);
            //buttons[i].GetComponentInChildren<Text>().text = answerTex[i];
        }
    }
    private void InitializeButtons()
    {
        for(int i = 0; i < buttons.Length; i++)
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

        if(question.answersTex[buttonIndex].isCorrectAns == false)
        {
            updateScores.DeductScore(1);
            responseTextElement.text = question.answersTex[buttonIndex].Responses;
            buttons[buttonIndex].GetComponent<Image>().color = Color.red;
            playSFX.WrongChoices();
            buttons[buttonIndex].GetComponent<Button>().interactable = false;
            //nextBtn.GetComponent<Button>().interactable = false;
        }
        else
        {
            updateScores.IncreaseScore(1);
            responseTextElement.text = question.answersTex[buttonIndex].Responses;
            playSFX.CorrectChoices();
            buttons[buttonIndex].GetComponent<Image>().color = Color.green;
            buttons[buttonIndex].GetComponent<Button>().interactable = false;

            if (question.totalAnswerNum > 0)
            {
                question.currentAnswerNum++;
                currentAnsweredNumberIs = question.currentAnswerNum;
                if (question.currentAnswerNum == question.totalAnswerNum)
                {
                    AnsweredNum(question.currentAnswerNum);
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        buttons[i].GetComponent<Button>().interactable = false;
                    }
                }
            }
            nextBtn.GetComponent<Button>().interactable = true;
        }
        
    }
    private void AnsweredNum(int numAnswered)
    {
        var question = GetCurrentQuestion();
        question.currentAnswerNum =+ numAnswered;
        if(question.currentAnswerNum == question.totalAnswerNum)
        {
            AnsweredQuestion(1);
            for(int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }
    }
    private void AnsweredQuestion(int questAnswered)
    {
        currenAnsweredQuestion += questAnswered;
        Debug.Log(currenAnsweredQuestion + " / "+ questions.Length);
        if(currenAnsweredQuestion == questions.Length)
        {
            quizSessionDone = true;
            //nextBtn.onClick.AddListener(LateCall);
        }
        else if(currenAnsweredQuestion < questions.Length)
        {
            quizSessionDone = false;
            currentQuestionIndex++;
            //nextBtn.onClick.AddListener(() => MovetoNextQuestionAfterDelay());
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
        if(currentAnsweredNumberIs == 0)
        {
            //Do nothing here
        }
        else if(currentAnsweredNumberIs > 0 && currentAnsweredNumberIs < cTotalAnsweredIs)
        {
            updateScores.ShowMessage("You haven't finished answer this question.");
        }
        else if(currentAnsweredNumberIs == cTotalAnsweredIs)
        {
            if (!quizSessionDone)
            {
                PresentCurrentQuestion();
                ResetButtons();
            }
            else
            {
                Destroy(gameObject, 1f);
            }
        }
    }
}

[Serializable]
public class Question
{
    [TextArea]
    public string QuestionText;
    public Sprite imageQ;
    public Answer[] answersTex;
    public int totalAnswerNum;
    public int currentAnswerNum = 0;

}
[Serializable]
public class Answer
{
    [TextArea]
    public string Answers;
    public bool isCorrectAns = false;
    [TextArea(2, 10)]
    public string Responses;
}