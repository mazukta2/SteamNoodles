using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Unity.Views;
using System.Linq;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class LeveMain : View, ILevel
    {

        public T FindObject<T>() where T : View
        {
            var views = FindObjectsOfType<View>();
            return views.OfType<T>().FirstOrDefault();
        }

        protected override void CreatedInner()
        {
        }

        protected override void DisposeInner()
        {
        }
    }
}