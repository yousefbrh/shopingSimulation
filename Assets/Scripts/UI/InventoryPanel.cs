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
        private List<CustomDataModel> _equippedList;
        private List<CustomDataModel> _purchasedList;
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

        private void CustomChoose(CustomDataModel customDataModel, GridSpot gridSpot)
        {
            var targetCustom = equippedCustomIcons.Find(icon => icon.ObjectsType == customDataModel.ObjectsType);
            gridSpot.ShowIcon(targetCustom.GetCustomDataModel());
            
            var equippedRemoveTarget = _equippedList.Find(model => 
                model.ObjectsType == 
                targetCustom.GetCustomDataModel().ObjectsType && model.Color == targetCustom.GetCustomDataModel().Color);
            _equippedList.Remove(equippedRemoveTarget);
            _equippedList.Add(customDataModel);
            
            var purchasedRemoveTarget = _purchasedList.Find(model => 
                model.Color == customDataModel.Color && model.ObjectsType == customDataModel.ObjectsType);
            _purchasedList.Remove(purchasedRemoveTarget);
            _purchasedList.Add(targetCustom.GetCustomDataModel());
            
            targetCustom.SetModel(customDataModel);

            var targetBodyParts = bodyParts.FindAll(part => part.ObjectsType == customDataModel.ObjectsType);
            foreach (var bodyPart in targetBodyParts)
            {
                bodyPart.SetColor(customDataModel.Color);
            }
        }

        private void FillInventorySpots()
        {
            ClearInventorySpots();
            var filterItems = _purchasedList.FindAll(model => model.ObjectsType == _currentSection);
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
                var targetModel = _equippedList.Find(model => model.ObjectsType == icon.ObjectsType);
                icon.SetModel(targetModel);
                var targetBodyParts = bodyParts.FindAll(part => part.ObjectsType == targetModel.ObjectsType);
                foreach (var bodyPart in targetBodyParts)
                {
                    bodyPart.SetColor(targetModel.Color);
                }
            }
        }
        
        private void ApplyCustomsOnPlayer()
        {
            var modelList = equippedCustomIcons.Select(equippedCustomIcon => equippedCustomIcon.GetCustomDataModel()).ToList();
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