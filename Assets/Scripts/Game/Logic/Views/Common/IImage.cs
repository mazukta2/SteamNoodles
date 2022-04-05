using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IImage
    {
        string Path { get; }
        void SetPath(string path);
    }
}
