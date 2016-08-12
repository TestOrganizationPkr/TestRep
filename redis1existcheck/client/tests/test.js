
/*
 This file contains the test cases in order to test Knockoutjs UI components
 */


/**
 * Test Suites
 */
describe('Knockoutjs ToDoViewModel Unit Test cases', function(){


    it('Get all tasks from ToDoViewModel', function (done) {

        // Expected results for get all request
        var expectedGetResponse = {"status":"Success","data":[{"_id":1,"task_name":"Task 1"}]};

        // Mock ajax call for Get request
        var getAllRequest = $.mockjax({
            url: 'api/todos',
            type: 'GET',
            async: false,
            dataType:'json',
            contentType: "application/json",
            responseText: expectedGetResponse
        });

        // Get To list
        ToDoViewModel.reset();
        //loadToDoList();
        expect(self.tasklist()).toEqual(expectedGetResponse.data);
        $.mockjax.clear(getAllRequest);
        done();
    });

    it('Add a task to ToDoViewModel', function (done) {

        // Expected results for get all request
        var expectedGetResponse = {"status":"Success","data":[{"_id":1,"task_name":"Task 1"}]};
        // Mock ajax call for Get request
        var getAllRequest = $.mockjax({
            url: 'api/todos',
            type: 'GET',
            async: false,
            dataType:'json',
            contentType: "application/json",
            responseText: expectedGetResponse
        });

        // Expected results for Post request
        var expectedPostResponse = {"status":"Success","data":[{"_id":1,"task_name":"Task 1"}]};
        // Mock ajax call for Post request
        var postRequest =  $.mockjax({
            url: 'api/todos',
            type: 'POST',
            async: false,
            dataType:'json',
            contentType: "application/json",
            responseText: expectedPostResponse
        });

        // Add a new task
        self.taskToAdd = ko.observable("Task 1")
        ToDoViewModel.addTask();

        expect(self.tasklist()).toEqual(expectedPostResponse.data);

        // clear the mockjax request
        $.mockjax.clear(getAllRequest);
        $.mockjax.clear(postRequest);
        done();
    });

    it('Update a task to ToDoViewModel', function (done) {

        // Expected results for get all request
        var expectedGetResponse = {"status":"Success","data":[{"_id":1,"task_name":"New Task 1"}]};
        // Mock ajax call for Get request
        var getAllRequest = $.mockjax({
            url: 'api/todos',
            type: 'GET',
            async: false,
            dataType:'json',
            contentType: "application/json",
            responseText: expectedGetResponse
        });

        // Expected results for Put request
        var expectedPutResponse = {"status":"Success","data":[{"_id":1,"task_name":"New Task 1"}]};
        // Mock ajax call for Put request
        var updateRequest =  $.mockjax({
            url: 'api/todos/1',
            type: 'PUT',
            dataType:'json',
            contentType: "application/json",
            responseText: expectedPutResponse
        });

        // Update a task
        var task = {"_id":1,"task_name":"New Task 1"};
        ToDoViewModel.saveTask(task);
        expect(self.tasklist()).toEqual(expectedPutResponse.data);

        // clear the mockjax request
        $.mockjax.clear(getAllRequest);
        $.mockjax.clear(updateRequest);
        done();
    });

    it('Delete a task from ToDoViewModel', function (done) {

        // Expected results for delete request
        var expectedGetResponse = {"status":"Success","data":[]};
        // Mock ajax call for Get request
        var getAllRequest = $.mockjax({
            url: 'api/todos',
            type: 'GET',
            async: false,
            dataType:'json',
            contentType: "application/json",
            responseText: expectedGetResponse
        });

        // Expected results for delete request
        var expectedDeleteResponse = {"status":"Success","data":[]};
        // Mock ajax call Delete request
        var deleteRequest = $.mockjax({
            url: 'api/todos/1',
            type: 'DELETE',
            dataType:'json',
            contentType: "application/json",
            responseText: expectedDeleteResponse
        });

        // Delete a task
        var task = {"_id":1,"task_name":"Task 1"};
        ToDoViewModel.deleteTask(task);
        expect(self.tasklist()).toEqual(expectedDeleteResponse.data);

        // clear the mockjax request
        $.mockjax.clear(getAllRequest);
        $.mockjax.clear(deleteRequest);
        done();
    });


    it('ToDoViewModel Template Test', function (done) {


        expect(ToDoViewModel.currentTemplate('')).toEqual('readOnly');

        done();
    });

});



