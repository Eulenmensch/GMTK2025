using System.Collections;
using Obi;
using Unity.Cinemachine;
using UnityEngine;

namespace Source.GameState
{
    public class Replay : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Material replayMat;
        
        [Header("Settings")]
        [SerializeField] private float replayDuration = 3f;
        
        private float currentReplayDuration;
        
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
            }
            CameraManager.Instance.SwitchCamera(EGameState.Replay);
            yield return new WaitForSeconds(1f);
            
            currentReplayDuration = 0;
            while (replayDuration > currentReplayDuration)
            {
                // Set shader variable
                yield return null;
                currentReplayDuration += Time.deltaTime;
            }
            // go to evaluation
        }
    }
}