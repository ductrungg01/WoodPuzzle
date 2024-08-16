using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WoodPuzzle.Core;

namespace WoodPuzzle.UI
{
    public class PopupEndGame : PopupBase
    {
        public EndGameType type;

        public Button nextLevelButton;
        public Button replayButton;
        public Button homeButton;

        private void Start()
        {
            homeButton.onClick.AddListener(OnClickHomeButton);
            if (type == EndGameType.WIN)
                nextLevelButton.onClick.AddListener(OnClickBtnNext);
            if (type == EndGameType.LOSE) 
                replayButton.onClick.AddListener(OnClickBtnReplay);
                
        }


        protected override void OnShown()
        {

        }
        public void OnClickBtnNext()
        {
            if (type == EndGameType.WIN)
            {
                GameBase.CurrentLevel++;
                LevelManager.Instance.GetNextLevelIndex();
            }

            UiManager.Instance.GetPopupLoading().Show(
                GameManager.Instance.PrepareGame,
                GameManager.Instance.StartGame
            );

            Hide();
        }

        public void OnClickHomeButton()
        {
            UiManager.Instance.GetPopupLoading().Show(
                null,
                UiManager.Instance.BackToHomePopup
            );

            Hide();
        }

        public void OnClickBtnReplay()
        {
            UiManager.Instance.GetPopupLoading().Show(
                GameManager.Instance.PrepareGame,
                GameManager.Instance.StartGame
            );
        }
    }
}


