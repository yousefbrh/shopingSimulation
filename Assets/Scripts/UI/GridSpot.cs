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
        private ObjectIcon _currentIcon;
        private ColorModel _colorModel;

        public ObjectIcon CurrentIcon => _currentIcon;

        public Action<ColorModel, GridSpot> onButtonClicked;

        private void Start()
        {
            actionButton.onClick.AddListener(ButtonClicked);
        }

        private void ButtonClicked()
        {
            onButtonClicked?.Invoke(_colorModel, this);
        }

        public void ShowIcon(ColorModel colorModel)
        {
            HideIcon();
            _colorModel = colorModel;
            var targetIcon = customIcons.Find(icon => icon.ObjectsType == _colorModel.objectsType);
            _currentIcon = Instantiate(targetIcon, transform);
            _currentIcon.SetModel(_colorModel);
        }

        public void HideIcon()
        {
            if (_currentIcon)
                Destroy(_currentIcon.gameObject);
        }
    }
}