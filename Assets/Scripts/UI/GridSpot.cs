using System;
using System.Collections.Generic;
using Entities;
using Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GridSpot : MonoBehaviour
    {
        [SerializeField] private List<ObjectIcon> customIcons;
        [SerializeField] private Button actionButton;
        [SerializeField] private Image purchasedBackground;
        private ObjectIcon _currentIcon;
        private CustomDataModel _customDataModel;

        public ObjectIcon CurrentIcon => _currentIcon;

        public Action<CustomDataModel, GridSpot> onButtonClicked;

        private void Start()
        {
            actionButton.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            onButtonClicked?.Invoke(_customDataModel, this);
        }

        public void ShowIcon(CustomDataModel customDataModel)
        {
            HideIcon();
            SetData(customDataModel);
            BackgroundHandling();
            FillVariables();
        }

        private void SetData(CustomDataModel customDataModel)
        {
            _customDataModel = customDataModel;
        }

        private void BackgroundHandling()
        {
            purchasedBackground.enabled = _customDataModel.IsPurchased;
        }

        private void FillVariables()
        {
            var targetIcon = customIcons.Find(icon => icon.ObjectsType == _customDataModel.ObjectsType);
            _currentIcon = Instantiate(targetIcon, transform);
            _currentIcon.SetModel(_customDataModel);
        }

        public void HideIcon()
        {
            if (_currentIcon)
                Destroy(_currentIcon.gameObject);
            purchasedBackground.enabled = false;
        }
    }
}