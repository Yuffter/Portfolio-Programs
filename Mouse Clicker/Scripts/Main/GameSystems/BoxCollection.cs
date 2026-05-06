using System.Collections.Generic;
using Project.Main.GameSystems.Actors;
using UnityEngine;

namespace Project.Main.GameSystems
{
    public class BoxCollection
    {
        private readonly List<IBox> _boxes = new List<IBox>(6);
        private const int Width = 2;
        private const int Height = 3;
        
        /// <summary>
        /// 引き出し
        /// </summary>
        public IReadOnlyList<IBox> Boxes => _boxes;
        
        public BoxCollection(List<IBox> boxes)
        {
            _boxes.AddRange(boxes);
        }
        
        public IBox GetBox(int x, int y)
        {
            return _boxes[y * Width + x];
        }
        
        public void SetBox(int x, int y, IBox box)
        {
            _boxes[y * Width + x] = box;
        }
    }
}