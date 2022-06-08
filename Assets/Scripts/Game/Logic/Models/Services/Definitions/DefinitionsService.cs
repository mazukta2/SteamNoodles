using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Units;
using Game.Assets.Scripts.Game.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Definitions
{
    public class DefinitionsService : IService
    {
        private readonly IModelServices _services;
        private readonly IGameDefinitions _definitions;

        public DefinitionsService(IModelServices services, IGameDefinitions definitions, bool loadDefinitions = true)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _definitions = definitions ?? throw new ArgumentNullException(nameof(definitions));

            //var unitSettings = definitions.Get<UnitsSettingsDefinition>();
            //var constructionSettings = definitions.Get<ConstructionsSettingsDefinition>();
            
            if(loadDefinitions)
                LoadDefinitions();
        }

        public T Get<T>()
        {
            return _definitions.Get<T>();
        }

        public T Get<T>(string id)
        {
            return _definitions.Get<T>(id);
        }

        public IReadOnlyCollection<T> GetList<T>()
        {
            return _definitions.GetList<T>();
        }

        public void LoadDefinitions()
        {
            LoadUnits();
            LoadConstructions();

        }

        private void LoadUnits()
        {
            var unitSettings = _definitions.Get<UnitsSettingsDefinition>();
            var repository = new Repository<UnitType>();
            foreach (var item in _definitions.GetList<CustomerDefinition>())
                repository.Add(new UnitType(item, unitSettings));
            _services.Add(repository);
        }

        private void LoadConstructions()
        {
            var repository = new Repository<ConstructionScheme>();
            var constructionsDefinitions = _definitions.GetList<ConstructionDefinition>();
            ConstructionScheme.FillWithDefinitions(constructionsDefinitions, repository);
            _services.Add(repository);
        }
    }
}
