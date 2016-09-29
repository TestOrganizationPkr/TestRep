/*This class will listen for notification from calling function,
 and perform the DB operations(Create, Read, Update, Delete) */

"use strict";

//Requiring the seneca modules
var seneca = require('seneca')();
seneca.use('seneca-entity');

/**
 * Connect to the database by getting the data as input
 * @param store - Storage plugin used for db operations
 * @param env - The object containing the URL and credential information to connect to the store
 * @param entity - The name of the initial entity to be used.
 * @param initialSchema - The create tables query which needs to be called as soon as the database is connected
 * @param callBack - Callback function to proceed to the calling function
 */
function initDB(store,env,entity,initialSchema,callBack){

    //seneca.use function is called to connect plugin and use the store for db operations
    seneca.use(store, env);
    //ready function is called as soon as the connection to the database is established
    seneca.ready(function () {
        //If there is an initial set of queries to be executed like table creation the following is executed
        if(initialSchema){
            createTable(entity,initialSchema,callBack);
        }
        else
        {
            callBack(null);
        }
    });
}



/**
 * Function which will call the native functions to execute queries
 * @param entity - The name of the initial entity to be used.
 * @param initialSchema - The create tables query which needs to be called as soon as the database is connected
 * @param callBack - Callback function to proceed to the calling function
 */
function createTable(entity,initialSchema,callBack) {

    /*The native driver can be accessed with native function, in this case, the connectionPool object using
    entity.native$(function (error, connectionPool) {...}).*/

    entity.native$(function (error, connectionpool) {
        if (!error) {
            connectionpool.query(initialSchema, function (error) {
                callBack(error);
            });
        }
    });
}


/*The `seneca.add` method adds a new pattern, and the function to execute whenever that pattern occurs.
* @param args The arguments passed when the pattern has occurred. The entity for which the objects have to be retrieved
*             from the database is passed in the args field.
* @param callBack to the calling function, where the pattern has occurred.
*/


//Get the list of entities from database
seneca.add({cmd: 'getAllObjectsEvent'}, function (args, callback) {
    // To get the list fo entities. Optionally a query can be passed.
    args.entity.list$({}, function (error, listOfObjects) {
        callback(error, listOfObjects);
    });
});

//Insert or update an entity to the database
seneca.add({cmd: 'saveObjectEvent'}, function (args, callback) {
    args.entity.save$(function (error, insertedObject) {
        callback(error, insertedObject);
    });

});

//Find an entity from the database using the "id" as the primary key
seneca.add({cmd: 'findObjectEvent'}, function (args, callback) {
    args.entity.load$({'id' : args.entity.id},function (error, objectFound) {
        callback(error, objectFound);
    });

});

//Delete an entity from the database using the "id" as the primary key
seneca.add({cmd: 'deleteObjectEvent'}, function (args, callback) {
    args.entity.remove$({'id' : args.entity.id}, function (error, success) {
        callback(error, success);
    });
});

//Export the functions and seneca instance created
exports.initDB = initDB;
exports.seneca=seneca;