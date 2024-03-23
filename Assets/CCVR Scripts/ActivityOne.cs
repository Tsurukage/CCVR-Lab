using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivityOne : MonoBehaviour
{
    [SerializeField]
    private GameObject itemsOnUtCart, actBtn, objsInCab; //The object holding the group of apparatus
    [SerializeField]
    private GameObject panelInWorld, task2;

    private playerHUDui showMessage;
    private Animation spraying;
    private AudioManager playSFX;

    private bool sprayIsPickedUp = false;
    //private bool isSprayed = false;

    // Start is called before the first frame update
    void Start()
    {
        if(task2 != null)
        {
            task2.transform.localScale = Vector3.zero;
        }
        panelInWorld.transform.localScale = Vector3.zero;
        itemsOnUtCart.transform.localScale = Vector3.zero;
        actBtn.transform.localScale = Vector3.one;
        //Vector3.Scale(new Vector3(1, 1, 1), new Vector3(0.1f, 0.1f, 0.1f));
        showMessage = FindObjectOfType<playerHUDui>();
        if(objsInCab != null)
        {
            objsInCab.transform.localScale = Vector3.zero;
        }
        playSFX = FindObjectOfType<AudioManager>();
    }

    public void ActiveCartItems()
    {
        FadeInItems();
        FadeInPanel();
        FadeOutBtn();
        
        objsInCab.transform.localScale = Vector3.one;
    }
    public void OpenTask2()
    {
        task2.transform.localScale = Vector3.one;
    }
    public void CloseTask2()
    {
        task2.transform.localScale = Vector3.zero;
    }
    
    public void NextActvtMessage()
    {
        StartCoroutine(NextMessage());
    }
    IEnumerator NextMessage()
    {
        yield return new WaitForSeconds(5f);
        showMessage.ShowMessage("There are two T25 cell culture flasks inside the biosafety cabinet. Please click on Flask A to observe the cells closely.");
    }
    
    #region Tween
    private void FadeInItems()
    {
        iTween.StopByName(itemsOnUtCart, "fadeInItems");
        iTween.ScaleTo(itemsOnUtCart, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInItems"));
    }
    private void FadeOutItems()
    {
        iTween.StopByName(itemsOnUtCart, "fadeOutItems");
        iTween.ScaleTo(itemsOnUtCart, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "fadeOutItems"));
    }
    private void FadeInBtn()
    {
        iTween.StopByName(actBtn, "fadeInButton");
        iTween.ScaleTo(actBtn, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInButton"));
    }
    private void FadeOutBtn()
    {
        iTween.StopByName(actBtn, "fadeOutButton");
        iTween.ScaleTo(actBtn, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "fadeOutButton"));
    }

    public void FadeInPanel()
    {
        iTween.StopByName(panelInWorld, "fadeInPanel");
        iTween.ScaleTo(panelInWorld, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "fadeInPanel"));
    }
    public void FadeOutPanel()
    {
        iTween.StopByName(panelInWorld, "fadeOutPanel");
        iTween.ScaleTo(panelInWorld, iTween.Hash("scale", Vector3.zero, "time", 1f, "delay", 5f, "name", "fadeOutPanel"));
    }
    #endregion Tween
}
