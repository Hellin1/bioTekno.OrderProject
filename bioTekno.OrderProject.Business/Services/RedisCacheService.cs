using bioTekno.OrderProject.Business.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.Business.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IConnectionMultiplexer _redisCon;
        private readonly IDatabase _cache;
        private TimeSpan ExpireTime => TimeSpan.FromDays(1);

        public RedisCacheService(IConnectionMultiplexer redisCon)
        {
            _redisCon = redisCon;
            _cache = redisCon.GetDatabase();
        }

        public async Task Clear(string key)
        {
            await _cache.KeyDeleteAsync(key);
        }

        public void ClearAll()
        {
            var endpoints = _redisCon.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = _redisCon.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> action) where T : class
        {
            var result = await _cache.StringGetAsync(key);
            if (result.IsNull)
            {
                result = JsonSerializer.SerializeToUtf8Bytes(await action());
                await SetValueAsync(key, result);
            }
            return JsonSerializer.Deserialize<T>(result);
        }

        public async Task<string> GetValueAsync(string key)
        {
            return await _cache.StringGetAsync(key);
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            return await _cache.StringSetAsync(key, value, ExpireTime);
        }

        public T GetOrAdd<T>(string hashKey, string itemKey, Func<T> createItemFunc) where T : class
        {


            var cachedItem = _cache.HashGet(hashKey, itemKey);

            if (cachedItem.IsNull)
            {
                var newItem = createItemFunc();

                if (newItem != null)
                {
                    var serializedItem = JsonSerializer.Serialize(newItem);
                    _cache.HashSet(hashKey, itemKey, serializedItem);
                    _cache.KeyExpire(hashKey, TimeSpan.FromMinutes(5));
                    return newItem;
                }
                return null;
            }
            return JsonSerializer.Deserialize<T>(cachedItem);
        }






        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> fetchFunc) where T : class
        {

            var redisKey = GetRedisKey(key);
            var cachedData = await _cache.HashGetAllAsync(redisKey);

            if (cachedData is object && cachedData.Length > 0)
            {
                var result = JsonSerializer.Deserialize<T>(cachedData[0].Value);
                return result;
            }

            var newData = await fetchFunc();
            if (newData != null)
            {
                var json = JsonSerializer.Serialize(newData);
                await _cache.HashSetAsync(redisKey, new[] { new HashEntry(redisKey, json) });
                await _cache.KeyExpireAsync(redisKey, ExpireTime);
            }
            return newData;
        }

        private string GetRedisKey(string key) => !string.IsNullOrEmpty(key) ? $"products:{key}":"products";






    }

}

