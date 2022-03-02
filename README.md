# CSU33013-Project
Group project for the Software Engineering Project 

## Pre-requisites
- [ODBC](https://www.microsoft.com/en-us/download/details.aspx?id=56567)
- [PostgreSQL ODBC Driver](https://www.postgresql.org/ftp/odbc/versions/msi/)

Once you've installed the ODBC driver you will need to add a new user data source. This is achieved by going to 'Control Panel', search for "ODBC", and click 'Set up ODBC data sources'. Then selecting 'Add' under 'User DSN'.

Select the relevant Driver for PostgreSQL.
![image](https://user-images.githubusercontent.com/78870995/156426109-062fd55a-9c73-4277-9d1a-4a9b2ee6f908.png)

Fill in your server and database details, seen in the SQL Shell (psql).
You must also specify a unique DSN name in the 'Data Source' field. This will be the name used to refer to the database connection within the program.
![image](https://user-images.githubusercontent.com/78870995/156426518-415eccc3-6119-4ff0-9dd8-be3fe594bd4f.png)

