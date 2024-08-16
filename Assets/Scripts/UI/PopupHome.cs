using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WoodPuzzle.Core;

namespace WoodPuzzle.UI
{
    public class PopupHome : PopupBase
    {
        public Button playBtn;

        private void Start()
        {
            playBtn.onClick.AddListener(OnClickBtnStart);
        }

        public void OnClickBtnStart()
        {
            UiManager.Instance.GetPopupLoading().Show(
                GameManager.Instance.PrepareGame,
                GameManager.Instance.StartGame
            );
        }
    }
}


