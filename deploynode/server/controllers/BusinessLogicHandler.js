/*global logger*/

/*This class will post notification to the DBHandler whenever server receives the request */

'use strict';

//  Require the dependant files.
var DBHander = require('./DBHandler');
var nodeStarterConfig = require('../vcap_parser/environment_parser.js');
var query = require('../config/QueryConstants');
var env = nodeStarterConfig.getEnv();
var seneca=DBHander.seneca;

/*
 Intialise the store and connect to the Database using the DBHandler. Get the store and initial setup queries from the
 QueryConstants file. seneca.make function is used to create data entity objects to store using the storage plugin.
 */
DBHander.initDB(query.STORE,env,seneca.make$('EntityName'),query.CREATE_TABLE_QUERY,function(error){
    //If no error has occurred while connecting to the Database
    if(!error) {
        logger.log("info","Succesfully connected to DB ...");
    }
});

/**
 * Create a new record
 * @param request
 * @param response
 * The Data to be inserted is passed in the request
 */
function create(request,response){
    //The seneca.make function is used to create seneca data entity objects which stores data in the table name "EntityName"
    var entity=seneca.make$('EntityName');
    //Assign the values obtained from the request to the entity.
   
    /*The seneca.act method accepts an object which is the "entity", and runs the command "saveObjectEvent" throwing
     the event that the pattern has matched. The associated function in DBHandler to insert the data, which has
      to be executed when this pattern occurs, will be executed.
     */
    seneca.act({cmd: 'saveObjectEvent',entity:entity }, function (error, data) {
        //The inserted object is returned in "data" and if any error is present the "error" object must be handled.

    });

}

//Export all functions in order to call from the route.js
exports.create = create;
