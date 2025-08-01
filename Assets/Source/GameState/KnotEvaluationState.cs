using Unity.Cinemachine;
using UnityEngine;

namespace Source.GameState
{
	public class KnotEvaluationState : MonoBehaviour
	{
		[SerializeField] private CinemachineCamera evaluationCam;
		[SerializeField] private CinemachineCamera replayCam;
		
		public void StartEvaluation()
		{
			replayCam.gameObject.SetActive(false);
			evaluationCam.gameObject.SetActive(true);
		}

		private void SlackRope()
		{
			
		}
	}
}