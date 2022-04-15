using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public interface IScreenManagerView : IPresenterView, IViewWithAutoInit
    {
        IViewContainer Screen { get; }
        ScreenManagerPresenter Presenter { get; }

        void IViewWithAutoInit.Init()
        {
            var assets = CoreAccessPoint.Core.Engine.Assets.Screens;
            new ScreenManagerPresenter(this, assets);
        }
    }
}
