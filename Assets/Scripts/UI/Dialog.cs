using System;
using UnityEngine;

namespace UI
{
    public class Dialog : MonoBehaviour
    {
        public Action<Dialog> onDialogClosed;
    }
}