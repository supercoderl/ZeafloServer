﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Entities;

namespace ZeafloServer.Domain
{
    public static class CacheKeyGenerator
    {
        public static string GetEntityCacheKey<TEntity>(TEntity entity) where TEntity : Entity<Guid>
        {
            return $"{typeof(TEntity)}-{entity.Id}";
        }

        public static string GetEntityCacheKey<TEntity, TId>(TId id) where TEntity : Entity<TId> where TId : IEquatable<TId>
        {
            return $"{typeof(TEntity)}-{id}";
        }
    }
}
