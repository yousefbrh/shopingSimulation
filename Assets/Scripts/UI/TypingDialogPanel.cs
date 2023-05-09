using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class TypingDialogPanel : Dialog
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TypeWriterUI typeWriterUI;
        [SerializeField] private float timeToOpenOrCloseDialog;
        [SerializeField] private float timeToCloseDialogAfterTextFullyAppeared;
        
        private bool _isShowing;
        private bool _canApplyAction;
        private float _tempTime;


        public void Initialize(string dialogText)
        {
            text.text = dialogText;
            ShowDialog();
            typeWriterUI.onTypingFinished += CloseDialogWithDelay;
            typeWriterUI.StartTyping(timeToOpenOrCloseDialog);
        }

        private void Update()
        {
            if (!_canApplyAction) return;
            _tempTime += Time.deltaTime / timeToOpenOrCloseDialog;
            var value = Mathf.Lerp(_isShowing ? 0 : 1, _isShowing ? 1 : 0, _tempTime);
            transform.localScale = new Vector3(value, value, 0);
            if (!(_tempTime >= 1)) return;
            _canApplyAction = false;
            if (!_isShowing)
                onDialogClosed?.Invoke(this);
        }

        private void ShowDialog()
        {
            _canApplyAction = true;
            _isShowing = true;
            _tempTime = 0;
        }

        private void CloseDialogWithDelay()
        {
            typeWriterUI.onTypingFinished -= CloseDialogWithDelay;
            Invoke("CloseDialog" , timeToCloseDialogAfterTextFullyAppeared);
        }

        private void CloseDialog()
        {
            _canApplyAction = true;
            _isShowing = false;
            _tempTime = 0;
            onDialogClosed?.Invoke(this);
            onDialogClosed = null;
        }
    }
}
