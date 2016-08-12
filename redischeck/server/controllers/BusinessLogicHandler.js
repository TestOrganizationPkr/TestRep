"use strict";

//Module dependencies.
/*globals logger*/
var DBHandler = require('./DBHandler');
var REDIS_KEY = "sample_key";
/**
 * connect to redis db
 * @param callBack - callback function to proceed to the route’s handler
 */

DBHandler.initDataBase(function (err) {
    if (err) {
        logger.log("error", "Error connecting to database " + err.stack);
        return;
    }
});

/**
 * Insert a record
 * @param req
 * @param res
 */
function create(req, res) {
    // Object to be inserted
    var data = req.body.columnName;
    DBHandler.create(REDIS_KEY, data, function (err, result) {
        if (error) {
            // Handle internal errors
        }
        else {
            // The request created a new resource object
        }
    });
}

exports.create = create;
