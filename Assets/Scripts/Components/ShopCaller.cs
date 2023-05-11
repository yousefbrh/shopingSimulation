using Managers;
using UnityEngine;

namespace Components
{
    public class ShopCaller : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            UIManager.Instance.OpenShop();
        }
    }
}
