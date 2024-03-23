using UnityEngine;
using System.Collections;

public class LastCorrectChoice : MonoBehaviour
{
    [SerializeField]
    private Transform handPos;

    private WorldSpaceMessageBox popMsj;
    private AudioManager playSFX;

    private AirPurgeCD startAP;

    private bool lastChoice = false;

    // Start is called before the first frame update
    void Start()
    {
        handPos = transform.Find("/Player/Main Camera/HandPos2");
        popMsj = FindObjectOfType<WorldSpaceMessageBox>();
        playSFX = FindObjectOfType<AudioManager>();
        startAP = FindObjectOfType<AirPurgeCD>();
        popMsj.MessagePopUp("<color=Green>[Module 6] Laboratory 4</color>\nAll three flasks have been placed in the incubator after proper labelling. Now, remove all the items in the biosafety cabinet by clicking on the items, one by one.");
        PlayerPrefs.SetInt("LastStep", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!lastChoice)
        {
            if(PlayerPrefs.GetInt("AllClear") == 0)
            {
                //Do nothing
            }
            if(PlayerPrefs.GetInt("AllClear") == 1)
            {
                if (handPos.childCount > 0 && handPos.GetChild(0).name.Contains("Spray"))
                {
                    playSFX.CorrectChoices();
                    popMsj.MessagePopUp("You are absolutely right! Do not forget to swab the work surface liberally with 70% ethanol in an inward to outward direction after using the biosafety cabinet.");
                    lastChoice = true;
                    PlayerPrefs.SetInt("LastStep", 1);
                    //After spray bottle is picked up
                    //LastStep playerpref goes to PlayAnimation.cs and CabinetSwitchManager.cs
                    StartCoroutine(StartAirPurge());
                }
            }
        }
    }
    IEnumerator StartAirPurge()
    {
        yield return new WaitForSeconds(6f);
        startAP.StartTheTime();
        popMsj.MessagePopUp("Before we switch off the biosafety cabinet, we should allow the air to purge for a while.");
    }
}
