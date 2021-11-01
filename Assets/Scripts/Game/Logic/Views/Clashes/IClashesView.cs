using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Units
{
    public interface IClashesView : IView
    {
        void SetStartClashAction(Action onStartClash);
        void ShowButton(bool v);
    }
}
