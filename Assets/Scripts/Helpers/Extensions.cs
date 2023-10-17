using UnityEngine;

namespace Helpers
{
    public static class Extensions
    {
        public static string GetBriefTextOfScore(float score)
        {
            var scoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah",
                "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay",
                "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp",
                "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };

            int i;
            for (i = 0; i < scoreNames.Length; i++)
            {
                if (score < 900)
                {
                    break;
                }
                else
                {
                    score = Mathf.Floor(score / 100f) / 10f;
                }
            }

            string result;
            if (score == Mathf.Floor(score))
            {
                result = score.ToString() + scoreNames[i];
            }
            else
            {
                result = score.ToString("F1") + scoreNames[i];
            }
            return result;
        }
    }
}
