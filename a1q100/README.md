# About the Framework – ASP.NET MVC Web

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
  	- Service Location
  - App
  	- Controllers
  	- Models
  	- Scripts
  	- Views
		
### Data Access Layer 

This is data access layer, which will interact with the database. Connection Strings should be configured in Web.Config. Database context will be created using this connection strings and database call will be invoked.

### Domain layer

This layer contains the domain model which is used in the application. In this we have used DataAnnotations for validing user input.

### Business Logic Layer

This layer is responsible for implementing business logic and conversion to / from DAL and Domain models. In this we have used auto mapper to do mapping between Domain and DAL models.

### App Layer

This is a Web application using MVC 5 architecture.

Below are the features implemented in the application. 

#### Logging

For this there is an adapter called Logger which is inside Util folder in the application. 
To enable logging in Azure, login to https://portal.azure.com and navigate to your web app. Then click on “All Settings” -> then click on “Diagnostics logs” inside Features. Then based on your requirement you can either choose Filesystem or Blob. You can also select the level of logging you want. After configuring the logging click “Save” on top of the panel. 

#### Validation

##### Client side validation

For client side validation you just need to put the below script in the view, it will automatically validate the form based on the model DataAnnotations 
```sh
@Scripts.Render("~/bundles/jqueryval")
```

##### Server side validation

For server side validation you need to use the below, this will return a bool based on model passed :-
```sh
if (ModelState.IsValid){}
```

#### Exception handling

If an exception is thrown in the application, user will be taken to an error page after logging the error. All the unhandled exceptions would be caught in the controller.

#### Dependency injection 

This is to inject the dependency. For this there is an adapter name "UnityConfig" in App_Start folder. You need to register your type inside RegisterTypes function.
```sh
 container.RegisterType<IProductRepository, ProductRepository>();
```

## Unit test case execution:

 The test cases for the operations are present in the <ApplicationProjectName>tests Project. These test cases are executed using Visual Studio.
 To run the test cases goto "Test" menu and select "Run" sub-menu then select the options like All Tests/Passed Tests/Failed Tests/Not Run Tests
 
## Code coverage:

We can check the code coverage for each of the file using Visual studio. To check the code coverage goto "Test" menu and select "Analyze Code Coverage" then select the options like Selected Tests/All Tests
 
## Code analysis:

- Code Analysis is important to deliver perfect product
- Code Analysis can be setting can be done by following below steps
- "Right Click" on Solution Explorer --> Properties --> Common Properties --> Code Analysis Settings --> 
 	"Select Each Project" --> "Microsoft Managed Recommended Rules"
  
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
## Angular Blank App

The objective of this application is to provide the user to give the ToDo app functionalities using Angular JS client functionality.

## Angular related file

- Index.cshtml
- toDoController.js
- toDoService.js

### Index.cshtml

This is the view which will render the html in browser by default when the application starts. The default routing in defined in RouteConfig.cs file which is inside App_Start. So from there it will call the Index method inside ToDoController controller which will render the Index View. 

### toDoController.js

Based on the event trigger in the Index.cshtml it will call the respective method in the controller. Then this controller will pass the request to service layer and wait for the response. Based on response received from the toDoService it will load the view accordingly.

### toDoService.js

This layer will take the input from toDoController and do a request to MVC controller and wait for the response. Once the response is received it will pass back the response to toDoController.
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

﻿# Azure Blob Storage

Azure Blob storage is a service for storing large amounts of unstructured object data, such as text or binary data, that can be accessed from anywhere in the world via HTTP or HTTPS. You can use Blob storage to expose data publicly to the world, or to store application data privately.

## To Run the application in Local

- Update the Key values in Web.Config appsettings section
>     <add key="<<ConnectionString_Name>>" value="BlobServerName_AuthenticationKey" />

## Overview of Blob Storage Layer

In the solution there is project name BlobStorage which is of type class library.

Inside this project there is an interface IBlob and a class called Blob which is responsible for the operations that can be done on Azure Blob.

In this we have three methods :-

- DeleteBlob
- GetBlob
- SaveBlob

Note : In the sample application we have a feature to upload and delete profile pic of the user. So in the sample application we are restricting user to upload only image file i.e (jpg, png, gif and jpeg). But blob storage can accept any type of file.
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

