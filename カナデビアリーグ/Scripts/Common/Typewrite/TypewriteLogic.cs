using UnityEngine;

namespace MinutesGame.Common.Typewrite
{
    public class TypewriteLogic
    {
        private int _currentCount = 0;
        private const int MAX_COUNT = 4; // 改行までのカウント数

        /// <summary>
        /// カウントをインクリメントします</br>
        /// 最大値に達している場合はfalseを返します
        /// </summary>
        /// <returns></returns>
        public bool IncrementCount()
        {
            if (_currentCount == MAX_COUNT)
            {
                return false;
            }

            _currentCount++;
            return true;
        }

        /// <summary>
        /// カウントをリセットします
        /// </summary>
        public void ResetCount()
        {
            _currentCount = 0;
        }
    }
}