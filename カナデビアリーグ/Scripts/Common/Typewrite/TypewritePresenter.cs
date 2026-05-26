using UnityEngine;
using VContainer;

namespace MinutesGame.Common.Typewrite
{
    public class TypewritePresenter
    {
        private readonly TypewriteLogic _typewriteLogic;
        private readonly TypewriteView _typewriteView;

        [Inject]
        public TypewritePresenter(TypewriteLogic typewriteLogic, TypewriteView typewriteView)
        {
            _typewriteLogic = typewriteLogic;
            _typewriteView = typewriteView;
        }

        public void StartTypewriteAnimation()
        {
            bool canIncrement = _typewriteLogic.IncrementCount();
            if (canIncrement)
            {
                _typewriteView.TypewriteText();
            }
            else
            {
                _typewriteView.CreateNewLine();
                _typewriteView.TypewriteText();
                _typewriteLogic.ResetCount();
            }
        }
    }
}