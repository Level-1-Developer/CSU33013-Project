# CSU33013-Project
Group project for the Software Engineering Project 

## Pre-requisites
- [ODBC](https://www.microsoft.com/en-us/download/details.aspx?id=56567)
- [PostgreSQL ODBC Driver](https://www.postgresql.org/ftp/odbc/versions/msi/)
- [Npgsql](https://www.nuget.org/packages/Npgsql/)
  
Once you've installed the ODBC driver you will need to add a new user data source. This is achieved by going to ``Control Panel``, search for ``ODBC``, and click ``Set up ODBC data sources``. Then select ``Add`` under the ``User DSN`` section.

Select the relevant Driver for PostgreSQL.  

![image](https://user-images.githubusercontent.com/78870995/156426109-062fd55a-9c73-4277-9d1a-4a9b2ee6f908.png)

Fill in your server and database details, seen in the SQL Shell (psql).
You must also specify a unique DSN name in the 'Data Source' field. This will be the name used to refer to the database connection within the program.  

![image](https://user-images.githubusercontent.com/78870995/156426518-415eccc3-6119-4ff0-9dd8-be3fe594bd4f.png)

Keep a record of details used for the new user data source, including password, as they will be used to connect to the database.

Next, create a new project on Visual Studio, select ``Console App (.Net Framework)`` and install the Npgsql package. 
This can be done by rightclicking the ``C# Project file`` and selecting ``Manage Nuget Packages`` 

![image](https://user-images.githubusercontent.com/77547327/159126719-42d533ff-9b88-4c66-9966-900dbf1eee4c.png)

From there, under Browse, search for ``npgsql`` and select the first entry as shown below and install. 

![image](https://user-images.githubusercontent.com/77547327/159126857-a7f0824f-440e-40c5-bc58-19f8ee438d50.png)

From there, copy paste the code from the sample file in the repo into your current C# file. (Remember to change the name of the namespace) 
Your current cs file should look like this

![image](https://user-images.githubusercontent.com/77547327/159127092-f4cdeb8f-34e8-4c34-8603-0a7237d1d5f1.png)

From here, change the ``connString`` variable by switching out the details with the ones you used to create your database from eariler.
Save and run the program from inside terminal/commandprompt by using the command ``dotnet run Program.cs``  
If there are no errors, you have successfully connected to the local database created on your pc. 
