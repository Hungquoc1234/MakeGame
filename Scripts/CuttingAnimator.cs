using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingAnimator : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private void Awake()
    {
        animator = this.GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCutting += CuttingCounter_OnCutting;
    }

    private void CuttingCounter_OnCutting(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Cut");
    }
}
