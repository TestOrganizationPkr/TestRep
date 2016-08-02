var io = require('socket.io-client');
var mocha = require('mocha');
var socketURL = 'http://0.0.0.0:3000';
var chai = require('chai');
var should = chai.should();

var chatUser1 = 'Tom';

describe("Chat Server",function(){

    it('Should broadcast messages and new user to all users', function(done){
        var client1 = io.connect(socketURL);
        var client2 = io.connect(socketURL);


        client1.on('connect', function(data){
            client1.emit('message', chatUser1,'Hello world!');

        });

        client2.on('broadcast', function(data){

            data.should.have.property('payload');
            data.should.have.property('source','Tom')
            client2.disconnect();
            done();
        });


    });
});