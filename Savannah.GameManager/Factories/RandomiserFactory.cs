using Savannah.Common.Facades;
using System.ComponentModel.Composition;

namespace Savannah.Common.Factories
{
    public class RandomiserFactory : IRandomiserFactory
    {
        public RandomiserFacade GetRandom()
        {
            return new RandomiserFacade();
        }
    }
}
