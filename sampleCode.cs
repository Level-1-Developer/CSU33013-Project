using Microsoft.Data.Odbc;

//https://wiki.postgresql.org/wiki/Using_Microsoft_.NET_with_the_PostgreSQL_Database_Server_via_ODBC

// Create the ODBC connection using the unique name you specified when 
// creating your DSN. If desired you may input less information at the
// DSN entry stage and put more in the "DSN=" line below.
OdbcConnection connection = new OdbcConnection("DSN=swengDSN");
// "DSN=MyDSN;UID=Admin;PWD=Test"
// "DSN is the data source name (name of db)
// UID is the username and PWD the password, standard postgres.

// Open the ODBC connection to the PostgreSQL database and display
// the connection state (status).
connection.Open();
System.Console.WriteLine("State: " + connection.State.ToString());

// get the query from the relevant text file. use if statements to choose file
string query = System.IO.File.ReadAllText(@"C:\Users\Public\TestFolder\sqlQuery1.txt");`

// create odbc command
OdbcCommand command = new OdbcCommand(query, connection);

// Execute the SQL command and return a reader for navigating the results.
OdbcDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);


// This loop will output the entire contents of the results, iterating
// through each row and through each field of the row.
while(reader.Read() == true) 
{
    Console.WriteLine("New Row:");
    for (int i = 0; i < reader.FieldCount; i++)
    {
        Console.WriteLine(reader.GetString(i));
    }
}

// Close the reader and connection (commands are not closed).
reader.Close();
connection.Close();

//command to compile: csc /out:run.exe /r:Microsoft.Data.Odbc.dll sampleCode.cs

