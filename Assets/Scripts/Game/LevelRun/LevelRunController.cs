using UnityEngine;

namespace Game.LevelRun
{
    public class LevelRunController : MonoBehaviour
    {
        [SerializeField]
        private LevelRunner _twoDimensionalLevelRunner;

        private LevelRunner _currentLevelRunner;

        public void Initialize()
        {
            _currentLevelRunner = _twoDimensionalLevelRunner;
            _currentLevelRunner.Initialize();
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
