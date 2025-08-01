using Source.GameState;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Menu.Buttons
{
    public class StartButton : MonoBehaviour
    {
        private Button _startButton;
        
        private void Awake()
        {
            _startButton = GetComponent<Button>();
            _startButton.onClick.AddListener(StartGame);
        }
        private void OnDestroy()
        {
            if (_startButton != null)
            {
                _startButton.onClick.RemoveListener(StartGame);
            }
        }
        
        public void StartGame()
        {
            Debug.Log("StartGameButton Test method called.");
            GameStateManager.Instance.TransitionToState(EGameState.CinematicIntro);
        }
    }
}