using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private AudioSource audioSource;

    // Update is called once per frame
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool playSound = false;
        if(e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying)
        {
            playSound = true;
        }

        if(playSound == true)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
