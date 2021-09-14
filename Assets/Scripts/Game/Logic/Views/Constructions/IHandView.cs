using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IHandView : IView
    {
        IHandConstructionView CreateConstruction();
    }
}
