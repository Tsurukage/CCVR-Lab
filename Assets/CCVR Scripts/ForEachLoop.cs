using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEachLoop : MonoBehaviour
{
    void Start()
    {
        string[] strings = new string[5];

        strings[0] = "First string";
        strings[1] = "Second string";
        strings[2] = "Third string";
        strings[3] = "Forth string";
        strings[4] = "Fifth string";

        foreach (string item in strings)
        {
            Debug.Log(item);
        }

        for(int i = 0; i < strings.Length; i++)
        {
            Debug.Log(strings[i]);
        }
    }
}
