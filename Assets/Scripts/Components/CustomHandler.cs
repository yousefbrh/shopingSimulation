using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using Managers;
using Models;
using UnityEngine;

namespace Components
{
    public class CustomHandler : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> hairsSprite;
        [SerializeField] private List<SpriteRenderer> eyesSprite;
        [SerializeField] private List<SpriteRenderer> skinsSprite;
        [SerializeField] private List<SpriteRenderer> clothsSprite;
        [SerializeField] private List<SpriteRenderer> trousersSprite;
        [SerializeField] private List<SpriteRenderer> shoesSprite;

        private void Start()
        {
            SetEquippedCustoms();
        }

        private void SetEquippedCustoms()
        {
            var equippedCustoms = InventoryManager.Instance.GetEquippedCustoms();
            ChangeCustom(equippedCustoms);
        }

        public void ChangeCustom(List<ColorModel> colorModels)
        {
            foreach (var colorModel in colorModels)
            {
                switch (colorModel.objectsType)
                {
                    case ObjectsType.Hair:
                        ChangeHairColor(colorModel.Color);
                        break;
                    case ObjectsType.Eyes:
                        ChangeEyesColor(colorModel.Color);
                        break;
                    case ObjectsType.Skin:
                        ChangeSkinsColor(colorModel.Color);
                        break;
                    case ObjectsType.Cloth:
                        ChangeClothsColor(colorModel.Color);
                        break;
                    case ObjectsType.Trousers:
                        ChangeTrousersColor(colorModel.Color);
                        break;
                    case ObjectsType.Shoes:
                        ChangeShoesColor(colorModel.Color);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ChangeHairColor(Color color)
        {
            foreach (var hair in hairsSprite)
            {
                hair.color = color;
            }
        }
        
        private void ChangeEyesColor(Color color)
        {
            foreach (var eye in eyesSprite)
            {
                eye.color = color;
            }
        }

        private void ChangeSkinsColor(Color color)
        {
            foreach (var skin in skinsSprite)
            {
                skin.color = color;
            }
        }

        private void ChangeClothsColor(Color color)
        {
            foreach (var cloth in clothsSprite)
            {
                cloth.color = color;
            }
        }
        
        private void ChangeTrousersColor(Color color)
        {
            foreach (var trousers in trousersSprite)
            {
                trousers.color = color;
            }
        }
        
        private void ChangeShoesColor(Color color)
        {
            foreach (var shoe in shoesSprite)
            {
                shoe.color = color;
            }
        }
    }
}