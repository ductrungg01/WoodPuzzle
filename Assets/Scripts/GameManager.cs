using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Unity.VisualScripting;


public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text msgText;
    [SerializeField] private Text timeRemainText;

    [Header("Delay time")]
    [SerializeField] private float startDelay = 0f;
    [SerializeField] private float endDelay = 0f;

    [Header("Level play time")]
    [SerializeField] private float levelTime = 1f;
    private float timeRemain = 0f;

    [Header("Player")]
    [SerializeField] private Player player;


    void Start()
    {
        this.timeRemain = levelTime;

        GameLoop();
    }

    private void Update()
    {
        // TODO: debug only, rememeber to remove
        if (Input.GetKeyDown(KeyCode.N))
        {
            GotoNextLevel();
        }
    }


    async void GameLoop()
    {
        await RoundStarting();
        await RoundPlaying();
        await RoundEnding();

        if (timeRemain > 0)
        {
            GotoNextLevel();
        }
        else
        {
            Replay();
        }
    }

    private async UniTask RoundStarting() {
        DisablePlayer();
        msgText.text = "Level " + GetLevelIndex();
        timeRemainText.text = FloatToTimeString(timeRemain);

        await UniTask.Delay(TimeSpan.FromSeconds(startDelay));
    }

    private async UniTask RoundPlaying()
    {
        EnablePlayer();

        while (!IsLevelDone() && timeRemain > 0)
        {
            timeRemain -= Time.deltaTime;
            timeRemainText.text = FloatToTimeString(timeRemain);
            await UniTask.Yield();
        }
    }

    private async UniTask RoundEnding()
    {
        DisablePlayer();
        msgText.text = EndMessage();

        await UniTask.Delay(TimeSpan.FromSeconds(endDelay));
    }

    private String EndMessage()
    {
        if (timeRemain < 0)
        {
            return "Time out";
        }
        else return "Complete";
    }

    private bool IsLevelDone()
    {
        return WoodManager.Instance.AllWoodIsFallover();
    }

    private void GotoNextLevel()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    public void Replay()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currSceneIndex);
    }

    private void DisablePlayer()
    {
        player.enabled = false;
    }

    private void EnablePlayer()
    {
        player.enabled = true;
    }

    private int GetLevelIndex()
    {
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;

        return currSceneIndex;
    }

    private String FloatToTimeString(float time)
    {
        if (time < 0) time = 0;

        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
