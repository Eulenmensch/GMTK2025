using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Source.GameState
{
    public class GameplayState : MonoBehaviour
    {
        public bool IsPlaying { get; private set; } = false;
        
        [Header("References")]
        [SerializeField] private Slider countDownSlider;
        [SerializeField] private RectTransform ropeSliderRectTransform;
        
        [Header("Settings")]
        [SerializeField] private float levelDuration;
        
        [Header("Slider Transition Settings")]
        [SerializeField] private Vector2 ropeSliderOnPos;
        [SerializeField] private Vector2 ropeSliderOffPos;
        [SerializeField] private Ease ropeSliderEase;
        [SerializeField] private float ropeSliderMoveDuration = 0.5f;
        
        private float _currentLevelDuration;
        
        public void StartLevel()
        {
            StartCoroutine(Level());
        }

        private IEnumerator Level()
        {
            ropeSliderRectTransform.DOAnchorPos(ropeSliderOnPos, ropeSliderMoveDuration)
                .SetEase(ropeSliderEase);
            
            yield return new WaitForSeconds(1f);
            
            IsPlaying = true;

            _currentLevelDuration = 0;
            while (_currentLevelDuration < levelDuration)
            {
                countDownSlider.value = (levelDuration - _currentLevelDuration) / levelDuration;
                yield return null;
                _currentLevelDuration += Time.deltaTime;
            }
            
            IsPlaying = false;
            
            ropeSliderRectTransform.DOAnchorPos(ropeSliderOffPos, ropeSliderMoveDuration)
                .SetEase(ropeSliderEase);
            
            yield return new WaitForSeconds(1f);
            
            GameStateManager.Instance.TransitionToState(EGameState.Replay);
        }
    }
}