# About the Framework – ASP.NET MVC Blank Web App

The ASP.NET MVC framework is a template provided by Microsoft, which implements the model–view–controller (MVC) pattern.

## About the App

  The "Blank" Web app is created using ASP.NET MVC framework in order to showcase a App using a database and to demonstrate other Web app functionalities. 

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
# About Azure DocumentDB

Azure DocumentDB is a fully managed NoSQL database service built for fast and predictable performance, high availability, automatic scaling, and ease of development. Its flexible data model, consistent low latencies, and rich query capabilities make it a great fit for web, mobile, gaming, and IoT, and many other applications that need seamless scale.

## To Run the application in Local machine

- Update the Server Key values in Web.Config AppSettings section

>     <add key="endpoint" value="<<EndPoint_Name>>"/>
>     <add key="authKey" value="<<Aunthentication_Key>>/>
>     <add key="database" value="<<DB_Name>>"/>
>     <add key="collection" value="<<Collection_Name>>"/>

## How DocumentDB is used in this ToDo App?

In this Blank app, the functionalities are seperated by layers. And DAL is responsible for making the communication with DocumentDB. 

- DAL
  	- Contract
		IRepository - Generic functionalities of DocumentDB method declarations
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

# About Azure Redis Cache

Microsoft Azure Redis Cache is based on the popular open source Redis Cache. It gives you access to a secure, dedicated Redis cache, managed by Microsoft. A cache created using Azure Redis Cache is accessible from any application within Microsoft Azure.

## To Run the application in Local machine

- Update the Server Key values in Web.Config ConnectionStrings section

>     <add name="<<ConnectionString_Name>>" connectionString="<<Redis_Server_Name>>" />

## Using the Cache layer
Instantiate the DataCache object and call the implemented methods like GetValue, Remove, Increment for retrieving a value, deleting and incrementing the value in Redis respectively.

## Connect to the cache
In this we have used StackExchange.Redis package to connect to Redis. The connection to the Azure Redis Cache is managed by the `ConnectionMultiplexer` class. This class is designed to be shared and reused throughout your client application, and does not need to be created on a per operation basis.

Once the connection is established, return a reference to the redis cache database by calling the `ConnectionMultiplexer.GetDatabase` method. The object returned from the `GetDatabase` method is a lightweight pass-through object and does not need to be stored.

	// Connection refers to a property that returns a ConnectionMultiplexer
	// as shown in the previous example.
	IDatabase cache = Connection.GetDatabase();

	// Perform cache operations using the cache object...
	// Simple put of integral data types into the cache
	cache.StringSet("key1", "value");
	cache.StringSet("key2", 25);

	// Simple get of data types from the cache
	string key1 = cache.StringGet("key1");
	int key2 = (int)cache.StringGet("key2");

## Add and retrieve objects from the cache

Items can be stored in and retrieved from a cache by using the `StringSet` and `StringGet` methods.

	// If key1 exists, it is overwritten.
	cache.StringSet("key1", "value1");

	string value = cache.StringGet("key1");

Redis stores most data as Redis strings, but these strings can contain many types of data, including serialized binary data, which can be used when storing .NET objects in the cache.

When calling `StringGet`, if the object exists, it is returned, and if it does not, `null` is returned. In this case you can retrieve the value from the desired data source and store it in the cache for subsequent use. This is known as the cache-aside pattern.

    string value = cache.StringGet("key1");
    if (value == null)
    {
        // The item keyed by "key1" is not in the cache. Obtain
        // it from the desired data source and add it to the cache.
        value = GetValueFromDataSource();

        cache.StringSet("key1", value);
    }

To specify the expiration of an item in the cache, use the `TimeSpan` parameter of `StringSet`.

	cache.StringSet("key1", "value1", TimeSpan.FromMinutes(90));
	
You can also serialize and de-serialize while store/retrieve the data.
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

