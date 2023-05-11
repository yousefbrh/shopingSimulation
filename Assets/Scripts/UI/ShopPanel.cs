using System;
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
        private InventoryManager _inventory;

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
            _currentSection = sectionCustomIcons.First().ObjectsType;
            _inventory = InventoryManager.Instance;
            UpdateLists();
        }

        private void UpdateLists()
        {
            _purchasableList = _inventory.GetPurchasableCustoms();
            _equippedList = _inventory.GetEquippedCustoms();
            _purchasedList = _inventory.GetPurchasedCustoms();
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
            buySection.onSell += CustomSold;
        }

        private void CustomPurchased(CustomDataModel model)
        {
            BuySectionActivationHandling(false);
            PurchaseListTradeHandling(model);
            SetCustomIcons();
            ApplyCustomsOnPlayer();
            SaveData();
            UpdateLists();
            FillGridSpots();
        }

        private void BuySectionActivationHandling(bool isActive)
        {
            buySection.gameObject.SetActive(isActive);
        }

        private void PurchaseListTradeHandling(CustomDataModel model)
        {
            var removeEquippedItem = _equippedList.Find(item => item.ObjectsType == model.ObjectsType);
            _equippedList.Remove(removeEquippedItem);
            _purchasableList.Remove(model);
            _purchasedList.Add(removeEquippedItem);
            _equippedList.Add(model);
        }

        private void CustomSold(CustomDataModel model)
        {
            SellListTradeHandling(model);
            SaveData();
            UpdateLists();
            FillGridSpots();
        }

        private void SellListTradeHandling(CustomDataModel model)
        {
            buySection.gameObject.SetActive(false);
            _purchasedList.Remove(model);
            _purchasableList.Add(model);
        }

        private void SaveData()
        {
            _inventory.SetEquippedCustoms(_equippedList);
            _inventory.SetPurchasedCustoms(_purchasedList);
            _inventory.SetPurchasableCustoms(_purchasableList);
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
            var filterPurchasableItems = _purchasableList.FindAll(model => model.ObjectsType == _currentSection);
            var filterPurchasedItems = _purchasedList.FindAll(model => model.ObjectsType == _currentSection);
            SortGridLists(filterPurchasableItems,filterPurchasedItems);
            InitializeGridSpots(filterPurchasableItems, filterPurchasedItems);
        }
        
        private void SortGridLists(List<CustomDataModel> filterPurchasableItems, List<CustomDataModel> filterPurchasedItems)
        {
            filterPurchasableItems.Sort((a,b) => a.Price.CompareTo(b.Price));
            filterPurchasedItems.Sort((a,b) => a.Price.CompareTo(b.Price));
        }

        private void InitializeGridSpots(List<CustomDataModel> filterPurchasableItems, List<CustomDataModel> filterPurchasedItems)
        {
            for (int i = 0; i < filterPurchasableItems.Count; i++)
            {
                gridSpots[i].ShowIcon(filterPurchasableItems[i]);
            }

            for (int i = filterPurchasableItems.Count; i < filterPurchasedItems.Count + filterPurchasableItems.Count; i++)
            {
                gridSpots[i].ShowIcon(filterPurchasedItems[i - filterPurchasableItems.Count]);
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

        private void UnsubscribeActions()
        {
            foreach (var sectionCustomIcon in sectionCustomIcons)
            {
                sectionCustomIcon.onSectionClicked -= ChangeCurrentSection;
            }
            foreach (var gridSpot in gridSpots)
            {
                gridSpot.onButtonClicked -= CustomChoose;
            }

            buySection.onPurchased -= CustomPurchased;
            buySection.onSell -= CustomSold;
        }

        private void OnDestroy()
        {
            UnsubscribeActions();
        }
    }
}