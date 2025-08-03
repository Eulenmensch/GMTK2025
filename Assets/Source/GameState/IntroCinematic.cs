using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.GameState;
using Source.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class IntroCinematic : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayableDirector director;
    [SerializeField] private TMP_Text tutorialTMP;
    
    [Header("Tutorial Text Settings")]
    [SerializeField] private List<string> tutorialLines = new List<string>();
    [SerializeField] private SlideInOutTweenData tutorialAnimData;
    [SerializeField] private float waitTime = 1f;
    
    [Header("Character Sprite Settings")]
    [SerializeField] private int hankSpawnLineIndex = 1;
    [SerializeField] private SlideInOutTweenData hankAnimData;
    
    [Header("Title Settings")]
    [SerializeField] private SlideInOutTweenData titleAnimData;
    
    [Header("Knot Image Settings")]
    [SerializeField] private int knotSpawnLineIndex = 2;
    [SerializeField] private SlideInOutTweenData knotAnimData;
    
    [Header("WASD Image Settings")]
    [SerializeField] private int wasdSpawnLineIndex = 3;
    [SerializeField] private SlideInOutTweenData wasdAnimData;
    
    private Coroutine _introCinematicRoutine;
    private bool _isPlaying;
    private Sequence _introSequence;

    public void PlayIntroCinematic()
    {
        _introCinematicRoutine = StartCoroutine(PlayIntroCinematicRoutine());
    }
    
    private IEnumerator PlayIntroCinematicRoutine()
    {
        if (_isPlaying)
            yield break;
        
        Debug.Log("Playing intro cinematic");
        _isPlaying = true;

        titleAnimData.SlideOutTween().Play();
        tutorialAnimData.SlideInTween().Play();
        
        int totalLineCharacterCount = 0;
        foreach (string line in tutorialLines)
            totalLineCharacterCount += line.Length;
        float averageCharDuration = (float)(director.duration / totalLineCharacterCount);
        
        _introSequence = DOTween.Sequence();
        for (var index = 0; index < tutorialLines.Count; index++)
        {
            string line = tutorialLines[index];
            float duration = averageCharDuration * line.Length - waitTime;

            _introSequence.AppendCallback(() => tutorialTMP.text = "");

            if (index == hankSpawnLineIndex) 
                _introSequence.Insert(_introSequence.Duration(), hankAnimData.SlideInTween());

            if (index == knotSpawnLineIndex) 
                _introSequence.Insert(_introSequence.Duration(), knotAnimData.SlideInTween());

            if (index == wasdSpawnLineIndex)
            {
                _introSequence.Insert(_introSequence.Duration(), wasdAnimData.SlideInTween());
            }

            _introSequence.Append(tutorialTMP
                .DOText(line, duration)
                .SetEase(Ease.Linear)
                .SetRelative());

            _introSequence.AppendInterval(waitTime);
        }

        director.Play();
        
        while (director.state == PlayState.Playing)
            yield return null;
        
        LeaveIntroCinematic();
    }

    private void LeaveIntroCinematic()
    {
        _introSequence.Kill();
        tutorialAnimData.SlideOutTween().Play();
        hankAnimData.SlideOutTween().Play();
        knotAnimData.SlideOutTween().Play();
        wasdAnimData.SlideOutTween().Play();
        
        _isPlaying = false;
        GameStateManager.Instance.TransitionToState(EGameState.LevelSelect);
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame 
            || Keyboard.current.spaceKey.wasPressedThisFrame 
            || Keyboard.current.enterKey.wasPressedThisFrame)
        {
            SkipTutorial();
        }
    }
    
    private void SkipTutorial()
    {
        if (director == null || director.playableAsset == null || !_isPlaying)
            return;
        
        StopCoroutine(_introCinematicRoutine);
        
        director.time = director.duration;
        director.Evaluate();
        director.Stop();
        
        LeaveIntroCinematic();
    }
}