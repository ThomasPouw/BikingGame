using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CarAudio : MonoBehaviour
{
    
    [SerializeField] float transition;
    [Header("Car tires")]
    [SerializeField] AudioClip[] tireNoises;
    [SerializeField] AudioClip currentTireNoise;
    [SerializeField] AudioSource soundOriginTire;
    [SerializeField] AudioMixerSnapshot CarAudioMain;
    [SerializeField] bool playSoundGeneral;
    [Header("Car horn")]
    [SerializeField] AudioClip[] hornNoises;
    [SerializeField] AudioClip currentHornNoise;
    [SerializeField] AudioSource soundOriginHorn;
    [SerializeField] bool playSoundHorn;
    //[SerializeField] [Range(0,100)]int MaxChance;
    [SerializeField] [Range(0,100)]int SuccesRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!playSoundGeneral)
        StartCoroutine(playCarNoise());
        if(!playSoundHorn && Random.Range(0,100) < SuccesRate)
        StartCoroutine(playCarHorn());
    }
    private void OnTriggerEnter(Collider other) {
        CarAudioMain.TransitionTo(transition);
    }
    IEnumerator playCarNoise(){
        int random = Random.Range(0, tireNoises.Length);
        playSoundGeneral = true;
        currentTireNoise = tireNoises[random];
        soundOriginTire.clip = currentTireNoise;
        soundOriginTire.Play();
        yield return new WaitForSeconds(currentTireNoise.length);
        playSoundGeneral = false;
    }
    IEnumerator playCarHorn(){
        int random = Random.Range(0, tireNoises.Length);
        playSoundHorn = true;
        currentHornNoise = tireNoises[random];
        soundOriginHorn.clip = currentHornNoise;
        soundOriginHorn.Play();
        yield return new WaitForSeconds(currentHornNoise.length);
        playSoundHorn = false;
    }
}
