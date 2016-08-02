/* This class formats the message, before sending to server. (MessageFormat - 1:51:32 PM - username - hi)*/
'use strict';

angular.module('chatApp')
  .value('messageFormatter', function(date, nick, message) {
    return date.toLocaleTimeString() + ' - ' +
           nick + ' - ' +
           message + '\n';

  });
