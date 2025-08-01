using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.GameState
{
    public class LevelSelection : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private RectTransform selectionRectTransform;
        [SerializeField] private Button leftButton;
        [SerializeField] private Button startLevelButton;
        [SerializeField] private TMP_Text startLevelText;
        [SerializeField] private Button rightButton;
        
        [Header("Transition Settings")]
        [SerializeField] private Vector2 selectionOnPos;
        [SerializeField] private Vector2 selectionOffPos;
        [SerializeField] private Ease selectionEase;
        [SerializeField] private float selectionMoveDuration = 0.5f;
        
        private int _selectedLevelIndex;

        private void Awake()
        {
            leftButton.onClick.AddListener(PreviousLevel);
            rightButton.onClick.AddListener(NextLevel);
            startLevelButton.onClick.AddListener(StartLevel);
        }

        private void OnDestroy()
        {
            leftButton.onClick.RemoveListener(PreviousLevel);
            rightButton.onClick.RemoveListener(NextLevel);
            startLevelButton.onClick.RemoveListener(StartLevel);
        }

        public void OpenLevelSelection()
        {
            _selectedLevelIndex = 0;
            selectionRectTransform.DOAnchorPos(selectionOnPos, selectionMoveDuration)
                .SetEase(selectionEase);
        }

        private void StartLevel()
        {
            StartCoroutine(StartLevelCoroutine());
        }

        private IEnumerator StartLevelCoroutine()
        {
            selectionRectTransform.DOAnchorPos(selectionOffPos, selectionMoveDuration)
                .SetEase(selectionEase);
            yield return new WaitForSeconds(selectionMoveDuration);
            GameStateManager.Instance.TransitionToState(EGameState.Gameplay);
        }
        
        private void PreviousLevel()
        {
            if (_selectedLevelIndex > 0)
            {
                _selectedLevelIndex--;
                startLevelText.text = _selectedLevelIndex.ToString();
            }
        }

        private void NextLevel()
        {
            _selectedLevelIndex++;
            startLevelText.text = _selectedLevelIndex.ToString();
        }
    }
}