using System;
using System.Collections;
using Sirenix.OdinInspector;
using Source.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace Source.GameState
{
    public class GameStateManager : BaseSingleton<GameStateManager>
    {
        public EGameState CurrentState { get; private set; } = EGameState.MainMenu;
        
        [EnumPaging]
        public EGameState StartState;
        
        [SerializeField] private IntroCinematic introCinematic;
        [SerializeField] private LevelSelection levelSelection;
        [SerializeField] private Replay replay;
        [SerializeField] private KnotEvaluationState evaluationState;
        
        [field: SerializeField] public GameplayState GameplayState { get; private set; }

        private void Start()
        {
            TransitionToState(StartState);
        }

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
                    GameplayState.StartLevel();
                    break;
                case EGameState.Replay:
                    replay.StartReplay();
                    break;
                case EGameState.Evaluation:
                    evaluationState.StartEvaluation();
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