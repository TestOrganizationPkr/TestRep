# About the Framework – Socket.io

  Socket.IO enables real-time bidirectional event-based communication. It works on every platform, browser or device, focusing equally on reliability and speed.

## About the App
  Chat App is developed using Angular.js + NodeJS + Express 3.5, Socket.IO modules for web socket support.
  This Chat application is a traditional chat program to demonstrate the use of Socket.IO. 
  Angular.Js is used for Frontend support.
  Node.js is used as a chat Websocket server.

## Prerequisite Technologies
* [Node.js](http://nodejs.org) - Download and Install Node.js, node school has free node tutorials to get you started.

## How to Run Application in local

Install Node Modules:		
```sh
$ cd <myApp> && npm install
```
To starter server run:  	
```sh
$ node server
```
Open a browser and go to:	
```sh
http://localhost:3000
```

## Running on a different port
If you have a rails, node, or other mean project already running, you may need to use a different port. You can set the port and start your new mean project with one command:
```sh
$ export PORT=3001 && gulp
```
Then, open a browser and change the port number before you visit:
```sh
http://localhost:3001
```

## Deploying To Cloud Foundry
  - Install the Cloud Foundry command line tools
  -	For Pivotal Web Services follow the below CLI commands :
```sh
cf login -a api.run.pivotal.io
```
  - For IBM Bluemix Web Services follow the below CLI commands :
```sh
$ cf login -a api.ng.bluemix.net
```

Pushing App to Cloud Foundry:
```sh
$ cf push <app name>
```
## Unit test case execution
  The server test cases are executed using:
```sh
$ mocha test
```

## Code Coverage
  Istanbul is all-javascript instrumentation library that tracks statement, branch, and function coverage.
  The code coverage for the unit test cases can be generated using:
```sh
$ istanbul cover _mocha -- -R spec
```