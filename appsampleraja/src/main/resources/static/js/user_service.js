'use strict';
 
App.factory('UserService', ['$http', '$q', function($http, $q){
 
    return {
    	
    fetchAllUsers: function() {
			return $http.get('/albums')
    	 //return $http.get('http://newui14.cfapps.io/albums')
            .then(
                    function(response){
                        return response.data;
                    }, 
                    function(errResponse){
                        console.error('Error while fetching users');
                        return $q.reject(errResponse);
                    }
            );
        },
    
   deleteUser: function(id){
        return $http.get('/deletealbum?id='+id)
        .then(
                function(response){
					//console.log(JSON.stringify(response.data));
					console.log('deleted user successfully')
                    return response.data;
					$route.reload();
                }, 
                function(errResponse){
                    console.error('Error while deleting user');
                    return $q.reject(errResponse);
					
                }
        );
    },
      createUser: function(user){
            return $http.post('/addAlbum?title='+user.title+'&artist='+user.artist+'&releaseYear='+user.releaseYear)
            .then(
                    function(response){
                        return response.data;
                    }, 
                    function(errResponse){
                        console.error('Error while creating user');
                        return $q.reject(errResponse);
                    }
            );
        },
      
       updateUser: function(user, id){
			 return $http.post(	'/editAlbum?id='+id+'&title='+user.title+'&artist='+user.artist+'&releaseYear='+user.releaseYear)
            //return $http.post('/editAlbum?id='+id+"&title="+user.title+"&artist="+user.artist+"&releaseYear="+user.releaseYear)
            .then(
                    function(response){
                        return response.data;
						console.log("updated response"+response.data);
                    }, 
                    function(errResponse){
                        console.error('Error while updating user');
                        return $q.reject(errResponse);
                    }
            );
        }
       

         
    };
 
}]);