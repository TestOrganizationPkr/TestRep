# ReactJS

React is front end library developed by Facebook. It's used for handling view layer for web and mobile apps. ReactJS allows us to create reusable UI components. React uses virtual DOM which is JavaScript object. This will improve apps performance since JavaScript virtual DOM is faster than the regular DOM.


### Client File Structure
```
└── client
│      └── src
│      │    ├── actions
│      │   	│     └── action.js
│      │    ├── components
│      │   	│     └── app.jsx
│      │    ├── css
│      │   	│     └── main.less
│      │    ├── dispatcher
│      │   	│     └── dispatcher.js
│      │    ├── index.jsx
│      │    ├── reducers
│      │   	│     └── reducer.js
│      │    ├── services
│      │   	│     └── service.js
│      │    ├── stores
│      │   	│     └── store.js
│      ├── tests
│      │   └── test.js
|      └── index.html
├── manifest.yml
└── package.json

```
### Files related to ReactJS
The application folder structure is modular, so that our components, actions and all of our services are in separate files. Having all of the functionality in different modules helps to understand the overall layout of the application, hence the re-use and testing of code is easy.

```
index.html  # Includes the main script and contains the HTML code to render the application.
```
**actions**

```
action.js     # This is the module which contains all the app actions. 
```
**components**

Component-based development allows for a front end that is highly reusable and extendable, with both data flows and interaction flows that are easy to follow and reason about. React offers the best tool for building view-layer components currently available.

The **stylesheets and JavasScript/JSX for all the UI components resides in the components directory**.
```
less         # Contains the styles information required for the UI
jsx         # This files renders the UI components
```

**css**
```
main.less         # Contains the styles information required for the UI
```

**dispatcher**
```
dispatcher.js        # This is a singleton class that operates as the central hub for application updates. This class dispatches the view and server actions
```

**reducers**
```
reducer.js        # Contains all the constants used for UI events
```
**services**
```
toDo.js     # This is the service module and is meant to interact with our Node API. All the service calls are present in this file. This ensures that we can test this code separate of our overall application.
```
**stores**
```
todo-store.js     # The store allows components to register/unregister listeners, and emit change events.

### Unit Testing

The unit tests in this application are written using jasmine . A sample template is given in the tests folder. This can be executed using *karma* tools or other testing tools and task runners can also be used.# Node Server
In this project, express module is used to create a server and the basic CRUD operations is given as node services. The following sections explain the technologies used and the usage of some of the important files present in the server code.


## Prerequisite Technologies

### Linux
* *Node.js* - <a href="http://nodejs.org/download/">Download</a> and Install Node.js, nodeschool has free <a href=" http://nodeschool.io/#workshoppers">node tutorials</a> to get you started.
* *MongoDB* - <a href="https://www.mongodb.org/downloads">Download</a> and Install mongodb - <a href="https://docs.mongodb.org/manual/">Checkout their manual</a> if you're just starting.

For using ubuntu, this is the preferred repository to use...

```bash
$ curl -sL https://deb.nodesource.com/setup | sudo bash -
$ sudo apt-get update
$ sudo apt-get install nodejs
```


### Windows
* *Node.js* - <a href="http://nodejs.org/download/">Download</a> and Install Node.js, nodeschool has free <a href=" http://nodeschool.io/#workshoppers">node tutorials</a> to get you started.
* *MongoDB* - Follow the great tutorial from the mongodb site - <a href="https://docs.mongodb.org/manual/tutorial/install-mongodb-on-windows/">"Install Mongodb On Windows"</a>


### OSX
* *Node.js* -  <a href="http://nodejs.org/download/">Download</a> and Install Node.js or use the packages within brew or macports.
* *MongoDB* - Follow the tutorial here - <a href="https://docs.mongodb.org/manual/tutorial/install-mongodb-on-os-x/">Install mongodb on OSX</a>

## Server File Structure
  ```
