'use strict';

/**
 * Module dependencies.
 */
var expect = require('expect.js');
// Redis client for node.js
var redis = require("redis");
// To generate unique id for storing new record
var uuid = require('node-uuid').v4;

// To get the redis connection details
var nodeStarterConfig = require('../vcap_parser/environment_parser.js');

// Redis client connection object
var redisClient = null;
var REDIS_KEY = 'tasks';
// To get the index of the inserted record
var index = -1;

/**
 * Globals
 */
var todo;

/**
 * Unit tests
 */
describe('Redis-DB-Unit Test', function () {
    before(function (before_done) {
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
            throw err;
        });
        before_done();
    });
    before(function (done) {
        // Object to be inserted
        todo = {
            // primary key for any object
            _id: uuid(),
            task_name: 'ToDo number one'
        };
        done();
    });

    describe('Method Save', function () {

        it('should be able to save without problems', function (done) {
            // Using key insert the object
            return redisClient.rpush(REDIS_KEY, JSON.stringify(todo), function (error, result) {
                expect(error).to.be(null);
                //Check the inserted result
                expect(result).to.not.equal(0);
                done();
            });

        });
    });
    describe('Method Get All', function () {
        it('should be able to retrieve all ToDos without any problem', function (done) {
            return redisClient.lrange(REDIS_KEY, 0, -1, function (error, result) {
                expect(error).to.be(null);
                //Since a previous insert has been done, there should be atleast one todo in the database
                expect(result.length).to.be.above(0);
                result = result.map(function (task) {
                    return JSON.parse(task);
                });
                //Check if the list of datas has the data which we have inserted.
                var isDataExists = 0;

                for (var i = 0; i < result.length; i++) {
                    // if db record and user input matches then set the index value
                    if (result[i].task_name === 'ToDo number one') {
                        isDataExists = 1;
                        // To get the index of the found record
                        index = i;
                    }
                }
                // Check input record exists or not
                expect(isDataExists).to.be(1);
                done();
            });
        });
    });

    describe('Method Update', function () {

        it('should be able to update without problems', function (done) {
            todo.task_name = 'ToDo number two';
            // update using the index of the record
            return redisClient.lset(REDIS_KEY, index, JSON.stringify(todo), function (error, result) {
                expect(error).to.be(null);
                //Check if the updated value is reflected
                expect(result).to.equal('OK');
                done();
            });
        });
    });

    describe('Method Delete', function () {

        it('should be able to delete without problems', function (done) {
            // delete the input record
            return redisClient.lrem(REDIS_KEY, 0, JSON.stringify(todo), function (error, result) {
                expect(error).to.be(null);
                expect(result).to.be(1);
                done();
            });

        });

    });

    after(function (done) {
        // delete the input record
        redisClient.lrem(REDIS_KEY, 0, JSON.stringify(todo), function () {
            done();
        });
    });
});

