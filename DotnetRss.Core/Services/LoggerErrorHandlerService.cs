// <copyright file="LoggerErrorHandlerService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DotnetRss.Core;
using Microsoft.Extensions.Logging;

namespace DotnetRss.Core
{
    /// <summary>
    /// Error Handler Service.
    /// </summary>
    public class LoggerErrorHandlerService : IErrorHandlerService
    {
        private IEnumerable<ILogger> loggers;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerErrorHandlerService"/> class.
        /// </summary>
        /// <param name="loggers">Loggers.</param>
        public LoggerErrorHandlerService(IEnumerable<ILogger>? loggers)
        {
            this.loggers = loggers ?? new List<ILogger>();
        }

        /// <inheritdoc/>
        public event EventHandler<ErrorHandlerEventArgs>? OnError;

        /// <inheritdoc/>
        public void HandleError(Exception exception)
        {
            if (exception == null)
            {
                return;
            }

            // TODO: Log exception to error handling service provider.
            string errorMessage = string.Format("Error", exception.GetType().FullName, exception.Message, exception.StackTrace);

            foreach (var logger in this.loggers)
            {
                logger.Log(LogLevel.Error, errorMessage);
            }

            this.OnError?.Invoke(this, new ErrorHandlerEventArgs(exception));
        }
    }
}