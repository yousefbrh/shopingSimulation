using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;
using Environment = Entities.Environment;

namespace Models
{
    [Serializable]
    [CreateAssetMenu(fileName = "EnvironmentData", menuName = "BlueGravity/EnvironmentData", order = 0)]
    public class EnvironmentData : ScriptableObject
    {
        public List<EnvironmentDataModel> EnvironmentDataModels;
    }
}