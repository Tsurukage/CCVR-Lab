using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChsTheCorrectAprts : MonoBehaviour
{
    public Text instruction, questn;
    [TextArea]
    public string instrucs, qstn;
    [SerializeField]
    private Transform hndParent;
    private bool isDebugged = false;
    private ActivityOne sndMsj;
    private playerHUDui score;
    private AudioManager playAD;
    private WorldSpaceMessageBox popMessage;

    // Start is called before the first frame update
    void Start()
    {
        if(instruction != null)
        {
            instruction.text = instrucs;
            instruction.color = Color.black;
        }
        if(questn != null)
        {
            questn.text = qstn;
            questn.color = new Color(0, 117f / 255f, 166f / 255f);
        }
        hndParent = transform.Find("/Player/Main Camera/HandPos2");
        sndMsj = FindObjectOfType<ActivityOne>();
        score = FindObjectOfType<playerHUDui>();
        playAD = FindObjectOfType<AudioManager>();
        popMessage = FindObjectOfType<WorldSpaceMessageBox>();
        PlayerPrefs.SetInt("SprayHasPicked", 0); // 0 = has not picked
    }

    // Update is called once per frame
    void Update()
    {
        if (hndParent.transform.childCount == 0)
        {
            if (PlayerPrefs.GetInt("SprayHasPicked") == 0)
            {
                if (instruction != null)
                {
                    instruction.text = instrucs;
                    instruction.color = Color.black;
                }
                isDebugged = false;
                if (questn != null)
                {
                    questn.text = qstn;
                    questn.color = new Color(0, 117f / 255f, 166f / 255f);
                }
            }
            else if(PlayerPrefs.GetInt("SprayHasPicked") == 1)
            {
                isDebugged = true;
            }
        }

        else if (hndParent.transform.GetChild(0).name == "Spray Bottle")
        {
            if (!isDebugged)
            {
                if(instruction != null)
                {
                    instruction.text = "Spray Bottle with 70% Ethanol";
                    instruction.color = new Color(11f / 255f, 120f / 255f, 24f / 255f, 1);
                }
                Debug.Log("Correct!");
                playAD.CorrectChoices();
                if(questn != null)
                {
                    questn.text = "Well done, always remember to clean the work surface with 70% ethanol to ensure it is free from contamination.";
                    questn.color = Color.blue;
                }
                isDebugged = true;
                StartCoroutine(UpText());
                score.IncreaseScore(1);
                PlayerPrefs.SetInt("SprayHasPicked", 1); // 1 = has picked
            }
        }

        else if (hndParent.transform.GetChild(0).name != "Spray Bottle")
        {
            
            if (!isDebugged)
            {
                if (instruction != null)
                {
                    instruction.text = hndParent.transform.GetChild(0).name;
                    instruction.color = Color.red;
                }
                Debug.Log("False!");
                playAD.WrongChoices();
                if(questn != null)
                {
                    questn.text = "No, this is not the required apparatus. Try again.";
                    questn.color = Color.red;
                }
                isDebugged = true;
                score.DeductScore(1);
            }
        }

        if(PlayerPrefs.GetInt("CleaningTaskDone") == 6)
        {
            popMessage.MessagePopUp("Now, you may transfer the items to their suitable locations. The items are placed differently according to the dominant hand. Please assume that you are right-handed for this purpose.");
            PlayerPrefs.SetInt("CleaningTaskDone", 7);
        }
    }
    IEnumerator UpText()
    {
        yield return new WaitForSeconds(5f);
        playAD.PopNitification();
        if(instruction != null)
        {
            instruction.text = "Well done!";
            instruction.color = Color.black;
        }
        if(questn != null)
        {
            questn.text = "Now, put down the spray and wait for the timer to finish counting down. When the cabinet is ready, you <color=green>must spray the appratus before transferring them into the biosafety cabinet</color>.";
            questn.color = new Color(0, 117f / 255f, 166f / 255f);
        }
        
    }
}
