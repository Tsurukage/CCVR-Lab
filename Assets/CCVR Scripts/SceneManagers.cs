using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagers : MonoBehaviour
{
    private playerHUDui showMessage;
    [SerializeField]
    private Button[] levelBtns;

    // Start is called before the first frame update
    void Start()
    {
        showMessage = GameObject.FindObjectOfType<playerHUDui>();
        if(levelBtns != null)
            InitializeSceneBtn();
    }

    private void InitializeSceneBtn()
    {
        for(int i = 0; i < levelBtns.Length; i++)
        {
            Button sceneBtn = levelBtns[i];

            int sceneIndex = i;
            sceneBtn.onClick.AddListener(() => ResponceScene(sceneIndex));
        }
    }
    private void ResponceScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex + 3);
        //print(sceneIndex + 2);
        print(SceneManager.GetSceneByBuildIndex(sceneIndex + 3).name);
    }

    public void ToLab()
    {
        SceneManager.LoadScene("SmallLabScene");
    }

    public void toMaleScn()
    {
        SceneManager.LoadScene("MaleScene");
    }
    public void toFemaleScn()
    {
        SceneManager.LoadScene("FemaleScene");
    }
    public void ToCabinetRoom()
    {
        SceneManager.LoadScene("CabinetsScene");
    }
    public void ToGenderSelectionRoom()
    {
        SceneManager.LoadScene(3);
    }
    public void ToSceneWithIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void ResetScene()
    {
        //SceneManager.LoadScene(0);
        //SceneManager.LoadScene("NewMainMenu");
        showMessage.ResetScore();
    }
    public void ResetScoreAndHScore()
    {
        //SceneManager.LoadScene(0);
        ResetScene();
        showMessage.ResetHScore();
    }
    public void backToMM()
    {
        SceneManager.LoadScene("NewMainMenu");
    }
    public void QuitApp()
    {
        Debug.Log("EXIT!");
        Application.Quit();
    }
    public void CorrectAnswer()
    {
        showMessage.ShowMessage("The answer is CORRECT!");
        showMessage.IncreaseScore(2);        
    }
    public void WrongAnswer()
    {
        showMessage.ShowMessage("The answer is WRONG!");
        showMessage.DeductScore(1);
    }
}
