using Project.Main.GameSystems;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Project.Main.Tests
{
    public class BoxCollectionTest : IInitializable
    {
        [Inject]
        private BoxCollection _boxCollection;

        public void Initialize()
        {
            Debug.Log(_boxCollection.Boxes.Count);
        }
    }
}