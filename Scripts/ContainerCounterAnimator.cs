using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnGrabKitchenObject += ContainerCounter_OnGrabKitchenObject;
    }

    private void ContainerCounter_OnGrabKitchenObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger("OpenClose");
    }
}
