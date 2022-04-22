using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Controls
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
            if (!_highlightedViews.Contains(view))
                throw new Exception($"View {view} is not exists");

            _highlightedViews.Remove(view);

            if (_highlightedViews.Count == 0)
                ClearTooltip();
            else
                SetTooltip(_highlightedViews.First());
        }

        private void HandlePointerEnter(IView view)
        {
            if (_highlightedViews.Contains(view))
                throw new Exception($"View {view} already exists");

            _highlightedViews.Add(view);

            SetTooltip(_highlightedViews.First());
        }

    }
}
