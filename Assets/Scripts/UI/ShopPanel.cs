using System.Collections.Generic;
using System.Linq;
using Entities;
using Enums;
using Managers;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopPanel : Panel
    {
        [SerializeField] private List<SectionIconButton> sectionCustomIcons;
        [SerializeField] private List<GridSpot> gridSpots;
        [SerializeField] private List<BodyPart> bodyParts;
        [SerializeField] private BuySection buySection;
        [SerializeField] private Button exitButton;

        private List<CustomDataModel> _purchasableList;
        private List<CustomDataModel> _equippedList;
        private List<CustomDataModel> _purchasedList;
        private Player _player;
        private ObjectsType _currentSection;

        private void Start()
        {
            FillVariables();
            SubscribeActions();
            SetCustomIcons();
            FillGridSpots();
        }
        
        private void FillVariables()
        {
            _player = GameManager.Instance.Player;
            _purchasableList = InventoryManager.Instance.GetPurchasableCustoms();
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

            buySection.onPurchased += CustomPurchased;
        }

        private void CustomPurchased(CustomDataModel model)
        {
            var removeEquippedItem = _equippedList.Find(item => item.ObjectsType == model.ObjectsType);
            _equippedList.Remove(removeEquippedItem);
            _purchasableList.Remove(model);
            _purchasedList.Add(removeEquippedItem);
            _equippedList.Add(model);
            SetCustomIcons();
            ApplyCustomsOnPlayer();
            FillGridSpots();
            SaveData();
        }

        private void SaveData()
        {
            InventoryManager.Instance.SetEquippedCustoms(_equippedList);
            InventoryManager.Instance.SetPurchasedCustoms(_purchasedList);
        }
        
        private void ApplyCustomsOnPlayer()
        {
            _player.CustomHandler.ChangeCustom(_equippedList);
        }

        private void SetCustomIcons()
        {
            foreach (var equipped in _equippedList)
            {
                var targetBodyParts = bodyParts.FindAll(part => part.ObjectsType == equipped.ObjectsType);
                foreach (var bodyPart in targetBodyParts)
                {
                    bodyPart.SetColor(equipped.Color);
                }
            }
        }
        
        private void ChangeCurrentSection(ObjectsType objectsType)
        {
            _currentSection = objectsType;
            FillGridSpots();
        }
        
        private void FillGridSpots()
        {
            ClearGridSpots();
            var filterItems = _purchasableList.FindAll(model => model.ObjectsType == _currentSection);
            filterItems.Sort((a,b) => a.Price.CompareTo(b.Price));
            if (filterItems.Count == 0) return;
            for (int i = 0; i < filterItems.Count; i++)
            {
                gridSpots[i].ShowIcon(filterItems[i]);
            }
        }

        private void CustomChoose(CustomDataModel customDataModel, GridSpot gridSpot)
        {
            buySection.gameObject.SetActive(true);
            buySection.Initialize(customDataModel);
        }

        private void ClearGridSpots()
        {
            foreach (var gridSpot in gridSpots)
            {
                gridSpot.HideIcon();
            }
        }
    }
}