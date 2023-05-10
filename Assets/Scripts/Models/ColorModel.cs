using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    [Serializable]
    public class ColorModel
    {
        [FormerlySerializedAs("CustomType")] public ObjectsType objectsType;
        public Color Color;
    }
}