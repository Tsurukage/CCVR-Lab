using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource quizFX;
    public AudioClip correctFX;
    public AudioClip wrongFX;
    public AudioClip sprayFX;
    public AudioClip wipingSound;
    public AudioClip notification;

    public void CorrectChoices()
    {
        quizFX.PlayOneShot(correctFX, 0.1f);
    }
    public void WrongChoices()
    {
        quizFX.PlayOneShot(wrongFX, 0.1f);
    }
    public void Spraying()
    {
        quizFX.PlayOneShot(sprayFX, 0.1f);
    }
    public void Wiping()
    {
        quizFX.PlayOneShot(wipingSound, 0.1f);
    }
    public void PopNitification()
    {
        quizFX.PlayOneShot(notification, 0.1f);
    }
}
