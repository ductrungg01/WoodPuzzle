using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WoodPuzzle.UI;

namespace WoodPuzzle.Core
{
    public class UiManager : MonoBehaviour, IGameFlow
    {
        public static UiManager Instance { get; private set; }

        public GameObject popupTargetPosition;
        public GameObject popupSplashPrefab;
        public GameObject popupHomePrefab;
        public GameObject popupLoadingPrefab;
        public GameObject popupInGamePrefab;
        public GameObject popupEndGameWinPrefab;
        public GameObject popupEndGameLosePrefab;


        private void Awake()
        {
            // Singleton
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

            Instance.GetPopupSplash().Show();
            DOVirtual.DelayedCall(0.25f, delegate
            {
                Instance.GetPopupHome().Show();
            });
        }


        #region POPUP SPLASH
        private PopupSplash _popupSplash;
        public PopupSplash GetPopupSplash()
        {
            if (_popupSplash != null)
                return _popupSplash;

            GameObject newObj = Instantiate(popupSplashPrefab, popupTargetPosition.transform.parent);
            _popupSplash = newObj.GetComponent<PopupSplash>();
            return _popupSplash;
        }
        #endregion

        #region POPUP HOME
        private PopupHome _popupHome;
        public PopupHome GetPopupHome()
        {
            if (_popupHome != null)
                return _popupHome;

            GameObject newObj = Instantiate(popupHomePrefab, popupTargetPosition.transform);
            _popupHome = newObj.GetComponent<PopupHome>();
            return _popupHome;
        }
        #endregion

        #region POPUP LOADING
        private PopupLoading _popupLoading;
        public PopupLoading GetPopupLoading()
        {
            if (_popupLoading != null)
                return _popupLoading;

            GameObject newObj = Instantiate(popupLoadingPrefab, popupTargetPosition.transform.parent);
            _popupLoading = newObj.GetComponent<PopupLoading>();
            return _popupLoading;
        }
        #endregion

        #region POPUP INGAME
        private PopupInGame _popupInGame;
        public PopupInGame GetPopupInGame()
        {
            if (_popupInGame != null)
                return _popupInGame;

            GameObject newObj = Instantiate(popupInGamePrefab, popupTargetPosition.transform);
            _popupInGame = newObj.GetComponent<PopupInGame>();
            return _popupInGame;
        }
        #endregion

        #region POPUP ENDGAME WIN - LOSE
        private PopupEndGame _popupEndGameWin;
        public PopupEndGame GetPopupEndGameWin()
        {
            if (_popupEndGameWin != null)
                return _popupEndGameWin;

            GameObject newObj = Instantiate(popupEndGameWinPrefab, popupTargetPosition.transform);
            _popupEndGameWin = newObj.GetComponent<PopupEndGame>();
            return _popupEndGameWin;
        }
        private PopupEndGame _popupEndGameLose;
        public PopupEndGame GetPopupEndGameLose()
        {
            if (_popupEndGameLose != null)
                return _popupEndGameLose;

            GameObject newObj = Instantiate(popupEndGameLosePrefab, popupTargetPosition.transform);
            _popupEndGameLose = newObj.GetComponent<PopupEndGame>();
            return _popupEndGameLose;
        }
        #endregion


        public void OnPrepareGame()
        {
            GetPopupInGame().Hide();
        }

        public void OnStartGame()
        {
            GetPopupHome().Hide();
            GetPopupInGame().Show();
            DOVirtual.DelayedCall(.25f, delegate
            {
                GetPopupLoading().Hide();
            });
        }

        public void OnPauseGame()
        {
            throw new System.NotImplementedException();
        }

        public void OnResumeGame()
        {
            throw new System.NotImplementedException();
        }

        public void OnFinishGame(EndGameType type)
        {
            UiManager.Instance.GetPopupInGame().Hide();

            if ((EndGameType)type == EndGameType.WIN)
            {
                UiManager.Instance.GetPopupEndGameWin().Show();
                return;
            }
            UiManager.Instance.GetPopupEndGameLose().Show();
        }

        public void BackToHomePopup()
        {
            GetPopupHome().Show();
            GetPopupInGame().Hide();

            DOVirtual.DelayedCall(.25f, delegate
            {
                GetPopupLoading().Hide();
            });
        }
    }
}


