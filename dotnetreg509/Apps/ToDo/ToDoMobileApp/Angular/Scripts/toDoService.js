'use strict';

/*global angular */

/*
    Module to make service calls to server component from UI component
*/
var config = {
    headers: {
        'ZUMO-API-VERSION': '2.0.0'
    }
};

var baseURL = "Put your base url here";

angular.module('toDo').factory('Todos', [
    '$http', function ($http) {
        return {
            GetItemCount: function () {
                //Get all todo's from the service component
                return $http.get(baseURL+ '/api/cache',config);
            },
            deleteCache: function () {
                //Delete a todo by making a deletecache call to the controller
                return $http.delete(baseURL + '/api/cache', config);
            },
             FileUpload: function (file) {
                var formData = new FormData();
                formData.append("file", file);
                //FileUpload a blob by making a upload file call to the controller
                return $http.post(baseURL + '/api/blob/', formData, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined, 'ZUMO-API-VERSION': '2.0.0' }
                });
            },
            GetFileName: function () {
                //Get get file name
                return $http.get(baseURL + '/api/blob/', config);
            },
            DeleteFile: function () {
                //Get get file name
                return $http.delete(baseURL + '/api/blob/', config);
            },
            get: function () {                
                //Get all todo's from the service component
                return $http.get(baseURL+'/api/Todo/', config);
            },
            create : function (todoData) {
                //Create todo by making post call to the controller
                return $http.post(baseURL + '/api/Todo/', todoData, config);
            },
            update : function (todoData) {
                //Update todo by making put call to the controller
                return $http.put(baseURL + '/api/Todo/', todoData, config);
            },
            delete: function (id) {
                //Delete a todo by making a delete call to the controller
                return $http.delete(baseURL+ '/api/Todo/' + id, config);
            }
        };
    }
]);
