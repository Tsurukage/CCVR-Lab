using UnityEngine;

public class ModulesDes : MonoBehaviour
{
    [SerializeField]
    private string modules;
    [SerializeField]
    [TextArea]
    private string moduleDes;
    private WorldSpaceMessageBox popMessage;

    // Start is called before the first frame update
    void Start()
    {
        if(popMessage == null)
        {
            popMessage = FindObjectOfType<WorldSpaceMessageBox>();
            popMessage.MessagePopUp("<color=Green>" + modules + "</color>\n" + moduleDes);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
