using System;
using Enums;

namespace Models
{
    [Serializable]
    public class DialogModel
    {
        public DialogType Type;
        public string DialogText;
    }
}