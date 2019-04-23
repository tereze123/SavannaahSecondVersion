using Savannah.Animals.Factories;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace Savannah.Client
{
    public class PluginLoader
    {
        public void LoadPlugins(IAnimalFactory animalFactory)
        {
            DirectoryCatalog catalog = new DirectoryCatalog(@"C:\Users\tereze.elize.empele\source\repos\ForPluginTest\ForPluginTest\bin\Debug\netcoreapp2.1", "*.dll");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(animalFactory);
        }
    }
}
