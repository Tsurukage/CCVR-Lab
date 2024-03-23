using UnityEngine;
using UnityEngine.UI;

public class SubcultureAtvt : MonoBehaviour
{
    public Transform handPos, pipettePos;
    public GameObject apparatusOri, apparatusPrefab, physicsPrefab1, scrapperPrefab, physicsPrefab2;
    public GameObject wasteLiquid;

    public GameObject newInstance, clickedObject;
    private SubcultureTask numStep;
    //public Sprite flask1Img, flask2Img, Flask3Img;
    public Image imgHolder;

    private bool isDebugged = false;
    public bool handHasChild = false;

    [SerializeField]
    private int steps = 0;

    // Start is called before the first frame update
    void Start()
    {
        handPos = transform.Find("/Player/Main Camera/HandPos2");
        numStep = GameObject.FindObjectOfType<SubcultureTask>();
        steps = 0;
        if(imgHolder != null)
        {
            imgHolder.transform.parent.localScale = Vector3.zero;
        }
    }

    public void ReceivedGameObject(GameObject clickedOb)
    {
        clickedObject = clickedOb;
    }
    #region Task Pipette
    //Pipette
    public void PipetteTask()
    {
        if(numStep.noStep < 0)
        {
            if (handPos.childCount > 0)
            {
                Debug.Log("Put down the item in your hand first.");
            }
            else
            {
                newInstance = Instantiate(apparatusPrefab, handPos.position, apparatusPrefab.transform.rotation, handPos);
                newInstance.name = newInstance.name.Replace("(Clone)", "");
                clickedObject.transform.localScale = Vector3.zero;
            }
        }
        

        if(numStep.noStep == 1)
        {
            if (handPos.childCount > 0)
            {
                Debug.Log("Put down the item in your hand first.");
            }
            else
            {
                newInstance = Instantiate(apparatusPrefab, handPos.position, apparatusPrefab.transform.rotation, handPos);
                newInstance.name = newInstance.name.Replace("(Clone)", "");
                clickedObject.transform.localScale = Vector3.zero;
            }
        }

        if (numStep.noStep == 4)
        {
            if (handPos.childCount > 0)
            {
                Debug.Log("Put down the item in your hand first.");
            }
            else
            {
                newInstance = Instantiate(apparatusPrefab, handPos.position, apparatusPrefab.transform.rotation, handPos);
                newInstance.name = newInstance.name.Replace("(Clone)", "");
                clickedObject.transform.localScale = Vector3.zero;
            }
        }
    }
    #endregion Task Pipette
    #region Task Flask A
    public void TouchThisFlask()
    {
        if(numStep.noStep < 0)//noStep = -1
        {
            if (handPos.transform.childCount > 0)
            {
                if (handPos.transform.GetChild(0).name != "Pipette")
                {
                    Debug.Log("Please pick up a new pipette first.");
                }
                else
                {
                    Debug.Log("Used medium is discarded into the waste beaker.");
                    clickedObject.transform.GetChild(2).gameObject.SetActive(false);
                    handPos.GetChild(0).GetChild(0).localScale = new Vector3(0.9f, 0.9f, 0.45f);
                    //Destroy(newInstance);
                    numStep.Task(1);//noStep = 0
                }
            }
            else
            {
                Debug.Log("You did not pick up a new pipette for this action.");
            }
        }

        else if (numStep.noStep == 2)
        {
            if(handPos.GetChild(0).name == "Pipette")
            {
                Debug.Log("Dispense medium");
                clickedObject.transform.GetChild(2).gameObject.SetActive(true);
                handPos.GetChild(0).GetChild(0).localScale = Vector3.zero;
                numStep.Task(4);//noStep = 3
            }
        }

        else if (numStep.noStep == 3)
        {
            if (handPos.GetChild(0).name == "Scrapper")
            {
                Debug.Log("Dissociate adherent cells from the culture vessels.");
                numStep.Task(5);//noStep = 4
            }
        }

        else if(numStep.noStep == 5)
        {
            if (steps == 4 && handPos.GetChild(0).name == "Pipette")
            {
                Debug.Log("Dispense 10ml medium.");
                handPos.GetChild(0).GetChild(0).localScale = Vector3.zero;
                steps = 5;
            }
            else if(steps == 5 && handPos.GetChild(0).name == "Pipette")
            {
                Debug.Log("Resuspend the cells");
                clickedObject.transform.GetChild(2).gameObject.SetActive(false);
                handPos.GetChild(0).GetChild(0).localScale = new Vector3(0.9f, 0.9f, 0.45f);
                numStep.Task(7);//noStep = 6
            }
        }
    }
    #endregion Task Flask A
    #region Task Waste Beaker
    public void WasteBeaker()
    {
        if (numStep.noStep < 0)
        {
            Debug.Log("Please complete the previous step first.");
        }

        if (numStep.noStep == 0)
        {
            //steps++;
            //steps = 1;
            if (steps == 0)
            {
                //Drain waster medium
                Debug.Log("Used medium is discarded into the waste beaker.");
                clickedObject.transform.GetChild(1).transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z/2);
                handPos.GetChild(0).GetChild(0).localScale = Vector3.zero;
                steps++;
            }
            else if( steps == 1)
            {
                //Discard used pipette
                Destroy(newInstance);
                Debug.Log("Used pipette is discarded into the waste beaker.");
                newInstance = Instantiate(physicsPrefab1, pipettePos.position, physicsPrefab1.transform.rotation, pipettePos);
                newInstance.transform.localScale = physicsPrefab1.transform.localScale;
                steps++;
                numStep.Task(2);//noStep = 1
            }
        }
        else if (numStep.noStep == 3)
        {
            //steps++;
            steps = 3;
            if(steps == 3 && handPos.childCount > 0)
            {
                //Discard use pipette
                Destroy(newInstance);
                newInstance = Instantiate(physicsPrefab1, pipettePos.position, physicsPrefab1.transform.rotation, pipettePos);
                newInstance.transform.localScale = physicsPrefab1.transform.localScale;
            }
        }
        else if(numStep.noStep == 4)
        {
            //steps++;
            steps = 4;
            if (steps == 4 && handPos.childCount > 0)
            {
                //Discard scrapper
                Destroy(newInstance);
                newInstance = Instantiate(physicsPrefab2, pipettePos.position, physicsPrefab2.transform.rotation, pipettePos);
                newInstance.transform.localScale = physicsPrefab2.transform.localScale;
            }
        }
        else if (numStep.noStep == 9)
        {
            //steps++;
            steps = 7;
            if (steps == 7 && handPos.childCount > 0)
            {
                //Discard pipette
                Destroy(newInstance);
                newInstance = Instantiate(physicsPrefab1, pipettePos.position, physicsPrefab1.transform.rotation, pipettePos);
                newInstance.transform.localScale = physicsPrefab1.transform.localScale;
            }
        }
    }
    #endregion Task Waste Beaker
    #region Task Scrapper
    public void Scrapper()
    {
        if (handPos.childCount > 0)
        {
            handHasChild = true;
        }
        else
        {
            handHasChild = false;
        }

        if (numStep.noStep < 3)
        {
            Debug.Log("Please complete the previous step first.");
        }
        else
        {
            if (!handHasChild)
            {
                newInstance = Instantiate(scrapperPrefab, handPos.position, scrapperPrefab.transform.rotation, handPos);
                newInstance.name = newInstance.name.Replace("(Clone)", "");
                clickedObject.transform.localScale = Vector3.zero;
                handHasChild = true;
            }
        }
    }
    #endregion Task Srapper
    #region Task Medium Bottle
    public void MediumBottle()
    {
        if(numStep.noStep < 1)
        {
            Debug.Log("Please conduct the previous step first.");
        }

        if(numStep.noStep == 1)
        {
            if(handPos.childCount == 0)
            {
                Debug.Log("You did not pick up a pipette.");
            }

            else if (handPos.GetChild(0).name == "Pipette")
            {
                Debug.Log("Absorb 5ml media");
                handPos.GetChild(0).GetChild(0).localScale = new Vector3(0.9f, 0.9f, 0.45f);
                numStep.Task(3);//noStep = 2
            }
        }

        if(numStep.noStep == 4)
        {
            if (handPos.childCount == 0)
            {
                Debug.Log("You did not pick up a pipette.");
            }
            else if (handPos.GetChild(0).name == "Pipette")
            {
                Debug.Log("Absorb 10ml media");
                handPos.GetChild(0).GetChild(0).localScale = new Vector3(0.9f, 0.9f, 0.45f);
                numStep.Task(6);//noStep = 5
            }
        }
    }
    #endregion Task Medium Bottle
    #region Task T25Flask
    public void T25Flask()
    {
        if (numStep.noStep < 0)
        {
            Debug.Log("Please complete the previous step first.");
        }

        if(numStep.noStep == 6)
        {
            if(!clickedObject.transform.GetChild(2).gameObject.activeSelf)
            {
                Debug.Log("Dispense 5ml into T25");
                clickedObject.transform.GetChild(2).gameObject.SetActive(true);
                numStep.Task(8);//noStep = 7;
            }
            else
            {
                Debug.Log("You have dispensed 5ml of cells into this flask.");
            }
            
        }
        else if (numStep.noStep == 7)
        { 
            if (!clickedObject.transform.GetChild(2).gameObject.activeSelf)
            {
                Debug.Log("Dispense 5ml into T25.");
                clickedObject.transform.GetChild(2).gameObject.SetActive(true);
                numStep.Task(9);//noStep = 8
            }
            else
            {
                Debug.Log("You have dispensed 5ml of cells into this flask.");
            }
            
        }
        else if (numStep.noStep == 8)
        {
            if(!clickedObject.transform.GetChild(2).gameObject.activeSelf)
            {
                Debug.Log("Dispense 5ml into T25");
                clickedObject.transform.GetChild(2).gameObject.SetActive(true);
                handPos.GetChild(0).GetChild(0).localScale = Vector3.zero;
                numStep.Task(10);//noStep = 9
            }
            else
            {
                Debug.Log("You have dispense 5ml cells into this flask.");
            }
        }
        else if(numStep.noStep == 9)
        {
            if (handPos.childCount > 0)
            {
                Debug.Log("Please discard the thing in your hand first.");
            }
            else
            {
                Debug.Log("Cell observed");
                numStep.Task(11);//noStep = 10
                ObserveCells oCell = FindObjectOfType<ObserveCells>();
                oCell.ClickTheFlask();
                ActivityFour aFour = FindObjectOfType<ActivityFour>();
                aFour.UpdateChange();
                //numStep.noStep++;
            }
        }
        else if(numStep.noStep == 10)
        {
            numStep.noStep++;
        }

        if(numStep.noStep == 11)
        {
            ObserveCells oCell = FindObjectOfType<ObserveCells>();
            oCell.ClickTheFlask();
        }
    }
    #endregion Task T25Flask
}