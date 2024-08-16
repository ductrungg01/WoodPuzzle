using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WoodPuzzle.Core;

namespace WoodPuzzle.UI
{
    public class PopupLoading : PopupBase
    {
        public CanvasGroup canvasGroup;

        // _fadingDoneCallback is invoked right after the fade-in animation completes, 
        // signaling that the popup is fully visible. You can use this to trigger actions 
        // that should occur as soon as the popup appears.
        private Action _fadingCompletedCb;

        // _fadingCompletedCallback is invoked after a delay, just before the fade-out animation starts,
        // indicating that the popup has been displayed for the desired duration and is now ready to be hidden.
        private Action _fadingDoneCb;

        protected override void OnShowing()
        {
            canvasGroup.alpha = 0f;
        }

        protected override void OnShown()
        {
            canvasGroup.DOFade(1f, .25f).OnComplete(delegate
            {
                _fadingDoneCb?.Invoke();
                UiManager.Instance.GetPopupInGame().Hide();
                UiManager.Instance.GetPopupEndGameLose().Hide();
                UiManager.Instance.GetPopupEndGameWin().Hide();
            });

            DOVirtual.DelayedCall(1.5f, delegate
            {
                _fadingCompletedCb?.Invoke();
            }).OnComplete(delegate
            {
                canvasGroup.DOFade(0f, .25f);
            });
        }

        public void Show(Action fadingDoneCb, Action fadingCompletedCb)
        {
            _fadingCompletedCb = fadingCompletedCb;
            _fadingDoneCb = fadingDoneCb;

            Show();
        }
    }
}


