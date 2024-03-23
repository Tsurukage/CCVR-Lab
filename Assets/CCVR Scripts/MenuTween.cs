using UnityEngine;
using UnityEngine.UI;

public class MenuTween : MonoBehaviour
{
    [SerializeField]
    private Button[] navButtons;
    [SerializeField]
    private Transform[] menuTransforms;

    // Start is called before the first frame update
    void Start()
    {
        PresentCurrentTransform();
        InitializedMenuButton();
    }

    //This method has the same name in AvatarCressCodePart.cs
    private void PresentCurrentTransform()
    {
        for(int i = 0; i< menuTransforms.Length; i++)
        {
            if(i == 0)
            {
                menuTransforms[i].localScale = Vector3.one;
            }
            else
            {
                menuTransforms[i].localScale = Vector3.zero;
            }
        }
    }
    private void InitializedMenuButton()
    {
        for(int i = 0; i < navButtons.Length; i++)
        {
            Button btn = navButtons[i];

            int btnIndex = i;
            btn.onClick.AddListener(() => ShowMenu(btnIndex));
        }
    }
    private void ShowMenu(int buttonIndex)
    {
        for (int i = 0; i < menuTransforms.Length; i++)
        {
            if(i == buttonIndex)
            {
                iTween.ScaleTo(menuTransforms[i].gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "ShowCurrentMenu"));
            }
            else
            {
                iTween.ScaleTo(menuTransforms[i].gameObject, iTween.Hash("scale", Vector3.zero, "time", 1f, "name", "ShowCurrentMenu"));
            }

            if (buttonIndex == 3)
            {
                iTween.ScaleTo(menuTransforms[0].gameObject, iTween.Hash("scale", Vector3.one, "time", 1f, "name", "ShowCurrentMenu"));
            }
        }

    }
}
