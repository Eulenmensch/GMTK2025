using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.GameState;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class IntroCinematic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayableDirector director;
    [SerializeField] private TMP_Text tutorialTMP;
    [SerializeField] private RectTransform tutorialPanel;
    [SerializeField] private RectTransform characterRectTransform;
    
    [Header("Tutorial Text Settings")]
    [SerializeField] private List<string> tutorialLines = new List<string>();
    [SerializeField] private Vector2 tutorialPanelOnPos;
    [SerializeField] private Vector2 tutorialPanelOffPos;
    [SerializeField] private Ease tutorialPanelEase;
    [SerializeField] private float tutorialPanelMoveDuration = 0.5f;
    
    [Header("Character Sprite Settings")]
    [SerializeField] private Vector2 characterOnPos;
    [SerializeField] private Vector2 characterOffPos;
    [SerializeField] private Ease characterEase;
    [SerializeField] private float characterMoveDuration = 0.5f;
    
    private Coroutine introCinematicRoutine;
    private bool isPlaying;

    public void PlayIntroCinematic()
    {
        introCinematicRoutine = StartCoroutine(PlayIntroCinematicRoutine());
    }
    
    private IEnumerator PlayIntroCinematicRoutine()
    {
        isPlaying = true;
        
        tutorialPanel.DOAnchorPos(tutorialPanelOnPos, tutorialPanelMoveDuration)
            .SetEase(tutorialPanelEase);
        characterRectTransform.DOAnchorPos(characterOnPos, characterMoveDuration)
            .SetEase(characterEase);
        
        int totalLineCharacterCount = 0;
        foreach (string line in tutorialLines)
            totalLineCharacterCount += line.Length;
        float averageCharDuration = (float)(director.duration / totalLineCharacterCount);

        Sequence sequence = DOTween.Sequence();
        foreach (string line in tutorialLines)
        {
            float duration = averageCharDuration * line.Length;
            
            sequence.AppendCallback(() => tutorialTMP.text = "");
            
            sequence.Append(tutorialTMP
                .DOText(line, duration)
                .SetEase(Ease.Linear)
                .SetRelative());
        }
        
        director.Play();
        while (director.state == PlayState.Playing)
            yield return null;
        
        tutorialPanel.DOAnchorPos(tutorialPanelOffPos, tutorialPanelMoveDuration)
            .SetEase(tutorialPanelEase);

        characterRectTransform.DOAnchorPos(characterOffPos, characterMoveDuration)
            .SetEase(characterEase);
        
        GameStateManager.Instance.TransitionToState(EGameState.LevelSelect);
        isPlaying = false;
    }

    private void Update()
    {
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            SkipTutorial();
        }
    }
    
    private void SkipTutorial()
    {
        if (director == null || director.playableAsset == null || !isPlaying)
            return;
        
        StopCoroutine(introCinematicRoutine);
        
        director.time = director.duration;
        director.Evaluate();
        director.Stop();
        
        tutorialPanel.DOAnchorPos(tutorialPanelOffPos, tutorialPanelMoveDuration)
            .SetEase(tutorialPanelEase);
        characterRectTransform.DOAnchorPos(characterOffPos, characterMoveDuration)
            .SetEase(characterEase);
        
        GameStateManager.Instance.TransitionToState(EGameState.LevelSelect);
        isPlaying = false;
    }
}