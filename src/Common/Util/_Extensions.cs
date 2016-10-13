using System;
using System.Collections.Generic;

namespace Common.Util.Extensions {
    public static class Tuple {
        // Tuple Helpers...
        public static void Add<T1, T2, T3>(this IList<Tuple<T1, T2, T3>> list, T1 item1, T2 item2, T3 item3) {
            list.Add(System.Tuple.Create(item1, item2, item3));
        }
    }
}
