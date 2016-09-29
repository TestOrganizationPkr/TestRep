/*
 Module to make service calls to server component from UI component.
 The service makes a request to the server, and lets your application handle the response.
 */

import $ from 'jquery';

const BASE_URL = '/api/todos/';

const AppService = {
  create: function(jsonData, success, failure) {
    $.ajax({
      url: BASE_URL,
      type: 'POST',
      dataType: 'json',
      data: jsonData,
      success: function(data) {
        success(data);
      },
      error: function() {
        failure();
      }
    });
  },


};

export default AppService;
