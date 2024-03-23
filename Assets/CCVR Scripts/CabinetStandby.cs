using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CabinetStandby : MonoBehaviour
{
    public GameObject timerCounter;
    public Text tCounter;
    public float countDown;
    private ActivityOne panelTggl;
    private CabinetSwitchManager uvOnOff;

    private bool cdFinished;

    // Start is called before the first frame update
    void Start()
    {
        tCounter.text = countDown.ToString("00");
        timerCounter.SetActive(false);
        panelTggl = FindObjectOfType<ActivityOne>();
        uvOnOff = FindObjectOfType<CabinetSwitchManager>();
        //countDown = 15;
        PlayerPrefs.SetInt("CountDown", 0); // 0 = count down has not done
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCounter.activeSelf)
        {
            StartTimer();
        }
    }
    public void StartTimer()
    {
        timerCounter.SetActive(true);
        countDown -= Time.deltaTime;
        DisplayTime(countDown);
        uvOnOff.UpdateText(1);
        
        if(countDown < 0)
        {
            if(!cdFinished)
            timerCounter.SetActive(false);
            countDown = 0;
            cdFinished = true;
            PlayerPrefs.SetInt("CountDown", 1); // 1 = count down has done

            panelTggl.OpenTask2();
            uvOnOff.UpdateText(0);
            PlayerPrefs.SetInt("CleaningTaskDone", 3); //Go to PlayAnimation.cs
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        tCounter.text = string.Format("{0:00}:{1:00} minute(s)", minutes, seconds);
    }
}
