using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class SavingCustomData
    {
        public List<CustomDataModel> CustomDataModels = new List<CustomDataModel>();
    }
}