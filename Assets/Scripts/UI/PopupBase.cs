using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using WoodPuzzle.Core;

namespace WoodPuzzle.UI
{
    public class PopupBase : MonoBehaviour
    {
        private Action callbackWhenHide;

        public bool IsShowing { get; private set; }

        public virtual void Awake()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            IsShowing = true;
            Show(null);
        }

        public virtual void Show(Action callback = null)
        {
            IsShowing = true;

            if (callback != null)
            {
                callbackWhenHide = callback;
            }

            transform.position = UiManager.Instance.popupTargetPosition.transform.position;

            OnShowing();
            gameObject.SetActive(true);
            OnShown();
        }

        public virtual void Hide()
        {
            IsShowing = false;

            OnHiding();
            gameObject.SetActive(false);
            OnHidden();
            if (callbackWhenHide != null)
            {
                callbackWhenHide();
            }
        }


        protected virtual void OnShowing()
        {

        }

        protected virtual void OnShown()
        {

        }

        protected virtual void OnHiding()
        {

        }

        protected virtual void OnHidden()
        {

        }
    }
}

