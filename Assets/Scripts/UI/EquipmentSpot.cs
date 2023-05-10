using System.Collections.Generic;
using Entities;
using Enums;
using Models;
using UnityEngine;

namespace UI
{
    public class EquipmentSpot : MonoBehaviour
    {
        [SerializeField] private List<ObjectIcon> objectIcons;
        [SerializeField] private ObjectsType objectsType;
        [SerializeField] private Transform iconSpot;

        public ObjectsType ObjectsType => objectsType;

        private ObjectIcon _currentIcon;

        private void Start()
        {
            if (_currentIcon == null)
                SetCurrentButton();
        }

        public void SetModel(ColorModel colorModel)
        {
            if (_currentIcon == null)
                SetCurrentButton();
            _currentIcon.SetModel(colorModel);
        }

        private void SetCurrentButton()
        {
            var targetIcon = objectIcons.Find(icon => icon.ObjectsType == objectsType);
            _currentIcon = Instantiate(targetIcon, iconSpot);
        }

        public ColorModel GetColorModel()
        {
            return _currentIcon.GetColorModel();
        }
    }
}