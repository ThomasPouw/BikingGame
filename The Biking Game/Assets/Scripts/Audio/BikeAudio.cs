using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BikeAudio : MonoBehaviour
{
    [SerializeField] AudioClip[] tireNoises;
    [SerializeField] AudioClip currentTireNoise;
    [SerializeField] AudioSource soundOrigin;
    [SerializeField] AudioMixerSnapshot BikeAudioMain;
    [SerializeField] float transition;
    [SerializeField] public bool playSound;
    [SerializeField] float currentVolume;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(soundOrigin.outputAudioMixerGroup.audioMixer != BikeAudioMain){
            BikeAudioMain.TransitionTo(transition);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playSound)
        StartCoroutine(playTireNoise());
    }
    IEnumerator playTireNoise(){
        int random = Random.Range(0, tireNoises.Length);
        playSound = false;
        currentTireNoise = tireNoises[random];
        soundOrigin.clip = currentTireNoise;
        soundOrigin.Play();
        yield return new WaitForSeconds(currentTireNoise.length);
        playSound = true;
    }
    public void changeVolume(float Volume){
        soundOrigin.volume = Volume;
    }
        
        /*Debug.Log("Here!");
        currentVolume = soundOrigin.volume;
        float volume = currentVolume;
        float time = 0.0f;
        while(volume != Volume){
            volume = Mathf.Lerp(currentVolume, Volume, time/1);
            time += 0.5f*Time.deltaTime;
            soundOrigin.volume = volume;
            yield return null;
        }
    }*/
}
