using UnityEngine;
using UnityEngine.UI;

public class ManualTween : MonoBehaviour
{
    [SerializeField]
    private GameObject manualQuad, manualTabs;
    [SerializeField]
    private GameObject[] btnManual;
    [SerializeField]
    private GameObject[] menuGrp;
    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        if(manualQuad != null)
        {
            manualQuad.transform.localScale = new Vector3(0.15f, 0.225f, 1f);
        }
        if (manualTabs != null)
        {
            manualTabs.transform.localScale = Vector3.zero;
        }
        for(int i = 0; i < menuGrp.Length; i++)
        {
            if(menuGrp[i] != null)
                menuGrp[i].transform.localScale = Vector3.zero;
        }
        InitializedButtonManual();
    }
    private void InitializedButtonManual()
    {
        for (int i = 0; i < btnManual.Length; i++)
        {
            if(btnManual[i] != null)
            {
                if (btnManual[i].GetComponent<Button>() == null)
                {
                    btnManual[i].AddComponent<Button>();
                    btnManual[i].GetComponent<Button>().transition = Selectable.Transition.None;
                }
            }
            Button mButton = btnManual[i].GetComponent<Button>();
            
            int mBtnIndex = i;
            mButton.onClick.AddListener(() => ShowCorrespondManual(mBtnIndex));
        }
    }

    private void ShowCorrespondManual(int manualIndex)
    {
        for(int i = 0; i < menuGrp.Length; i++)
        {
            if(i == manualIndex)
            {
                iTween.ScaleFrom(menuGrp[i].gameObject, Vector3.zero, 1f);
                iTween.ScaleTo(menuGrp[i].gameObject, Vector3.one, 1f);
            }
            else
            {
                menuGrp[i].transform.localScale = Vector3.zero;
            }

            if (menuGrp[i].transform.childCount > 0)
            {
                Button clsBtn = menuGrp[i].transform.GetChild(1).GetComponent<Button>();
                int mBtnIndex = i;
                clsBtn.onClick.AddListener(() => CloseManualTransform(mBtnIndex));
            }
        }
    }
    public void SwitchFade()
    {
        if (!isOpen)
        {
            iTween.ScaleTo(manualTabs, Vector3.one, 1f);
            isOpen = true;
        }
        else
        {
            iTween.ScaleTo(manualTabs, Vector3.zero, 1f);
            isOpen = false;
        }
    }
    private void CloseManualTransform(int manualIndex)
    {
        iTween.ScaleTo(menuGrp[manualIndex].gameObject, Vector3.zero, 1f);
    }

    #region Tween
    /*public void TweenOpen()
    {
        iTween.StopByName(menuGrp.gameObject, "OpenManual");
        iTween.ScaleTo(menuGrp.gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "OpenManual"));
        isOpen = true;
        //Scale(new Vector3(1,1,1), new Vector3(0.15f, 0.15f, 0.15f))
    }
    public void TweenClose()
    {
        iTween.StopByName(menuGrp.gameObject, "CloseManual");
        iTween.ScaleTo(menuGrp.gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "CloseManual"));
        isOpen = false;
    }*/
    #endregion Tween
}
