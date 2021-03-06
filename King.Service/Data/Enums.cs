﻿namespace King.Service.Data
{
    /// <summary>
    /// Queue Priority
    /// </summary>
    /// <remarks>
    /// This shapes
    /// - Timing Strategy
    /// - Concurrency
    /// - Cost
    /// - Throughput
    /// Default = Low
    /// </remarks>
    public enum QueuePriority
    {
        Low = 0,
        Medium = 1,
        High = 2,
    }
}