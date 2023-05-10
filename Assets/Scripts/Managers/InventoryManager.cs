using System;
using System.Collections.Generic;
using Models;
using UnityEngine;

namespace Managers
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private CustomData customData;

        private CustomData _customData;

        public static InventoryManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }

            _customData = customData;
        }

        private void Start()
        {
            GetInventoryData();
        }

        private void GetInventoryData()
        {
            
        }

        public List<ColorModel> GetPurchasedCustoms()
        {
            var modelList = new List<ColorModel>();
            foreach (var dataModel in _customData.CustomDataModels)
            {
                if (dataModel.IsPurchased && !dataModel.IsEquipped)
                {
                    modelList.Add(dataModel.ColorModel);
                }
            }

            return modelList;
        }

        public List<ColorModel> GetEquippedCustoms()
        {
            var modelList = new List<ColorModel>();
            foreach (var dataModel in _customData.CustomDataModels)
            {
                if (dataModel.IsPurchased && dataModel.IsEquipped)
                {
                    modelList.Add(dataModel.ColorModel);
                }
            }

            return modelList;
        }
    }
}