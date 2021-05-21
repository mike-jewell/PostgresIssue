﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Example.Docker
{
    internal static class ParallelForeachAsync
    {
        /// <summary>
        ///     Executes a foreach asynchronously.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="dop">The degrees of parallelism.</param>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in System.Collections.Concurrent.Partitioner.Create(source).GetPartitions(dop)
                select Task.Run(async delegate
                {
                    using (partition)
                    {
                        while (partition.MoveNext())
                            await body(partition.Current);
                    }
                }));
        }
    }
}
