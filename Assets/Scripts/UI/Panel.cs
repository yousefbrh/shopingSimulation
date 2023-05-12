using System;
using UnityEngine;

namespace UI
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] protected float timeToOpenOrClosePanel;

        public Action<Panel> onDialogClosed;
        
        private bool _isShowing;
        private bool _canApplyAction;
        private float _tempTime;

        protected virtual void Start()
        {
            transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            if (!_canApplyAction) return;
            _tempTime += Time.deltaTime / timeToOpenOrClosePanel;
            var value = Mathf.Lerp(_isShowing ? 0 : 1, _isShowing ? 1 : 0, _tempTime);
            transform.localScale = new Vector3(value, value, 0);
            if (!(_tempTime >= 1)) return;
            _canApplyAction = false;
            if (!_isShowing)
                onDialogClosed?.Invoke(this);
        }

        public virtual void Initialize()
        {
            ShowDialog();
        }

        private void ShowDialog()
        {
            _canApplyAction = true;
            _isShowing = true;
            _tempTime = 0;
        }

        protected virtual void CloseDialog()
        {
            _canApplyAction = true;
            _isShowing = false;
            _tempTime = 0;
        }
    }
}