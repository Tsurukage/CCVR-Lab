using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerHUDui : MonoBehaviour
{
    [SerializeField]
    private Image scorePanel;
    [SerializeField]
    private Image messagePanel;
    [SerializeField]
    private Text scoreCounter;
    [SerializeField]
    private Text mmScoreCounter;
    [SerializeField]
    private Text mmHighscoreCounter;

    private int score = 0;
    private int hScore = 0;
    [TextArea]
    public string firstMesssage;
    public Text messageText;

    public GameObject taskBox;
    private bool tBoxIsOpen;

    private AudioManager playSFX;

    // Start is called before the first frame update
    void Start()
    {
        messagePanel.transform.localScale = Vector3.zero;
        score = PlayerPrefs.GetInt("CurrentScore");
        scoreCounter.text = "Score: " + score;
        hScore = PlayerPrefs.GetInt("highscore");
        playSFX = FindObjectOfType<AudioManager>();
        if(firstMesssage != "")
        {
            ShowMessage(firstMesssage);
        }
        
        if(mmScoreCounter != null)
        {
            mmScoreCounter.text = score.ToString();
        }
        if(mmHighscoreCounter != null)
        {
            mmHighscoreCounter.text = hScore.ToString();
        }
        if(taskBox != null)
        {
            taskBox.transform.localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowMessage(string message)
    {
        messageText.text = message;
        FadeInMessage();
        FadeOutMessage();

    }
    private void FadeInMessage()
    {
        iTween.StopByName(messagePanel.gameObject, "fadeInMessage");
        iTween.ScaleTo(messagePanel.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInMessage"));
    }
    private void FadeOutMessage()
    {
        iTween.StopByName(messagePanel.gameObject, "fadeOutMessage");
        iTween.ScaleTo(messagePanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "delay", 4f, "name", "fadeOutMessage"));
    }
    public void IncreaseScore(int increase)
    {
        playSFX.CorrectChoices();
        score += increase;
        scoreCounter.text = "Score: " + score;
        PlayerPrefs.SetInt("CurrentScore", score);
        if(score > hScore)
        {
            hScore = score;
            PlayerPrefs.SetInt("highscore", hScore);
        }
    }
    public void DeductScore(int decrease)
    {
        score -= decrease;
        playSFX.WrongChoices();
        if(score <= 0)
        {
            score = 0;
            scoreCounter.text = "Score: " + score;
        }
        else
        {
            scoreCounter.text = "Score: " + score;
        }
        
        PlayerPrefs.SetInt("CurrentScore", score);
    }
    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("CurrentScore");
        score = PlayerPrefs.GetInt("CurrentScore");
        scoreCounter.text = "Score: " + score;
        mmScoreCounter.text = score.ToString();
    }
    public void ResetHScore()
    {
        PlayerPrefs.DeleteKey("highscore");
        hScore = PlayerPrefs.GetInt("highscore");
        mmHighscoreCounter.text = hScore.ToString();
    }
    
    private void FadeInTaskBox()
    {
        iTween.StopByName(taskBox.gameObject, "fadeInTaskBox");
        iTween.ScaleTo(taskBox.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInTaskBox"));
    }
    private void FadeOutTaskBox()
    {
        iTween.StopByName(taskBox.gameObject, "fadeOutTaskBox");
        iTween.ScaleTo(taskBox.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f,"Delay", 2f, "name", "fadeOutTaskBox"));
    }
    public void ShowTaskBox()
    {
        if (!tBoxIsOpen)
        {
            FadeInTaskBox();
            tBoxIsOpen = true;
        }
        else
        {
            FadeOutTaskBox();
            tBoxIsOpen = false;
        }
    }
}
