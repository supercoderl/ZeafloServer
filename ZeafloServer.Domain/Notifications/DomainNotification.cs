﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Shared.Events;

namespace ZeafloServer.Domain.Notifications
{
    public class DomainNotification : DomainEvent
    {
        public string Key { get; }
        public string Value { get; }
        public string Code { get; }
        public object? Data { get; }
        public int Verson { get; private set; } = 1;

        public DomainNotification(
            string key,
            string value,
            string code,
            object? data = null,
            Guid? aggregateId = null
        ) : base(aggregateId ?? Guid.Empty)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            Code = code ?? throw new ArgumentNullException(nameof(code));

            Data = data;
        }
    }
}
