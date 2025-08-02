using System.Collections;
using Source.Rope;
using UnityEngine;

namespace Source.GameState
{
	public class KnotEvaluationState : MonoBehaviour
	{
		[SerializeField] private float maxStrain;
		[SerializeField] private float tugSpeed;
		
		private bool _isTugging;
		public void StartEvaluation()
		{
			StartCoroutine(Tauten());
		}

		private IEnumerator Tauten()
		{
			var playerRb = PlayerController.Instance.Rigidbody;
			var rope = RopeController.Instance.Rope;
			playerRb.isKinematic = true;
			while (_isTugging)
			{
				var ropeStrain = rope.restLength / rope.CalculateLength();
				Debug.Log(ropeStrain);
				if (ropeStrain > maxStrain) _isTugging = false;
				playerRb.MovePosition(playerRb.transform.forward * tugSpeed);
				yield return null;
			}
		}
	}
}