using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Components
{
    public class UIFader : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private float timeToGetFade;
        [SerializeField] private float fadeTargetValue = 0.5f;
        [SerializeField] private float unFadeTargetValue = 1f;
        
        private float _tempTime;
        private bool _canApplyAction;
        private bool _isFading;
        private Action _customCallback;

        private void Update()
        {
            if (!_canApplyAction) return;
            _tempTime += Time.deltaTime / timeToGetFade;
            var currentColor = image.color;
            var value = Mathf.Lerp(currentColor.a, _isFading ? fadeTargetValue : unFadeTargetValue, _tempTime);
            var targetColor = new Color(currentColor.r, currentColor.b, currentColor.b, value);
            image.color = targetColor;

            if (_tempTime >= 1)
            {
                _canApplyAction = false;
                _customCallback?.Invoke();
            }
        }
        
        public void StartFading(Action callBack)
        {
            _customCallback = callBack;
            _tempTime = 0;
            _canApplyAction = true;
            _isFading = true;
        }

        public void StopFading(Action callback)
        {
            _customCallback = callback;
            _tempTime = 0;
            _canApplyAction = true;
            _isFading = false;
        }
    }
}