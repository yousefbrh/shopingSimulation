using System;
using System.Collections.Generic;
using DefaultNamespace;
using Entities;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BuySection : MonoBehaviour
    {
        [SerializeField] private List<ObjectIcon> objectIcons;
        [SerializeField] private Transform iconPlacement;
        [SerializeField] private TextMeshProUGUI textPrice;
        [SerializeField] private Button buyButton;
        [SerializeField] private Button sellButton;
        [SerializeField] private Image buyButtonBackground;
        [SerializeField] private Color notEnoughMoneyColor;

        private ObjectIcon _currentIcon;
        private CustomDataModel _customDataModel;

        public Action<CustomDataModel> onPurchased;
        public Action<CustomDataModel> onSell;
        public void Initialize(CustomDataModel customDataModel)
        {
            SetData(customDataModel);
            ResetSection();
            ButtonAppearanceHandling();
            SubscribeButtons();
            BackgroundColorHandling();
            FillVariables();
        }

        private void SetData(CustomDataModel customDataModel)
        {
            _customDataModel = customDataModel;
        }

        private void ButtonAppearanceHandling()
        {
            if (_customDataModel.IsPurchased)
            {
                sellButton.gameObject.SetActive(true);
                buyButton.gameObject.SetActive(false);
            }
            else
            {
                buyButton.gameObject.SetActive(true);
                sellButton.gameObject.SetActive(false);
            }
        }

        private void ResetSection()
        {
            if (_currentIcon)
                Destroy(_currentIcon.gameObject);
            buyButton.onClick.RemoveAllListeners();
            sellButton.onClick.RemoveAllListeners();
        }

        private void SubscribeButtons()
        {
            buyButton.onClick.AddListener(BuyButtonClicked);
            sellButton.onClick.AddListener(SellButtonClicked);
        }

        private void BackgroundColorHandling()
        {
            buyButtonBackground.color = !CurrencyHandler.CanDecrease(_customDataModel.Price) ? notEnoughMoneyColor : Color.white;
        }

        private void FillVariables()
        {
            textPrice.text = _customDataModel.Price.ToString();
            var targetIcon = objectIcons.Find(icon => icon.ObjectsType == _customDataModel.ObjectsType);
            _currentIcon = Instantiate(targetIcon, iconPlacement);
            _currentIcon.SetModel(_customDataModel);
        }

        private void BuyButtonClicked()
        {
            if (!CurrencyHandler.CanDecrease(_customDataModel.Price)) return;
            CurrencyHandler.DecreaseMoney(_customDataModel.Price);
            buyButton.onClick.RemoveAllListeners();
            onPurchased?.Invoke(_customDataModel);
        }

        private void SellButtonClicked()
        {
            CurrencyHandler.IncreaseMoney(_customDataModel.Price);
            sellButton.onClick.RemoveAllListeners();
            onSell?.Invoke(_customDataModel);
        }
    }
}