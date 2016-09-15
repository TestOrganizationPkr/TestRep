'use strict';
/*global angular */
/*global document */

/*
 This is the controller which contains all the actions which has to be carried out from the UI. The functions which
 are explained here include the getall, create and delete methods
 */
angular.module('toDo',[])
    .controller('ToDoController',  ['$scope','Todos', function($scope, Todos) {
        //formData is the data entered in the text field which has to be empty initially
        $scope.formData = {};


        //function to toggle the buttons which are displayed in the view
        function toggleEdit(todo) {
            //Get all the four icon buttons from DOM
            var bforEdit = document.getElementById("icon10" + todo.Name);
            var AfterEdit = document.getElementById("icon11" + todo.Name);
            //When edit button is clicked isEditable is enabled and edit options must be displayed
            if (!todo.isEditable) {
                bforEdit.style.display = "none";
                AfterEdit.style.display = "block";
            }
                //When cancel/save button is clicked isEditable is disabled and delete/edit buttons must be displayed
            else {
                bforEdit.style.display = "block";
                AfterEdit.style.display = "none";

            }
            //Toggle the value, to show and hide the text box and save/cancel buttons
            todo.isEditable = !todo.isEditable;
        }
        /*
         This function is called when the edit button is clicked. The text box along with the save and cancel buttons
         are displayed according to the toggle value (isEditable)
         */
        $scope.toggleEdit = function (todo) {
            toggleEdit(todo);
        };

        //This function is called when the edit is cancelled. The old value is replaced after cancel.
        $scope.cancelEdit = function (todo) {
            toggleEdit(todo);
            //Get the text box from DOM
            var toDoEdit = document.getElementById(todo.Name);
            //Replace the old value in the text box
            toDoEdit.value=todo.Name;

        };

        /*This function is called when the edited value has to be saved. A PUT service call is made to the server and
         the value is updated in the database
         */
        $scope.saveEdit = function (todo) {
            //show the loading indicator when the create operation is called
            $scope.loading = true;
            var toDoEdit = document.getElementById(todo.Name);
            if(toDoEdit) {
                // call the update function from our service
                Todos.update({"Name": toDoEdit.value,"Id": todo.Id})

                    // if successful creation, call our get function to get all the new todos
                    .success(function (data) {
                        todo.isEditable = !todo.isEditable;
                        // assign our new list of todos
                        Todos.get()
                            .success(function (data) {
                                //assign the todos which are obtained from the service response to show in the UI
                                $scope.todos = data;
                                //Hide the loading indicator
                                $scope.loading = false;
                            });
                    });
            }
        };
        /*
         loading is the variable which contained the Boolean value which indicates which the loading indicator
         should be displayed
         */
        $scope.loading = true;
        /* GET -
         when landing on the page, get all todos and show them
         use the service to get all the todos */
        Todos.get()
            .success(function (data) {
                //assign the todos which are obtained from the service response to show in the UI
                $scope.todos = data;
                //Hide the loading indicator
                $scope.loading = false;
            });

                Todos.GetItemCount()
                      .success(function (data) {
                          $scope.itemCount = data;
                      });
        $scope.imageURL = "Images/ProfileIcon.png";
        Todos.GetFileName()
       .success(function (data) {
           if (data == "") {
               $scope.imageURL = "Images/ProfileIcon.png"
           }
           else {
               $scope.imageURL = data;
           }
       });

        /* CREATE -
         when submitting the add form, send the text to the node API */
        $scope.createTodo = function () {

            /* validate the formData to make sure that something is there
             if form is empty, nothing will happen */
            if ($scope.formData.Name !== undefined) {
                //show the loading indicator when the create operation is called
                $scope.loading = true;
                // call the create function from our service
                Todos.create($scope.formData)

                    // if successful creation, call our get function to get all the new todos
                    .success(function (data) {
                        // clear the form so our user is ready to enter another
                        $scope.formData = {};
                        // assign our new list of todos
                        Todos.get()
                            .success(function (data) {
                                //assign the todos which are obtained from the service response to show in the UI
                                $scope.todos = data;
                                //Hide the loading indicator
                                $scope.loading = false;
                            });

                                Todos.GetItemCount()
                      .success(function (data) {
                          $scope.itemCount = data;
                      });
                    });
            }
        };

        /* Upload Profile Pic -
         when submitting the add file, send the file to the API */
        $scope.FileUpload = function (request) {

            if (request.length > 0) {
                // call the create function from our service
                Todos.FileUpload(request[0])
                    // if successful creation, call our get function to get all the new todos
                    .success(function (data) {
                        Todos.GetFileName()
                        .success(function (data) {
                            if (data == "") {
                                $scope.imageURL = "Images/ProfileIcon.png"
                            }
                            else {
                                $scope.imageURL = data;
                            }
                        });
                        
                    }).error(function (data, status, headers, config) {
                        if (status == 415) {
                            alert("You have uploaded an invalid file type, please upload only image file.")
                        }
                        });
            }
        };

        /* Delete Profile Pic -
       */
        $scope.DeleteFile = function () {

            Todos.DeleteFile()
            .success(function (data) {
                $scope.imageURL = "Images/ProfileIcon.png";
                Todos.GetFileName()
               .success(function (data) {
                   if (data == "") {
                       $scope.imageURL = "Images/ProfileIcon.png"
                   }
                   else {
                       $scope.imageURL = data;
                   }
               });
            });
        };
        // DELETE
        // delete a cache after checking it
        $scope.deleteCache = function () {
            //show the loading indicator when the delete operation is called
            $scope.loading = true;
            //Call the delete service with the id of the ToDo as input
            Todos.deleteCache()
                // if successful creation, call our get function to get all the new todos
                .success(function (data) {
                    Todos.GetItemCount()
                       .success(function (data) {
                           $scope.itemCount = data;
                       });
                });
        };

        // DELETE
        // delete a todo after checking it
        $scope.deleteTodo = function (id) {
            //show the loading indicator when the delete operation is called
            $scope.loading = true;
            //Call the delete service with the id of the ToDo as input
            Todos.delete(id)
                // if successful creation, call our get function to get all the new todos
                .success(function (data) {
                    // assign our new list of todos
                    Todos.get()
                        .success(function (data) {
                            //assign the todos which are obtained from the service response to show in the UI
                            $scope.todos = data;
                            //Hide the loading indicator
                            $scope.loading = false;
                        });
                });
        };
    }]);
