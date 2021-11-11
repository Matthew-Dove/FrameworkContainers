﻿using System;

namespace FrameworkContainers.Network
{
    public readonly struct HttpOptions
    {
        /// <summary>The time to wait (in seconds) before giving up on a HTTP response.</summary>
        public int TimeoutSeconds { get; }

        public HttpOptions(int timeoutSeconds)
        {
            if (timeoutSeconds <= 0 || timeoutSeconds >= 14400) throw new ArgumentOutOfRangeException(nameof(timeoutSeconds));

            TimeoutSeconds = timeoutSeconds;
        }

        internal static HttpOptions Default => new HttpOptions(30);
    }
}