using System;
using Components;
using Enums;
using UnityEngine;

namespace Entities
{
    public class Environment : MonoBehaviour
    {
        [SerializeField] private Transform spawnPlacement;
        [SerializeField] private EnvironmentChanger environmentChanger;

        public Transform SpawnPlacement => spawnPlacement;
        public void SetEnvironmentChangerCallback(Action<EnvironmentType> callback)
        {
            environmentChanger.onEnvironmentChange = callback;
        }
    }
}