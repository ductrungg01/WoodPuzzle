using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WoodPuzzle.UI;

namespace WoodPuzzle.Core
{
    public class Level : MonoBehaviour, IGameFlow
    {
        [SerializeField] private float levelTime = 60f;
        private float remainTime = 0;

        public Transform woodParentInHierachy;
        public Transform boltParentInHierachy;
        public Transform holdParentInHierachy;

        private int numberOfWood = 0;

        private PopupLoading popupLoading;

        public void AddWood() { numberOfWood++; }
        public void RemoveWood() { 
            numberOfWood--; 
            if (numberOfWood <= 0)
            {
                GameManager.Instance.FinishGame(EndGameType.WIN);
            }
        }

        private void Start()
        {
            remainTime = levelTime;
            ++remainTime; // 1s for player see the level time;

            popupLoading = UiManager.Instance.GetPopupLoading();
        }
        
        private void Update()
        {
            UiManager.Instance.GetPopupInGame().UpdateRemainTime(remainTime);

            if (!popupLoading.IsShowing)
                remainTime -= Time.deltaTime;

            if (remainTime <= 0 )
            {
                GameManager.Instance.FinishGame(EndGameType.LOSE);
            }
        }

        public void OnFinishGame(EndGameType type)
        {
            Destroy(gameObject);
        }

        public void OnPauseGame()
        {
            
        }

        public void OnPrepareGame()
        {
            Wood.ResetWoodUsedPosition();
        }

        public void OnResumeGame()
        {
            
        }

        public void OnStartGame()
        {
            
        }
    }
}


