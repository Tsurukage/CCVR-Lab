using UnityEngine;
using UnityEngine.EventSystems;

public class GONameSend : MonoBehaviour, IPointerEnterHandler
{
    private GetGOName getName;

    // Start is called before the first frame update
    void Start()
    {
        getName = GameObject.Find("GameController").GetComponent<GetGOName>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        getName.ReceiveName(this.transform.gameObject);
    }
}
