/* This class notifies the server when user send the message and user joined.
   Updates the UI whenever message received from server.
 */
'use strict';

angular.module('chatApp')
.controller('SocketCtrl', function ($log, $scope, chatSocket, messageFormatter, nickName) {
  $scope.nickName = nickName;
  $scope.messageLog = 'Ready to chat!';

    $scope.showLogin = true;
    $scope.showChat = false;
    $scope.isHidden = true;

    // This method sends message to the server.
  $scope.sendMessage = function() {

    nickName = $scope.nickName;

    $log.debug('sending message', $scope.message);
    chatSocket.emit('message', nickName, $scope.message);
    $scope.message = '';
  };
    // This method notifies the server when a new user is joined.
    $scope.addUser = function() {
      $scope.showLogin = false;
      $scope.showChat = true;
      $scope.nickName = $scope.username;
      chatSocket.emit('message', $scope.nickName, 'joined');
    };

  // This method listens for messages from server and updates the UI.
  $scope.$on('socket:broadcast', function(event, data) {
    $log.debug('got a message', event.name);
    if (!data.payload) {
      $log.error('invalid message', 'event', event, 'data', JSON.stringify(data));
      return;
    }
    $scope.$apply(function() {
      $scope.messageLog = $scope.messageLog + messageFormatter(new Date(), data.source, data.payload);
    });
  });

});
