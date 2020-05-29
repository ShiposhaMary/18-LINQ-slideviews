using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{

    public static class My_ExtensionsTask
    {
        /// <summary>
        /// Медиана списка из нечетного количества элементов — это серединный элемент списка.
        /// Медиана списка из четного количества элементов — среднее арифметическое двух серединных элементов списка.
        /// </summary>
        /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
        public static double Median(this IEnumerable<double> items)
        {
            var list = items.OrderBy(x=>x).ToList();
            if (list.Count == 0)
                throw new InvalidOperationException();
            
            return list.Count % 2 == 0 ? (list[list.Count / 2] + list[list.Count / 2 - 1]) / 2 : list[list.Count / 2];
        }

        /// <returns>
        /// Возвращает последовательность, состоящую из пар соседних элементов.
        /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
        /// </returns>
        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            var first = false;
            var previouse = default(T);
            foreach (var item in items)
            {
                if (first)
                    yield return Tuple.Create(previouse, item);
                else
                    first = true;
                previouse = item;

            }
        }
    }
}