using System;
using System.Collections.Generic;
using DefaultNamespace;
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
            var savedData = Prefs.GetCustomData();
            if (savedData == null) return;
            _customData.CustomDataModels.Clear();
            foreach (var model in savedData.CustomDataModels)
            {
                var data = new CustomDataModel()
                {
                    ColorModel = model.ColorModel,
                    IsEquipped = model.IsEquipped,
                    IsPurchased = model.IsPurchased
                };
                _customData.CustomDataModels.Add(data);
            }
        }

        public void SetPurchasedCustoms(List<ColorModel> models)
        {
            foreach (var model in models)
            {
                var data = _customData.CustomDataModels.Find(dataModel => dataModel.ColorModel.Color == model.Color && dataModel.ColorModel.objectsType == model.objectsType);
                data.IsEquipped = false;
            }
            Prefs.SaveCustomData(_customData);
        }
        
        public void SetEquippedCustoms(List<ColorModel> models)
        {
            foreach (var model in models)
            {
                var data = _customData.CustomDataModels.Find(dataModel => dataModel.ColorModel.Color == model.Color && dataModel.ColorModel.objectsType == model.objectsType);
                data.IsEquipped = true;
            }
            Prefs.SaveCustomData(_customData);
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
        
        public List<ColorModel> GetPurchasableCustoms()
        {
            var modelList = new List<ColorModel>();
            foreach (var dataModel in _customData.CustomDataModels)
            {
                if (!dataModel.IsPurchased)
                {
                    modelList.Add(dataModel.ColorModel);
                }
            }

            return modelList;
        }
    }
}