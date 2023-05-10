using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Enums;
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
        [SerializeField] private List<CustomIcon> sectionCustomIcons;
        [SerializeField] private List<InventorySpot> inventorySpots;
        [SerializeField] private Button exitButton;

        private Player _player;
        private List<ColorModel> _equippedColorModel;
        private List<ColorModel> _purchasedColorModel;
        private CustomType _currentSection;

        private void Start()
        {
            FillVariables();
            SubscribeActions();
            SetCustomIcons();
            FillInventorySpots();
        }

        private void FillVariables()
        {
            _player = GameManager.Instance.Player;
            _equippedColorModel = InventoryManager.Instance.GetEquippedCustoms();
            _purchasedColorModel = InventoryManager.Instance.GetPurchasedCustoms();
            _currentSection = sectionCustomIcons.First().CustomType;
        }

        private void SubscribeActions()
        {
            exitButton.onClick.AddListener(CloseDialog);

            foreach (var sectionCustomIcon in sectionCustomIcons)
            {
                sectionCustomIcon.onSectionChoosed += ChangeCurrentSection;
            }
        }

        private void ChangeCurrentSection(CustomType customType)
        {
            _currentSection = customType;
            FillInventorySpots();
        }

        private void CustomChoose(ColorModel colorModel, CustomIcon customIcon)
        {
            var targetCustom = equippedCustomIcons.Find(icon => icon.CustomType == colorModel.CustomType);
            customIcon.SetModel(targetCustom.GetColorModel());
            targetCustom.SetModel(colorModel);
        }

        private void FillInventorySpots()
        {
            ClearInventorySpots();
            var filterItems = _purchasedColorModel.FindAll(model => model.CustomType == _currentSection);
            if (filterItems.Count == 0) return;
            for (int i = 0; i < filterItems.Count; i++)
            {
                inventorySpots[i].ShowIcon(filterItems[i]);
                choosingCustomIcons.Add(inventorySpots[i].CurrentIcon);
            }
            foreach (var choosingCustomIcon in choosingCustomIcons)
            {
                choosingCustomIcon.onCustomChoose += CustomChoose;
            }
        }

        private void ClearInventorySpots()
        {
            foreach (var inventorySpot in inventorySpots)
            {
                inventorySpot.HideIcon();
            }
        }

        private void SetCustomIcons()
        {
            foreach (var icon in equippedCustomIcons)
            {
                var targetModel = _equippedColorModel.Find(model => model.CustomType == icon.CustomType);
                icon.SetModel(targetModel);
            }
        }
        
        private void ApplyCustomsOnPlayer()
        {
            var modelList = equippedCustomIcons.Select(equippedCustomIcon => equippedCustomIcon.GetColorModel()).ToList();
            _player.CustomHandler.ChangeCustom(modelList);
        }

        protected override void CloseDialog()
        {
            base.CloseDialog();
            ApplyCustomsOnPlayer();
            exitButton.onClick.RemoveAllListeners();
        }
    }
}