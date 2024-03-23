using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchAttire : MonoBehaviour
{
    public Transform switchPart1, switchPart2;
    public Transform onPart, offPart;

    // Start is called before the first frame update
    void Start()
    {
        if(switchPart1 != null)
            switchPart1.localScale = Vector3.one;
        if(switchPart2 != null)
            switchPart2.localScale = Vector3.zero;
        if(onPart != null)
            onPart.localScale = Vector3.zero;   //initially is hidden
        if (offPart != null)
            offPart.localScale = Vector3.one;   //initially is appear
    }

    public void SwitchParts()
    {
        switchPart1.localScale = Vector3.zero;
        switchPart2.localScale = Vector3.one;
    }

    public void SwitchBack()
    {
        switchPart1.localScale = Vector3.one;
        switchPart2.localScale = Vector3.zero;
    }

    public void OnParts()
    {
        if (onPart != null)
            onPart.localScale = Vector3.one;
        if (offPart != null)
            offPart.localScale = Vector3.zero;
    }

    public void OffPart()
    {
        if(onPart != null)
            onPart.localScale = Vector3.zero;
        if (offPart != null)
            offPart.localScale = Vector3.one;
    }
}
