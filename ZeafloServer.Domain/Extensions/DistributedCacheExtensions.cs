﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeafloServer.Domain.Extensions
{
    public static class DistributedCacheExtensions
    {
        private static readonly JsonSerializerSettings s_jsonserializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.All,
        };

        public static async Task<T?> GetOrCreateJsonAsync<T>(
            this IDistributedCache cache,
            string key,
            Func<Task<T?>> factory,
            DistributedCacheEntryOptions options,
            CancellationToken cancellationToken = default) where T : class
        {
            var json = await cache.GetStringAsync(key, cancellationToken);

            if (!string.IsNullOrWhiteSpace(json))
            {
                return JsonConvert.DeserializeObject<T>(json, s_jsonserializerSettings);
            }

            var value = await factory();

            if (value == default)
            {
                return value;
            }

            json = JsonConvert.SerializeObject(value, s_jsonserializerSettings);

            await cache.SetStringAsync(key, json, options, cancellationToken);

            return value;
        }
    }
}
