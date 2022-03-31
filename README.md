# Action Items Dashboard (in partnership with Dell)

<p align="left">Welcome to Group 35's project for the module Software Engineering Project at Trinity College Dublin. We are working with Dell to make an Action Items Dashboard. This Dashboard will provide Dell employees a web-based UI with supporting APIs on top of the existing database that allows for management of the action items events data.
<img width="300" alt="image" align="right" src="https://user-images.githubusercontent.com/34750736/161159178-300b01e9-6897-4925-ab84-13dfee90bac0.png">

 
### A brief description of the existing action items system
<p align="justify">The action items system supports marketing campaigns that allow Dell to reengage customers who have abandoned their carts through email. A series of microservices across Cart & Personalization communicates with each other through HTTP as well as AMQP to allow for the events to be processed accordingly. Events are stored in a Postgres database to allow persistence of up to four weeks. Currently management and visibility of the events are cumbersome and not efficient. For example, to see how many customers would receive abandoned cart emails that contain a specific product, require an engineer to write dedicated queries in the production database which is not practical.</p>
<br>

![image](https://user-images.githubusercontent.com/34750736/161156525-6726ab50-8aad-4072-97fd-49ecd4274759.png)


### What are we trying to make?
<p align="justify">Our goal is to build a fully featured web-based UI with supporting APIs on top of the existing database that allows for all analytical as well as management tasks of the action items events data. This will increase visibility as well as productivity of all admin tasks related to the dataset. This project will be developed for running on Windows only.</p>

### Prerequisites to run this project.
<p align=justify>You need a few things before you can run this project.</p>

#### Visual Studio 2022
<p align=justify>You can install Visual Studio 2022 on <a href="https://visualstudio.microsoft.com/vs/">Microsoft's Visual Studio downloads page</a>. You need Visual Studio 2022 as you will be using .NET 6.0 which is only compatible on Visual Studio 2022. Make sure to install the '.NET Desktop Development' Workload when installing.</p>

#### .NET 6.0
<p align=justify>You can install .NET 6.0 on <a href="https://dotnet.microsoft.com/en-us/download/dotnet/6.0">Microsoft's .NET downloads page</a>.</p>

#### PostgreSQL
<p align=justify>You can install PostgreSQL for Windows on the <a href="https://www.postgresql.org/download/windows/"> PostgreSQL Windows installer downloads page</a>. Keep a record of the details used for the hostname, username, password, and database name as they will be needed. </p>


## How to run the project
1. Clone the repository.
2. Open Visual Studio 2022, and click on "Open a project or solution"
  <p align="center">
  <img width="390" alt="Screenshot 2022-03-31 at 23 39 20" align="centre" src="https://user-images.githubusercontent.com/34750736/161161200-6aa52df7-773e-4c45-a32f-6c34a34ea57b.png"></p>
  
3. Click on the ActionItemsDashboard.sln file inside the ``ActionItemsDashboard`` directory from this project that you cloned.
 <p align="center">
<img width="700" alt="Screenshot 2022-03-31 at 23 45 52" src="https://user-images.githubusercontent.com/34750736/161162018-b8310738-360a-47c1-819c-e27e85d15d0f.png"></p>

4. Now that the ActionItemsDashboard C# project is in your Visual Studio 2022 IDE, you need to right click on the project and click `Manage Nuget Packages`.

<p align="center">
<img width="350" alt="Screenshot 2022-03-31 at 23 45 52" src="https://user-images.githubusercontent.com/77547327/159126719-42d533ff-9b88-4c66-9966-900dbf1eee4c.png"></p>

5. From there, under Browse, search for ``npgsql`` and select the first entry as shown below and install. You can learn more about it [here](https://www.nuget.org/packages/Npgsql/).

<p align="center">
<img width="700" alt="Screenshot 2022-03-31 at 23 45 52" src="https://user-images.githubusercontent.com/77547327/159126857-a7f0824f-440e-40c5-bc58-19f8ee438d50.png"></p>

6. Now search for ``DotEnv.Core`` and select the first entry as shown below and install. You can learn more about it [here](https://www.nuget.org/packages/DotEnv.Core/).

<p align="center">
<img width="668" alt="Screenshot 2022-04-01 at 00 14 03" src="https://user-images.githubusercontent.com/34750736/161164545-6a71ffb4-743f-40bb-9345-5a4b6d9d61b8.png"></p>


7. Now you need to setup the environment variables for the C# code to connect to your PostgreSQL server. Go into the main directory of the project, and run the shell script using `sh ./setupEnv.sh`. It will ask you a number of questions, and it will make and fill a `.env` file inside of `/ActionItemsDashboard` for you.

<p align="center">
<img width="668" alt="Screenshot 2022-04-01 at 00 14 03" src="https://user-images.githubusercontent.com/34750736/161165810-6410bb5d-3a82-4345-a960-3738d68a443f.gif"></p>

8. Now you can run the program `Program.cs` inside of Visual Studio 2022. You can see the PostgreSQL database being populated using PgAdmin4 which is installed automatically when you install PostgreSQL for Windows. It should be on the left, an example would be under Servers -> PostgreSQL -> Databases -> postgres -> Schemas -> actionitemsdata -> Tables -> actionitems. You can right click this, and press 'View/Edit Data' and finally click 'All Rows', and you can see the sample data loaded into the table.
  
## Authors

| Author                                           | Current Year |            Course             |
| :----------------------------------------------- | :----------: | :---------------------------: |
| [Prathamesh Sai](https://github.com/saisankp)    |   3rd year   |       Computer Science        |
| [Luke Seckerson](https://github.com/Level-1-Developer)      |   3rd year   |       Computer Science        |
| [Varjak Alex Wolfe](https://github.com/varjakw)   |   3rd year   | Computer Science  |
| [Thomas Moroney](https://github.com/tmoroney)    |   2nd year   |       Computer Science        |
| [Kevin Morley]()        |   2nd year   |       Computer Science        |
| [Eimear Ryan](https://github.com/eimearryan)  |   2nd year   |       Computer Science and Business        |
