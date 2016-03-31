﻿
using System;
using Serilog.Configuration;
using Serilog.Events;

// Copyright 2014 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using Serilog.Sinks.Slack;
using SlackChannelCollection = Serilog.Sinks.Slack.SlackChannelCollection;

namespace Serilog
{
    /// <summary>
    /// Adds the WriteTo.MSSqlServer() extension method to <see cref="LoggerConfiguration"/>.
    /// </summary>
    public static class LoggerConfigurationSlackExtensions
    {
        /// <summary>
        /// Adds a sink that writes log events to a channel in Slack.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="channels">List of Slack channels.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">FormatProvider to apply to <see cref="LogEvent.RenderMessage(IFormatProvider)"/>.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration Slack(
            this LoggerSinkConfiguration loggerConfiguration,
            SlackChannelCollection channels,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null
        )
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException("loggerConfiguration");

            if (channels == null)
                throw new ArgumentNullException("channels");

            if (channels.Count == 0)
                throw new ArgumentException("Must have at least one Slack channel defined.");

            return loggerConfiguration.Sink(
                new SlackSink(
                    channels,
                    formatProvider
                ),
                restrictedToMinimumLevel);
        }

        /// <summary>
        /// Adds a sink that writes log events to a channel in Slack.
        /// </summary>
        /// <param name="loggerConfiguration">The logger configuration.</param>
        /// <param name="channelId">Slack Channel Id.</param>
        /// <param name="token">Token that allows Slack authentication. To manage tokens go to https://api.slack.com/tokens.</param>
        /// <param name="restrictedToMinimumLevel">The minimum log event level required in order to write an event to the sink.</param>
        /// <param name="formatProvider">FormatProvider to apply to <see cref="LogEvent.RenderMessage(IFormatProvider)"/>.</param>
        /// <returns>Logger configuration, allowing configuration to continue.</returns>
        /// <exception cref="ArgumentNullException">A required parameter is null.</exception>
        public static LoggerConfiguration Slack(
            this LoggerSinkConfiguration loggerConfiguration,
            string channelId,
            string token,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null
        )
        {
            if (loggerConfiguration == null)
                throw new ArgumentNullException("loggerConfiguration");

            if (string.IsNullOrWhiteSpace(channelId))
                throw new ArgumentNullException("channelId");

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException("token");

            return loggerConfiguration.Sink(
                new SlackSink(
                    channelId,
                    token,
                    formatProvider
                ),
                restrictedToMinimumLevel);
        }
    }
}