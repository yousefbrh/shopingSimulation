using System;
using Enums;
using UnityEngine;

namespace Components
{
    public class EnvironmentChanger : MonoBehaviour
    {
        [SerializeField] private EnvironmentType environmentType;

        public Action<EnvironmentType> onEnvironmentChange;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            onEnvironmentChange?.Invoke(environmentType);
            onEnvironmentChange = null;
        }
    }
}