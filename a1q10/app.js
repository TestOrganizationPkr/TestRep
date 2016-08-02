'use strict';

// Add Dependent modules.
var express = require('express'),
    io = require('socket.io'),
    http = require('http'),
    app = express(),
    server = http.createServer(app),
    io = io.listen(server),
    path = require('path'),
    favicon = require('static-favicon'),
    cookieParser = require('cookie-parser'),
    bodyParser = require('body-parser');


// set up our socket server
require('./server/chatserver')(io);

var httpRouter = require('./server/routes');

// start the server
server.listen(process.env.PORT);

// middleware settings
app.use(favicon());
// parse application/json
app.use(bodyParser.json());
// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded());
// arse Cookie header and populate req.cookies
app.use(cookieParser());

//To serve static files such as images, CSS files, and JavaScript files, use the express.static built-in middleware function in Express
app.use(express.static(__dirname +  '/client/'));

app.use('/', httpRouter);

/// catch 404 and forwarding to error handler
app.use(function (req, res, next) {
  var err = new Error('Not Found');
  err.status = 404;
  next(err);
});

/// error handlers

// development error handler
// will print stacktrace
if (app.get('env') === 'development') {
  app.use(function (err, req, res, next) {
    res.status(err.status || 500);
    res.render('error', {
      message: err.message,
      error: err
    });
  });
}

// production error handler
// no stacktraces leaked to user
app.use(function (err, req, res, next) {
  res.status(err.status || 500);
  res.render('error', {
    message: err.message,
    error: {}
  });
});


module.exports = app;
