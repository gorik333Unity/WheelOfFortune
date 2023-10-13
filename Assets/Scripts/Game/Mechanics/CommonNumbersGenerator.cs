using UnityEngine;

namespace Game.Mechanics
{
    [System.Serializable]
    public class CommonNumbersGenerator : INumbersGenerator
    {
        [SerializeField]
        private int _min;

        [SerializeField]
        private int _max;

        [SerializeField]
        private int _minIntervalBetweenNumbers;

        [SerializeField, Header("if 100, number will be xx00, like 1000, 10000")]
        private int _multipliesNumber;

        public int[] GenerateNumbers(int count)
        {

            return new int[count];
        }
    }
}
