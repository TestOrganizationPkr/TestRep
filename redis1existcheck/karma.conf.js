module.exports = function(config){

    config.set({
        frameworks: ["jasmine"],


        plugins : ['karma-jasmine', 'karma-phantomjs-launcher', 'karma-coverage','karma-spec-reporter'],
        browsers: ["PhantomJS"],

        files: [
            'http://ajax.aspnetcdn.com/ajax/knockout/knockout-3.1.0.js',
            'https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js',
            'https://cdnjs.cloudflare.com/ajax/libs/jquery-mockjax/1.6.2/jquery.mockjax.js',
            './knockout_client/controllers/toDo.js',
            './knockout_client/tests/test.js'
        ],

        port: 3000,
        singleRun: true,
        reporters: ["spec", 'coverage'],
        preprocessors: { './knockout_client/controllers/toDo.js': ['coverage'] },
        coverageReporter: {
            type : 'html',
            dir : 'client/tests/results/coverage'
        }

    });

};



