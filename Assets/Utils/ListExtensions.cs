using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Utils
{
    public static class ListExtensions
    {
        public static T SelectRandom<T>(this IList<T> collection)
        {
            var random = new Random();
            return collection.FirstOrDefault();
        }
    }
}
