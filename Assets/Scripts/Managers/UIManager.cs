using System;
using System.Collections.Generic;
using Enums;
using Models;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private DialogData data;
        [SerializeField] private TypingPanelPanel typingPanelPanel;
        [SerializeField] private InventoryPanel inventoryPanel;
        [SerializeField] private ShopPanel shopPanel;

        private List<Panel> _openPanels = new List<Panel>();

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

        private void Start()
        {
            InputManager.Instance.onInventoryCallsToOpen += OpenInventory;
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

            var cloneDialog = Instantiate(typingPanelPanel, transform);
            cloneDialog.onDialogClosed += RemoveDialogFromOpenList;
            cloneDialog.SetDialog(targetDialog.DialogText);
            cloneDialog.Initialize();
            _openPanels.Add(cloneDialog);
        }

        private void OpenInventory()
        {
            if (_openPanels.Count > 0) return;
            var clonePanel = Instantiate(inventoryPanel, transform);
            clonePanel.onDialogClosed += RemoveDialogFromOpenList;
            clonePanel.Initialize();
            _openPanels.Add(clonePanel);
        }

        public void OpenShop()
        {
            if (_openPanels.Count > 0) return;
            var clonePanel = Instantiate(shopPanel, transform);
            clonePanel.onDialogClosed += RemoveDialogFromOpenList;
            clonePanel.Initialize();
            _openPanels.Add(clonePanel);
        }

        private void RemoveDialogFromOpenList(Panel panel)
        {
            _openPanels.Remove(panel);
            panel.onDialogClosed = null;
            Destroy(panel.gameObject);
        }
    }
}