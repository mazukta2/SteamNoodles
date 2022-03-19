using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Creation
{
    // prefab must create view presenter by request
    public abstract class ViewPrefab 
    {
        public abstract View Create<T>(ContainerView conteiner) where T : View;
    }
}
