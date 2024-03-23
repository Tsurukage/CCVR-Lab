using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AirPurgeCD : MonoBehaviour
{
    [SerializeField]
    private Transform timerCounter;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    public float timer;
    [SerializeField]

    private bool startTimer = false;
    private bool cdDone = false;

    private WorldSpaceMessageBox popMsj;

    // Start is called before the first frame update
    void Start()
    {
        popMsj = FindObjectOfType<WorldSpaceMessageBox>();

        DisplayTime(timer);
        timerCounter.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            if (!cdDone)
            {
                StartTimer();
            }
        }
    }

    void StartTimer()
    {
        timer -= Time.deltaTime;
        DisplayTime(timer);

        if(timer < 0)
        {
            timer = 0;
            DisplayTime(timer);
            timerCounter.localScale = Vector3.zero;
            cdDone = true;
            StartCoroutine(MessageForLastTask());
            PlayerPrefs.SetInt("LastStep", 2);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void StartTheTime()
    {
        startTimer = true;
        timerCounter.localScale = new Vector3(0.01f, 0.01f, 1f);
    }
    IEnumerator MessageForLastTask()
    {
        yield return new WaitForSeconds(6f);
        popMsj.MessagePopUp("You are almost finished! The final step is to switch off the biosafety cabinet in the same manner it was switched on earlier. The steps are similar but in a reversed order. You may do so by clicking on a specific location on the biosafety cabinet.");
    }
}
