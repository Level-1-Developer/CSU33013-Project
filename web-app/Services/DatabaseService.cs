using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using web_app.Models;
using System;
using System.Collections;
using Npgsql;
using System.Globalization;
using System;
using System.Collections;
using Npgsql;
using DotEnv.Core;

namespace web_app.Services
{
    public class DatabaseService
    {
        public static List<ActionItem> finalActionItems = new List<ActionItem>();
        public static List<Message> finalMessages = new List<Message>();
        public static List<Batch> finalBatches = new List<Batch>();
        public static List<BatchFile> finalBatchFiles = new List<BatchFile>();
        public static List<BatchForward> finalBatchForwards = new List<BatchForward>();
        public static List<BatchForwardError> finalBatchForwardErrors = new List<BatchForwardError>();
        public static List<ExternalID> finalExternalID = new List<ExternalID>();
        public DatabaseService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            Task.Run(async () => await getListOfObjects());
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName => Path.Combine(WebHostEnvironment.WebRootPath, "data", "database.json");

        public IEnumerable<ActionItem> GetActionItems()
        {
            return finalActionItems;
        }
        public IEnumerable<Message> GetMessages()
        {
            return finalMessages;
        }
        public IEnumerable<Batch> GetBatches()
        {
            return finalBatches;
        }
        public IEnumerable<BatchFile> GetBatchFiles()
        {
            return finalBatchFiles;
        }
        public IEnumerable<BatchForward> GetBatchForwards()
        {
            return finalBatchForwards;
        }
        public IEnumerable<BatchForwardError> GetBatchForwardErrors()
        {
            return finalBatchForwardErrors;
        }
        public IEnumerable<ExternalID> GetExternalID()
        {
            return finalExternalID;
        }

        static async Task getListOfObjects()
        {
            //1. Load values from .env file to establish connection to postgres server running locally.
            new EnvLoader().Load();
            var envReader = new EnvReader();
            var connString = "Host=" + envReader["HOST"] + ";Username=" + envReader["NAME"] + ";Password=" + envReader["PASSWORD"] + "; Database=" + envReader["DATABASE"] + ";";
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            
    
            //2. Declaration of a list of ActionItem objects, each element in this list representing a row in the table.
            List<ActionItem> actionItems = new List<ActionItem>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.actionitems", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an ActionItem object, and add it to our list.
                    actionItems.Add(new ActionItem(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString()));
                }
                finalActionItems = actionItems;
                //Now that all the ActionItem objects have been added, we can print them out.
                for (int i = 0; i < actionItems.Count; i++)
                {
                    Console.WriteLine("Action Item on row " + (i + 1) + " has:");
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

            //Pull data from messages table into list of Message

            //3. Declaration of a list of Message objects, each element in this list representing a row in the table.
            List<Message> messages = new List<Message>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.messages", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an ActionItem object, and add it to our list.
                    messages.Add(new Message(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), int.Parse(reader[9].ToString()), reader[10].ToString()));
                }

                finalMessages = messages;
                //Now that all the ActionItem objects have been added, we can print them out.
                for (int i = 0; i < messages.Count; i++)
                {
                    Console.WriteLine("Message on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + messages[i].ID.ToString());
                }
            }

            //Pull data from Batch table into list of Batch

            //4. Declaration of a list of Batch objects, each element in this list representing a row in the table.
            List<Batch> batches = new List<Batch>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batches", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an Batch object, and add it to our list.
                    batches.Add(new Batch(reader[0].ToString(), reader[1].ToString(), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                }

                finalBatches = batches;
                //Now that all the Batch objects have been added, we can print them out.
                for (int i = 0; i < batches.Count; i++)
                {
                    Console.WriteLine("Batch on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + batches[i].ID.ToString());
                }
            }

            //Pull data from BatchFile table into list of BatchFile

            //5. Declaration of a list of BatchFile objects, each element in this list representing a row in the table.
            List<BatchFile> batchfiles = new List<BatchFile>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batchfiles", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an BatchFile object, and add it to our list.
                    batchfiles.Add(new BatchFile(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                }
                finalBatchFiles = batchfiles;
                //Now that all the BatchFile objects have been added, we can print them out.
                for (int i = 0; i < batchfiles.Count; i++)
                {
                    Console.WriteLine("BatchFile on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + batchfiles[i].ID.ToString());
                }
            }

            //Pull data from BatchForward table into list of BatchForward

            //6. Declaration of a list of BatchForward objects, each element in this list representing a row in the table.
            List<BatchForward> batchforwards = new List<BatchForward>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batchforwards", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an BatchForward object, and add it to our list.
                    batchforwards.Add(new BatchForward(reader[0].ToString(), int.Parse(reader[1].ToString()), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), reader[8].ToString()));
                }

                finalBatchForwards = batchforwards;
                //Now that all the BatchForward objects have been added, we can print them out.
                for (int i = 0; i < batchforwards.Count; i++)
                {
                    Console.WriteLine("BatchForward on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + batchforwards[i].ID.ToString());
                }
            }

            //Pull data from BatchForwardError table into list of BatchForwardError

            //7. Declaration of a list of BatchForwardError objects, each element in this list representing a row in the table.
            List<BatchForwardError> batchforwarderrors = new List<BatchForwardError>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batchforwarderrors", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an BatchForwardError object, and add it to our list.
                    batchforwarderrors.Add(new BatchForwardError(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                }

                finalBatchForwardErrors = batchforwarderrors;
                //Now that all the BatchForwardError objects have been added, we can print them out.
                for (int i = 0; i < batchforwarderrors.Count; i++)
                {
                    Console.WriteLine("BatchForwardError on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + batchforwarderrors[i].ID.ToString());
                }
            }

            //Pull data from ExternalID table into list of ExternalID

            //8. Declaration of a list of ExternalID objects, each element in this list representing a row in the table.
            List<ExternalID> externalID = new List<ExternalID>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.externalid", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an ExternalID object, and add it to our list.
                    externalID.Add(new ExternalID(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), reader[2].ToString(), reader[3].ToString()));
                }

                finalExternalID = externalID;
                //Now that all the ExternalID objects have been added, we can print them out.
                for (int i = 0; i < externalID.Count; i++)
                {
                    Console.WriteLine("ExternalID on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + externalID[i].ID.ToString());
                }
            }
        }
    }
}
