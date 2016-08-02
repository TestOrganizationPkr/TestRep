'use strict';

describe('Service: messageFormatter', function () {

  // load the service's module
  beforeEach(angular.mock.module('chatApp'));

  // instantiate service
  var $messageFormatter;
  beforeEach(angular.mock.inject(function (_messageFormatter_) {
    $messageFormatter = _messageFormatter_;
  }));


  describe('messageFormatter', function () {
    it('format message', function () {
      var $scope = {};
      var d = new Date("July 21, 1983 01:15:00");
      var x = $messageFormatter(d,'test','hi');

      expect(x).toContain('1:15:00 AM IST - test - hi');
    });
  });


});
