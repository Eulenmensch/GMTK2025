using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Source.GameState
{
    public class GameplayState : MonoBehaviour
    {
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

            while (_currentLevelDuration < levelDuration)
            {
                countDownSlider.value = (1f - _currentLevelDuration) / levelDuration;
                yield return null;
                _currentLevelDuration += Time.deltaTime;
            }
            
            ropeSliderRectTransform.DOAnchorPos(ropeSliderOffPos, ropeSliderMoveDuration)
                .SetEase(ropeSliderEase);
            
            yield return new WaitForSeconds(1f);
            
            GameStateManager.Instance.TransitionToState(EGameState.Replay);
        }
    }
}