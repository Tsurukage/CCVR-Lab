using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubcultureTask : MonoBehaviour
{
    [SerializeField] private int TotalNumSteps = 11;
    public int noStep = -1;
    [TextArea]
    public string[] steps;
    [SerializeField] private Text[] stepsText;
    private bool isdebugged = false;

    [SerializeField] private Text descriptionText;
    ActivityFour aFour;

    // Start is called before the first frame update
    void Start()
    {
        aFour = GameObject.FindObjectOfType<ActivityFour>();
        noStep = -1;
        PresentTask();
        descriptionText.text = steps[0];
        isdebugged = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (noStep < 0)
        {
            for (int i = 0; i < TotalNumSteps + 1; i++)
            {
                stepsText[i].color = new Color(0.196f, 0.196f, 0.196f);
            }
        }
        else if (noStep == TotalNumSteps)
        {
            //aFour.UpdateChange();
            stepsText[noStep].color = Color.green;
            if (!isdebugged)
            {
                Debug.Log("Subculture Done");
                isdebugged = true;
            }
        }
        else
        {
            stepsText[noStep].color = Color.green;
        }
    }

    private void DisplayTaskStep(int nextStep)
    {
        descriptionText.text = steps[nextStep];
    }

    //Present all steps
    private void PresentTask()
    {
        for(int i = 0; i < stepsText.Length; i++)
        {
            if(i >= steps.Length)
            {
                stepsText[i].gameObject.SetActive(false);
                continue;
            }
            if(i < steps.Length)
            {
                stepsText[i].text = steps[i];
            }
            stepsText[i].gameObject.SetActive(true);
            stepsText[i].fontSize = 11;
        }
    }
    #region Unused
    //Click for task 1, display Task 2 when done
    public void TaskOneA()
    {
        if(noStep < 0)
        {
            noStep++;
        }
        if(noStep == 0)
        {
            DisplayTaskStep(1);
            Debug.Log("Please continue to the next step");
        }
    }
    //Task 1 is done, click for Task 2, display Task 3 when done
    public void TaskTwo()
    {
        if(noStep == 0)
        {
            noStep++;
        }

        if(noStep == 1)
        {
            DisplayTaskStep(2);
            Debug.Log("You have done the step, please proceed.");
        }
        else
        {
            Debug.Log("Please conduct the previous step before this.");
        }
        
    }
    //Task 2 is done, click for Task 3, display Task 4 when done
    public void TaskThree()
    {
        if (noStep == 1)
        {
            noStep++;
        }

        if (noStep == 2)
        {
            DisplayTaskStep(3);
            Debug.Log("You have done the step, please proceed.");
        }
        else
        {
            Debug.Log("Please conduct the previous step before this.");
        }
    }
    #endregion Unused
    #region Universal Task Method
    //Task (currenTask - 1) is done, click for currentTask, display (currentTask + 1) when done
    //currentTask start with 1
    //For example if Task(1), means noStep = 0;
    public void Task(int currentTask)
    {
        if (noStep == currentTask - 2)
        {
            noStep++;
        }

        if (noStep == currentTask - 1)
        {
            DisplayTaskStep(currentTask);            
            //Debug.Log("You have done the step, please proceed.");
        }
        else if(noStep < currentTask - 1)
        {
            Debug.Log("Please conduct the previous step before this.");
        }
        else if(noStep > currentTask - 1)
        {
            Debug.Log("You have done this step. Please proceed");
        }
    }
    #endregion Universal Task Method
}
