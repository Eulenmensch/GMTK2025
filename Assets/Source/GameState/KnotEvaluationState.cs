using System.Collections;
using System.Collections.Generic;
using Obi;
using Source.Rope;
using UnityEngine;

namespace Source.GameState
{
	public class KnotEvaluationState : MonoBehaviour
	{
		[SerializeField] private float maxStrain;
		[SerializeField] private float tugSpeed;
		[SerializeField] private ObiParticleAttachment playerParticleAttachment;
		[SerializeField] private ObiRope evaluationRope;
		[SerializeField] private ObiParticleAttachment originAttachment;
		[SerializeField] private ObiParticleAttachment tuggingAttachment;
		[SerializeField] private GameObject origin;
		[SerializeField] private GameObject tugger;
		
		private bool _isTugging;
		private ObiRopeBlueprint blueprint;
		
		public void StartEvaluation()
		{
			StartCoroutine(Tauten());
			Debug.Log("Tauten");
		}

		private IEnumerator Tauten()
		{
			var rope = RopeController.Instance.Rope;
			// yield return BuildRopeBlueprint(rope, evaluationRope);
			
			var playerRb = PlayerController.Instance.Rigidbody;
			playerRb.isKinematic = true;
			// playerRb.useGravity = true;

			_isTugging = true;
			while (_isTugging)
			{
				var ropeStrain = rope.CalculateLength() / rope.restLength;
				Debug.Log(ropeStrain);
				if (ropeStrain > maxStrain) _isTugging = false;
				playerRb.MovePosition(playerRb.transform.position + playerRb.transform.forward * (tugSpeed * Time.deltaTime));
				yield return null;
			}
		}

		private IEnumerator BuildRopeBlueprint(ObiRope sourceRope, ObiRope targetRope)
		{
			var ropeElements = sourceRope.elements;
			var ropeParticles = new List<int>();
			foreach (var element in ropeElements)
			{
				if (!ropeParticles.Contains(element.particle1))
				{
					ropeParticles.Add(element.particle1);
				}
				if (!ropeParticles.Contains(element.particle2))
				{
					ropeParticles.Add(element.particle2);
				}
			}
			
			blueprint = ScriptableObject.CreateInstance<ObiRopeBlueprint>();
			blueprint.pooledParticles = 500;
			blueprint.resolution = 0.0002f;
			
			var newParticleIndex = 0;
			foreach (var particleIndex in ropeParticles)
			{ 
				var localPos = sourceRope.solver.renderablePositions[sourceRope.solverIndices[particleIndex]];
				var mass = sourceRope.solver.invMasses[sourceRope.solverIndices[particleIndex]];
				var rotMass = sourceRope.solver.invRotationalMasses[sourceRope.solverIndices[particleIndex]];
				blueprint.path.AddControlPoint(
					localPos, 
					Vector3.zero, 
					Vector3.zero, 
					Vector3.up, 
					mass, 
					rotMass, 
					1, 
					0, 
					Color.white, 
					"point" + newParticleIndex 
					);
				newParticleIndex++;
			}

			blueprint.path.FlushEvents();
			yield return blueprint.Generate();
			
			targetRope.ropeBlueprint = blueprint;

			originAttachment.target = origin.transform;
			originAttachment.particleGroup = blueprint.groups[0];
			tuggingAttachment.target = tugger.transform;
			tuggingAttachment.particleGroup = blueprint.groups[^1];
			// targetRope.Teleport(sourceRope.transform.position, sourceRope.transform.rotation);
			sourceRope.gameObject.SetActive(false);
		}
	}
}