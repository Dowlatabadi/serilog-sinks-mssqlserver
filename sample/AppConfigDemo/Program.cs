﻿using System;
using System.Threading;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace AppConfigDemo
{
    public static class Program
    {
        private const string _connectionString = "Server=localhost;Database=LogTest;Integrated Security=SSPI;";
        private const string _schemaName = "dbo";
        private const string _tableName = "LogEvents";

        public static void Main()
        {
            Log.Logger = new LoggerConfiguration().WriteTo
                .MSSqlServer(
                    connectionString: _connectionString,
                    tableName: _tableName,
                    restrictedToMinimumLevel: LogEventLevel.Debug,
                    batchPostingLimit: MSSqlServerSink.DefaultBatchPostingLimit,
                    period: null,
                    formatProvider: null,
                    autoCreateSqlTable: true,
                    columnOptions: null,
                    schemaName: _schemaName,
                    logEventFormatter: null)
                .CreateLogger();

            Log.Debug("Getting started");

            Log.Information("Hello {Name} from thread {ThreadId}", Environment.GetEnvironmentVariable("USERNAME"),
                Thread.CurrentThread.ManagedThreadId);

            Log.Warning("No coins remain at position {@Position}", new { Lat = 25, Long = 134 });

            Log.CloseAndFlush();
        }
    }
}
