using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Engine.Controls
{
    public class PointerManager : IDisposable
    {
        private IControls _controls;

        public IView CurrentTooltip { get; private set; }
        public event Action OnTooltipChanged = delegate { };

        protected readonly List<IView> _highlightedViews = new List<IView>();
        
        public PointerManager(IControls controls)
        {
            _controls = controls;
            _controls.OnPointerEnter += HandlePointerEnter;
            _controls.OnPointerExit += HandlePointerExit;
        }

        public void Dispose()
        {
            _controls.OnPointerEnter -= HandlePointerEnter;
            _controls.OnPointerExit -= HandlePointerExit;
        }

        protected void SetTooltip(IView view)
        {
            CurrentTooltip = view;
            OnTooltipChanged();
        }

        protected void ClearTooltip()
        {
            CurrentTooltip = null;
            OnTooltipChanged();
        }

        private void HandlePointerExit(IView view)
        {
            _highlightedViews.Remove(view);
            if (_highlightedViews.Count == 0)
                ClearTooltip();
        }

        private void HandlePointerEnter(IView view)
        {
            _highlightedViews.Add(view);
            SetTooltip(view);
        }

    }
}
