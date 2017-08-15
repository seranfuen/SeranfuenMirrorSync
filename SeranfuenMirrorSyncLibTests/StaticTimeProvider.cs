using System;
using SeranfuenMirrorSyncLib.Controllers;

namespace SeranfuenMirrorSyncLibTests
{
    internal class StaticTimeProvider : ITimeProvider
    {
        private DateTime _staticTime;

        public StaticTimeProvider(DateTime staticTime)
        {
            CurrentTime = staticTime;
        }

        public DateTime CurrentTime
        {
            get;
            private set;
        }
    }
}