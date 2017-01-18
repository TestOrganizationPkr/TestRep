# About the Framework – ASP.NET MVC Web

The ASP.NET MVC framework is a template provided by Microsoft, which implements the model–view–controller (MVC) pattern.

## About the App

  The "ToDo" Web app is created using ASP.NET MVC framework in order to showcase a App that will do CRUD operations using a database and to demonstrate other Web app functionalities. 

## Prerequisites
  - Visual Studio
  
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

This is a Web application using MVC 4 architecture.

Below are the features implemented in the application. 

#### Logging

For this there is an adapter called Logger which is inside Util folder in the application. 
You can see the log in Log tab in PCF portal.

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

## Deploying to PCF

- You need to build the solution before you will push the application to PCF.
- You need to open the commond prompt and login in PCF i.e. cf login
- Then go inside you project folder, startup project i.e. cd NET-sample-app/ViewEnvironment 
- Then run this command to push the application i.e.  cf push <<application_name>> -s <<stack_name>> -b binary_buildpack -p ./
## React related file

- Index.cshtml
- todo.jsx

### Index.cshtml

This is the view which will render the html in browser by default when the application start. The default routing in defined in RouteConfig.cs file which is inside App_Start. So from there it will call the Index method inside ToDoController controller which will render the Index View. 

### todo.jsx

Based on the even trigger in the Index.cshtml it will call the respective method in the todo.jsx file. After that it will do validation and call a method of MVC controller through ajax. Based on response received from the controller it will load the view accordingly.
# About MySQL Datatbase

MySQL is an open-source relational database management system (RDBMS). MySQL is the world's most popular open source database. With its proven performance, reliability and ease-of-use, MySQL has become the leading database choice for web-based applications, used by high profile web properties including Facebook, Twitter, YouTube, Yahoo! and many more.

## To Run the Solution in Local machine

- Update Connection string in Web.Config as given below
>      <add name="<<Connection_Name>>" connectionString="<<Server_Name>>" providerName="<<Data_Provider_Name>>" />

## To Run the Solution in PCF

In this ToDo app, the functionalities are seperated by layers. And VCapsParser is responsible for parsing the connection string from PCF evnironment and returing the connection string to the DAL layer. 

## How MySQL is used in this ToDo App?

In this ToDo app, the functionalities are seperated by layers. And DAL is responsible for making the communication with MySQL. And the communicateion is established through Entity Framework.

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

	Connection should be established to make a communication between client and MySQL. The below code will establish the connection object.

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

# About  Redis Cache

Redis Cache is based on the popular open source Redis Cache. In this application we are using cache as a seperate layer for performing CRUD operations. Initiallty It returns the cache count and the count increases depending upon the CRUD operations performed.


## To Run the application in Local machine

- Update the Server Key values in Web.Config ConnectionStrings section

>     <add name="<<ConnectionString_Name>>" connectionString="<<Redis_Server_Name>>" />

## Using the Cache layer
Instantiate the DataCache object and call the implemented methods like GetValue, Remove, Increment for retrieving a value, deleting and incrementing the value in Redis respectively.

## Connect to the cache
In this we have used StackExchange.Redis package to connect to Redis. The connection to the  Redis Cache is managed by the `ConnectionMultiplexer` class. This class is designed to be shared and reused throughout your client application, and does not need to be created on a per operation basis.

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
