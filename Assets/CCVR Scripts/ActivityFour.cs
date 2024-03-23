using UnityEngine;

public class ActivityFour : MonoBehaviour
{
    [SerializeField] private GameObject quizPnl1, subcultureTaskIndi;
    [SerializeField] private GameObject subcultureSet, subcultureDoneSet;
    [SerializeField] private GameObject endBtn, incubatorUI, lastQuiz;
    bool executed, executed2;

    //[SerializeField] private GameObject spray1, spray1Patch, spray2, spray2Patch;

    private playerHUDui sMessage;
    private QuizManager showQuiz;
    private WorldSpaceMessageBox msjPop;
    private AudioManager playSFX;

    // Start is called before the first frame update
    void Start()
    {
        executed = false;
        executed2 = false;
        sMessage = FindObjectOfType<playerHUDui>();
        showQuiz = FindObjectOfType<QuizManager>();
        playSFX = FindObjectOfType<AudioManager>();
        quizPnl1 = GameObject.Find("FlaskA_Quiz2");

        if (endBtn != null)
            endBtn.transform.localScale = Vector3.zero;
        if (incubatorUI != null)
            incubatorUI.transform.localScale = Vector3.zero;

        subcultureSet.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        subcultureTaskIndi.transform.localScale = Vector3.one;
        if(subcultureDoneSet != null)
            subcultureDoneSet.transform.localScale = Vector3.zero;
        
        /*spray1.transform.localScale = Vector3.one;
        spray1Patch.transform.localScale = Vector3.one;
        spray2.transform.localScale = Vector3.zero;
        spray2Patch.transform.localScale = Vector3.zero;*/
    }

    // Update is called once per frame
    void Update()
    {
        if(quizPnl1 == null)
        {
            if (!executed)
            {
                //apparatusGrp1.transform.localScale = Vector3.zero;
                //apparatusGrp2.transform.localScale = Vector3.one;
                //subcultureTask.transform.localScale = new Vector3(0.003f, 0.003f);
                //subcultureTaskIndi.transform.localScale = Vector3.one;
                executed = true;
            }
        }
        if(lastQuiz == null)
        {
            if (!executed2)
            {
                ObserveCells oCell = FindObjectOfType<ObserveCells>();
                oCell.ActiveCollider();
                msjPop.MessagePopUp("Good. Now, click on any of the flask to collect all the flasks.");
                executed2 = true;
            }
        }
    }
    public void UpdateChange()
    {
        subcultureSet.transform.localScale = Vector3.zero;
        subcultureDoneSet.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        subcultureTaskIndi.transform.localScale = Vector3.zero;
        showQuiz.GetQuizIndex(3);
    }
    public void EndActivityButton()
    {
        incubatorUI.transform.localScale = new Vector3(0.0011f, 0.0011f);
        endBtn.transform.localScale = new Vector3(0.0011f, 0.0011f);
        playSFX.PopNitification();
    }
    /*
    public void SwitchSprayBottle()
    {
        spray1.transform.localScale = Vector3.zero;
        spray2Patch.transform.localScale = Vector3.zero;
        spray2.transform.localScale = Vector3.one;
        spray2Patch.transform.localScale = Vector3.zero;
    }*/
}
