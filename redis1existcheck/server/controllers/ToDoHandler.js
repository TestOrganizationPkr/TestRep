"use strict";

//Module dependencies.
/*globals logger*/
var ToDoDbHandler = require('./DBHandler');
// To generate unique id for storing new record
var uuid = require('node-uuid').v4;
// Constants Messages
var TASK_MESSAGE = 'Task name must not be empty';
var DELETE_MESSAGE = 'Deleted Successfully';
var TASK_ID_NOT_VALID = 'Task ID is not valid';
var REDIS_KEY = 'tasks';
var todoKey = 'task_name';
/**
 * connect to redis db
 * @param callBack - callback function to proceed to the route’s handler
 */

ToDoDbHandler.initDataBase(function (err) {
    if (err) {
        logger.log("error", "Error connecting to database " + err.stack);
        return;
    }
});


/**
 * Response Handler
 * @param response - Object through which response is sent
 * @param status - Boolean status of the process
 * @param data - contains the error or data information to be returned
 * The response handler will send the data in json format according to the status.
 */
function responseHandler(response, status, data) {
    if (status) {
        return response.status(200).json({
            status: "Success",
            data: data
        });
    }
    else {
        return response.status(500).json({
            status: "Error",
            data: data
        });
    }
}


/**
 * Insert a new todo record
 * @param req
 * @param res
 */
function create(req, res) {
    if (req.body === undefined || req.body.task_name === undefined || req.body.task_name === "") {
        responseHandler(res, false, TASK_MESSAGE);
    }
    else {
        // Retrieve the data to insert from the POST body
        // Object to be inserted
        var data = {
            // primary key for any object
            _id: uuid(),
            task_name: req.body.task_name
        };
        ToDoDbHandler.create(REDIS_KEY, data, function (err, result) {
            if (err) {
                // internal errors
                responseHandler(res, false, "Failed to create todo list");
            }
            else {
                // The request created a new resource object
                responseHandler(res, true, result);
            }
        });
    }
}
/**
 * to get all the todo list
 * @param req
 * @param res
 */
function all(req, res) {
    // Stream results back one row at a time
    ToDoDbHandler.getAll(REDIS_KEY, function (err, result) {
        if (err) {
            // internal errors
            responseHandler(res, false, "Failed to get todo list");
        }
        else {
            // To do list of records
            responseHandler(res, true, result);
        }
    });
}
/**
 * Update task name
 * @param req
 * @param res
 * @returns {*}
 */
function update(req, res) {
    if (req.body === undefined || req.body.task_name === undefined || req.body.task_name === "") {
        responseHandler(res, false, TASK_MESSAGE);
    }
    else {
        // We access the ID param on the request object
        var data = {
            id: req.params._id,
            task_name: req.body.task_name
        };
        ToDoDbHandler.update(REDIS_KEY, data, todoKey, function (err, result) {
            if (err) {
                // internal errors
                responseHandler(res, false, 'Failed to update todo list');
            }
            else if (result === undefined) {
                responseHandler(res, false, TASK_ID_NOT_VALID);
            }
            else {
                responseHandler(res, true, result);
            }
        });
    }
}
/**
 * delete todo record based on _id
 * @param req
 * @param res
 */
function remove(req, res) {
    // We access the ID param on the request object
    var id = req.params._id;
    // SQL Query > Delete Data
    ToDoDbHandler.remove(REDIS_KEY, id, function (err, result) {
        if (err) {
            // internal errors
            responseHandler(res, false, "Failed to delete todo list");
        }
        else if (result === undefined) {
            responseHandler(res, false, TASK_ID_NOT_VALID);
        }
        else {
            responseHandler(res, true, DELETE_MESSAGE);
        }
    });
}

exports.create = create;
exports.update = update;
exports.all = all;
exports.remove = remove;
