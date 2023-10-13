using UnityEngine;

namespace Game.LevelRun
{
    public class LevelRunController : MonoBehaviour
    {
        [SerializeField]
        private LevelRunner _twoDimensionalLevelRunner;

        private LevelRunner _currentLevelRunner;

        private void Awake()
        {
            _currentLevelRunner = _twoDimensionalLevelRunner;
            _twoDimensionalLevelRunner.Initialize();
            _twoDimensionalLevelRunner.Activate();
        }

        public void Activate()
        {
            _currentLevelRunner.Activate();
        }

        public void Deactivate()
        {
            _currentLevelRunner.Deactivate();
        }
    }
}
