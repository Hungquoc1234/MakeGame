using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    private InterfaceHasProgress hasProgress;

    void Start()
    {
        hasProgress = hasProgressGameObject.GetComponent<InterfaceHasProgress>();

        hasProgress.OnProgressChange += HasProgress_OnProgressChange;

        barImage.fillAmount = 0f;

        this.gameObject.SetActive(false);
    }

    private void HasProgress_OnProgressChange(object sender, InterfaceHasProgress.OnProgressChangeEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if (e.progressNormalized <= 0f || e.progressNormalized >= 1f)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
