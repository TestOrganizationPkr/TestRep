using System;
using Microsoft.ApplicationInsights;

namespace ToDoWebApp.AppAnalytics
{
    public class Analytics //: IAnalytics
    {
        /// <summary>
        /// Method to Track each events
        /// </summary>
        /// <param name="eventName"></param>
        public static void TrackEvent(string eventName)
        {
            var telemetry = new TelemetryClient();
            telemetry.TrackEvent(eventName);
        }

        /// <summary>
        /// Method to track the metrics
        /// </summary>
        /// <param name="message"></param>
        /// <param name="metricValue"></param>
        public void TrackMetrics(string message, double metricValue)
        {
            var telemetry = new TelemetryClient();
            telemetry.TrackMetric(message, metricValue);
        }
    }
}
