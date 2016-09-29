"use strict";
/* global logger */

// Add Dependent modules.
var express = require('express');
var app = express();
var bodyParser = require('body-parser');
var seneca = require('seneca')();
// initializes routes
var httpRouter = require('./server/routes');
var port = process.env.PORT || 3000;
var logModule = require("./server/controllers/LogHandler");
/*  Initialise Logging */
global.logger = logModule.initLogModule();

//To serve static files such as images, CSS files, and JavaScript files, use the express.static built-in middleware function in Express
app.use(express.static('./client'));

// enables our Express application to parse incoming JSON post bodies
app.use(bodyParser.json());

// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({'extended': 'true'}));


// integrate Seneca with Express
app.use(seneca.export('web'));

// If no route is matched by now, it must be a 404
app.use(function (req, res) {
    res.status(404);
    res.send({error: 'Page Not found'});
    return;
});
// Uncaught exception handler
process.on('uncaughtException', function (err) {
    logger.log("error", err.stack);
});
// START THE SERVER
app.listen(port);
logger.log("info", "App listening on port " + port);
module.exports = app;