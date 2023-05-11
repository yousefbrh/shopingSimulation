﻿using System;
 using System.Collections.Generic;
 using System.Globalization;
 using System.Linq;
 using Models;
 using Newtonsoft.Json;
 using UnityEngine;

namespace DefaultNamespace
{
    public static class Prefs
    {
        private static string CustomData
        {
            get => PlayerPrefs.GetString("CustomData", "");
            set => PlayerPrefs.SetString("CustomData", value);
        }
        public static void SaveCustomData(CustomData customData)
        {
            var json = JsonUtility.ToJson(customData);
            CustomData = json;
        }

        public static SavingCustomData GetCustomData()
        {
            var json = CustomData;
            var data = JsonUtility.FromJson<SavingCustomData>(json);
            return data;
        }
    }
}