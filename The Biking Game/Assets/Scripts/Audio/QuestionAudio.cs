using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class QuestionAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] CorrectAudio;
    [SerializeField] AudioClip[] InCorrectAudio;
    [SerializeField] AudioClip currentSolutionNoise;
    [SerializeField] AudioSource soundOrigin;
    

    public void QuestionAnswerAudio(bool Correct)
    {
        int random = Random.Range(0, Correct ? CorrectAudio.Length : InCorrectAudio.Length);
        soundOrigin.PlayOneShot(Correct ? CorrectAudio[random] : InCorrectAudio[random]);
    }
}
