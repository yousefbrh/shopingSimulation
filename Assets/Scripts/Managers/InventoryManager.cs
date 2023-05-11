using System;
using System.Collections.Generic;
using DefaultNamespace;
using Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField] private CustomData defaultCustomData;
        [SerializeField] private CustomData customData;
        
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

            customData.CustomDataModels.Clear();
            foreach (var data in defaultCustomData.CustomDataModels)
            {
                var model = new CustomDataModel()
                {
                    Color = data.Color,
                    ObjectsType = data.ObjectsType,
                    Price = data.Price,
                    IsPurchased = data.IsPurchased,
                    IsEquipped = data.IsPurchased
                };
                customData.CustomDataModels.Add(model);
            }
        }

        private void Start()
        {
            GetInventoryData();
        }

        private void GetInventoryData()
        {
            var savedData = Prefs.GetCustomData();
            if (savedData == null) return;
            customData.CustomDataModels.Clear();
            foreach (var model in savedData.CustomDataModels)
            {
                var data = new CustomDataModel()
                {
                    Color = model.Color,
                    ObjectsType = model.ObjectsType,
                    IsEquipped = model.IsEquipped,
                    IsPurchased = model.IsPurchased,
                    Price = model.Price
                };
                customData.CustomDataModels.Add(data);
            }
        }

        public void SetPurchasedCustoms(List<CustomDataModel> models)
        {
            foreach (var model in models)
            {
                var data = customData.CustomDataModels.Find(dataModel => 
                    dataModel.Color == model.Color && dataModel.ObjectsType == model.ObjectsType);
                data.IsEquipped = false;
                data.IsPurchased = true;
            }
            Prefs.SaveCustomData(customData);
        }
        
        public void SetEquippedCustoms(List<CustomDataModel> models)
        {
            foreach (var model in models)
            {
                var data = customData.CustomDataModels.Find(dataModel => 
                    dataModel.Color == model.Color && dataModel.ObjectsType == model.ObjectsType);
                    data.IsEquipped = true;
                    data.IsPurchased = true;
            }
            Prefs.SaveCustomData(customData);
        }
        
        public void SetPurchasableCustoms(List<CustomDataModel> models)
        {
            foreach (var model in models)
            {
                var data = customData.CustomDataModels.Find(dataModel => 
                    dataModel.Color == model.Color && dataModel.ObjectsType == model.ObjectsType);
                data.IsEquipped = false;
                data.IsPurchased = false;
            }
            Prefs.SaveCustomData(customData);
        }

        public List<CustomDataModel> GetPurchasedCustoms()
        {
            var modelList = new List<CustomDataModel>();
            foreach (var dataModel in customData.CustomDataModels)
            {
                if (dataModel.IsPurchased && !dataModel.IsEquipped)
                {
                    modelList.Add(dataModel);
                }
            }

            return modelList;
        }

        public List<CustomDataModel> GetEquippedCustoms()
        {
            var modelList = new List<CustomDataModel>();
            foreach (var dataModel in customData.CustomDataModels)
            {
                if (dataModel.IsPurchased && dataModel.IsEquipped)
                {
                    modelList.Add(dataModel);
                }
            }

            return modelList;
        }
        
        public List<CustomDataModel> GetPurchasableCustoms()
        {
            var modelList = new List<CustomDataModel>();
            foreach (var dataModel in customData.CustomDataModels)
            {
                if (!dataModel.IsPurchased)
                {
                    modelList.Add(dataModel);
                }
            }

            return modelList;
        }
    }
}