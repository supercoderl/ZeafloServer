using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Application.Common;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.Common
{
    public class MessageResult
    {
        public ResponseType Code { get; set; }
        public string Msg { get; set; } = string.Empty;
        public object? Data { get; set; }

        public static MessageResult Instance
        {
            get { return new MessageResult(); }
        }

        public MessageResult Ok(string msg, object? data)
        {
            Code = ResponseType.Success;
            Msg = msg;
            Data = data;
            return this;
        }

        public MessageResult Error(string msg, object? data)
        {
            Code = ResponseType.Error;
            Msg = msg;
            Data = data;
            return this;
        }
    }
}