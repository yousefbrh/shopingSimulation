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
        [SerializeField] private List<ObjectIcon> sectionCustomIcons;
        [SerializeField] private List<GridSpot> inventorySpots;
        [SerializeField] private BuySection buySection;
        [SerializeField] private Button exitButton;

        private List<ColorModel> _purchasableColorModel;
        private List<ObjectIcon> _choosingCustomIcons;
        private Player _player;
        private ObjectsType _currentSection;

        private void Start()
        {
            FillVariables();
            SubscribeActions();
            // SetCustomIcons();
            FillGridSpots();
        }
        
        private void FillVariables()
        {
            _player = GameManager.Instance.Player;
            _purchasableColorModel = InventoryManager.Instance.GetPurchasableCustoms();
            _currentSection = sectionCustomIcons.First().ObjectsType;
        }
        
        private void SubscribeActions()
        {
            exitButton.onClick.AddListener(CloseDialog);

            foreach (var sectionCustomIcon in sectionCustomIcons)
            {
                // sectionCustomIcon.onSectionChoosed += ChangeCurrentSection;
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
            _choosingCustomIcons.Clear();
            var filterItems = _purchasableColorModel.FindAll(model => model.objectsType == _currentSection);
            if (filterItems.Count == 0) return;
            for (int i = 0; i < filterItems.Count; i++)
            {
                inventorySpots[i].ShowIcon(filterItems[i]);
                _choosingCustomIcons.Add(inventorySpots[i].CurrentIcon);
            }
            foreach (var choosingCustomIcon in _choosingCustomIcons)
            {
                // choosingCustomIcon.onCustomChoose += CustomChoose;
            }
        }

        private void CustomChoose(ColorModel arg1, ObjectIcon arg2)
        {
            throw new System.NotImplementedException();
        }

        private void ClearGridSpots()
        {
            foreach (var inventorySpot in inventorySpots)
            {
                inventorySpot.HideIcon();
            }
        }
    }
}