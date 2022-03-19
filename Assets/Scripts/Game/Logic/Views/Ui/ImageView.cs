using Game.Assets.Scripts.Game.Environment.Engine;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ImageView : View
    {
        public event Action OnImageSetted = delegate {};
        public string Path { get; private set; }

        public ImageView(ILevel level) : base(level)
        {
        }

        public void SetImage(string path)
        {
            Path = path;
            OnImageSetted();
        }
    }
}
