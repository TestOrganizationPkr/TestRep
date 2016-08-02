// Karma configuration
// http://karma-runner.github.io/0.10/config/configuration-file.html

// karma.conf.js
module.exports = function(config) {
  config.set({
    frameworks: ['jasmine'],
    reporters: ['spec'],
    browsers: ['PhantomJS'],
    files: [
      '../client/bower_components/angular/angular.js',
      '../client/bower_components/angular-mocks/angular-mocks.js',
      '../client/bower_components/angular-resource/angular-resource.js',
      '../client/bower_components/angular-cookies/angular-cookies.js',
      '../client/bower_components/angular-sanitize/angular-sanitize.js',
      '../client/bower_components/angular-route/angular-route.js',
      '../client/scripts/*.js',
      '../client/scripts/**/*.js',
      'test/mock/**/*.js',
      'test/spec/**/*.js'
    ],
    port: 8080,
  });
};