"use strict";
/**
 * @return db details as returnObject
 */
var config = require('../config/node_starter_kit_config');
var cfenv = require('cfenv'),
    appEnv = cfenv.getAppEnv();
var cfRedisUrl = (function () {
    if (appEnv.getService(config.service_name)) {
        return appEnv.getService(config.service_name).credentials;
    } else {
        throw new Error('No service name ' + config.service_name + ' bound to the application.');
    }
}());

/**
 * Parse VCAP services objects, based on service type
 * @return db details as returnObject
 */

module.exports.getEnv = function () {
    var redisCreds = cfRedisUrl;
    return {
        host: redisCreds.hostname,
        password: redisCreds.password,
        port: redisCreds.port
    };
};
