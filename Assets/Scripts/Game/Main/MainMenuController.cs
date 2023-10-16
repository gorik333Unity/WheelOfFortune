using Game.LevelRun;
using UnityEngine;

namespace Game.Main
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private LevelRunController _levelRunController;

        private void Awake()
        {
            _levelRunController.Initialize();
            _levelRunController.Activate();
        }
    }
}
