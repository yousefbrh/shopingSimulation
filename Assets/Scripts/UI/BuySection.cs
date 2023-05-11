using System;
using System.Collections.Generic;
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

        private ObjectIcon _currentIcon;
        private CustomDataModel _customDataModel;

        public Action<CustomDataModel> onPurchased;
        public void Initialize(CustomDataModel customDataModel)
        {
            _customDataModel = customDataModel;
            if (_currentIcon)
                Destroy(_currentIcon.gameObject);
            textPrice.text = _customDataModel.Price.ToString();
            var targetIcon = objectIcons.Find(icon => icon.ObjectsType == _customDataModel.ObjectsType);
            _currentIcon = Instantiate(targetIcon, iconPlacement);
            _currentIcon.SetModel(_customDataModel);
            buyButton.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            buyButton.onClick.RemoveAllListeners();
            onPurchased?.Invoke(_customDataModel);
        }
    }
}