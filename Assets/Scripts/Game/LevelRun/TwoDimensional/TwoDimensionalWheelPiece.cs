using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.LevelRun.TwoDimensional
{
    [RequireComponent(typeof(RectTransform))]
    public class TwoDimensionalWheelPiece : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private VerticalLayoutGroup _verticalLayoutGroup;

        [SerializeField]
        private TMP_Text _rewardText;

        public RectTransform RectTransform => _rectTransform;
        public RectTransform Content => _content;
        public VerticalLayoutGroup VerticalLayoutGroup => _verticalLayoutGroup;
        public TMP_Text RewardText => _rewardText;
    }
}