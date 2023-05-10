using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class TypingPanelPanel : Panel
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private TypeWriterUI typeWriterUI;
        [SerializeField] private float timeToCloseDialogAfterTextFullyAppeared;
        
        public override void Initialize()
        {
            base.Initialize();
            typeWriterUI.onTypingFinished += CloseDialogWithDelay;
            typeWriterUI.StartTyping(timeToOpenOrClosePanel);
        }

        public void SetDialog(string dialogText)
        {
            text.text = dialogText;
        }

        private void CloseDialogWithDelay()
        {
            typeWriterUI.onTypingFinished -= CloseDialogWithDelay;
            Invoke("CloseDialog" , timeToCloseDialogAfterTextFullyAppeared);
        }
    }
}
