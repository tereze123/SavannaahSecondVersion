using Savannah.Animals.Factories;
using Savannah.Common;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Savannah.Client
{
    public class PluginLoader
    {
        private readonly IConfiguration configuration;

        public PluginLoader(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void LoadPlugins(IAnimalFactory animalFactory)
        {
            var path = configuration.GetAssemblyPath();
            DirectoryCatalog catalog = new DirectoryCatalog(path, "*.dll");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(animalFactory);
        }
    }
}
