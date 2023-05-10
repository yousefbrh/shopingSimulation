using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Entities
{
    public class CustomIcon : MonoBehaviour
    {
        [SerializeField] private CustomType customType;
        [SerializeField] private List<Image> iconImages;
        [SerializeField] private List<Image> playerImages;

        public CustomType CustomType => customType;
        public void SetColor(Color color)
        {
            foreach (var iconImage in iconImages)
            {
                iconImage.color = color;
            }

            foreach (var playerImage in playerImages)
            {
                playerImage.color = color;
            }
        }
    }
}