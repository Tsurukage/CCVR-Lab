using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityThree : MonoBehaviour
{
    [SerializeField]
    private GameObject QuizAPnl, QuizBPnl, QuizCPnl;
    private bool hasChanged = false, first = false, second = false, bfirst = false;
    [SerializeField]
    private Transform nextSceneBtn;
    private WorldSpaceMessageBox msjBox;

    [SerializeField]
    private Transform flasksA, flasksB;
    [SerializeField]
    private Transform flaskGroupIncubator;
    [SerializeField]
    private GameObject flaskPrefab;
    private GameObject newInstance;
    [SerializeField]
    private Transform patchInBSC;
    [SerializeField]
    private Transform handPos;

    // Start is called before the first frame update
    void Start()
    {
        hasChanged = false;
        first = false;
        second = false;
        QuizAPnl = GameObject.Find("FlaskA_Quiz");
        QuizBPnl = GameObject.Find("FlaskB_Quiz");
        QuizCPnl = GameObject.Find("FlaskA_Quiz2");

        msjBox = FindObjectOfType<WorldSpaceMessageBox>();
        nextSceneBtn.localScale = Vector3.zero;

        handPos = transform.Find("/Player/Main Camera/HandPos2");
        if (flasksA != null)
            flasksA.localScale = Vector3.zero;
        if (flasksB != null)
            flasksB.localScale = Vector3.zero;
        if (flaskGroupIncubator != null)
            flaskGroupIncubator.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        if (patchInBSC != null)
            patchInBSC.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!first)
        {
            if (QuizAPnl == null)
            {
                msjBox.MessagePopUp("Now, click on Flask B to observe the cell closely.");
                first = true;
            }
        }
        
        if (!hasChanged)
        {
            if (QuizAPnl == null && QuizBPnl == null)
            {
                flasksA.transform.localScale = Vector3.zero;
                flasksB.transform.localScale = Vector3.one;
                //flaskAPanel.transform.localScale = Vector3.one;
                msjBox.MessagePopUp("Contaminated Flask B had been discarded. The type of contaminant as well as the actions which could lead to contaimination had been identified too.\n Let's focus on Flask A now! The cells in Flask A has reached 90% confluency and should be subcultured as soon as possible. The protocol can be found in the data sheet provided by the manufacturer. Click on the data sheet and read the details carefully. \n <color=red> After reading the information in the data sheet, click on Flask A to continue.</color>");
                hasChanged = true;
            }
        }

        if (!second)
        {
            if (QuizCPnl == null)
            {
                nextSceneBtn.localScale = new Vector3(0.0011f, 0.0011f, 1f);
                second = true;
            }
        }
    }
    public void TakeFlasks()
    {
        if (handPos.childCount == 0)
        {
            newInstance = Instantiate(flaskPrefab, handPos.position, flaskPrefab.transform.rotation, handPos);
            newInstance.name = newInstance.name.Replace("(Clone)", "");
            patchInBSC.localScale = new Vector3(0.1f, 0.001f, 0.1f);
            flaskGroupIncubator.localScale = Vector3.zero;
        }
    }
    public void PutFlaskInBSC()
    {
        if (handPos.childCount > 0)
        {
            Destroy(newInstance);
            patchInBSC.localScale = Vector3.zero;
            flasksA.localScale = Vector3.one;
            msjBox.MessagePopUp("Great! Now, click on flask A to observe the cell closely.");
        }
    }
    public void QuizSequence()
    {
        if (QuizAPnl != null && QuizBPnl != null)
        {
            msjBox.MessagePopUp("Please click Flask A fisrt before Flask B.");
        }
        else if (QuizAPnl == null && QuizBPnl != null)
        {
            msjBox.MessagePopUp("Now, click on Flask B to observe the cell closely.");
        }
    }
}
