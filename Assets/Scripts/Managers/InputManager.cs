using System;
using UnityEngine;

namespace Managers
{
    public class InputManager : MonoBehaviour
    {
        public Action<Vector2> onMovementInputChanged;
        public Action onInventoryCallsToOpen;
        public static InputManager Instance;

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
        }
        private void Update()
        {
            MovementHandler();
            InventoryHandler();
        }

        private void MovementHandler()
        {
            var inputMovement = Vector2.zero;
            inputMovement.x = Input.GetAxis("Horizontal");
            inputMovement.y = Input.GetAxis("Vertical");
            onMovementInputChanged?.Invoke(inputMovement);
        }

        private void InventoryHandler()
        {
            if (Input.GetKeyDown("return"))
            {
                onInventoryCallsToOpen?.Invoke();
            }
        }

        private void OnDestroy()
        {
            onMovementInputChanged = null;
            onInventoryCallsToOpen = null;
        }
    }
}