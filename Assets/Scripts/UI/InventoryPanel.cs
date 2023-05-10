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
        [SerializeField] private List<EquipmentSpot> equippedCustomIcons;
        [SerializeField] private List<SectionIconButton> sectionCustomIcons;
        [SerializeField] private List<GridSpot> gridSpots;
        [SerializeField] private List<BodyPart> bodyParts;
        [SerializeField] private Button exitButton;

        private Player _player;
        private List<ColorModel> _equippedList;
        private List<ColorModel> _purchasedList;
        private ObjectsType _currentSection;

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
            _equippedList = InventoryManager.Instance.GetEquippedCustoms();
            _purchasedList = InventoryManager.Instance.GetPurchasedCustoms();
            _currentSection = sectionCustomIcons.First().ObjectsType;
        }

        private void SubscribeActions()
        {
            exitButton.onClick.AddListener(CloseDialog);

            foreach (var sectionCustomIcon in sectionCustomIcons)
            {
                sectionCustomIcon.onSectionClicked += ChangeCurrentSection;
            }
            foreach (var gridSpot in gridSpots)
            {
                gridSpot.onButtonClicked += CustomChoose;
            }
        }

        private void ChangeCurrentSection(ObjectsType objectsType)
        {
            _currentSection = objectsType;
            FillInventorySpots();
        }

        private void CustomChoose(ColorModel colorModel, GridSpot gridSpot)
        {
            var targetCustom = equippedCustomIcons.Find(icon => icon.ObjectsType == colorModel.objectsType);
            gridSpot.ShowIcon(targetCustom.GetColorModel());
            
            var equippedRemoveTarget = _equippedList.Find(model => 
                model.objectsType == targetCustom.GetColorModel().objectsType && model.Color == targetCustom.GetColorModel().Color);
            _equippedList.Remove(equippedRemoveTarget);
            _equippedList.Add(colorModel);
            
            var purchasedRemoveTarget = _purchasedList.Find(model => model.Color == colorModel.Color && model.objectsType == colorModel.objectsType);
            _purchasedList.Remove(purchasedRemoveTarget);
            _purchasedList.Add(targetCustom.GetColorModel());
            
            targetCustom.SetModel(colorModel);

            var targetBodyParts = bodyParts.FindAll(part => part.ObjectsType == colorModel.objectsType);
            foreach (var bodyPart in targetBodyParts)
            {
                bodyPart.SetColor(colorModel.Color);
            }
        }

        private void FillInventorySpots()
        {
            ClearInventorySpots();
            var filterItems = _purchasedList.FindAll(model => model.objectsType == _currentSection);
            if (filterItems.Count == 0) return;
            for (int i = 0; i < filterItems.Count; i++)
            {
                gridSpots[i].ShowIcon(filterItems[i]);
            }
        }

        private void ClearInventorySpots()
        {
            foreach (var inventorySpot in gridSpots)
            {
                inventorySpot.HideIcon();
            }
        }

        private void SetCustomIcons()
        {
            foreach (var icon in equippedCustomIcons)
            {
                var targetModel = _equippedList.Find(model => model.objectsType == icon.ObjectsType);
                icon.SetModel(targetModel);
                var targetBodyParts = bodyParts.FindAll(part => part.ObjectsType == targetModel.objectsType);
                foreach (var bodyPart in targetBodyParts)
                {
                    bodyPart.SetColor(targetModel.Color);
                }
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
            InventoryManager.Instance.SetEquippedCustoms(_equippedList);
            InventoryManager.Instance.SetPurchasedCustoms(_purchasedList);
            exitButton.onClick.RemoveAllListeners();
        }
    }
}