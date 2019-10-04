const gulp = require('gulp');

/*----------------------------------------------------------------------------------------------
	Watch
 ----------------------------------------------------------------------------------------------*/
gulp.task('watch', (done) => {
	// watch .scss files
	gulp.watch(['src/scss/**/*.scss', 'src/html/**/**/*.scss'], gulp.series('css', 'sasslint'));

	// watch .js files
	gulp.watch('src/js/**/*.js', gulp.series('js'));

	// watch .hbs and .json files
	gulp.watch(['src/html/**/*.hbs', 'src/html/**/*.json'], gulp.series('hbs'));

	done();
});
