using UnityEngine;
using UnityEngine.UI;

public class AvatarDressCodePart : MonoBehaviour
{
    [SerializeField]
    private Button[] avatarParts;
    [SerializeField]
    private Transform[] questionPanels;

    //private int currentTransformIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        PresentCurrentTransform();
        InitializedAvatarButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Access by DressCodeDone.cs
    public void PresentCurrentTransform()
    {
        for(int i = 0; i < avatarParts.Length; i++)
        {
            if(i == 0)
            {
                questionPanels[i].localScale = Vector3.one;
            }
            else
            {
                questionPanels[i].localScale = Vector3.zero;
            }
        }
        
    }
    private void InitializedAvatarButton()
    {
        for(int i = 0; i < avatarParts.Length; i++)
        {
            Button btn = avatarParts[i];

            int btnIndex = i;
            btn.onClick.AddListener(() => ShowTransform(btnIndex));
        }
    }
    private void ShowTransform(int buttonIndex)
    {
        for(int i = 0; i < avatarParts.Length; i++)
        {
            if(i == buttonIndex)
            {
                questionPanels[i].localScale = Vector3.one;
            }
            else
            {
                questionPanels[i].localScale = Vector3.zero;
            }
            
        }
        
    }
}
