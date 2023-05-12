using System;
using Enums;
using Environment = Entities.Environment;

namespace Models
{
    [Serializable]
    public class EnvironmentDataModel
    {
        public EnvironmentType EnvironmentType;
        public Environment Environment;
    }
}