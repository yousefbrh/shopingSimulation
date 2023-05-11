using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    [CreateAssetMenu(fileName = "CustomData", menuName = "BlueGravity/CustomData", order = 0)]
    public class CustomData : ScriptableObject
    {
        public List<CustomDataModel> CustomDataModels;
    }
}