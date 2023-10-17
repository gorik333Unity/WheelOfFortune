using Helpers;
using SaveModule;
using TMPro;
using UnityEngine;

namespace Economy
{
    public class SimpleEconomy : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text[] _briefScoreText;

        private static SimpleEconomy _instance;

        public static void AddScore(float score)
        {
            if (score < 0)
            {
                Debug.LogError("Not valid score");
                return;
            }

            var totalScore = SaveSystem.LoadScore();
            totalScore += score;
            SaveSystem.SaveScore(totalScore);

            var briefScore = Extensions.GetBriefTextOfScore(totalScore);
            foreach (var item in _instance._briefScoreText)
            {
                item.text = briefScore;
            }
        }

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("Not null");
            }

            _instance = this;

            RefreshTotalScore();
        }

        private void RefreshTotalScore()
        {
            var totalScore = SaveSystem.LoadScore();
            var briefScore = Extensions.GetBriefTextOfScore(totalScore);
            foreach (var item in _instance._briefScoreText)
            {
                item.text = briefScore;
            }
        }
    }
}
