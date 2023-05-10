using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> spriteRenderers;
        [SerializeField] private float timeToGetFade;
        [SerializeField] private float fadeTargetValue = 0.5f;
        [SerializeField] private float unFadeTargetValue = 1f;

        private float _tempTime;
        private bool _canApplyAction;
        private bool _isFading;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            StartFading();
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            StopFading();
        }

        private void Update()
        {
            if (!_canApplyAction) return;
            _tempTime += Time.deltaTime / timeToGetFade;
            foreach (var spriteRenderer in spriteRenderers)
            {
                var currentColor = spriteRenderer.color;
                var value = Mathf.Lerp(currentColor.a, _isFading ? fadeTargetValue : unFadeTargetValue, _tempTime);
                var targetColor = new Color(currentColor.r, currentColor.b, currentColor.b, value);
                spriteRenderer.color = targetColor;
            }

            if (_tempTime >= 1)
                _canApplyAction = false;
        }

        private void StartFading()
        {
            _tempTime = 0;
            _canApplyAction = true;
            _isFading = true;
        }

        private void StopFading()
        {
            _tempTime = 0;
            _canApplyAction = true;
            _isFading = false;
        }
    }
}