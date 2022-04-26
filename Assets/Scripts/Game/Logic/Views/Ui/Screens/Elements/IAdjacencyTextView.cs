﻿using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Elements
{
    public interface IAdjacencyTextView : IView
    {
        IWorldText Text { get; }
    }
}
