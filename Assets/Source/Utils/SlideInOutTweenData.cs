using DG.Tweening;
using UnityEngine;

namespace Source.Utils
{
    [System.Serializable]
    public class SlideInOutTweenData
    {
        [field: SerializeField] public RectTransform Transform { get; set; }
        [field: SerializeField] public Vector2 OnPos { get; private set; }
        [field: SerializeField] public Vector2 OffPos { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; } = Ease.OutBack;
        [field: SerializeField] public float MoveDuration { get; private set; } = 0.5f;

        public Tween SlideInTween()
        {
            return Transform.DOAnchorPos(OnPos, MoveDuration).SetEase(Ease);
        }

        public Tween SlideOutTween()
        {
            return Transform.DOAnchorPos(OffPos, MoveDuration).SetEase(Ease);
        }
    }
}