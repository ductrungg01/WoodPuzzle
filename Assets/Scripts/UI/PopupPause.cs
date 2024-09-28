using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WoodPuzzle.Core;

namespace WoodPuzzle.UI {
    public class PopupPause : PopupBase
    {
        public Button continueBtn;
        public Button homeButton;

        private void Start()
        {
            continueBtn.onClick.AddListener(OnClickContinueButton);
            homeButton.onClick.AddListener(OnClickHomeButton);  
        }

        public void OnClickContinueButton()
        {
            GameManager.Instance.ResumeGame();
        }

        public void OnClickHomeButton()
        {
            Time.timeScale = 1;

            UiManager.Instance.GetPopupLoading().Show(
                () => LevelManager.Instance.OnFinishGame(EndGameType.DRAW),
                UiManager.Instance.BackToHomePopup
            );

            Hide();
        }

    }

    
}


