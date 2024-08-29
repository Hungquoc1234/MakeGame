using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private GameObject stoveGameObject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        if(e.state != StoveCounter.State.DoingNothing && e.state != StoveCounter.State.Burned){
            particlesGameObject.SetActive(true);
            stoveGameObject.SetActive(true);
        }
        else
        {
            particlesGameObject.SetActive(false);
            stoveGameObject.SetActive(false);
        }
    }
}
