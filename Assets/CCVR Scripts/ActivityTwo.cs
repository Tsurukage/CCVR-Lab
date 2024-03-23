//This script begin the activity of identify contamination inside 2 different flasks
//Quizs for contamination
using UnityEngine;

public class ActivityTwo : MonoBehaviour
{
    private GameObject theMsj;
    private WorldSpaceMessageBox msjBox;
    //[SerializeField]
    
    //private GameObject groupOne, groupTwo;

    // Start is called before the first frame update
    void Start()
    {
        theMsj = GameObject.Find("WorldSpaceMBox");
        msjBox = theMsj.GetComponent<WorldSpaceMessageBox>();
        msjBox.MessagePopUp("<color=green>[Module 4] Laboratory 2</color>\nYou cultured two flasks of RAW246.7 cells a few days ago. Let’s check on their condition now. Please transfer those two flasks of cells into the biosafety cabinet.\nYou may look at the <color=green>green spot</color> in front of the incubator to move there.");
        

        //Great job! All the appratus has placed on their respective place inside the BSC. Now, please click on Flask A to observe the cells closely.
        /*groupOne = GameObject.Find("InCabinet");
        groupTwo = GameObject.Find("Atvt2");
        if(PlayerPrefs.GetInt("Transfered") == 1)
        {
            groupTwo.transform.localScale = Vector3.one;
        }
        else if (PlayerPrefs.GetInt("Transfered") == 0)
        {
            groupTwo.transform.localScale = Vector3.zero;
        }*/

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*public void ChangeAprtsGroup()
    {
        groupOne.transform.localScale = Vector3.zero;
        groupTwo.transform.localScale = Vector3.one;
    }*/
    
}
