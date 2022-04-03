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
        public static List<ActionItem> finalActionItemsInTimeRange = new List<ActionItem>();
        public static Message messageLinkedWithActionItem;
        public static Batch batchLinkedWithMessage;
        public static BatchFile batchFileLinkedWithBatch;

        public DatabaseService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
            Task.Run(async () => await getListOfObjects());
            //Examples of running some new added functions (can be used for the frontend)
            //Console.WriteLine(GetActionItemsInTimeRange("2022-01-24 06:00:30", "2022-01-24 06:00:50", "expiry").Count());
            //Console.WriteLine(GetBatchFromMessage("00102b83-80ab-4b16-b03c-0211cfe92463").ID.ToString());
            //Console.WriteLine(GetMessageFromActionItemID("17981494").ID.ToString());
            //Console.WriteLine(GetBatchFileFromBatch("000d2be7-aeb5-4909-98bb-c4fa755cfc49").ID.ToString());
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

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

        public IEnumerable<ActionItem> GetActionItemsInTimeRange(String startDate, String endDate, String dateAttribute)
        {
            ActionItemsTimeRange(startDate, endDate, dateAttribute);
            if (finalActionItemsInTimeRange != null)
            {
                return finalActionItemsInTimeRange;
            }
            else
            {
                return new List<ActionItem>();
            }
        }

        public Message GetMessageFromActionItemID(String actionItemID)
        {
            getMessageLinkedWithActionItem(actionItemID);
            if (messageLinkedWithActionItem != null)
            {
                return messageLinkedWithActionItem;
            }
            else
            {
                return new Message("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "ERROR", "ERROR", "ERROR", "ERROR", "2022-01-24 06:04:19", "2022-01-24 06:04:29", "ERROR", 99999999, "ERROR");
            }
            
        }

        public Batch GetBatchFromMessage(String batchID)
        {
            getBatchLinkedWithMessage(batchID);
            if (batchLinkedWithMessage != null)
            {
                return batchLinkedWithMessage;
            }
            else
            {
                return new Batch("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", 999999, "ERROR", "ERROR", "2022-01-24 06:04:19");
            }

        }

        public BatchFile GetBatchFileFromBatch(String batchID)
        {
            getBatchFileLinkedWithBatch(batchID);
            if (batchFileLinkedWithBatch != null)
            {
                return batchFileLinkedWithBatch;
            }
            else
            {
                return new BatchFile("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "ERROR", "ERROR", "2022-01-24 06:04:19", "ERROR");
            }

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
            }
        }
        
        static async Task ActionItemsTimeRange(String startDate, String endDate, String dateAttribute)
    {
            //1. Load values from .env file to establish connection to postgres server running locally.
            new EnvLoader().Load();
            var envReader = new EnvReader();
            var connString = "Host=" + envReader["HOST"] + ";Username=" + envReader["NAME"] + ";Password=" + envReader["PASSWORD"] + "; Database=" + envReader["DATABASE"] + ";";
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();
            String command = "SELECT * FROM actionitemsdata.actionitems WHERE " + dateAttribute.ToString() + " BETWEEN '" + startDate.ToString() + "' AND '" + endDate.ToString() + "'";
            //SQL Query below only works with Parameters and empty parameters in {}. 
            await using var cmd = new NpgsqlCommand(command, conn){Parameters ={}};

            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                //list of action items objects
                List<ActionItem> actionItemsInTimeRange = new List<ActionItem>();
                while (await reader.ReadAsync())
                {
                    //Convert each row into an action item object and add to list
                    actionItemsInTimeRange.Add(new ActionItem(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString()));
                }
                finalActionItemsInTimeRange = actionItemsInTimeRange;
            }
        }


        static async Task getMessageLinkedWithActionItem(string actionItemID)
        {
            //1. Load values from .env file to establish connection to postgres server running locally.
            new EnvLoader().Load();
            var envReader = new EnvReader();
            var connString = "Host=" + envReader["HOST"] + ";Username=" + envReader["NAME"] + ";Password=" + envReader["PASSWORD"] + "; Database=" + envReader["DATABASE"] + ";";
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.messages WHERE action_item_id = (@p1);", conn)
            {
                Parameters =
                {
                    new("p1", int.Parse(actionItemID))
                }
            };

            Message messageReceived = new Message("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "ERROR", "ERROR", "ERROR", "ERROR", "2022-01-24 06:04:19", "2022-01-24 06:04:29", "ERROR", 99999999, "ERROR");
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    messageReceived = new Message(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), int.Parse(reader[9].ToString()), reader[10].ToString());
                }

            }
            messageLinkedWithActionItem = messageReceived;
        }


        static async Task getBatchLinkedWithMessage(string batchID)
        {
            //1. Load values from .env file to establish connection to postgres server running locally.
            new EnvLoader().Load();
            var envReader = new EnvReader();
            var connString = "Host=" + envReader["HOST"] + ";Username=" + envReader["NAME"] + ";Password=" + envReader["PASSWORD"] + "; Database=" + envReader["DATABASE"] + ";";
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batches WHERE ID = (@p1);", conn)
            {
                Parameters =
                {
                    new("p1", batchID)
                }
            };

            Batch batchReceived = new Batch("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", 999999, "ERROR", "ERROR", "2022-01-24 06:04:19");
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    batchReceived = new Batch(reader[0].ToString(), reader[1].ToString(), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                }

            }
            batchLinkedWithMessage = batchReceived;
        }

        static async Task getBatchFileLinkedWithBatch(string batchID)
        {
            //1. Load values from .env file to establish connection to postgres server running locally.
            new EnvLoader().Load();
            var envReader = new EnvReader();
            var connString = "Host=" + envReader["HOST"] + ";Username=" + envReader["NAME"] + ";Password=" + envReader["PASSWORD"] + "; Database=" + envReader["DATABASE"] + ";";
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batchfiles WHERE batch_id = (@p1);", conn)
            {
                Parameters =
                {
                    new("p1", batchID)
                }
            };

            BatchFile batchFileReceived = new BatchFile("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF", "ERROR", "ERROR", "2022-01-24 06:04:19", "ERROR");
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    batchFileReceived = new BatchFile(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                }

            }
            batchFileLinkedWithBatch = batchFileReceived;
        }
    }
}
