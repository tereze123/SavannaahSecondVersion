using Savannah.Common.Facades;

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
