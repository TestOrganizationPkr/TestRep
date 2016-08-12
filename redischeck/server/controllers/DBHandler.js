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
        callback(err, result.map(function (redisData) {
            return JSON.parse(redisData);
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
 * @param callback
 */
function get(redisKey, callback) {
    // Get all records to perform match operation based on the user input
    getAll(redisKey, function (err, data) {
        if (err) return callback(err);
        callback(null, data);
    });
}
/**
 * To delete record
 * @param redisKey - key which stores the object
 * @param callback
 */
function remove(redisKey, callback) {
    // To get the record
    get(redisKey, function (err, redisData) {
        if (err) return callback(err);
        // delete the input record
        redisClient.lrem(redisKey, 0, JSON.stringify(redisData), function (err, result) {
            callback(err, result);
        });
    });
}
/**
 * To update record
 * @param redisKey - key which stores the object
 * @param data - input data to be updated
 * @param redisDataKeyKey - key name inside the object value
 * @param callback
 */
function update(redisKey, data, redisDataKeyKey, callback) {
    // To get the record
    get(redisKey, data, function (err, redisData, index) {

        if (err) return callback(err);
       
        // Prevent data storage and update the input given by user using the field name
        if (data.hasOwnProperty(redisDataKeyKey)) {
            redisData[redisDataKeyKey] = data[redisDataKeyKey];
        }
        // update using the index of the record
        redisClient.lset(redisKey, index, JSON.stringify(redisData), function (err) {
            callback(err, redisData);
        });
    });
}

//Export all the functions
exports.initDataBase = initDataBase;
exports.getAll = getAll;
exports.create = create;
exports.update = update;
exports.remove = remove;