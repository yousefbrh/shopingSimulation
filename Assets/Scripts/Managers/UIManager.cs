using System;
using System.Collections.Generic;
using Enums;
using Models;
using UI;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private DialogData data;
        [SerializeField] private TypingDialogPanel typingDialogPanel;

        private List<Dialog> _openPanels = new List<Dialog>();

        public static UIManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
        }

        public void ShowTypingDialogPanel(DialogType type)
        {
            if (_openPanels.Count > 0) return;
            var targetDialog = data.Models.Find(dialog => dialog.Type == type);
            if (targetDialog == null)
            {
                Debug.LogError("Dialog Not Found!!!");
                return;
            }

            var cloneDialog = Instantiate(typingDialogPanel, transform);
            cloneDialog.onDialogClosed += RemoveDialogFromOpenList;
            cloneDialog.Initialize(targetDialog.DialogText);
            _openPanels.Add(cloneDialog);
        }

        private void RemoveDialogFromOpenList(Dialog dialog)
        {
            _openPanels.Remove(dialog);
        }
    }
}