using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Models;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entities
{
    public class CustomIcon : MonoBehaviour
    {
        [SerializeField] private CustomType customType;
        [SerializeField] private List<Image> iconImages;
        [SerializeField] private List<Image> playerImages;
        [SerializeField] private Button actionButton;
        [SerializeField] private bool isEquipmentIcon;
        [SerializeField] private bool isSectionIcon;

        public Action<ColorModel, CustomIcon> onCustomChoose;
        public Action<CustomType> onSectionChoosed;

        private void Start()
        {
            if (isEquipmentIcon)
                actionButton.interactable = false;
            if (isSectionIcon)
                ApplySectionChoosingAction();
        }

        public void ApplyCustomChoosingAction()
        {
            actionButton.onClick.AddListener(ActionButtonForCustomChoosingClicked);
        }

        private void ApplySectionChoosingAction()
        {
            actionButton.onClick.AddListener(ActionButtonForSectionChoosingClicked);
        }

        private void ActionButtonForCustomChoosingClicked()
        {
            var model = new ColorModel()
            {
                CustomType = customType,
                Color = iconImages.First().color
            };
            onCustomChoose?.Invoke(model, this);
        }

        private void ActionButtonForSectionChoosingClicked()
        {
            onSectionChoosed?.Invoke(customType);
        }

        public CustomType CustomType => customType;
        public void SetModel(ColorModel colorModel)
        {
            foreach (var iconImage in iconImages)
            {
                iconImage.color = colorModel.Color;
            }
            
            if (playerImages.Count == 0) return;
            
            foreach (var playerImage in playerImages)
            {
                playerImage.color = colorModel.Color;
            }
        }

        public ColorModel GetColorModel()
        {
            var model = new ColorModel()
            {
                CustomType = customType,
                Color = iconImages.First().color
            };
            return model;
        }
    }
}