const gulp = require('gulp');
const connect = require('gulp-connect');

gulp.task('server', () => {
	connect.server({
		root: 'dist',
		livereload: true
	});
});