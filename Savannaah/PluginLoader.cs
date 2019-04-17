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
            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(Assembly.LoadFrom(@"C:\Users\tereze.elize.empele\source\repos\Savannaah\Savannaah.Animals\obj\Debug\netcoreapp2.1\Savannah.Animals.dll")));
            var container = new CompositionContainer(catalog);
            container.ComposeParts(animalFactory);
        }
    }
}
