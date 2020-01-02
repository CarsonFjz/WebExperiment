﻿using System;
using System.Data;
using System.Transactions;
using Basic.CapWithSugarExtension.Options;
using DotNetCore.CAP;
using Microsoft.Extensions.DependencyInjection;

namespace Basic.CapWithSugarExtension
{
    public static class CapExtension
    {
        public static IServiceCollection AddCapWithSqlSugar(this IServiceCollection services)
        {
            var dbConfig = "";
            var rabbitMqConfig = new RabbitMqOptions();
            var capConfig = new CapOption();
            services.AddCap(x =>
            {
                x.UseMySql(dbConfig);
                x.UseRabbitMQ(opt =>
                {
                    opt.HostName = rabbitMqConfig.HostName;
                    opt.UserName = rabbitMqConfig.UserName;
                    opt.Password = rabbitMqConfig.Password;
                    opt.Port = rabbitMqConfig.Port;
                });
                x.FailedRetryCount = capConfig.FailedRetryCount;
                x.ConsumerThreadCount = capConfig.ConsumerThreadCount;
                x.FailedRetryInterval = capConfig.FailedRetryInterval;
                x.Version = capConfig.Version;
            });

            return services;
        }


        public static void JoinTransaction(this ICapTransaction transaction, IDbTransaction dbTransaction)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("Sqlsugar Transaction Not Begin");
            }

            transaction.DbTransaction = dbTransaction;

            transaction.AutoCommit = false;
        }
    }
}