using UnityEngine;

namespace Game.LevelRun.TwoDimensional
{
    public class TwoDimensionalWheelPieceData
    {
        public TwoDimensionalWheelPieceData(float reward, float dropChance)
        {
            Reward = reward;
            DropChance = dropChance;
        }

        public float Reward { get; private set; }
        public float DropChance { get; private set; }
        public int Index { get; private set; }
        public float Weight { get; private set; }

        public void SetIndex(int index)
        {
            if (index < 0)
            {
                Debug.LogError("Incorrect  index");
            }

            Index = index;
        }

        public void SetWeight(float weight)
        {
            if (weight < 0)
            {
                Debug.LogError("Incorrect weight");
            }

            Weight = weight;
        }
    }
}