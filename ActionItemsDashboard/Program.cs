using System;
using System.Collections;
using Npgsql;
using System.Globalization;
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
            //1. Load values from .env file to establish connection to postgres server running locally.
            new EnvLoader().Load();
            var envReader = new EnvReader();
            var connString = "Host=" + envReader["HOST"] + ";Username=" + envReader["NAME"] + ";Password=" + envReader["PASSWORD"] + "; Database=" + envReader["DATABASE"] + ";";
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            //2. Create the schema in the database.
            await using (var createSchema = new NpgsqlCommand("CREATE SCHEMA IF NOT EXISTS actionitemsdata", conn))
            await createSchema.ExecuteNonQueryAsync();

            //3. Create the tables in the schema.
            
            //Creating action items table
            await using (var createActionItemsTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS postgres.actionitemsdata.actionitems (ID int PRIMARY KEY, target varchar(500) NOT NULL, status varchar(200) NOT NULL, campaign varchar(300) NOT NULL, expiry timestamp NOT NULL, when_created timestamp NOT NULL, when_updated timestamp NOT NULL,  content varchar(100) NOT NULL, country varchar(200) NOT NULL, language varchar(200) NOT NULL, customerset varchar(300) NOT NULL); ", conn))
            await createActionItemsTable.ExecuteNonQueryAsync();

            //Creating batch forward table
            await using (var createBatchForwardTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS postgres.actionitemsdata.batchforwards (ID varchar(100) PRIMARY KEY, batch_size int NOT NULL, delivery_method varchar(200) NOT NULL, campaign varchar(300) NOT NULL, started_at timestamp NOT NULL, completed_at timestamp NOT NULL, total_processed_messages int NOT NULL, total_processed_batches int NOT NULL, forward_status varchar(200) NOT NULL); ", conn))
            await createBatchForwardTable.ExecuteNonQueryAsync();

            //Creating batch forward error table
            await using (var createBatchForwardErrorTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS postgres.actionitemsdata.batchforwarderrors (ID varchar(100) PRIMARY KEY, error varchar(500) NOT NULL, batch_forward_id varchar(100) NOT NULL, FOREIGN KEY(batch_forward_id) REFERENCES postgres.actionitemsdata.batchforwards (ID)); ", conn))
            await createBatchForwardErrorTable.ExecuteNonQueryAsync();

            //Creating batch table
            await using (var createBatchTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS postgres.actionitemsdata.batches (ID varchar(100) PRIMARY KEY, batch_forward_id varchar(100) NOT NULL, size int NOT NULL, status varchar(200) NOT NULL,  campaign varchar(300) NOT NULL,  dispatched_at timestamp NOT NULL, FOREIGN KEY(batch_forward_id) REFERENCES postgres.actionitemsdata.batchforwards (ID)); ", conn))
            await createBatchTable.ExecuteNonQueryAsync();

            //Creating batch file table
            await using (var createBatchFileTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS postgres.actionitemsdata.batchfiles (ID varchar(100) PRIMARY KEY, batch_id varchar(100) NOT NULL, file_name varchar(500) NOT NULL, status varchar(200) NOT NULL, dispatched_at timestamp NOT NULL, content varchar(100) NOT NULL, FOREIGN KEY(batch_id) REFERENCES postgres.actionitemsdata.batches (ID)); ", conn))
            await createBatchFileTable.ExecuteNonQueryAsync();

            //Creating message table
            await using (var createMessageTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS postgres.actionitemsdata.messages (ID varchar(100) PRIMARY KEY, batch_id varchar(100) NOT NULL, subject varchar(500) NOT NULL, body varchar(500) NOT NULL, campaign varchar(300) NOT NULL, delivery_method varchar(200) NOT NULL, created_at timestamp NOT NULL, sent_at timestamp NOT NULL, status varchar(200) NOT NULL, action_item_id int NOT NULL, target varchar(500) NOT NULL, FOREIGN KEY(batch_id) REFERENCES postgres.actionitemsdata.batches (ID)); ", conn))
            await createMessageTable.ExecuteNonQueryAsync();

            //Creating external ID table
            await using (var createExternalIDTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS postgres.actionitemsdata.externalID (ID varchar(100) PRIMARY KEY, action_items_id int NOT NULL, external_id varchar(100) NOT NULL, created_at timestamp NOT NULL, FOREIGN KEY(action_items_id) REFERENCES postgres.actionitemsdata.actionitems (ID)); ", conn))
            await createExternalIDTable.ExecuteNonQueryAsync();


            //4. Clear the tables in the schema.

            //Clearing external ID table
            await using (var clearexternalIDTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.externalID;", conn))
            await clearexternalIDTable.ExecuteNonQueryAsync();

            //Clearing action items table
            await using (var clearActionItemsTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.actionitems;", conn))
            await clearActionItemsTable.ExecuteNonQueryAsync();

            //Clearing batch forward error table
            await using (var clearBatchForwardErrorTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.batchforwarderrors;", conn))
            await clearBatchForwardErrorTable.ExecuteNonQueryAsync();

            //Clearing batch file table
            await using (var clearBatchFileTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.batchfiles;", conn))
            await clearBatchFileTable.ExecuteNonQueryAsync();

            //Clearing message table
            await using (var clearMessageTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.messages;", conn))
            await clearMessageTable.ExecuteNonQueryAsync();

            //Clearing batch table
            await using (var clearBatchTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.batches;", conn))
            await clearBatchTable.ExecuteNonQueryAsync();

            //Clearing batch forward table
            await using (var clearBatchForwardTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.batchforwards;", conn))
            await clearBatchForwardTable.ExecuteNonQueryAsync();


            //5. Insert the sample data into the tables.

            //Inserting sample data into action items table.
            await using (var insertActionItemData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.actionitems VALUES (17981491, 'johndoe@dell.com', 'converted', 'saved-cart', '2022-01-24 06:00:30', '2022-01-24 04:00:30', '2022-01-24 06:00:31', '{\"cart\": {\"id\": [{\"id\":}]},}', 'jp', 'ja', 'jpbsd1')," +
                                                                                                                              "(17981492, 'johndoe@dell.com', 'purchased', 'abandoned-cart', '2022-01-24 06:00:47', '2022-01-24 04:00:47', '2022-01-24 04:00:47', '{\"cart\": {\"id\": [{\"id\":}]},}', 'jp', 'ja', 'jpdhs1')," +
                                                                                                                              "(17981493, 'johndoe@dell.com', 'purchased', 'abandoned-cart', '2022-01-24 06:00:49', '2022-01-24 04:00:49', '2022-01-24 04:00:49', '{\"cart\": {\"id\": [{\"id\":}]},}', 'us', 'en', '4')," +
                                                                                                                              "(17981494, 'johndoe@dell.com', 'converted', 'abandoned-cart', '2022-01-24 06:05:24', '2022-01-24 04:00:52', '2022-01-24 06:05:29', '{\"cart\": {\"id\": [{\"id\":}]},}', 'us', 'en', '4')," +
                                                                                                                              "(17981495, 'johndoe@dell.com', 'purchased', 'abandoned-cart', '2022-01-24 06:04:19', '2022-01-24 04:00:53', '2022-01-24 04:04:19', '{\"cart\": {\"id\": [{\"id\":}]},}', 'us', 'en', '6099')", conn))
            await insertActionItemData.ExecuteNonQueryAsync();

            //Inserting sample data into batch forward table.
            await using (var insertBatchForwardData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.batchforwards VALUES ('00275822-788e-4f40-8da7-2d7f0ab12040', 1000, 'BatchDelivery', 'abandoned-cart', '2022-03-10 10:10:00', '2022-03-10 10:10:01', 355, 2, 'Forwarded')," +
                                                                                                                                  "('0057a4d5-022b-4351-a902-211e43140482', 1000, 'BatchDelivery', 'saved-cart', '2022-03-20 16:00:00', '2022-03-20 16:00:01', 34, 2, 'Forwarded')," +
                                                                                                                                  "('00596c4e-364f-407e-bbcf-af69273e2e81', 1000, 'BatchDelivery', 'saved-cart', '2022-02-01 17:00:00', '2022-02-01 17:00:01', 57, 2, 'Forwarded')," +
                                                                                                                                  "('006055af-0f5d-4b46-a0d8-0fec96728cf9', 1000, 'BatchDelivery', 'saved-cart', '2022-03-07 06:00:00', '2022-03-07 06:00:01', 20, 2, 'Forwarded')," +
                                                                                                                                  "('0073a032-5d54-4c87-9e2a-9471abc3979b', 1000, 'BatchDelivery', 'abandoned-car', '2022-03-14 09:10:00', '2022-03-14 09:10:02', 307, 2, 'Forwarded')", conn))
             await insertBatchForwardData.ExecuteNonQueryAsync();


            //Inserting sample data into batch forward error table.
            await using (var insertBatchForwardErrorData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.batchforwarderrors VALUES ('00275822-788a-4f40-8da7-2d7f0ab12040', 'Forwarding Error', '00275822-788e-4f40-8da7-2d7f0ab12040')," +
                                                                                                                                            "('0057a4d5-022a-4351-a902-211e43140482', 'Forwarding Error', '0057a4d5-022b-4351-a902-211e43140482')," +
                                                                                                                                            "('00596c4e-364a-407e-bbcf-af69273e2e81', 'Forwarding Error', '00596c4e-364f-407e-bbcf-af69273e2e81')," +
                                                                                                                                            "('006055af-0f5a-4b46-a0d8-0fec96728cf9', 'Forwarding Error', '006055af-0f5d-4b46-a0d8-0fec96728cf9')," +
                                                                                                                                            "('0073a032-5d5a-4c87-9e2a-9471abc3979b', 'Forwarding Error', '0073a032-5d54-4c87-9e2a-9471abc3979b')", conn))
            await insertBatchForwardErrorData.ExecuteNonQueryAsync();

            //Inserting sample data into batch table.
            await using (var insertBatchData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.batches VALUES ('000d2be7-aeb5-4909-98bb-c4fa755cfc49', '00275822-788e-4f40-8da7-2d7f0ab12040', 21, 'Dispatched', 'saved-cart', '2022-02-08 03:00:00')," +
                                                                                                                     "('00102b83-80ab-4b16-b03c-0211cfe92463', '0057a4d5-022b-4351-a902-211e43140482', 385, 'Dispatched', 'abandoned-cart', '2022-02-22 12:10:01')," +
                                                                                                                     "('003471bf-5071-430f-a9cf-9b9e9f05456e', '00596c4e-364f-407e-bbcf-af69273e2e81', 101, 'Dispatched', 'abandoned-cart', '2022-01-28 17:10:00')," +
                                                                                                                     "('0036b880-9f97-42ef-adc4-c8e4d9760c57', '006055af-0f5d-4b46-a0d8-0fec96728cf9', 41, 'Dispatched', 'saved-cart', '2022-02-28 23:00:01')," +
                                                                                                                     "('0071d61f-81d0-4505-8a60-2d574d0ad51b', '0073a032-5d54-4c87-9e2a-9471abc3979b', 48, 'Dispatched', 'saved-cart', '2022-02-21 12:00:01')", conn))
            await insertBatchData.ExecuteNonQueryAsync();


            //Inserting sample data into batch file table.
            await using (var insertBatchFileData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.batchfiles VALUES ('00334323-ff5b-4e5c-917e-2f0cd2400cfb', '000d2be7-aeb5-4909-98bb-c4fa755cfc49', 'RSPNSV_DELL_V2_SAVEDCART_68e053ba26ba4f76a19bf7e6c4760f68_20220311180000.596.DAT', 'Dispatched', '2022-03-11 12:00:01', 'binary data')," +
                                                                                                                            "('003f59c6-c6c0-4a17-8c81-1e4137f7cb62', '00102b83-80ab-4b16-b03c-0211cfe92463', 'RSPNSV_DELL_V2_ABANDONCART_3813020e6e424866ba44b46672dbc521_20220315201000.780.DAT', 'Dispatched', '2022-03-15 15:10:01', 'binary data')," +
                                                                                                                            "('007111be-fc10-4618-a14d-f9f1c6580286', '003471bf-5071-430f-a9cf-9b9e9f05456e', 'RSPNSV_DELL_V2_ABANDONCART_4323a2cc8aec49f89e19bb7bcf670615_20220213221000.843.DAT', 'Dispatched', '2022-02-13 16:10:01', 'binary data')," +
                                                                                                                            "('00758abd-cbaf-4fd7-ab64-6a7b32d17818', '0036b880-9f97-42ef-adc4-c8e4d9760c57', 'RSPNSV_DELL_V2_ABANDONCART_509004ede8784d3ab00ce72145e12951_20220209141000.594.DAT', 'Dispatched', '2022-02-09 08:10:01', 'binary data')," +
                                                                                                                            "('00c60959-ab2f-419a-8828-d012ea414862', '0071d61f-81d0-4505-8a60-2d574d0ad51b', 'RSPNSV_DELL_V2_ABANDONCART_ea9766d791834157b52de7e2ef785a4e_20220302231000.544.DAT', 'Dispatched', '2022-03-02 17:10:01', 'binary data')", conn))
            await insertBatchFileData.ExecuteNonQueryAsync();


            //Inserting sample data into message table.
            await using (var insertMessageData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.messages VALUES ('000083b9-b837-4b9a-a7aa-1d310bd273f3', '000d2be7-aeb5-4909-98bb-c4fa755cfc49', 'Email|~|First Name|~|Last name|~|Saved Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Saved Cart Name|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews', 'johndoe@dell.com|~|...', 'saved-cart', 'BatchDelivery', '2022-03-05 10:58:52', '2022-03-05 17:00:01', 'Dispatched', 18513415, 'johndoe@dell.com')," +
                                                                                                                        "('0000987d-fd46-460d-aa5e-c92b556d5d67', '00102b83-80ab-4b16-b03c-0211cfe92463', 'Email|~|First Name|~|Last name|~|Abandoned Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews', 'johndoe@dell.com|~|...', 'abandoned-cart', 'BatchDelivery', '2022-02-17 04:19:15', '2022-02-17 11:10:01', 'Dispatched', 18255817, 'johndoe@dell.com')," +
                                                                                                                        "('0000b17f-c49c-4483-9e65-92270218fc5f', '003471bf-5071-430f-a9cf-9b9e9f05456e', 'Email|~|First Name|~|Last name|~|Abandoned Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews', 'johndoe@dell.com|~|...', 'abandoned-cart', 'BatchDelivery', '2022-03-12 07:48:11', '2022-03-12 14:10:01', 'Dispatched', 18642742, 'johndoe@dell.com')," +
                                                                                                                        "('0000df93-dde2-4636-8c15-b7f87f4fba0a', '0036b880-9f97-42ef-adc4-c8e4d9760c57', 'Email|~|First Name|~|Last name|~|Abandoned Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews', 'johndoe@dell.com|~|...', 'abandoned-cart', 'BatchDelivery', '2022-02-23 04:53:06', '2022-02-23 11:10:01', 'Dispatched', 18357752, 'johndoe@dell.com')," +
                                                                                                                        "('0001141e-2253-4ce5-8a0b-c7c14e593d7f', '0071d61f-81d0-4505-8a60-2d574d0ad51b', 'Email|~|First Name|~|Last name|~|Saved Cart ID|~|Item Id|~|Item Type|~|Item title|~|Item Adjusted Total Price|~|Cat 1 Name|~|Cat 2 Name|~|Cat 3 Name|~|ISO Country Code|~|Currency Code|~|Customer Set|~|Language Code|~|Created Time|~|Last Update Time|~|Cart Type|~|Saved Cart Name|~|Marketable flag|~|Product destination|~|Destination order|~|Product type|~|Item image|~|Item URL|~|Rating|~|Number of reviews', 'johndoe@dell.com|~|...', 'saved-cart', 'BatchDelivery', '2022-02-23 00:51:24', '2022-02-23 07:00:01', 'Dispatched', 18355685, 'johndoe@dell.com')", conn))
            await insertMessageData.ExecuteNonQueryAsync();

            //Inserting sample data into external ID table.
            await using (var insertExternalIDData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.externalID VALUES (4574318, 17981491, '631eeeef-fb06-4894-a5df-00eb26d63984', '2022-01-24 04:00:31')," +
                                                                                                                        "(4574319, 17981492, '38004c5e-4bb1-46d9-8a84-401c62476499', '2022-01-24 04:00:48')," +
                                                                                                                       "(4574320, 17981493, 'df671c77-f6a4-4470-9975-057c390287b6', '2022-01-24 04:00:50')," +
                                                                                                                        "(4574321, 17981494, '49a7f987-1ee2-4c0d-b6bc-a43a1e0b1714', '2022-01-24 04:00:53')," +
                                                                                                                        "(4574322, 17981495, '6fff3862-b960-496a-9661-d3abed37ea5f', '2022-01-24 04:00:53')", conn))
            await insertExternalIDData.ExecuteNonQueryAsync();



            //6. Pull data from the postgres database.

            //Pull data from action items table into list of ActionItem

            //Declaration of a list of ActionItem objects, each element in this list representing a row in the table.
            List<ActionItem> actionItems = new List<ActionItem>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.actionitems", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
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


            //Pull data from messages table into list of Message

            //Declaration of a list of Message objects, each element in this list representing a row in the table.
            List<Message> messages = new List<Message>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.messages", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an ActionItem object, and add it to our list.
                    messages.Add(new Message(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), int.Parse(reader[9].ToString()), reader[10].ToString()));
                }

                //Now that all the ActionItem objects have been added, we can print them out.
                for (int i = 0; i < messages.Count; i++)
                {
                    Console.WriteLine("Message on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + messages[i].ID.ToString());
                }
            }

            //Pull data from Batch table into list of Batch

            //Declaration of a list of Batch objects, each element in this list representing a row in the table.
            List<Batch> batches = new List<Batch>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batches", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an Batch object, and add it to our list.
                    batches.Add(new Batch(reader[0].ToString(), reader[1].ToString(), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                }

                //Now that all the Batch objects have been added, we can print them out.
                for (int i = 0; i < batches.Count; i++)
                {
                    Console.WriteLine("Batch on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + batches[i].ID.ToString());
                }
            }

            //Pull data from BatchFile table into list of BatchFile

            //Declaration of a list of BatchFile objects, each element in this list representing a row in the table.
            List<BatchFile> batchfiles = new List<BatchFile>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batchfiles", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an BatchFile object, and add it to our list.
                    batchfiles.Add(new BatchFile(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString()));
                }

                //Now that all the BatchFile objects have been added, we can print them out.
                for (int i = 0; i < batchfiles.Count; i++)
                {
                    Console.WriteLine("BatchFile on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + batchfiles[i].ID.ToString());
                }
            }

            //Pull data from BatchForward table into list of BatchForward

            //Declaration of a list of BatchForward objects, each element in this list representing a row in the table.
            List<BatchForward> batchforwards = new List<BatchForward>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batchforwards", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an BatchForward object, and add it to our list.
                    batchforwards.Add(new BatchForward(reader[0].ToString(), int.Parse(reader[1].ToString()), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), int.Parse(reader[6].ToString()), int.Parse(reader[7].ToString()), reader[8].ToString()));
                }

                //Now that all the BatchForward objects have been added, we can print them out.
                for (int i = 0; i < batchforwards.Count; i++)
                {
                    Console.WriteLine("BatchForward on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + batchforwards[i].ID.ToString());
                }
            }

            //Pull data from BatchForwardError table into list of BatchForwardError

            //Declaration of a list of BatchForwardError objects, each element in this list representing a row in the table.
            List<BatchForwardError> batchforwarderrors = new List<BatchForwardError>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.batchforwarderrors", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an BatchForwardError object, and add it to our list.
                    batchforwarderrors.Add(new BatchForwardError(reader[0].ToString(), reader[1].ToString(), reader[2].ToString()));
                }

                //Now that all the BatchForwardError objects have been added, we can print them out.
                for (int i = 0; i < batchforwarderrors.Count; i++)
                {
                    Console.WriteLine("BatchForwardError on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + batchforwarderrors[i].ID.ToString());
                }
            }

            //Pull data from ExternalID table into list of ExternalID

            //Declaration of a list of ExternalID objects, each element in this list representing a row in the table.
            List<ExternalID> externalID = new List<ExternalID>();
            await using (var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.externalid", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    //Convert each row into an ExternalID object, and add it to our list.
                    externalID.Add(new ExternalID(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), reader[2].ToString(), reader[3].ToString()));
                }

                //Now that all the ExternalID objects have been added, we can print them out.
                for (int i = 0; i < externalID.Count; i++)
                {
                    Console.WriteLine("ExternalID on row " + (i + 1) + " has:");
                    Console.WriteLine("ID: " + externalID[i].ID.ToString());
                }
            }


            //7. Transfer all lists to the frontend (TODO)
            
            //8. Call query functions
            ActionItemsCampaign(conn);
        }
        
        static async void ActionItemsCampaign(NpgsqlConnection conn)
        {
            //todo: take user input from front end
            string campaign = "saved-cart";

            await using var cmd = new NpgsqlCommand("SELECT * FROM actionitemsdata.actionitems WHERE campaign = @p1", conn)
            {
                Parameters =
                {
                    new("p1", campaign)
                }
            };
            await using (var reader = await cmd.ExecuteReaderAsync())
            {
                //list of message objects
                List<ActionItem> actionItems = new List<ActionItem>();
                while (await reader.ReadAsync())
                {
                    //Convert each row into a message item and add to list
                    actionItems.Add(new ActionItem(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString()));
                }

                //print stuff
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
            this.expiry = DateTime.Parse(expiry);
            this.when_created = DateTime.Parse(when_created);
            this.when_updated = DateTime.Parse(when_updated);
            this.content = content;
            this.country = country;
            this.language = language;
            this.customerset = customerset;
        }
    }

    class Message
    {
        public Guid ID;
        public Guid batch_id;
        public String subject;
        public String body;
        public String campaign;
        public String delivery_method;
        public DateTime created_at;
        public DateTime sent_at;
        public String status;
        public int action_item_id;
        public String target;
        public Message(String ID, String batch_id, String subject, String body, String campaign, String delivery_method, String created_at, String sent_at, String status, int action_item_id, String target)
        {
            this.ID = Guid.Parse(ID);
            this.batch_id = Guid.Parse(batch_id);
            this.subject = subject;
            this.body = body;
            this.campaign = campaign;
            this.delivery_method = delivery_method;
            this.created_at = DateTime.Parse(created_at);
            this.sent_at = DateTime.Parse(sent_at);
            this.status = status;
            this.action_item_id = action_item_id;
            this.target = target;
        }
    }

    class Batch
    {
        public Guid ID;
        public Guid batch_forward_id;
        public int size;
        public String status;
        public String campaign;
        public DateTime dispatched_at;
        public Batch(String ID, String batch_forward_id, int size, String status, String campaign, String dispatched_at)
        {
            this.ID = Guid.Parse(ID);
            this.batch_forward_id = Guid.Parse(batch_forward_id);
            this.size = size;
            this.status = status;
            this.campaign = campaign;
            this.dispatched_at = DateTime.Parse(dispatched_at);
        }
    }

    class BatchFile
    {
        public Guid ID;
        public Guid batch_id;
        public String file_name;
        public String status;
        public DateTime dispatched_at;
        public String content;

        public BatchFile(String ID, String batch_id, String file_name, String status, String dispatched_at, String content)
        {
            this.ID = Guid.Parse(ID);
            this.batch_id = Guid.Parse(batch_id);
            this.file_name = file_name;
            this.status = status;
            this.dispatched_at = DateTime.Parse(dispatched_at);
            this.content = content;
        }
    }

    class BatchForward
    {
        public Guid ID;
        public int batch_size;
        public String delivery_method;
        public String campaign;
        public DateTime started_at;
        public DateTime completed_at;
        public int total_processed_messages;
        public int total_processed_batches;
        public String forward_status;

        public BatchForward(String ID, int batch_size, String delivery_method, String campaign, String started_at, String completed_at, int total_processed_messages, int total_processed_batches, String forward_status)
        {
            this.ID = Guid.Parse(ID);
            this.batch_size = batch_size;
            this.delivery_method = delivery_method;
            this.campaign = campaign;
            this.started_at = DateTime.Parse(started_at);
            this.completed_at = DateTime.Parse(completed_at);
            this.total_processed_messages = total_processed_messages;
            this.total_processed_batches = total_processed_batches;
            this.forward_status = forward_status;
        }
    }

    class BatchForwardError
    {
        public Guid ID;
        public String error;
        public Guid batch_forward_id;

        public BatchForwardError(String ID, String error, String batch_forward_id)
        {
            this.ID = Guid.Parse(ID);
            this.error = error;
            this.batch_forward_id = Guid.Parse(batch_forward_id);
        }
    }

    class ExternalID
    {
        public int ID;
        public int action_items_id;
        public Guid external_id;
        public DateTime created_at;

        public ExternalID(int ID, int action_items_id, String external_id, String created_at)
        {
            this.ID = ID;
            this.action_items_id = action_items_id;
            this.external_id = Guid.Parse(external_id);
            this.created_at = DateTime.Parse(created_at);
        }
    }
}
