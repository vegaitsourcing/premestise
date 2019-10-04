const gulp = require('gulp');
const gulpif = require('gulp-if');

const globalVars = require('./_global-vars');

/*----------------------------------------------------------------------------------------------
	Assets Files
 ----------------------------------------------------------------------------------------------*/
gulp.task('assets', () => {
	return gulp.src(['src/assets/**/*'])
		.pipe(gulpif(!!globalVars.webFolder, gulp.dest(`${globalVars.webFolder}/assets`)))
		.pipe(gulp.dest('dist/assets'));
});
