using System;
using System.Collections;
using Npgsql;

namespace ReturnDataAsArray // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var connString = "Host=localhost;Username=postgres;Password=Helloworld;Database=postgres";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            //below code needs an existing table/relation called data (can be changed) to run


            // Retrieve all rows
            int numberOfEntries = 6;
            int numberOfColumns = 3;
            int n = numberOfColumns;
            String[] results = new string[numberOfEntries]; //also need to figure out proper lenghth

            
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitems", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                Console.WriteLine("Displaying results");

                List<string> ID = new List<string>();
                List<string> email = new List<string>();
                while (await reader.ReadAsync())
                {

                    Console.WriteLine(reader[0]);
                    ID.Add(reader[0].ToString());
                    email.Add(reader[1].ToString());
                    

                    //Check the data schema and proceed 
                  
                }
                Console.WriteLine(ID[0] + " has the email of " + email[0]);
               
            }


        }
    }

}

