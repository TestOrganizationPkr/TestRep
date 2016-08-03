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

