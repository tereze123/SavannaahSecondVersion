using Savannah.Animals.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Text;

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
