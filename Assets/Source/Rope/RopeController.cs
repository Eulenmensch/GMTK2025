using Obi;
using Source.GameState;
using UnityEngine;

namespace Source.Rope
{
	public class RopeController : MonoBehaviour
	{
		[SerializeField] private float growthSpeed;
		
		private ObiRopeCursor _cursor;
		private ObiRope _rope;

		void Start () {
			_cursor = GetComponentInChildren<ObiRopeCursor>();
			_rope = _cursor.GetComponent<ObiRope>();
		}

		void FixedUpdate () {
			if(GameStateManager.Instance.GameplayState.IsPlaying)
				_cursor.ChangeLength(growthSpeed * Time.deltaTime);
		}
		
	}
}