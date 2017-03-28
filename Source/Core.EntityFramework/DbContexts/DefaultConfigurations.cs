/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer3.EntityFramework.DbContexts
{
    public static class DefaultConfigurations
    {
        public static void UseSqlServer(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[EfConstants.ConnectionName];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format("Please ensure that the connection string {0} is present in your config file.", EfConstants.ConnectionName));
            }

            optionsBuilder.UseSqlServer(connectionString.ConnectionString);
        }

        public static Action<DbContextOptionsBuilder> UseSqlServer(string connectionStringName)
        {
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException(
                    string.Format("Please ensure that the connection string {0} is present in your config file.", EfConstants.ConnectionName));
            }

            return (optionsBuilder) =>
            {
                optionsBuilder.UseSqlServer(connectionString.ConnectionString);
            };
        }
    }
}
