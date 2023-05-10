using System;
using System.Collections.Generic;
using Entities;
using Enums;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InventoryPanel : Panel
    {
        [SerializeField] private List<CustomIcon> customIcons;
        [SerializeField] private Button exitButton;

        private Player _player;
        private CharacterColorModel _characterColorModel;

        private void Start()
        {
            FillVariables();
            SubscribeActions();
            SetCustomIcons();
        }

        private void FillVariables()
        {
            _player = GameManager.Instance.Player;
            _characterColorModel = _player.CustomHandler.GetCharacterColorModel();
        }

        private void SubscribeActions()
        {
            exitButton.onClick.AddListener(CloseDialog);
        }

        private void SetCustomIcons()
        {
            foreach (var icon in customIcons)
            {
                var targetModel = _characterColorModel.ColorModels.Find(model => model.CustomType == icon.CustomType);
                icon.SetColor(targetModel.Color);
            }
        }

        protected override void CloseDialog()
        {
            base.CloseDialog();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}