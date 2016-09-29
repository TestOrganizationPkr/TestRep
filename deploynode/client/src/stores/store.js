/*The store allows components to register/unregister listeners, and emit change events.*/
import AppDispatcher from '../dispatcher/dispatcher';
import AppConstants from '../reducers/reducer';
import {EventEmitter} from 'events';

import assign from 'object-assign';

const CHANGE_EVENT = 'change';


/* The store only needs to allow components to register/unregister listeners,
and emit change events. */
const Store = assign({}, EventEmitter.prototype, {
  emitChange: function () {
    this.emit(CHANGE_EVENT);
  },

  addChangeListener: function(callback) {
    this.on(CHANGE_EVENT, callback);
  },

  removeChangeListener: function(callback) {
    this.removeListener(CHANGE_EVENT, callback);
  }

});

/* Register with the App Dispatcher, and declare how the store handles various
actions. This should be the sole way in which a client side model gets updated */
AppDispatcher.register( (payload) => {
  let action = payload.action;

});

export default Store;
