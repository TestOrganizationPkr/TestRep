/*

A singleton that operates as the central hub for application updates.
*/

import {Dispatcher} from 'flux';
import assign from 'object-assign';

const AppDispatcher = assign(new Dispatcher(), {
  handleViewAction: function(action) {
    this.dispatch({
      source: 'VIEW_ACTION',
      action: action
    });
  },

  handleServerAction: function(action) {
    this.dispatch({
      source: 'SERVER_ACTION',
      action: action
    });
  }
});

export default AppDispatcher;
