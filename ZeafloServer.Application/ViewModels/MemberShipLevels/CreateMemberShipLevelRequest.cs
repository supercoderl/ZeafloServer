﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeafloServer.Domain.Enums;

namespace ZeafloServer.Application.ViewModels.MemberShipLevels
{
    public sealed record CreateMemberShipLevelRequest
    (
        LevelType Type,
        int MinPoint
    );
}
