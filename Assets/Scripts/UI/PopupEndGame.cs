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

        public List<ParticleSystem> win_ParticleSystems;
        public List<ParticleSystem> lose_ParticleSystems;

        private void Start()
        {
            homeButton.onClick.AddListener(OnClickHomeButton);
            if (type == EndGameType.WIN)
                nextLevelButton.onClick.AddListener(OnClickBtnNext);
            if (type == EndGameType.LOSE) 
                replayButton.onClick.AddListener(OnClickBtnReplay);
                
        }

        private void OnEnable()
        {
            if (type == EndGameType.WIN)
            {
                foreach (ParticleSystem p in win_ParticleSystems)
                {
                    p.Play();
                }
            }
            else if (type == EndGameType.LOSE)
            {
                foreach (ParticleSystem p in lose_ParticleSystems) 
                { 
                    p.Play(); 
                }
            }
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


