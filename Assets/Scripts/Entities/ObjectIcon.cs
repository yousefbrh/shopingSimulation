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

        private ColorModel _colorModel;
        public ObjectsType ObjectsType => objectsType;

        public void SetModel(ColorModel colorModel)
        {
            _colorModel = colorModel;
            
            foreach (var iconImage in iconImages)
            {
                iconImage.color = _colorModel.Color;
            }
            
            // if (playerImages.Count == 0) return;
            //
            // foreach (var playerImage in playerImages)
            // {
            //     playerImage.color = colorModel.Color;
            // }
        }

        public ColorModel GetColorModel()
        {
            return _colorModel;
        }
    }
}