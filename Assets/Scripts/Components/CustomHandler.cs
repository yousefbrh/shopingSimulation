using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
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

        private readonly ColorModel _hairColorModel = new ColorModel();
        private readonly ColorModel _eyesColorModel = new ColorModel();
        private readonly ColorModel _skinColorModel = new ColorModel();
        private readonly ColorModel _clothColorModel = new ColorModel();
        private readonly ColorModel _trousersColorModel = new ColorModel();
        private readonly ColorModel _shoesColorModel = new ColorModel();

        private void Start()
        {
            SetEquippedCustoms();
            FillModels();
        }

        private void SetEquippedCustoms()
        {
            
        }

        private void FillModels()
        {
            _hairColorModel.CustomType = CustomType.Hair;
            _hairColorModel.Color = hairsSprite.First().color;
            _eyesColorModel.CustomType = CustomType.Eyes;
            _eyesColorModel.Color = eyesSprite.First().color;
            _skinColorModel.CustomType = CustomType.Skin;
            _skinColorModel.Color = skinsSprite.First().color;
            _clothColorModel.CustomType = CustomType.Cloth;
            _clothColorModel.Color = clothsSprite.First().color;
            _trousersColorModel.CustomType = CustomType.Trousers;
            _trousersColorModel.Color = trousersSprite.First().color;
            _shoesColorModel.CustomType = CustomType.Shoes;
            _shoesColorModel.Color = shoesSprite.First().color;
        }

        public void ChangeCustom(CharacterColorModel characterColorModel)
        {
            foreach (var colorModel in characterColorModel.ColorModels)
            {
                switch (colorModel.CustomType)
                {
                    case CustomType.Hair:
                        ChangeHairColor(colorModel.Color);
                        break;
                    case CustomType.Eyes:
                        ChangeEyesColor(colorModel.Color);
                        break;
                    case CustomType.Skin:
                        ChangeSkinsColor(colorModel.Color);
                        break;
                    case CustomType.Cloth:
                        ChangeClothsColor(colorModel.Color);
                        break;
                    case CustomType.Trousers:
                        ChangeTrousersColor(colorModel.Color);
                        break;
                    case CustomType.Shoes:
                        ChangeShoesColor(colorModel.Color);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            FillModels();
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

        public CharacterColorModel GetCharacterColorModel()
        {
            var modelList = new List<ColorModel>();
            modelList.Add(_hairColorModel);
            modelList.Add(_eyesColorModel);
            modelList.Add(_skinColorModel);
            modelList.Add(_trousersColorModel);
            modelList.Add(_clothColorModel);
            modelList.Add(_shoesColorModel);

            var characterModel = new CharacterColorModel()
            {
                ColorModels = modelList
            };

            return characterModel;
        }
    }
}