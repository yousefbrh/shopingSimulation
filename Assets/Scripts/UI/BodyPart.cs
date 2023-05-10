using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BodyPart : MonoBehaviour
    {
        [SerializeField] private ObjectsType objectsType;
        [SerializeField] private List<Image> images;

        public ObjectsType ObjectsType => objectsType;
        
        public void SetColor(Color color)
        {
            foreach (var image in images)
            {
                image.color = color;
            }
        }
    }
}