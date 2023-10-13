using UnityEngine;

namespace Game.LevelRun
{
    public abstract class LevelRunner : MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void Activate();
        public abstract void Deactivate();
    }
}
