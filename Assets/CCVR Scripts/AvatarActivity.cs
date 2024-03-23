using UnityEngine;

public class AvatarActivity : MonoBehaviour
{
    private WorldSpaceMessageBox popMsg;
    [SerializeField]
    private Transform dc_IntroPanel;
    [SerializeField]
    private Transform dc_quizPanel;

    private AudioManager playSFX;

    // Start is called before the first frame update
    void Start()
    {
        popMsg = GameObject.Find("WorldSpaceMBox").GetComponent<WorldSpaceMessageBox>();
        playSFX = GameObject.Find("AudioSource").GetComponent<AudioManager>();
        MessageToPlayer();
        if (dc_IntroPanel != null)
            dc_IntroPanel.localScale = new Vector3(0.0025f, 0.0025f);
        if (dc_quizPanel != null)
            dc_quizPanel.localScale = Vector3.zero;
    }

    public void MessageToPlayer()
    {
        popMsg.MessagePopUp("<color=Green>[Module 1] Dress Code</color>\nWelcome! \n\n It is compulsory to follow specific dress code when working in a cell culture facility. The dress code has a dual purpose of protecting the person as well as the product. \n\n <color=green>Please identify the appropriate dress code before entering a cell culture room.</color>");
    }

    public void StartDCQuiz()
    {
        FadeInQuizPanel();
        FadeOutIntroPanel();
        playSFX.PopNitification();
    }
    public void EndDCQuiz()
    {
        FadeOutQuizPanel();
        FadeInIntroPanel();
        playSFX.PopNitification();
        dc_IntroPanel.GetChild(1).localScale = Vector3.zero;
    }

    void FadeInQuizPanel()
    {
        iTween.StopByName(dc_quizPanel.gameObject, "DCquizPanel");
        iTween.ScaleTo(dc_quizPanel.gameObject, iTween.Hash("scale", new Vector3(0.004f, 0.004f), "time", 1f, "name", "DCquizPanel"));
    }
    void FadeOutQuizPanel()
    {
        iTween.StopByName(dc_quizPanel.gameObject, "DCquizPanel");
        iTween.ScaleTo(dc_quizPanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "DCquizPanel"));
    }

    void FadeInIntroPanel()
    {
        iTween.StopByName(dc_IntroPanel.gameObject, "DCintroPanel");
        iTween.ScaleTo(dc_IntroPanel.gameObject, iTween.Hash("scale", new Vector3(0.0025f, 0.0025f), "time", 1f, "name", "DCintroPanel"));
    }
    void FadeOutIntroPanel()
    {
        iTween.StopByName(dc_IntroPanel.gameObject, "DCintroPanel");
        iTween.ScaleTo(dc_IntroPanel.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "DCintroPanel"));
    }
}
