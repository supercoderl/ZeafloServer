﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.UserLevels
{
    public sealed record AddPointRequest
    (
        Guid UserId,
        ActionType ActionType
    );
}
