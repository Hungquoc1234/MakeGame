using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private Image gameTimer;

    private void Start()
    {
        GameManager.Instance.OnTimeChanged += Instance_OnTimeChanged;

        gameTimer.fillAmount = 0f;

        gameObject.SetActive(false);
    }

    private void Instance_OnTimeChanged(object sender, GameManager.OnTimeChangedEventArgs e)
    {
        gameTimer.fillAmount = e.timeLeft;
        
        if(gameTimer.fillAmount > 0)
        {
            gameObject.SetActive(true);

            if(gameTimer.fillAmount <= 0.2f)
            {
                gameTimer.color = new Color(1f, 0.1f, 0.1f, 1f);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
