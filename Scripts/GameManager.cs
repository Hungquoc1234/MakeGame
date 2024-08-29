using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingTimeToStartTimer = 1f;
    private float countDownToStartTimer = 3f;
    private bool isGamePause = false;
    [SerializeField] private float gamePlayingTimer = 10f;
    [SerializeField] private float gamePlayingTimerMax = 10f;
    [SerializeField] private GameInput gameInput; 

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;
    public event EventHandler<OnTimeChangedEventArgs> OnTimeChanged;

    public class OnTimeChangedEventArgs
    {
        public float timeLeft;
    }

    private void Awake()
    {
        Instance = this;
        
        state = State.WaitingToStart;
    }

    private void Start()
    {
        gameInput.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        PauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingTimeToStartTimer -= Time.deltaTime;

                if(waitingTimeToStartTimer <= 0f)
                {
                    state = State.CountDownToStart;

                    if(OnStateChanged != null)
                    {
                        OnStateChanged(this, EventArgs.Empty);
                    }
                }

                break;

            case State.CountDownToStart:
                countDownToStartTimer -= Time.deltaTime;

                if (countDownToStartTimer <= 0f)
                {
                    state = State.GamePlaying;

                    if (OnStateChanged != null)
                    {
                        OnStateChanged(this, EventArgs.Empty);
                    }
                }

                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;

                if (OnTimeChanged != null)
                {
                    OnTimeChanged(this, new OnTimeChangedEventArgs { timeLeft = gamePlayingTimer / gamePlayingTimerMax });
                }

                if (gamePlayingTimer <= 0f)
                {
                    state = State.GameOver;

                    if (OnStateChanged != null)
                    {
                        OnStateChanged(this, EventArgs.Empty);
                    }
                }

                break;

            case State.GameOver:

                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStart()
    {
        return state == State.CountDownToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public void PauseGame()
    {
        isGamePause = !isGamePause;

        if (isGamePause)
        {
            Time.timeScale = 0f;

            if (OnGamePause != null)
            {
                OnGamePause(this, EventArgs.Empty);
            }
        }
        else
        {
            Time.timeScale = 1f;

            if(OnGameResume != null)
            {
                OnGameResume(this, EventArgs.Empty);
            }
        }
    }
}
