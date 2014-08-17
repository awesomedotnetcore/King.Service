﻿namespace King.Service
{
    using King.Service.Timing;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Dynamic Task, base class for Tasks which change their own timing based on quantity of work
    /// </summary>
    public abstract class DynamicTask : TaskManager
    {
        #region Members
        /// <summary>
        /// Timing Helper
        /// </summary>
        protected readonly IDynamicTiming timing = null;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor for Mocking
        /// </summary>
        /// <param name="timing">Timing</param>
        /// <param name="minimumPeriodInSeconds">Minimum, time in seconds</param>
        /// <param name="maximumPeriodInSeconds">Maximum, time in seconds</param>
        public DynamicTask(IDynamicTiming timing, int minimumPeriodInSeconds = 45, int maximumPeriodInSeconds = 300)
            : base(minimumPeriodInSeconds, minimumPeriodInSeconds)
        {
            if (null == timing)
            {
                throw new ArgumentNullException("timing");
            }

            this.timing = timing;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Run
        /// </summary>
        public override void Run()
        {
            bool workWasDone;
            this.Run(out workWasDone);

            Trace.TraceInformation("{0}: {1}Work done. (@ {2})", this.GetType().ToString(), workWasDone ? string.Empty : "No ", DateTime.UtcNow);

            var newTime = this.timing.Get(workWasDone);

            if (base.Every.TotalSeconds != newTime)
            {
                var ts = TimeSpan.FromSeconds(newTime);
                
                base.ChangeTiming(ts);

                Trace.TraceInformation("{0}: Changed timing to: {1}. (@ {2})", this.GetType().ToString(), ts, DateTime.UtcNow);
            }
        }

        /// <summary>
        /// Run
        /// </summary>
        /// <param name="workWasDone">Work Was Done</param>
        public abstract void Run(out bool workWasDone);
        #endregion
    }
}