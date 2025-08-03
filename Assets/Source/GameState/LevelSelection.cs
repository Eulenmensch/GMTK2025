using System;
using System.Collections;
using DG.Tweening;
using Source.Rope;
using Source.Utils;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace Source.GameState
{
    public class LevelSelection : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button leftButton;
        [SerializeField] private Button startLevelButton;
        [SerializeField] private TMP_Text startLevelText;
        [SerializeField] private Button rightButton;
        
        [Header("Animation")]
        [SerializeField] private SlideInOutTweenData logBookAnimData;
        
        private int _selectedLevelIndex;

        private void Awake()
        {
            startLevelButton.onClick.AddListener(SelectLevel);
        }
        
        private void OnDestroy()
        {
            leftButton.onClick.RemoveListener(PreviousLevel);
            rightButton.onClick.RemoveListener(NextLevel);
            startLevelButton.onClick.RemoveListener(SelectLevel);
        }

        public void OpenLevelSelection()
        {
            _selectedLevelIndex = 1;
            startLevelText.text = _selectedLevelIndex.ToString();

            logBookAnimData.SlideInTween().Play();
        }

        private void SelectLevel()
        {
            StartCoroutine(SelectLevelCoroutine());
        }

        private IEnumerator SelectLevelCoroutine()
        {
            CameraManager.Instance.SwitchCamera(EGameState.Gameplay);
            logBookAnimData.SlideOutTween().Play();
            
            yield return new WaitForSeconds(logBookAnimData.MoveDuration);
            GameStateManager.Instance.TransitionToState(EGameState.Gameplay);
        }
        
        private void PreviousLevel()
        {
            if (_selectedLevelIndex <= 1) 
                return;
            
            _selectedLevelIndex--;
            startLevelText.text = _selectedLevelIndex.ToString();
        }

        private void NextLevel()
        {
            _selectedLevelIndex++;
            startLevelText.text = _selectedLevelIndex.ToString();
        }
    }
}