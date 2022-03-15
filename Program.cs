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

            //below code needs an existing table/relation called data (can be changed) to run
            /* Insert some data
            await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
            {
                cmd.Parameters.AddWithValue("p", "Hello world");
                await cmd.ExecuteNonQueryAsync();
            }

             Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                    Console.WriteLine(reader.GetString(0));
            }
            */

        }
    }

}
    
