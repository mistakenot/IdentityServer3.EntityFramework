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
using IdentityServer3.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer3.EntityFramework
{
    public class BaseDbContext : DbContext
    {
        private readonly Action<DbContextOptionsBuilder> _onConfiguring;

        public string Schema { get; protected set; }

        public BaseDbContext()
            : this(DefaultConfigurations.UseSqlServer, null)
        {
        }

        public BaseDbContext(Action<DbContextOptionsBuilder> onConfiguring, string schema)
            : base()
        {
            _onConfiguring = onConfiguring;

            this.Schema = schema;

            ConfigureChildCollections();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _onConfiguring(optionsBuilder);
        }

        protected virtual void ConfigureChildCollections() { }

    }
}
