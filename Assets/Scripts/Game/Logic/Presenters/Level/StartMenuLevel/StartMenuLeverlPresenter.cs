using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Infrastructure;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Music;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.StartMenuLevel
{
    public class StartMenuLeverlPresenter : Disposable, IPresenter
    {

        public StartMenuLeverlPresenter(Models.Levels.Variations.StartMenu level) : this(level, IInfrastructure.Default.Application.Music)
        {
        }

        public StartMenuLeverlPresenter(Models.Levels.Variations.StartMenu level, MusicManager musicManager)
        {
            level.OnDispose += Dispose;

        }

    }
}

