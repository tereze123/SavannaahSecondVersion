using System;

namespace Savannah.Common.Facades
{
    public class RandomiserFacade
    {
        public int Next(int first, int lastExcluded)
        {
            Random random = new Random();
            return random.Next(first, lastExcluded);
        }
    }
}
