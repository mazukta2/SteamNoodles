using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Creation
{
    // prefab must create view presenter by request
    public abstract class ViewPrefab<T> : IPrefab where T : ViewPresenter
    {
        public abstract T Create(ContainerViewPresenter conteiner);
    }
}
