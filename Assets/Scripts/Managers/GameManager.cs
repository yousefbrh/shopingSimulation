using System;
using DefaultNamespace;
using Entities;
using Enums;
using Models;
using UnityEngine;
using Environment = Entities.Environment;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private EnvironmentData environmentData;

        private EnvironmentDataModel _currentEnvironmentModel;
        public Player Player => player;
        public static GameManager Instance;

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
            InitializeCurrency();
        }

        private void Start()
        {
            FillVariables();
            InitializeEnvironment();
        }

        private void FillVariables()
        {
            if (!player)
                player = FindObjectOfType<Player>();
        }

        private void InitializeCurrency()
        {
            CurrencyHandler.Initialize();
        }

        private void InitializeEnvironment()
        {
            _currentEnvironmentModel = new EnvironmentDataModel()
            {
                EnvironmentType = EnvironmentType.World
            };
            CreateEnvironment();
        }

        private void EnvironmentChanger(EnvironmentType environmentType)
        {
            _currentEnvironmentModel.EnvironmentType = environmentType;
            player.Movement.StopMovement();
            UIManager.Instance.FadeScreen(PrepareToCreateEnvironment);
        }

        private void PrepareToCreateEnvironment()
        {
            Destroy(_currentEnvironmentModel.Environment.gameObject);
            CreateEnvironment(true);
        }

        private void CreateEnvironment(bool isFading = false)
        {
            var targetEnvironment =
                environmentData.EnvironmentDataModels.Find(data => 
                    data.EnvironmentType == _currentEnvironmentModel.EnvironmentType).Environment;
            _currentEnvironmentModel.Environment = Instantiate(targetEnvironment);
            _currentEnvironmentModel.Environment.SetEnvironmentChangerCallback(EnvironmentChanger);
            player.transform.position = _currentEnvironmentModel.Environment.SpawnPlacement.position;
            if (!isFading) return;
            UIManager.Instance.ClearScreen(EnvironmentCreationFinished);
        }

        private void EnvironmentCreationFinished()
        {
            player.Movement.StartMovement();
        }
    }
}