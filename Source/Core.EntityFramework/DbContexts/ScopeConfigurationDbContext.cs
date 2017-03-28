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

using IdentityServer3.EntityFramework.DbContexts;
using IdentityServer3.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer3.EntityFramework
{
    public class ScopeConfigurationDbContext : BaseDbContext, IScopeConfigurationDbContext
    {
        public ScopeConfigurationDbContext(string connectionString, string schema = null)
            : base(DefaultConfigurations.UseSqlServer(connectionString), schema)
        {
        }

        protected override void ConfigureChildCollections()
        {
            this.RegisterScopeChildTablesForDelete<Scope>();
        }

        public DbSet<Scope> Scopes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConfigureScopes(Schema);
        }
    }
}
