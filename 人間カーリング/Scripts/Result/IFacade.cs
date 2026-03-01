using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Result
{
    public interface IFacade
    {
        /// <summary>
        /// 結果画面を表示する
        /// </summary>
        /// <param name="scores"></param>
        /// <param name="totalScore"></param>
        UniTask ShowResultScreen(List<float> scores, bool isNewHighScore = false);
    }
}
