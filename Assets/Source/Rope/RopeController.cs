using Obi;
using UnityEngine;

namespace Source.Rope
{
	public class RopeController : MonoBehaviour
	{
		[SerializeField] private float growthSpeed;
		
		private ObiRopeCursor cursor;
		private ObiRope rope;

		void Start () {
			cursor = GetComponentInChildren<ObiRopeCursor>();
			rope = cursor.GetComponent<ObiRope>();
		}

		void FixedUpdate () {
				cursor.ChangeLength(growthSpeed * Time.deltaTime);
				Debug.Log(rope.restLength);
		}
		
	}
}