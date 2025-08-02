using Obi;
using Source.GameState;
using Source.Utils;
using UnityEngine;

namespace Source.Rope
{
	public class RopeController : BaseSingleton<RopeController>
	{
		[SerializeField] private float growthSpeed;
		
		private ObiRopeCursor _cursor;
		public ObiRope Rope { get; private set; }

		void Start () {
			_cursor = GetComponentInChildren<ObiRopeCursor>();
			Rope = _cursor.GetComponent<ObiRope>();
		}

		void FixedUpdate()
		{
			if (GameStateManager.Instance.GameplayState.IsPlaying)
			{
				_cursor.ChangeLength(growthSpeed);
			}
		}
	}
}