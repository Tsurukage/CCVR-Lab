using UnityEngine;
using UnityEngine.UI;

public class PutDown : MonoBehaviour
{
    [SerializeField]
    private Transform handPos;
    [SerializeField]
    private GameObject[] patchesInCab;
    private int currentTask = 0;
    private int totalNumTask = 5;

    [SerializeField]
    private Text taskHolder;
    [SerializeField]
    private bool transferedDone = false;
    [SerializeField]
    private Transform nextSceneBtn;

    // Start is called before the first frame update
    void Start()
    {
        handPos = transform.Find("/Player/Main Camera/HandPos2");
        transferedDone = false;
        InitializePatches();
        if (taskHolder != null)
            taskHolder.text = currentTask + "/" + totalNumTask;
        if(nextSceneBtn != null)
            nextSceneBtn.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!transferedDone)
            this.enabled = true;
        else
            this.enabled = false;

        if (currentTask == totalNumTask)
        {
            transferedDone = true;
            nextSceneBtn.localScale = new Vector3(0.0011f, 0.0011f, 1f);
            Destroy(this, 2f);
        }
        else if (currentTask != totalNumTask)
            transferedDone = false;

        if(handPos.childCount > 0 && handPos.childCount < 2)
        {
            if (handPos.GetChild(0).name.Contains("Spray"))
            {
                InitializePatches();
            }
            else if (!handPos.GetChild(0).name.Contains("Spray"))
            {
                ShowPacthesInCab();
            }
        }
        else if(handPos.childCount == 0)
        {
            InitializePatches();
        }
    }

    public void TaskIncrease(int task)
    {
        currentTask++;
        taskHolder.text = currentTask + "/" + totalNumTask;
    }

    public void InitializePatches()
    {
        if(patchesInCab.Length != 0)
        {
            for (int i = 0; i < patchesInCab.Length; i++)
            {
                patchesInCab[i].transform.localScale = Vector3.zero;
            }
        }
        
    }
    public void ShowPacthesInCab()
    {
        if (patchesInCab.Length != 0)
        {
            for (int i = 0; i < patchesInCab.Length; i++)
            {
                patchesInCab[i].transform.localScale = new Vector3(0.1f, 0.001f, 0.1f);
            }
        }
        
    }
}
