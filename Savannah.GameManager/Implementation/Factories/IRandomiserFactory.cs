using Savannah.Common.Facades;

namespace Savannah.Common.Factories
{
    public interface IRandomiserFactory
    {
        RandomiserFacade GetRandom();
    }
}