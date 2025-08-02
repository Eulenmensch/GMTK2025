using System;
using Source.Rope;
using Source.Utils;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Source.GameState
{
    public class CameraManager : BaseSingleton<CameraManager>
    {
        public float CameraTransitionDuration { get; private set; }
        
        [SerializeField] private CinemachineCamera levelSelectionCam;
        [SerializeField] private CinemachineCamera gameplayCam;
        [SerializeField] private CinemachineCamera replayCam;
        [SerializeField] private CinemachineCamera mainMenuCam;

        private CinemachineCamera _currentCamera;

        protected override void Awake()
        {
            base.Awake();
            var cinemachineBrain = FindFirstObjectByType<CinemachineBrain>();
            CameraTransitionDuration = cinemachineBrain != null ? cinemachineBrain.DefaultBlend.BlendTime : 2f;
            _currentCamera = mainMenuCam;
        }
        
        private void Start()
        {
            var player = FindFirstObjectByType<PlayerController>();
            if (player != null)
                gameplayCam.Target.TrackingTarget = player.transform;
        }

        public void SwitchCamera(EGameState gameState)
        {
            CinemachineCamera newCamera;
            
            switch (gameState)
            {
                case EGameState.MainMenu:
                    newCamera = mainMenuCam;
                    break;
                case EGameState.LevelSelect:
                    newCamera = levelSelectionCam;
                    break;
                case EGameState.Gameplay:
                    newCamera = gameplayCam;
                    break;
                case EGameState.Replay:
                    newCamera = replayCam;
                    break;
                case EGameState.CinematicIntro:
                case EGameState.ResultWin:
                case EGameState.ResultLose:
                default:
                    return;
            }
            newCamera.gameObject.SetActive(true);
            _currentCamera.gameObject.SetActive(false);
            _currentCamera = newCamera;
        }
    }
}