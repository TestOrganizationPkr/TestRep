var gulp = require('gulp');
var Server = require('karma').Server;

gulp.task('test', function (done) {
    return new Server({
        //configFile: 'karma.conf.js',
        configFile: require('path').resolve('karma.conf.js'),
        singleRun: true
    }, done).start();
});