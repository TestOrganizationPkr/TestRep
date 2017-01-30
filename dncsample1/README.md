# About the Framework – .NET Core MVC Web

The .NET Core MVC framework is a template provided by Microsoft, which implements the model–view–controller (MVC) pattern in .Net core.

## About the App

  The "ToDo" Web app is created using .NET Core MVC framework in order to showcase an App that will do CRUD operations using a database and to demonstrate other Web app functionalities. .Net core app can be deployed in other platform like Linux and Mac, but not supported in unix.

## Prerequisites
  - Visual Studio 2015 Update 3
  - .NET Core 1.0.1 - VS 2015 Tooling Preview 2
  
## Folder Structure
  - DataAccessLayer
  	- Contract
	- Migrations
  	- Models
  	- Repository
  - Domain
  	- Models
  - BusinessLogic
  - App
  	- Controllers
  	- Models
  	- wwwroot (for Scripts, css and images)
  	- Views
		
### Data Access Layer 

This is data access layer, which will interact with the database. Connection Strings should be configured in appsettings.json. At First, connection string will be read from VCAP environment variable. If not then, it will be read from appsettings.json. Database context will be created using this connection strings and database call will be invoked. Environment variables are the means by which the Cloud Foundry runtime communicates with a deployed application about its environment.

### Domain layer

This layer contains the domain model which is used in the application. In this we have used DataAnnotations for validating user input.

### Business Logic Layer

This layer is responsible for implementing business logic and conversion to / from DAL and Domain models. In this we have used auto mapper to do mapping between Domain and DAL models.

### App Layer

This is a Web application using MVC 5 architecture.

Below are the features implemented in the application. 

#### Startup class

The Startup class is where you define the request handling pipeline and where any services needed by the app are configured. The Startup class must be public.

#### Program class

Main method uses WebHostBuilder, which follows the builder pattern, to create a web application host. The builder has methods that define the web server (for example UseKestrel) and the startup class (UseStartup).  For cross platform, webhost builder is required.

#### Logging

For logging, we are using LoggerFactory. There is in built dependency injection which will take care resolving the dependency. We just need to pass ILoggerFactory to access LogInformation, LogError methods.

#### Validation

####Client side validation

For client side validation you just need to put the below script reference in the view, it will automatically validate the form based on the model DataAnnotations

- jquery-1.11.3.min.js
- jquery.validate.min.js
- jquery.validate.unobtrusive.min.js

##### Server side validation

For server side validation you need to use the below, this will return a bool based on model passed :-
```sh
if (ModelState.IsValid){}
```

#### Exception handling

If an exception is thrown in the application, user will be taken to an error page after logging the error. All the unhandled exceptions would be caught in the controller.

#### Dependency injection 

Dependecy injection isdone using in built method AddScope in IServiceCollection services
services.AddScoped<IToDoRepository, ToDoRepository>();


## Unit test case execution:

 We have used InMemory database for unit tesing. The test cases for the operations are present in the tests Project. These test cases are executed using Visual Studio. To run the test cases goto "Test" menu and select "Run" sub-menu then select the options like All Tests/Passed Tests/Failed Tests/Not Run Tests.
 
## Code coverage:

We can check the code coverage for each of the file using Visual studio. To check the code coverage goto "Test" menu and select "Analyze Code Coverage" then select the options like Selected Tests/All Tests
 
## How to run the application in your local machine:

- Add/Update connection details of the database in Appsettings.json.
- Build and Run the solution

## .Net core Knockout related file

- Index.cshtml
- todo.js

### Index.cshtml

This is the view which will render the html in browser by default when the application start. The default routing in defined in Startup.cs file which is inside Configure Method. So from there it will call the Index method inside ToDoController controller which will render the Index View.

### todo.js

Based on the even trigger in the Index.cshtml it will call the respective method in the todo.js file. After that it will do validation and call a method of MVC controller through ajax. Based on response received from the controller it will load the view accordingly.
# About Azure DocumentDB

Azure DocumentDB is a fully managed NoSQL database service built for fast and predictable performance, high availability, automatic scaling, and ease of development. Its flexible data model, consistent low latencies, and rich query capabilities make it a great fit for web, mobile, gaming, and IoT, and many other applications that need seamless scale.

## To Run the application in Local machine

- Update the Server Key values in Web.Config AppSettings section

>     <add key="endpoint" value="<<EndPoint_Name>>"/>
>     <add key="authKey" value="<<Aunthentication_Key>>/>
>     <add key="database" value="<<DB_Name>>"/>
>     <add key="collection" value="<<Collection_Name>>"/>

## How DocumentDB is used in this ToDo App?

In this ToDo app, the functionalities are seperated by layers. And DAL is responsible for making the communication with DocumentDB. 

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

## List of functionlaities implented for ToDoItem Object
	
	Connection should be established to make a communication between client and DocumentDB. The below code will establish the connection object.

			new DocumentClient(new Uri(<<Document DB EndPoint>>), <<Primary/Secondary Key of the DocumentDB>>);

	- Create
	- GetAll
	- GetByID
	- Update
	- Delete

### Create Method
	
	This method is responsible for creating/inserting a generic object/document into specified DocumentDB.

	client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(<<Your_DatabaseName>>, <<Your_CollectionName>>), <<Your Object>>)

	The above method returns a document which was inserted.

### GetAll Method
	
	This method is responsible for fetching the documents from the mentioned collection as a list.

	client.CreateDocumentQuery<TObject>(UriFactory.CreateDocumentCollectionUri(<<Your_DatabaseName>>, <<Your_CollectionName>>)).AsDocumentQuery();

### GetByID Method
	
	This method is responsible for fetching the documents based upon the search criteria.

	client.ReadDocumentAsync(UriFactory.CreateDocumentUri(<<Your_DatabaseName>>, <<Your_CollectionName>>, <<Search_ID>>));

### Update Method
	
	This method is responsible for updating the document.

	client.ReplaceDocumentAsync(UriFactory.CreateDocumentCollectionUri(<<Your_DatabaseName>>, <<Your_CollectionName>>), <<Object_To_Be_Replaced>>, null);

### Delete Method
	
	This method is responsible for deleting the document.

	client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(<<Your_DatabaseName>>, <<Your_CollectionName>>, <<ID_To_Be_Deleted));

# About Pivotal Redis Cloud Cache

Pivotal Redis Cloud Cache is based on the popular open source Redis Cache. It gives you access to a secure, dedicated Redis cache, managed by Pivotal. A cache created using Pivotal Redis Cloud Cache is accessible from any application within Pivotal.

## To Run the application in Local machine

- Update the Server Key values in appsettings.json ConnectionStrings section

  "Data": {
    "RedisConnection": "Please put your connection here"
  },
  
  ex:
  "Data": {
    "RedisConnection": "11.111.11.11:1111,password=xyz"
  }, 
## Using the Cache layer
Instantiate the DataCache object and call the implemented methods like GetValue, Remove, Increment for retrieving a value, deleting and incrementing the value in Redis respectively.

## Connect to the cache
In this we have used StackExchange.Redis package to connect to Redis. The connection to the Pivotal Redis Cloud Cache is managed by the `ConnectionMultiplexer` class. This class is designed to be shared and reused throughout your client application, and does not need to be created on a per operation basis.

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
