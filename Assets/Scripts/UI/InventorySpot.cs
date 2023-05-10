using System.Collections.Generic;
using Entities;
using Models;
using UnityEngine;

namespace UI
{
    public class InventorySpot : MonoBehaviour
    {
        [SerializeField] private List<CustomIcon> customIcons;
        private CustomIcon _currentIcon;

        public CustomIcon CurrentIcon => _currentIcon;

        public void ShowIcon(ColorModel colorModel)
        {
            var targetIcon = customIcons.Find(icon => icon.CustomType == colorModel.CustomType);
            _currentIcon = Instantiate(targetIcon, transform);
            _currentIcon.ApplyCustomChoosingAction();
            targetIcon.SetModel(colorModel);
        }

        public void HideIcon()
        {
            if (_currentIcon)
                Destroy(_currentIcon.gameObject);
        }
    }
}