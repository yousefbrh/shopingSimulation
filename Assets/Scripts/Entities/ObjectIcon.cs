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
    public class ObjectIcon : MonoBehaviour
    {
        [SerializeField] private ObjectsType objectsType;
        [SerializeField] private List<Image> iconImages;

        private CustomDataModel _customDataModel;
        public ObjectsType ObjectsType => objectsType;

        public void SetModel(CustomDataModel customDataModel)
        {
            _customDataModel = customDataModel;
            
            foreach (var iconImage in iconImages)
            {
                iconImage.color = _customDataModel.Color;
            }
        }

        public CustomDataModel GetCustomDataModel()
        {
            return _customDataModel;
        }
    }
}