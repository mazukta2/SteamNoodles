using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using TMPro;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionTooltipUnityView : UnityView<HandConstructionTooltipPresenter>, IHandConstructionTooltipView
    {
        [SerializeField] TextMeshProUGUI _name;

        [SerializeField] TextMeshProUGUI _points;
        public IText Name { get; private set; }
        public IText Points { get; private set; }

        protected override void PreAwake()
        {
            Name = new UnityText(_name);
            Points = new UnityText(_points);
        }
    }

}
