'use strict';
/*global angular */
/*
    Module to make service calls to server component from UI component
*/
angular.module('toDo').factory('Todos', [
    '$http', function ($http) {
        return {
            FileUpload: function (file) {
                var formData = new FormData();
                formData.append("file", file);
                //FileUpload a blob by making a upload file call to the controller
                return $http.post('/blob/FileUpload/', formData, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                });
            },
            GetFileName: function () {
                //Get get file name
                return $http.get('/blob/GetFileName');
            },
            DeleteFile: function () {
                //Get get file name
                return $http.post('/blob/DeleteFile');
            },
            get : function () {
                //Get all todo's from the service component
                return $http.get('/todo/getall');
            },
            create : function (todoData) {
                //Create todo by making post call to the controller
                return $http.post('/todo/create', todoData);
            },
            update : function (todoData) {
                //Update todo by making put call to the controller
                return $http.post('/todo/update', todoData);
            },
            delete: function (id) {
                //Delete a todo by making a delete call to the controller
                return $http.post('/todo/delete/' + id);
            }
        };
    }
]);
