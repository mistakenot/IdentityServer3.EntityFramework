﻿using System;
using IdentityServer3.EntityFramework;
using IdentityServer3.EntityFramework.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Core.EntityFramework.IntegrationTests
{
    public class ClientConfigurationDbContextTests : IDisposable
    {
        private const string ConfigConnectionStringName = "Config";

        public ClientConfigurationDbContextTests()
        {
            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                db.Database.EnsureCreated();
            }
        }

        [Fact]
        public void CanAddAndDeleteClientScopes()
        {
            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                db.Clients.Add(new Client
                {
                    ClientId = "test-client-scopes",
                    ClientName = "Test Client"
                });

                db.SaveChanges();
            }

            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                var client = db.Clients.Include(x => x.AllowedScopes).First();

                client.AllowedScopes.Add(new ClientScope
                {
                    Scope = "test"
                });

                db.SaveChanges();
            }

            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                var client = db.Clients.Include(x => x.AllowedScopes).First();
                var scope = client.AllowedScopes.First();

                client.AllowedScopes.Remove(scope);

                db.SaveChanges();
            }

            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                var client = db.Clients.Include(x => x.AllowedScopes).First();

                Assert.Equal(0, client.AllowedScopes.Count());
            }
        }

        [Fact]
        public void CanAddAndDeleteClientRedirectUri()
        {
            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                db.Clients.Add(new Client
                {
                    ClientId = "test-client",
                    ClientName = "Test Client"
                });

                db.SaveChanges();
            }

            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                var client = db.Clients.Include(x => x.RedirectUris).First();

                client.RedirectUris.Add(new ClientRedirectUri
                {
                    Uri = "https://redirect-uri-1"
                });

                db.SaveChanges();
            }

            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                var client = db.Clients.Include(x => x.RedirectUris).First();
                var redirectUri = client.RedirectUris.First();

                client.RedirectUris.Remove(redirectUri);

                db.SaveChanges();
            }

            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                var client = db.Clients.Include(x => x.RedirectUris).First();

                Assert.Equal(0, client.RedirectUris.Count());
            }
        }

        public void Dispose()
        {
            using (var db = new ClientConfigurationDbContext(ConfigConnectionStringName))
            {
                db.Database.EnsureDeleted();
            }
        }
    }
}