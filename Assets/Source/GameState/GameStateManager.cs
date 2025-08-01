using System;
using Source.Utils;
using UnityEngine;

namespace Source.GameState
{
    public class GameStateManager : BaseSingleton<GameStateManager>
    {
        public EGameState CurrentState { get; private set; } = EGameState.MainMenu;
        
        [SerializeField] private IntroCinematic introCinematic;
        [SerializeField] private LevelSelection levelSelection;
        [SerializeField] private GameplayState gameplayState;
        [SerializeField] private Replay replay;

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
                    gameplayState.StartLevel();
                    break;
                case EGameState.Replay:
                    replay.StartReplay();
                    break;
                case EGameState.ResultWin:
                    break;
                case EGameState.ResultLose:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}