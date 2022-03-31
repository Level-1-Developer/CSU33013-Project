using System;
using System.Collections;
using Npgsql;

using System;
using System.Collections;
using Npgsql;
using DotEnv.Core;


namespace ActionItemsDashboard // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Load values from .env file to establish connection to postgres server running locally.
            new EnvLoader().Load();
            var envReader = new EnvReader();
            var connString = "Host=" + envReader["HOST"] + ";Username=" + envReader["NAME"] + ";Password=" + envReader["PASSWORD"] + "; Database=" + envReader["DATABASE"] + ";";
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            //Create the schema in the database.
            await using (var createSchema = new NpgsqlCommand("CREATE SCHEMA IF NOT EXISTS actionitemsdata", conn))
            await createSchema.ExecuteNonQueryAsync();

            //Create the table in the schema.
            await using (var createTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS postgres.actionitemsdata.actionitems (ID int PRIMARY KEY, target varchar(100) NOT NULL, status varchar(100) NOT NULL, campaign varchar(100) NOT NULL, expiry timestamp NOT NULL, when_created timestamp NOT NULL, when_updated timestamp NOT NULL,  content varchar(100) NOT NULL, country varchar(2) NOT NULL, language varchar(2) NOT NULL, customerset varchar(100) NOT NULL); ", conn))
            await createTable.ExecuteNonQueryAsync();

            //Clear the table in the schema.
            await using (var clearTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.actionitems;", conn))
            await clearTable.ExecuteNonQueryAsync();

            //Insert the sample data into the table.
            await using (var insertData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.actionitems VALUES (17981491, 'johndoe@dell.com', 'converted', 'saved-cart', '2022-01-24 06:00:30', '2022-01-24 04:00:30', '2022-01-24 06:00:31', '{\"cart\": {\"id\": [{\"id\":}]},}', 'jp', 'ja', 'jpbsd1')," +
                                                                                                             "(17981492, 'johndoe@dell.com', 'purchased', 'abandoned-cart', '2022-01-24 06:00:47', '2022-01-24 04:00:47', '2022-01-24 04:00:47', '{\"cart\": {\"id\": [{\"id\":}]},}', 'jp', 'ja', 'jpdhs1')," +
                                                                                                             "(17981493, 'johndoe@dell.com', 'purchased', 'abandoned-cart', '2022-01-24 06:00:49', '2022-01-24 04:00:49', '2022-01-24 04:00:49', '{\"cart\": {\"id\": [{\"id\":}]},}', 'us', 'en', '4')," +
                                                                                                             "(17981494, 'johndoe@dell.com', 'converted', 'abandoned-cart', '2022-01-24 06:05:24', '2022-01-24 04:00:52', '2022-01-24 06:05:29', '{\"cart\": {\"id\": [{\"id\":}]},}', 'us', 'en', '4')," +
                                                                                                             "(17981495, 'johndoe@dell.com', 'purchased', 'abandoned-cart', '2022-01-24 06:04:19', '2022-01-24 04:00:53', '2022-01-24 04:04:19', '{\"cart\": {\"id\": [{\"id\":}]},}', 'us', 'en', '6099')", conn))
            await insertData.ExecuteNonQueryAsync();

            //Pull data from the postgres database.
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.actionitems", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
              //Declaration of a list of ActionItem objects, each element in this list representing a row in the table.
              List<ActionItem> actionItems = new List<ActionItem>();
              while (await reader.ReadAsync())
              {
                 //Convert each row into an ActionItem object, and add it to our list.
                 actionItems.Add(new ActionItem(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString()));
              }

              //Now that all the ActionItem objects have been added, we can print them out.
              for(int i = 0; i < actionItems.Count; i++)
                {
                    Console.WriteLine("Action Item on row " + (i+1) + " has:");
                    Console.WriteLine("ID: " + actionItems[i].ID);
                    Console.WriteLine("Target: " + actionItems[i].target);
                    Console.WriteLine("Status: " + actionItems[i].status);
                    Console.WriteLine("Campaign: " + actionItems[i].campaign);
                    Console.WriteLine("Expiry: " + actionItems[i].expiry);
                    Console.WriteLine("When_Created: " + actionItems[i].when_created);
                    Console.WriteLine("When_Updated: " + actionItems[i].when_updated);
                    Console.WriteLine("Content: " + actionItems[i].content);
                    Console.WriteLine("Country: " + actionItems[i].country);
                    Console.WriteLine("Language: " + actionItems[i].language);
                    Console.WriteLine("CustomerSet: " + actionItems[i].customerset);
                }
            }


        }
    }

    class ActionItem
    {
        public int ID;
        public String target;
        public String status;
        public String campaign;
        public DateTime expiry;
        public DateTime when_created;
        public DateTime when_updated;
        public String content;
        public String country;
        public String language;
        public String customerset;
        public ActionItem(int ID, String target, String status, String campaign, String expiry, String when_created, String when_updated, String content, String country, String language, String customerset)
        {
            this.ID = ID;
            this.target = target;
            this.status = status;
            this.campaign = campaign;
            this.expiry = new DateTime(int.Parse(expiry.ToString().Substring(5, 5)), int.Parse(expiry.ToString().Substring(0, 1)), int.Parse(expiry.ToString().Substring(2, 2)), int.Parse(expiry.ToString().Substring(10, 10).Substring(0, 1)), int.Parse(expiry.ToString().Substring(10, 10).Substring(2, 2)), int.Parse(expiry.ToString().Substring(10, 10).Substring(5, 2)));
            this.when_created = new DateTime(int.Parse(when_created.ToString().Substring(5, 5)), int.Parse(when_created.ToString().Substring(0, 1)), int.Parse(when_created.ToString().Substring(2, 2)), int.Parse(when_created.ToString().Substring(10, 10).Substring(0, 1)), int.Parse(when_created.ToString().Substring(10, 10).Substring(2, 2)), int.Parse(when_created.ToString().Substring(10, 10).Substring(5, 2)));
            this.when_updated = new DateTime(int.Parse(when_updated.ToString().Substring(5, 5)), int.Parse(when_updated.ToString().Substring(0, 1)), int.Parse(when_updated.ToString().Substring(2, 2)), int.Parse(when_updated.ToString().Substring(10, 10).Substring(0, 1)), int.Parse(when_updated.ToString().Substring(10, 10).Substring(2, 2)), int.Parse(when_updated.ToString().Substring(10, 10).Substring(5, 2)));
            this.content = content;
            this.country = country;
            this.language = language;
            this.customerset = customerset;

        }
    }
}