'use strict';
/*global angular */
/*
    Module to make service calls to server component from UI component
*/
angular.module('toDo').factory('Todos', [
    '$http', function ($http) {
        return {
            GetItemCount: function () {
    //Get all todo's from the service component
    return $http.get('/cache/GetItemCount');
},
deleteCache : function(){
    //Delete a todo by making a delete call to the controller
    return $http.post('/cache/DeleteCache/');
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
