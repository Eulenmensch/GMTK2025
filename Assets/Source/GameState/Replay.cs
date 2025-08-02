using System.Collections;
using Obi;
using Unity.Cinemachine;
using UnityEngine;

namespace Source.GameState
{
    public class Replay : MonoBehaviour
    {
        private static readonly int REPLAY_PERCENTAGE_PROP_ID = Shader.PropertyToID("_ReplayPercentage");

        [Header("References")]
        [SerializeField] private Material replayMat;
        
        [Header("Settings")]
        [SerializeField] private float replayDuration = 3f;
        
        private float _currentReplayDuration;
        
        public void StartReplay()
        {
            StartCoroutine(ReplayCoroutine());
        }

        private IEnumerator ReplayCoroutine()
        {
            ObiRopeExtrudedRenderer ropeRenderer = FindFirstObjectByType<ObiRopeExtrudedRenderer>();
            if (ropeRenderer != null)
            {
                ropeRenderer.material = replayMat;
                ropeRenderer.normalizeV = true;
                ropeRenderer.uvAnchor = 1f;
            }
            CameraManager.Instance.SwitchCamera(EGameState.Replay);
            yield return new WaitForSeconds(CameraManager.Instance.CameraTransitionDuration);
            
            _currentReplayDuration = 0;
            while (replayDuration > _currentReplayDuration)
            {
                replayMat.SetFloat(REPLAY_PERCENTAGE_PROP_ID, _currentReplayDuration/replayDuration);
                yield return null;
                _currentReplayDuration += Time.deltaTime;
            }
            replayMat.SetFloat(REPLAY_PERCENTAGE_PROP_ID, 1f);
            // go to evaluation
        }
    }
}