var chai = require('chai');
var chaiHttp = require('chai-http');
var server = require('../../app.js');
var should = chai.should();
chai.use(chaiHttp);


/**
 * Test Suites
 */
describe('<Unit Test>', function () {
    // Start the server before the test case with delay of 1second to instantiate the routers
    before(function (done) {
        this.request = chai.request(server);
    });
    describe('Test Case Title', function () {
        it('Test case desciption', function (done) {
            /*
             Test the requests to the server by using 'this.request.get', 'this.request.post', 'this.request.put',
             'this.request.delete'
             eg.
             this.request.get('/path/parameters')
             .end(function (error, response) {
             Check the properties of response and error using response.should.have
             done();
             });

             */

        });
    });

});

