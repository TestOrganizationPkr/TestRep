"use strict";

/*
 This files creates DB Connection and perform all the DB Crud Operations.
 */
// Redis client for node.js
var redis = require("redis");

// To get the redis connection details
var nodeStarterConfig = require('../vcap_parser/environment_parser');
// Redis client connection object
var redisClient = null;

/**
 * Create DB Connection
 * @param callBack contains err if connection fails
 */
function initDataBase(callBack) {
    // Get the DB Configuration from environment_parser.js and create connection to the database.
    var redisDBCredentials = nodeStarterConfig.getEnv();
    // create a new redis redisClient and connect to redis instance
    redisClient = redis.createClient(redisDBCredentials);
    if ('password' in redisDBCredentials) {
        // To authenticate against Redis
        redisClient.auth(redisDBCredentials.password);
    }
    // if an error occurs, return error message to user
    redisClient.on('error', function (err) {
        return callBack(err);
    });
}
/**
 * To get all the records based on the unique redis key
 * @param redisKey
 * @param callback
 */
function getAll(redisKey, callback) {
    // return all records using the key name and passing the limit option (0,-1)
    redisClient.lrange(redisKey, 0, -1, function (err, result) {
        callback(err, result.map(function (task) {
            return JSON.parse(task);
        }));
    });
}
/**
 * To insert new record
 * @param redisKey - key which stores the object
 * @param data - new object to insert
 * @param callback
 */
function create(redisKey, data, callback) {
    // Using key insert the object
    redisClient.rpush(redisKey, JSON.stringify(data), function (err, result) {
        callback(err, data);
    });
}
/**
 * To read all the records
 * @param redisKey - key which stores the object
 * @param id - primary key for any object
 * @param callback
 */
function get(redisKey, id, callback) {
    // To get the index of the id if the record is found in DB
    // Get all records to perform match operation based on the user input
    getAll(redisKey, function (err, data) {
        if (err) return callback(err);

        var match, index = -1;

        for (var i = 0; i < data.length && !match; i++) {
            // if db record and user input matches then set the index value
            if (data[i]._id === id) {
                match = data[i];
                index = i;
            }
        }

        callback(null, match, index);
    });
}
/**
 * To delete record
 * @param redisKey - key which stores the object
 * @param id - primary key for any object
 * @param callback
 */
function remove(redisKey, id, callback) {
    // To get the record
    get(redisKey, id, function (err, task) {
        if (err) return callback(err);
        // If input record is not found, throw error to user
        if (task === undefined) return callback(null, task);

        // delete the input record
        redisClient.lrem(redisKey, 0, JSON.stringify(task), function (err, result) {
            callback(err, result);
        });
    });
}
/**
 * To update record
 * @param redisKey - key which stores the object
 * @param data - input data to be updated
 * @param taskKey - key name inside the object value
 * @param callback
 */
function update(redisKey, data, taskKey, callback) {
    // To get the record
    get(redisKey, data.id, function (err, task, index) {

        if (err) return callback(err);
        // If input record is not found, throw error to user
        if (task === undefined) return callback(null, task);

        // Prevent data storage and update the input given by user using the field name
        if (data.hasOwnProperty(taskKey)) {
            task[taskKey] = data[taskKey];
        }
        // update using the index of the record
        redisClient.lset(redisKey, index, JSON.stringify(task), function (err) {
            callback(err, task);
        });
    });
}

//Export all the functions
exports.initDataBase = initDataBase;
exports.getAll = getAll;
exports.create = create;
exports.update = update;
exports.remove = remove;