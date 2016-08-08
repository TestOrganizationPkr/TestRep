# About the Framework – ASP.NET MVC Blank Web App

The ASP.NET MVC framework is a template provided by Microsoft, which implements the model–view–controller (MVC) pattern.

## About the App

  The "ToDo" Web app is created using ASP.NET MVC framework in order to showcase a App that will do CRUD operations using a database and to demonstrate other Web app functionalities. 

## Prerequisites
  - Visual Studio
  - Microsoft Azure SDK 
  - Azure subscription
  
## Folder Structure
  - DataAccessLayer
  	- Contract
  	- Models
  	- Repository
  - Domain
  	- Models
  - BusinessLogic
  - App
  	- Controllers
  	- Views
		
### Data Access Layer 

This is data access layer, which will interact with the database. Connection Strings should be configured in Web.Config. Database context will be created using this connection strings and database call will be invoked.

### Domain layer

This layer contains the domain model which is used in the application. In this we have used DataAnnotations for validing user input.

### Business Logic Layer

This layer is responsible for implementing business logic and conversion to / from DAL and Domain models. In this we have used auto mapper to do mapping between Domain and DAL models.

### App Layer

This is a Web application using MVC 5 architecture with default MVC View and Default controller with default action method.
  
## How to run the application in your local machine:

- Add/Update connection details of the database in Web.Config.
- Build and Run the solution

## Deploying to Azure

- Select the WebApp which needs to be Hosted in Server
- Right Click on the Project and choose "Publish"
- Choose/Create new Deployment profile
- Update Connetion/Settings if any
- Verify the package which is being deployed using Preview
- Click "Publish"
## Knockout related file

- Index.cshtml
- default.js

### Index.cshtml

This file will reference the default services.

### default.js

This default service js file will have implementation of service call to default methods
# About Azure SQL Datatbase

SQL Database is a relational database service in the cloud based on the market-leading Microsoft SQL Server engine, with mission-critical capabilities. SQL Azure is based on the SQL Server engine, SQL Database supports existing SQL Server tools, libraries and APIs, which makes it easier for you to move and extend to the cloud.

SQL databases is available in Basic, Standard, and Premium service tiers. Each service tier offers different levels of performance and capabilities to support lightweight to heavyweight database workloads. You can build your first app on a small database for a few bucks a month, then change the service tier manually or programmatically at any time as your app goes viral worldwide, without downtime to your app or your customers.

## To Run the Solution in Local machine

- Update Connection string in Web.Config as given below
>      <add name="<<Connection_Name>>" connectionString="<<Server_Name>>" providerName="<<Data_Provider_Name>>" />
## How SQL Azure is used in this ToDo App?

In this ToDo app, the functionalities are seperated by layers. And DAL is responsible for making the communication with SQL Azure. And the communicateion is established through Entity Framework.

- DAL
  	- Contract
		IRepository - Generic functionalities of method declarations
		Repository  - Implementations of generic methods
   	
	- Models
		ToDoItem - Model object. (Note: Add model objects here to extend this application)

   	- Repository
		IToDoRepository - Functionalities specific to ToDoItem object are declared here. This interface should have to inherit IRepository
		ToDoRepository  - Method Implementations of ToDoItem is defined here. This class should implement IToDoRepository

## Steps to Extend the application functionalities

	- Create new model object under DAL - Models folder
	- Create new Object specific repository class and interfaces
	- Inherit the Object specific interface from IRepository interface
	- Override the functionalities in the new class and make a call to the methods of Repository class

## List of functionlaities implented for ToDoItem Object

	Connection should be established to make a communication between client and SQL Azure. The below code will establish the connection object.

    - SqlContext.cs
    public sealed class SqlContext : DbContext
    {
        public SqlContext() : base("name=DefaultConnection") { }
        public DbSet<ToDoItem> ToDoItems { get; set; }
    }

    Repository.cs
    protected DbSet<TObject> DbSet
    {
       get
       {
          return Context.Set<TObject>();
       }
    }

	- Create
	- GetAll
	- GetByID
	- Update
	- Delete

### Create Method
	
	This method is responsible for creating/inserting a generic object into specified SQL DB.

	public virtual TObject Create(TObject parameter)
    {
        var newEntry = DbSet.Add(parameter);
        Context.SaveChanges();
        return newEntry;
    }

### GetAll Method
	
	The above method returns the list of rows from the table.

	public virtual IQueryable<TObject> All()
    {
        return DbSet.AsQueryable();
    }

### GetByID Method
	
	This method is responsible for fetching the particular row based upon the search criteria.

	public virtual TObject Find(params object[] keys)
    {
        return DbSet.Find(keys
    }

### Update Method
	
	This method is responsible for updating the row.

	public virtual int Update(TObject parameter)
    {
        var entry = Context.Entry(parameter);
        DbSet.Attach(parameter);
        entry.State = EntityState.Modified;
        return Context.SaveChanges();
    }

### Delete Method
	
	This method is responsible for deleting the row.

	public virtual int Delete(TObject parameter)
    {
        DbSet.Remove(parameter);
        return Context.SaveChanges();
    }

# About the Azure Apllication Insights

Application Insights is an extensible analytics service that monitors live application and can track custom events and metrics. 

## To Run the application in Local

- Update the instrumentation Key of the App Insights in ApplicationInsights.config

>     <InstrumentationKey><<Key>></InstrumentationKey>

### App Analytics

This layer is responsible for collecting data required for analysis. Below is the list of methods that have been implemented.
	- TrackEvent
	- TrackMetric

### App Insights in Azure Portal

	- Right Click on the application and choose Application Insights -> Open Application Insights
	- Azure Portal will be openned and user can explore/view the Charts, graphs by choosing the specific Insights

### New Project

	- Create a new Project with "Application Insights" and provide your Azure subscription Credentials
	- Run the Application
	- Right Click on the application and choose Application Insights -> Search Debug Session Telemetry.
	- In new Window we can able to see the list of analytics on Requests, Events, Dependency, Exception, Metrics, Pageview, Traces

