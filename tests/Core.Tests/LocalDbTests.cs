using System;
using System.Diagnostics;
using RimDev.Automation.Sql;
using Xunit;

namespace RimDev.Automation.Core
{
    public class LocalDbTests
    {
        [Fact]
        public void Can_Create_LocalDB_with_custom_instance_name()
        {
            using (var db = new LocalDb(instance: "v12.0"))
            {
                Assert.NotNull(db);
            }
        }

        [Fact]
        public void LocalDB_has_localdb_in_name()
        {
            using (var db = new LocalDb())
            {
                Assert.Contains("localdb", db.DatabaseName);
            }
        }

        [Fact]
        public void LocalDb_defaults_to_MSSQLLocalDB()
        {
            using (var db = new LocalDb())
            {
                Assert.Equal(LocalDb.DefaultInstanceName, db.Instance);
            }
        }

        [Fact]
        public void LocalDb_set_databaseSuffixGenerator()
        {
            var guid = Guid.NewGuid().ToString("N");
            using (var db = new LocalDb(databaseSuffixGenerator: () => guid ))
            {
                Assert.Contains(guid, db.DatabaseName);
                Debug.WriteLine(db.DatabaseName);
            }
        }

        [Fact]
        public void LocalDb_allows_configuration_of_connection_timeout()
        {
            const int Timeout = 1000;

            using (var db = new LocalDb(connectionTimeout: Timeout))
            {
                Console.WriteLine(db.ConnectionString);
                Assert.Contains(string.Format("Connection Timeout={0};", Timeout), db.ConnectionString);
            }
        }

        [Fact]
        public void LocalDb_allows_configuration_of_MultipleActiveResultSets()
        {
            using (var db = new LocalDb(multipleActiveResultSets: true))
            {
                Console.WriteLine(db.ConnectionString);
                Assert.Contains("MultipleActiveResultSets=true;", db.ConnectionString);
            }
        }
    }
}
