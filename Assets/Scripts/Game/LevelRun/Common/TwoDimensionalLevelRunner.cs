using UnityEngine;

namespace Game.LevelRun.TwoDimensional
{
    public class TwoDimensionalLevelRunner : LevelRunner
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeReference, SubclassSelector]
        private TwoDimensionalGameController _twoDimensionalGameController;

        public override void Initialize()
        {
            _twoDimensionalGameController.Initialize();
            _twoDimensionalGameController.Deactivate();

            _content.gameObject.SetActive(false);
        }

        public override void Activate()
        {
            _twoDimensionalGameController.Activate();

            _content.gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            _twoDimensionalGameController.Deactivate();

            _content.gameObject.SetActive(false);
        }
    }
}
