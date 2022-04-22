using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Tests.Views.Common
{
    public class ImageMock : IImage
    {
        public string Path { get; private set; }

        public ImageMock()
        {
        }

        public void SetPath(string path)
        {
            Path = path;
        }
    }
}