└── server
│       ├── config
│       │  └── node_starter_kit_config.json
│       │  └── QueryConstants.json
│       ├── controllers
│       │  └── DBHandler.js
│       │  └── BusinessLogicHandler.js
│       │  └── LogHandler.js
│       ├── tests
│       │  └── test.js
│       └── vcap_parser
│       │  └── environment_parser.js
│       └── routes.js
├── app.js
├── manifest.yml
└── package.json
```
**app.js**

This file contains the server object and is the base of the project. It is the starting point of the application and starts the server.
By default, the server listens at the port which is defined in the environment variable named *PORT*. If this is not defined, *3000* is taken as the default port.
The server port can be set to either the environment variable or some other value of choice.
  ```
    app.set('port', process.env.PORT || 3000);
  ```

  In case of malicious attacks or mistyped URLs, the error is handled and *404 Page Not Found Error* is thrown. 
  
    app.use(function (req, res) {
      res.status(404);
      res.send({error: 'Page Not found'});
      return;
    });
  


**route.js**

This file contains all the routes and matches them with the appropriate handler function.
The server code implements sample GET, POST, PUT, and DELETE services in this template.



### MongoDB

MongoDB is a free and open-source cross-platform document-oriented database. Classified as a NoSQL database, MongoDB avoids the traditional table-based relational database structure in favor of JSON-like documents with dynamic schemas, making the integration of data in certain types of applications easier and faster. The sample template provided performs basic CRUD operations.
In this application the seneca plugins are used to connect to MySQL. The storage engine named "seneca-mongo-store" is used to persist data in MongoDB.


#### Files related to Mongo

The crux of the DB operations are present in the DBHandler file, present in the controllers directory. The database connection establishment and the CRUD operations are given in this file.


**QueryConstant**   
This file contains the constant values of the store name and the initial queries (create table queries) to be called after connecting to the database. It is present in the config directory.

**DBHandler**   
Database operations are done here. The operations are done based on the events received from the BusinessLogicHandler. The code to connect to the server and perform basic CRUD operations are done in this file. It takes the input as the table name and data to be manipulated.


**LogHandler**      
Logging operations are done here. This file has a utility function which takes the log level and the message to be logged as the input and logs to the console output.

**BusinessLogicHandler**     
Business logic like response handling and error handling operations are done here. This is the file which contains handling of the requests like validation and forming of model objects and sends it to the DBHandler file. The error or response sent back from the DBHandler is sent to the client in the appropriate format.
This file creates events for each action type which is caught by the DBHandler.

## Deploying the application

The Application can be run locally or deployed in cloud foundry and in both scenarios MongoDB service has to be integrated with the application.

### Deploying the application locally

In order to run the app locally, the *local_environment_parser.js* file, present in the *vcap_parser* directory, has to be changed according to the local DB information.

Replace the following line accordingly

`    return {
        name: 'Database_NAme',
        host: 'localhost',
        username: 'username',
        password: '****',
        port: 27017
    };`

Rename the *local_environment_parser* to *environment_parser*.

### Deploying to Cloud Foundry
The following denotes the steps required to deploy the application to PAAS systems like Pivotal and Bluemix.
When deployed in the cloud foundry through starter kit, there are no changes to be done in the code. When deploying to the Cloud foundry manually, the environment_parser.js must be replaced with the appropriate file provided for the PAAS and the node_starter_kit_config.json (present in the config folder) must contain the required “service instance name”  which was created in cloud foundry .

`  "service_name":"mongolab", `  // The service instance name created in cloud foundry 


##### Logging in to Cloud Foundry
  - For Pivotal Web Services follow the below CLI commands for login :

```sh
$ cf login -a api.run.pivotal.io
```
  - For IBM Bluemix Web Services follow the below CLI commands :
```sh
$ cf login -a api.ng.bluemix.net
```

##### Creating Mongo DB service

 IBM Bluemix & Pivotal Web Services offer a free MongoLabs service.
  - If you are using IBM Bluemix, run
```sh
$ cf create-service mongolab sandbox service_instance_name
$ cf bind-service AppName service_instance_name
```
  - If you are using Pivotal Web Services, run
```sh
$ cf create-service mongodb 100 service_instance_name
$ cf bind-service AppName service_instance_name
```

##### Manifest


Application manifests tell cf push what to do with applications. This includes everything from how many instances to create and how much memory to allocate to what services applications should use.

A manifest can help you automate deployment, especially of multiple applications at once.

By default, the cf push command deploys an application using a manifest.yml file in the current working directory.


###### Example Manifest:


Manifests are written in YAML. The manifest below illustrates some YAML conventions, as follows:

* The manifest may begin with three dashes.
* The applications block begins with a heading followed by a colon.
* The application name is preceded by a single dash and one space.
* Subsequent lines in the block are indented two spaces to align with name.


```
applications:

- name: nifty-gui
  memory: 512M
  host: nifty
```

A minimal manifest requires only an application name. To create a valid minimal manifest, remove the memory and host properties from this example.


###### Disk quota attribute:


Use the disk_quota attribute to allocate the disk space for your app instance. 


```
disk_quota: 1024M
```


###### Domain attribute:


Every cf push deploys applications to one particular Cloud Foundry instance. Every Cloud Foundry instance may have a shared domain set by an admin. Unless you specify a domain, Cloud Foundry incorporates that shared domain in the route to your application.

You can use the domain attribute when you want your application to be served from a domain other than the default shared domain.


```
domain: unique-example.com
```


###### Instances attribute:


Use the instances attribute to specify the number of app instances that you want to start upon push:


```
instances: 2
```


###### Memory attribute:


Use the memory attribute to specify the memory limit for all instances of an app.  For example:

```
  memory: 1024M
```


The default memory limit is 1G. You might want to specify a smaller limit to conserve quota space if you know that your app instances do not require 1G of memory.


###### Host attribute:


Use the host attribute to provide a hostname, or subdomain, in the form of a string. This segment of a route helps to ensure that the route is unique. If you do not provide a hostname, the URL for the app takes the form of APP-NAME.DOMAIN.


```
host: my-app
```

##### Pushing the application to cloud foundry

The following command has to be executed to push the application to cloud foundry.

```sh
$ cf push <app name>
```
Use the following command from your root directory to push the application to cloud foundary using the *manifest.yml*.

```sh
$ cf push
```



## Unit Testing

The test cases are given in the test folder. A sample template written using *mocha* and *chai* is given. It can be run using the mocha tool or any other test case tools can also be used.


