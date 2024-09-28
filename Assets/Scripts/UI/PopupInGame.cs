using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WoodPuzzle.Core;

namespace WoodPuzzle.UI
{
    public class PopupInGame : PopupBase
    {
        public TMP_Text levelText;
        public Button ReplayButton;
        public Button PauseButton;
        public Button TestWinButton;
        public Button TestLoseButton;
        public TMP_Text remainTimeText;

        private void Start()
        {
            ReplayButton.onClick.AddListener(OnClickBtnReplay);
            PauseButton.onClick.AddListener(OnClickBtnPause);
            TestWinButton.onClick.AddListener( () => OnClickBtnEndGame(EndGameType.WIN));
            TestLoseButton.onClick.AddListener( () => OnClickBtnEndGame(EndGameType.LOSE));
        }

        protected override void OnShown()
        {
            levelText.text = "Level " + GameBase.CurrentLevel;
        }

        public void OnClickBtnReplay()
        {
            UiManager.Instance.GetPopupLoading().Show(
                () => LevelManager.Instance.OnFinishGame(EndGameType.DRAW),
                UiManager.Instance.GetPopupHome().OnClickBtnStart
            );
        }

        public void OnClickBtnPause()
        {
            if (GameManager.CurrentGameState == GameState.PLAYING)
                GameManager.Instance.PauseGame();
            else
                GameManager.Instance.ResumeGame();
        }

        public void OnClickBtnEndGame(EndGameType type)
        {
            GameManager.Instance.FinishGame(type);
        }

        public void UpdateRemainTime(float time)
        {
            remainTimeText.text = FloatToTimeString(time);
        }

        private String FloatToTimeString(float time)
        {
            if (time < 0) time = 0;

            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}


