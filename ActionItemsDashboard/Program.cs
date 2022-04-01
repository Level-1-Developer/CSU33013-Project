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


            //4. Clear the tables in the schema.

            //Clearing action items table
            await using (var clearActionItemsTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.actionitems;", conn))
            await clearActionItemsTable.ExecuteNonQueryAsync();

            //Clearing batch forward error table
            await using (var clearBatchForwardErrorTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.batchforwarderrors;", conn))
            await clearBatchForwardErrorTable.ExecuteNonQueryAsync();

            //Clearing batch file table
            await using (var clearBatchFileTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.batchfiles;", conn))
            await clearBatchFileTable.ExecuteNonQueryAsync();

            //Clearing batch table
            await using (var clearBatchTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.batches;", conn))
            await clearBatchTable.ExecuteNonQueryAsync();

            //Clearing batch forward table
            await using (var clearBatchForwardTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.batchforwards;", conn))
            await clearBatchForwardTable.ExecuteNonQueryAsync();


            //Clearing message table
            await using (var clearMessageTable = new NpgsqlCommand("DELETE FROM postgres.actionitemsdata.messages;", conn))
            await clearMessageTable.ExecuteNonQueryAsync();

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
                                                                                                                                  "('006055af-0f5d-4b46-a0d8-0fec96728cf91', 1000, 'BatchDelivery', 'saved-cart', '2022-03-07 06:00:00', '2022-03-07 06:00:01', 20, 2, 'Forwarded')," +
                                                                                                                                  "('0073a032-5d54-4c87-9e2a-9471abc3979b', 1000, 'BatchDelivery', 'abandoned-car', '2022-03-14 09:10:00', '2022-03-14 09:10:02', 307, 2, 'Forwarded')", conn))
             await insertBatchForwardData.ExecuteNonQueryAsync();


            //Inserting sample data into batch forward error table.
            await using (var insertBatchForwardErrorData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.batchforwarderrors VALUES ('00275822-788z-4f40-8da7-2d7f0ab1204', 'Forwarding Error', '00275822-788e-4f40-8da7-2d7f0ab12040')," +
                                                                                                                                            "('0057a4d5-022z-4351-a902-211e43140482', 'Forwarding Error', '0057a4d5-022b-4351-a902-211e43140482')," +
                                                                                                                                            "('00596c4e-364z-407e-bbcf-af69273e2e81', 'Forwarding Error', '00596c4e-364f-407e-bbcf-af69273e2e81')," +
                                                                                                                                            "('006055af-0f5z-4b46-a0d8-0fec96728cf9', 'Forwarding Error', '006055af-0f5d-4b46-a0d8-0fec96728cf91')," +
                                                                                                                                            "('0073a032-5d5z-4c87-9e2a-9471abc3979b', 'Forwarding Error', '0073a032-5d54-4c87-9e2a-9471abc3979b')", conn))
            await insertBatchForwardErrorData.ExecuteNonQueryAsync();

            //Inserting sample data into batch table.
            await using (var insertBatchData = new NpgsqlCommand("INSERT INTO postgres.actionitemsdata.batches VALUES ('000d2be7-aeb5-4909-98bb-c4fa755cfc49', '00275822-788e-4f40-8da7-2d7f0ab12040', 21, 'Dispatched', 'saved-cart', '2022-02-08 03:00:00')," +
                                                                                                                     "('00102b83-80ab-4b16-b03c-0211cfe92463', '0057a4d5-022b-4351-a902-211e43140482', 385, 'Dispatched', 'abandoned-cart', '2022-02-22 12:10:01')," +
                                                                                                                     "('003471bf-5071-430f-a9cf-9b9e9f05456e', '00596c4e-364f-407e-bbcf-af69273e2e81', 101, 'Dispatched', 'abandoned-cart', '2022-01-28 17:10:00')," +
                                                                                                                     "('0036b880-9f97-42ef-adc4-c8e4d9760c57', '006055af-0f5d-4b46-a0d8-0fec96728cf91', 41, 'Dispatched', 'saved-cart', '2022-02-28 23:00:01')," +
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