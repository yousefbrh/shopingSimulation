using System;
using Enums;
using Managers;
using UnityEngine;

namespace Entities
{
    public class ShopKeeper : MonoBehaviour
    {
        [SerializeField] private DialogType dialogType;

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            UIManager.Instance.ShowTypingDialogPanel(dialogType);
        }
    }
}