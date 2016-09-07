'use strict';

App.controller('AppController', [
		'$scope',
		'UserService',
		function($scope, UserService, $route, $routeParams, $location) {
			var self = this;
			self.user = {
				id : null,
				title : '',
				artist : '',
				releaseYear : ''
			};
			var InputEdit= this;
			InputEdit.user={
				id:null,
				title:'',
				artist:'',
				releaseyear:''
			};
			self.users = [];

			self.fetchAllUsers = function() {
				var title=document.getElementById("title");
				var artist=document.getElementById("artist");
				var releaseYear=document.getElementById("releaseYear");
				title.style.color= "black";
				artist.style.color= "black";
				releaseYear.style.color= "black";
				UserService.fetchAllUsers().then(function(d) {
					self.users = d;
					$("#title").val('');
					$("#artist").val('');
					$("#releaseYear").val('');
				}, function(errResponse) {
					console.error('Error while fetching Currencies');
				});
			};

			self.fetchAllUsers();

			self.deleteUser = function(id) {
				var title=document.getElementById("title");
				var artist=document.getElementById("artist");
				var releaseYear=document.getElementById("releaseYear");
				title.style.color= "black";
				artist.style.color= "black";
				releaseYear.style.color= "black";
				UserService.deleteUser(id).then(self.fetchAllUsers,
						function(errResponse) {
							console.error('Error while deleting User.');
						});
			};

			self.remove = function(id) {
				self.deleteUser(id);
			};

			self.createUser = function(user) {
				var title=document.getElementById("title");
				var artist=document.getElementById("artist");
				var releaseYear=document.getElementById("releaseYear");
				title.style.color= "black";
				artist.style.color= "black";
				releaseYear.style.color= "black";
				UserService.createUser(user).then(self.fetchAllUsers,
						function(errResponse) {
							console.error('Error while creating User.');
						}, function(d) {
							console.log('createUser inside');
							self.users = d;
						}, function(errResponse) {
							console.error('Error while fetching Currencies');
						});
			};

			self.updateUser = function(user, id,title) {
				console.log("entering update user");
				var edit = document.getElementById("edit"+id);
				var save=document.getElementById("save"+id);
				var savetxt=document.getElementById("savetxt"+id);
				var cancel=document.getElementById("cancel"+id);
				var canceltxt=document.getElementById("canceltxt"+id);
				var inputdiv=document.getElementById("inputdiv"+id);
				console.log(inputdiv);
				var titlelbl=document.getElementById("titlelbl"+id);
				var artistlbl=document.getElementById("artistlbl"+id);
				var releaselbl=document.getElementById("releaselbl"+id);
				var edittxt=document.getElementById("edittxt"+id);
				var remove=document.getElementById("remove"+id);
				var removetxt=document.getElementById("removetxt"+id);
				var title=document.getElementById("title");
				var titlee=document.getElementById("title");
				var artist=document.getElementById("artist");
				var releaseYear=document.getElementById("releaseYear");
				
				
				save.style.display="none";
				savetxt.style.display="none";
				cancel.style.display="none";
				canceltxt.style.display="none";
				titlelbl.style.display="block";
				artistlbl.style.display="block";
				releaselbl.style.display="block";
				inputdiv.style.display="none";
				edittxt.style.display="block";
				remove.style.display="block";
				removetxt.style.display="block";
				console.log("edit"+title);
				edit.style.display="block";
				UserService.updateUser(user, id,title).then(self.fetchAllUsers,
						function(errResponse) {
							console.error('Error while updating User.');
						}, function(d) {
							console.log('createUser inside');
							self.users = d;
						}, function(errResponse) {
							console.error('Error while fetching Currencies');
						});
			};

			self.submit = function() {
				console.log('submit .....');
				console.log(self.user.id);
				if (self.user.id === null) {
					console.log('Saving New User', self.user, self.user.id,
							self.user.title);
					self.createUser(self.user);
				} else {
					console.log("entering update....");
					console.log("title......."+self.user.id);
					self.updateUser(self.user, self.user.id,self.user.title);
					
					console.log("Updated user"+JSON.stringify(self.user));
					console.log('User updated with id ', self.user.id);
				}
				self.reset(self.user.id,self.user.title);
				
			};
			
			self.edit = function(id,title) {
		
				var edit = document.getElementById("edit"+id);
				var save=document.getElementById("save"+id);
				var savetxt=document.getElementById("savetxt"+id);
				var cancel=document.getElementById("cancel"+id);
				var canceltxt=document.getElementById("canceltxt"+id);
				var inputdiv=document.getElementById("inputdiv"+id);
				console.log(inputdiv);
				var titlelbl=document.getElementById("titlelbl"+id);
				var artistlbl=document.getElementById("artistlbl"+id);
				var releaselbl=document.getElementById("releaselbl"+id);
				var edittxt=document.getElementById("edittxt"+id);
				var remove=document.getElementById("remove"+id);
				var removetxt=document.getElementById("removetxt"+id);
				var titlee=document.getElementById("title");
				var artist=document.getElementById("artist");
				var releaseYear=document.getElementById("releaseYear");
				
				edit.style.display = "none";
				titlee.style.color= "transparent";
				artist.style.color= "transparent";
				releaseYear.style.color= "transparent";
				save.style.display="block";
				savetxt.style.display="block";
				cancel.style.display="block";
				canceltxt.style.display="block";
				titlelbl.style.display="none";
				artistlbl.style.display="none";
				releaselbl.style.display="none";
				inputdiv.style.display="block";
				edittxt.style.display="none";
				remove.style.display="none";
				removetxt.style.display="none";
				console.log("edit"+title);
				
				
				var add = document.getElementById("add"+title);
				
				console.log('id to be edited', id);
				 for (var i = 0; i < self.users.length; i++) {
					//console.log("selfuSERS"+JSON.stringify(self.users))
					if (self.users[i].id === id) {
					   console.log("selfusers"+JSON.stringify(self.users[i]));
					   console.log("onlyid"+id);
						self.user = angular.extend(self.users[i]);
						break;
					}
				} 

			};

			self.reset = function(id,title) {
				console.log("resettitle"+title);
				var edit = document.getElementById("edit"+id);
				var save=document.getElementById("save"+id);
				var savetxt=document.getElementById("savetxt"+id);
				var cancel=document.getElementById("cancel"+id);
				var canceltxt=document.getElementById("canceltxt"+id);
				var inputdiv=document.getElementById("inputdiv"+id);
				console.log(inputdiv);
				var titlelbl=document.getElementById("titlelbl"+id);
				var artistlbl=document.getElementById("artistlbl"+id);
				var releaselbl=document.getElementById("releaselbl"+id);
				var edittxt=document.getElementById("edittxt"+id);
				var remove=document.getElementById("remove"+id);
				var removetxt=document.getElementById("removetxt"+id);
				var titlee=document.getElementById("title");
				var artist=document.getElementById("artist");
				var releaseYear=document.getElementById("releaseYear");
				
				edit.style.display="block";
				save.style.display="none";
				savetxt.style.display="none";
				cancel.style.display="none";
				canceltxt.style.display="none";
				titlelbl.style.display="block";
				artistlbl.style.display="block";
				releaselbl.style.display="block";
				inputdiv.style.display="none";
				edittxt.style.display="block";
				remove.style.display="block";
				removetxt.style.display="block";
				console.log("edit"+title);
				
				self.user = {
					id : null,
					title : '',
					artist : '',
					releaseYear : ''
				};
				//location.reload();
				
				//self.reset(id,title);
				titlee.style.color= "black";
				artist.style.color= "black";
				releaseYear.style.color= "black";
				//location.reload();
				self.fetchAllUsers();
			}

		} ]);
