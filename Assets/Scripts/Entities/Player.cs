using Components;
using UnityEngine;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private CustomHandler customHandler;
        [SerializeField] private Movement movement;

        public CustomHandler CustomHandler => customHandler;
        public Movement Movement => movement;
    }
}