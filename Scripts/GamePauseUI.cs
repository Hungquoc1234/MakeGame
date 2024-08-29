using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance { get; private set; }

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button settingButton;

    private void Awake()
    {
        Instance = this;

        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.PauseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        settingButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);

            SettingUI.Instance.gameObject.SetActive(true);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGameResume += Instance_OnGameResume;
        GameManager.Instance.OnGamePause += Instance_OnGamePause;

        gameObject.SetActive(false);
    }

    private void Instance_OnGameResume(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void Instance_OnGamePause(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
    }
}
