//Check the angular is existy or not
//Check the module myApp is defined or not

describe('angular', function () {
    it('should exist', function () {
        expect(window.angular).toBeDefined();
    });

    describe('module myApp', function () {
        var module;
        beforeEach(function () {
            module = angular.module('myApp');
        });
        it('should exist', function () {
            expect(module).toBeDefined();
        });
        describe('AppController', function () {
            it('should exist', function () {
                expect(module.controller('AppController')).toBeDefined();
            });
        });
    });
});

describe('userService tests', function (){
	  var UserService;
	  
	  // excuted before each "it" is run.
	  beforeEach(function (){
	    
	    // load the module.
	    module('myApp');
	    
	    // inject your service for testing.
	    // The _underscores_ are a convenience thing
	    // so you can have your variable name be the
	    // same as your injected service.
	    inject(function(_UserService_) {
	    	UserService = _UserService_;
	    });
	  });
	     
	  // check to see if it has the expected function
	  it('should have an fetchAllUsers function', function () { 
	    expect(angular.isFunction(UserService.fetchAllUsers)).toBe(true);
	  });
	  
	  it('should have an deleteUser function', function () { 
		    expect(angular.isFunction(UserService.deleteUser)).toBe(true);
		  });
	  it('should have an createUser function', function () { 
		    expect(angular.isFunction(UserService.createUser)).toBe(true);
		  });
	  it('should have an updateUser function', function () { 
		    expect(angular.isFunction(UserService.updateUser)).toBe(true);
		  });
	  
	
	});




describe('Album specs for curd operations', function () {
	  var UserService,
	      httpBackend;
	  
	  beforeEach(function (){  
	    // load the module.
	    module('myApp');
	    
	    // get your service, also get $httpBackend
	    // $httpBackend will be a mock, thanks to angular-mocks.js
	    inject(function($httpBackend, _UserService_) {
	    	UserService = _UserService_;      
	      httpBackend = $httpBackend;
	    });
	  });
	  
	  // make sure no expectations were missed in your tests.
	  // (e.g. expectGET or expectPOST)
	  afterEach(function() {
	    httpBackend.verifyNoOutstandingExpectation();
	    httpBackend.verifyNoOutstandingRequest();
	  });

	  it('get Albums spec.', function (){
	    // set up some data for the http call to return and test later.
	    var returnData = { excited: true };
	    
	    // expectGET to make sure this is called once.
	    httpBackend.expectGET('/albums').respond(returnData);
	    
	    // make the call.
	    var returnedPromise = UserService.fetchAllUsers();
	    
	    // set up a handler for the response, that will put the result
	    // into a variable in this scope for you to test.
	    var result;
	    returnedPromise.then(function(response) {
	      result = response;
	    });
	    
	    // flush the backend to "execute" the request to do the expectedGET assertion.
	    httpBackend.flush();
	    
	    // check the result. 
	    // (after Angular 1.2.5: be sure to use `toEqual` and not `toBe`
	    // as the object will be a copy and not the same instance.)
	    expect(result).toEqual(returnData);
	  });  
	 
	
	  it('delete album spec', function (){
		    // set up some data for the http call to return and test later.
		    var returnData = { excited: true };
		    
		    // expectGET to make sure this is called once.
		    httpBackend.expectGET('/deletealbum?id=1').respond(returnData);
		    
		    // make the call.
		    var returnedPromise = UserService.deleteUser(1);
		    
		    // set up a handler for the response, that will put the result
		    // into a variable in this scope for you to test.
		    var result;
		    returnedPromise.then(function(response) {
		      result = response;
		    });
		    
		    // flush the backend to "execute" the request to do the expectedGET assertion.
		    httpBackend.flush();
		    
		    // check the result. 
		    // (after Angular 1.2.5: be sure to use `toEqual` and not `toBe`
		    // as the object will be a copy and not the same instance.)
		    expect(result).toEqual(returnData);
		  });  
	  
	  
	  it('Edit Album Spec.', function (){
		    // set up some data for the http call to return and test later.
		    var returnData = { excited: true };
		    
		    // expectGET to make sure this is called once.
		    httpBackend.expectPOST('/editAlbum?id=1&title=cn&artist=singer&releaseYear=2016').respond(returnData);
		   var user = {
					"title": "cn",
					"artist": "singer",
					"releaseYear": "2016"
				};
		    
		    // make the call.
		    var returnedPromise = UserService.updateUser(user,1);
		    
		    // set up a handler for the response, that will put the result
		    // into a variable in this scope for you to test.
		    var result;
		    returnedPromise.then(function(response) {
		      result = response;
		    });
		    
		    // flush the backend to "execute" the request to do the expectedGET assertion.
		    httpBackend.flush();
		    
		    // check the result. 
		    // (after Angular 1.2.5: be sure to use `toEqual` and not `toBe`
		    // as the object will be a copy and not the same instance.)
		    expect(result).toEqual(returnData);
		  });  

	  
	  
	  it('Add Album Spec.', function (){
		    // set up some data for the http call to return and test later.
		    var returnData = { excited: true };
		    
		    // expectGET to make sure this is called once.
		    httpBackend.expectPOST('/addAlbum?title=cn&artist=singer&releaseYear=2016').respond(returnData);
		   var user = {
					"title": "cn",
					"artist": "singer",
					"releaseYear": "2016"
				};
		    
		    // make the call.
		    var returnedPromise = UserService.createUser(user);
		    
		    // set up a handler for the response, that will put the result
		    // into a variable in this scope for you to test.
		    var result;
		    returnedPromise.then(function(response) {
		      result = response;
		    });
		    
		    // flush the backend to "execute" the request to do the expectedGET assertion.
		    httpBackend.flush();
		    
		    // check the result. 
		    // (after Angular 1.2.5: be sure to use `toEqual` and not `toBe`
		    // as the object will be a copy and not the same instance.)
		    expect(result).toEqual(returnData);
		  });  

	
	});











