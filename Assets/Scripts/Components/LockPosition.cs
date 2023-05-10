using UnityEngine;

namespace Components
{
    public class LockPosition : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private bool followX = true;
        [SerializeField] private bool followY = true;
        [SerializeField] private bool followZ = true;
        [SerializeField] private Vector3 offset = Vector3.zero;
        [SerializeField] private bool useSmoothing = false;
        [SerializeField] private float smoothing = 0.5f;
    
    
        private void Update()
        {
            var targetPosition = target.position + offset;
            var currentPosition = transform.position;
            var newPosition = currentPosition;
            if (followX)
            {
                newPosition.x = targetPosition.x;
            }
            if (followY)
            {
                newPosition.y = targetPosition.y;
            }
            if (followZ)
            {
                newPosition.z = targetPosition.z;
            }
            if (useSmoothing)
            {
                transform.position = Vector3.Lerp(currentPosition, newPosition, smoothing * Time.deltaTime);
            }
            else
            {
                transform.position = newPosition;
            }
        }
    }
}