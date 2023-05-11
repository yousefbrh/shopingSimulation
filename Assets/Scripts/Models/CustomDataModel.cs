using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    [Serializable]
    public class CustomDataModel
    {
        public ObjectsType ObjectsType;
        public Color Color;
        public int Price;
        public bool IsEquipped;
        public bool IsPurchased;
    }
}