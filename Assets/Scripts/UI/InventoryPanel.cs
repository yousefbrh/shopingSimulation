using System.Collections.Generic;
using System.Linq;
using Entities;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class InventoryPanel : Panel
    {
        [SerializeField] private List<CustomIcon> equippedCustomIcons;
        [SerializeField] private List<CustomIcon> choosingCustomIcons;
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
            foreach (var choosingCustomIcon in choosingCustomIcons)
            {
                choosingCustomIcon.onCustomChoose += CustomChoose;
            }
        }

        private void CustomChoose(ColorModel colorModel, CustomIcon customIcon)
        {
            var targetCustom = equippedCustomIcons.Find(icon => icon.CustomType == colorModel.CustomType);
            customIcon.SetModel(targetCustom.GetColorModel());
            targetCustom.SetModel(colorModel);
        }

        private void SetCustomIcons()
        {
            foreach (var icon in equippedCustomIcons)
            {
                var targetModel = _characterColorModel.ColorModels.Find(model => model.CustomType == icon.CustomType);
                icon.SetModel(targetModel);
            }
        }
        
        private void ApplyCustomsOnPlayer()
        {
            var modelList = equippedCustomIcons.Select(equippedCustomIcon => equippedCustomIcon.GetColorModel()).ToList();
            var characterModel = new CharacterColorModel()
            {
                ColorModels = modelList
            };
            _player.CustomHandler.ChangeCustom(characterModel);
        }

        protected override void CloseDialog()
        {
            base.CloseDialog();
            ApplyCustomsOnPlayer();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}