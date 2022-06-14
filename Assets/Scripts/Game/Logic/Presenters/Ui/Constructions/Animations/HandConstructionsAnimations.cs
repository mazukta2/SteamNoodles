using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions.Animations
{
    public class HandConstructionsAnimations : Disposable
    {
        public event Action OnAnimationsCompleted = delegate { };
        private IHandConstructionView _view;
        private bool _isAnimating;
        private int _currentValue;

        private List<States> _requestedAnimations = new List<States>();

        public HandConstructionsAnimations(IHandConstructionView view)
        {
            _view = view;
            _view.Amount.Value = "0";
            _view.Animator.OnFinished += Animator_OnFinished;
        }

        protected override void DisposeInner()
        {
            _view.Animator.OnFinished -= Animator_OnFinished;
        }

        public void Add(int amount)
        {
            for (int i = 0; i < amount; i++)
                _requestedAnimations.Add(States.Add);
            ResolveAnimation();
        }

        public void Remove(int amount)
        {
            for (int i = 0; i < amount; i++)
                _requestedAnimations.Add(States.Remove);
            ResolveAnimation();
        }

        public void Destroy()
        {
            _requestedAnimations.Add(States.Destroy);
            ResolveAnimation();
        }

        private void ResolveAnimation()
        {
            if (_requestedAnimations.Count == 0)
            {
                PlayAnimation(Animations.Idle);
                OnAnimationsCompleted();
                return;
            }

            if (_isAnimating)
                return;

            var state = _requestedAnimations.First();
            _requestedAnimations.RemoveAt(0);

            if (state == States.Add)
            {
                _isAnimating = true;
                _currentValue++;
                _view.Amount.Value = _currentValue.ToString();
                if (_currentValue == 1)
                    PlayAnimation(Animations.NewOne);
                else
                    PlayAnimation(Animations.ChangesInStack);
            }
            else if (state == States.Remove)
            {
                _isAnimating = true;
                _currentValue--;
                _view.Amount.Value = _currentValue.ToString();
                PlayAnimation(Animations.ChangesInStack);
            } 
            else if (state == States.Destroy)
            {
                _isAnimating = true;
                PlayAnimation(Animations.Destroy);
            }
        }

        private void Animator_OnFinished()
        {
            _isAnimating = false;
            PlayAnimation(Animations.Idle);
            ResolveAnimation();
        }

        private void PlayAnimation(Animations animations)
        {
            _view.Animator.Play(animations.ToString());
        }

        enum States
        {
            Add,
            Remove,
            Destroy
        }

        enum Animations
        {
            Idle,
            NewOne,
            ChangesInStack,
            Destroy
        }
    }
}
