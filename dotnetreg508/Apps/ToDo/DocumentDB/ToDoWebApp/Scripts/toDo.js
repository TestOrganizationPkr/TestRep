"use strict";
// Method to Load all tasklist by making call to WEB API GET method
function loadToDoList() {
    $.ajax({
        type: "GET",
        url: "/todo/getall",
        async: false
    }).done(function (result) {
        ko.applyBindings(new ToDoViewModel(result.data));
    }).fail(function (err) {
        alert(err.status);
    });
}
var FileName = ko.observable("Images/ProfileIcon.png");
function GetFileName() {
    $('#profile').attr("src", "Images/ProfileIcon.png");
        $.ajax({
            type: "get",
            url: "/blob/GetFileName",
            async: false
        }).done(function (data) {
            if (data == "") {
                $('#profile').attr("src", "Images/ProfileIcon.png");
            }
            else {
                FileName = data;
                $('#profile').attr("src", FileName);
            }
        }).fail(function (err) {
            alert("Error Occures, Please Reload the Page and Try Again " + err.status);
        });
    };


//construct an task

var Task = function (Id,Name) {
    this.Id = Id;
    this.Name = Name;
};

var itemCount = ko.observable("0");

// Method to Delete the Record
function getItemCount () {
    $.ajax({
        type: "GET",
        url: "/cache/GetItemCount",
        async: false
    }).done(function (result) {
        itemCount(result);
    }).fail(function (err) {
        alert(err.status);
    });
};

// The ViewModel where the Templates are initialized
var ToDoViewModel = function (tasklist) {
    var self = this;
    this.taskToAdd = ko.observable("");
    //Hide the loading indicator
    this.isLoading = ko.observable(false);
    this.tasklist = ko.observableArray(tasklist);
    this.selectedTask = ko.observable();
    // Method to insert new task
    this.addTask = function () {
        var newItem = "";
        if ($.trim(self.taskToAdd()) !== "") {
            // Adds the todo. Writing to the "tasklist" observableArray causes any associated UI to update
            var postData = {'Name': self.taskToAdd()};
            //Ajax request to create a new task
            $.ajax({
                url: '/todo/create',
                type: 'POST',
                data: postData,
                async: false
            }).done(function (result) {
                newItem = new Task(result.data.Id, result.data.Name);
                self.tasklist.push(newItem);
                getItemCount();
            }).fail(function (err) {
                alert("Error Occurs, Please Reload the Page and Try Again " + err.status);
            });
            // Clears the text box, because it's bound to the "taskToAdd" observable
            self.taskToAdd("");
        }       
    };
    // Method to Delete the Record
    this.deleteTask = function (taskToDelete) {
        $.ajax({
            type: "post",
            url: "/todo/delete/" + taskToDelete.Id,
            async: false
        }).done(function (data) {
        }).fail(function (err) {
            alert("Error Occures, Please Reload the Page and Try Again " + err.status);
        });
        self.tasklist.remove(taskToDelete);
        self.selectedTask(null);
    };

    // Method to Delete the Record
this.deleteCache = function (taskToDelete) {
    $.ajax({
        type: "post",
        url: "/cache/DeleteCache/",
        async: false
    }).done(function (data) {
        getItemCount();
    }).fail(function (err) {
        alert("Error Occures, Please Reload the Page and Try Again " + err.status);
    });
    self.tasklist.remove(taskToDelete);
    self.selectedTask(null);
};
    // Method to Upload File in Blob 
    this.FileUpload = function (request) {
        var formData = new FormData();
        if (request.length > 0 && request[0].files != "undefined" && request[0].files.length > 0) {

            formData.append("file", request[0].files[0])

            $.ajax({
                type: "post",
                url: "/blob/FileUpload",
                data: formData,
                cache: false,
                contentType: false,
                processData: false,
                async: false
            }).done(function (data) {
                if (data == "error") {
                    alert("You have uploaded an invalid file type, please upload only image file.")
                }
                else {
                    GetFileName();
                }
            }).fail(function (err) {
                alert("Error Occures, Please Reload the Page and Try Again " + err.status);
            });
        }
    };

    // Method to Delete file from Blob
    this.DeleteFile = function () {
        $.ajax({
            type: "post",
            url: "/blob/DeleteFile",
            async: false
        }).done(function (data) {
            GetFileName();
        }).fail(function (err) {
            alert("Error Occures, Please Reload the Page and Try Again " + err.status);
        });
    };
   

    // Toggle edit for Save or Cancel
    this.editTask = function (todo) {
        self.selectedTask(todo);
    };
    // Method to Update the Record
    this.acceptTaskEdit = function () {
        //Edit the Record
        $.ajax({
            type: "post",
            url: "/todo/update/" ,
            async: false,
            data: {"Id":self.selectedTask().Id , "Name":self.selectedTask().Name}
        }).done(function (data) {
        }).fail(function (err) {
            alert("Error Occured, Please Reload the Page and Try Again " + err.status);
        });
        self.selectedTask(null);
    };
    //Function to cancel edit effect
     this.cancelTaskEdit = function () {
        self.selectedTask(null);
        $.ajax({
            type: "GET",
            url: "/todo/getall",
            async: false
        }).done(function (result) {
            self.tasklist = ko.observableArray(result.data);
            ko.cleanNode(document.getElementById("ToDoRowKnockout"));
            ko.applyBindings(self, document.getElementById("ToDoRowKnockout"));
        }).fail(function (err) {
            alert(err.status);
        });
    };
    // Method to decide the Current Template (readonlyTemplate or editTemplate)
    this.templateToUse = function (todo) {
        return self.selectedTask() === todo ? "editMode" : "readOnly";
    };
};
// On initial page load to get the tasklist
loadToDoList();
getItemCount();
GetFileName();
