﻿using SeranfuenMirrorSyncLib.Data;
using System;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public interface ITimeProvider
    {
        DateTime CurrentTime { get; }
        DaysOfWeekFlag DayOfWeek { get; }
    }
}