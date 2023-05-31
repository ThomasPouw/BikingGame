using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LevelEditorAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] dropElementNoises;
    [SerializeField] AudioSource dropElementAudioMain;
    [SerializeField] AudioClip[] rotateElementNoises;
    [SerializeField] AudioSource rotateElementAudioMain;
    [SerializeField] AudioMixerSnapshot LevelEditorAudioMain;
    private void Start() {
        LevelEditorAudioMain.TransitionTo(0);
    }
    public void playDropElement(){
        int random = Random.Range(0, dropElementNoises.Length);
        //currentTireNoise = tireNoises[random];
        dropElementAudioMain.clip = dropElementNoises[random];
        dropElementAudioMain.Play();
        //playSound = false;
    }
    public void playRotateElement(){
        int random = Random.Range(0, rotateElementNoises.Length);
        //currentTireNoise = tireNoises[random];
        rotateElementAudioMain.clip = rotateElementNoises[random];
        rotateElementAudioMain.Play();
        //playSound = false;
    }
}
