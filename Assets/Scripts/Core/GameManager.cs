using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WoodPuzzle.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private GameState _currentState = GameState.PREPARE;

        public static GameState CurrentGameState
        {
            get
            {
                return Instance._currentState;
            }
            set
            {
                Instance._currentState = value; 
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }

        private void Start()
        {
            LevelManager.Instance.GetNextLevelIndex();
        }

        public void PrepareGame()
        {
            CurrentGameState = GameState.PREPARE;

            UiManager.Instance.OnPrepareGame();
            LevelManager.Instance.OnPrepareGame();
        }

        public void StartGame()
        {
            CurrentGameState = GameState.PLAYING;

            UiManager.Instance.OnStartGame();
        }

        public void PauseGame()
        {
            CurrentGameState = GameState.PAUSING;

            UiManager.Instance.OnPauseGame();
        }

        public void ResumeGame()
        {
            CurrentGameState = GameState.PLAYING;

            UiManager.Instance.OnResumeGame();
        }

        public void FinishGame(EndGameType type)
        {
            if (CurrentGameState == GameState.FINISH) return;

            CurrentGameState = GameState.FINISH;

            UiManager.Instance.OnFinishGame(type);
            LevelManager.Instance.OnFinishGame(type);
        }
        
    }
}


