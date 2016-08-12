"use strict";

//Require the appropriate modules and file needed
var express = require('express');
var BusinessLoginHandler = require('./controllers/BusinessLogicHandler');

// Create the express router object
var router = express.Router();

/*
 Assign the appropriate functions which are present in the BusinessLogicHandler to be called for different route paths.
 As a sample the following functions are given


 router.post('/path/:parameters', BusinessLoginHandler.create);
 router.get('/path/:parameters', BusinessLoginHandler.getAll);
 router.put('/path/:parameters', BusinessLoginHandler.update);
 router.get('/path/:parameters', BusinessLoginHandler.get);
 router.delete('/path/:parameters', BusinessLoginHandler.remove);


 */

module.exports = router;
