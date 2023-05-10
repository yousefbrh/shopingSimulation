using System;
using System.Collections.Generic;
using Entities;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SectionIconButton : MonoBehaviour
    {
        [SerializeField] private List<ObjectIcon> objectIcons;
        [SerializeField] private ObjectsType objectsType;
        [SerializeField] private Transform iconSpot;
        [SerializeField] private Button actionButton;

        public ObjectsType ObjectsType => objectsType;
        
        public Action<ObjectsType> onSectionClicked;

        private void Start()
        {
            var targetIcon = objectIcons.Find(icon => icon.ObjectsType == objectsType);
            Instantiate(targetIcon, iconSpot);
            actionButton.onClick.AddListener(ButtonClicked);
        }
        
        private void ButtonClicked()
        {
            onSectionClicked?.Invoke(objectsType);
        }
    }
}