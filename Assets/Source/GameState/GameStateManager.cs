using System;
using System.Collections;
using Source.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace Source.GameState
{
    public class GameStateManager : BaseSingleton<GameStateManager>
    {
        public EGameState CurrentState { get; private set; } = EGameState.MainMenu;
        
        [SerializeField] private IntroCinematic introCinematic;
        [SerializeField] private LevelSelection levelSelection;

        public void TransitionToState(EGameState state)
        {
            if (CurrentState == state)
                return;
            
            switch (state)
            {
                case EGameState.MainMenu:
                    break;
                case EGameState.CinematicIntro:
                    introCinematic.PlayIntroCinematic();
                    break;
                case EGameState.LevelSelect:
                    levelSelection.OpenLevelSelection();
                    break;
                case EGameState.Gameplay:
                    break;
                case EGameState.Replay:
                    break;
                case EGameState.ResultWin:
                    break;
                case EGameState.ResultLose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #region CinematicIntro

        public void StartGame()
        {
            Debug.Log("StartGameButton Test method called.");
            TransitionToState(EGameState.CinematicIntro);
        }

        #endregion
    }
}