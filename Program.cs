using System;
using Npgsql;

namespace PGDemo // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connString = "Host=localhost;Username=postgres;Password=Helloworld;Database=postgres";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

        }
    }

}
    