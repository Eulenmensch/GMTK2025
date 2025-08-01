using Unity.Cinemachine;
using UnityEngine;

namespace Source.GameState
{
    public class Replay : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera gameplayCam;
        [SerializeField] private CinemachineCamera replayCam;
        
        public void StartReplay()
        {
            gameplayCam.gameObject.SetActive(false);
            replayCam.gameObject.SetActive(true);
        }
    }
}