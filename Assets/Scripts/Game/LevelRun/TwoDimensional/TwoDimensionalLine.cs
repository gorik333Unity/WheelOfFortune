using UnityEngine;

namespace Game.LevelRun.TwoDimensional
{
    [RequireComponent(typeof(RectTransform))]
    public class TwoDimensionalLine : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
    }
}