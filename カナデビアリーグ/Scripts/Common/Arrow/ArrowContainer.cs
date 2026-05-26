using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace MinutesGame.Common.Arrow
{
    public class ArrowContainer
    {
        private List<GameObject> _arrowObjList = new List<GameObject>();
        private Queue<ArrowController> _arrowControllerQueue = new Queue<ArrowController>();
        private List<ArrowController> _arrowControllerList = new List<ArrowController>();

        public Queue<ArrowController> ArrowControllerQueue => _arrowControllerQueue;
        public List<GameObject> ArrowsObjList => _arrowObjList;
        public List<ArrowController> ArrowControllerList => _arrowControllerList;

        [Inject]
        public ArrowContainer(List<ArrowController> arrows)
        {
            foreach (var arrow in arrows)
            {
                _arrowObjList.Add(arrow.gameObject);
                _arrowControllerQueue.Enqueue(arrow);
                _arrowControllerList.Add(arrow);
            }
        }

        /// <summary>
        /// 全ての矢印のフォーカスを外す
        /// </summary>
        public void UnfocusAll()
        {
            foreach (var arrowController in _arrowControllerList)
            {
                arrowController.Unfocus();
            }
        }
    }
}