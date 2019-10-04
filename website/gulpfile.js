const gulp = require('gulp');
const fs = require('fs-extra');
const globalVars = require('./src/config/gulp-tasks/_global-vars');
require('require-dir')('./src/config/gulp-tasks');



//get web folder path
function getWebFolder(done) {
	const getDirectories = source => fs.readdirSync(source).filter(name => name.indexOf('.Web') > -1)[0];
	const result = getDirectories('../');
	if(result) globalVars.webFolder = `../${result}`;

	done();
}

// copy favicon
function copyFavicon(done) {
	fs.readdirSync('./src/favicon').forEach(file => {
		if(globalVars.webFolder) {
			fs.copyFileSync(`./src/favicon/${file}`, `${globalVars.webFolder}/${file}`);
		}
		fs.copyFileSync(`./src/favicon/${file}`, `./dist/${file}`);
	});

	done();
}


// build all files for DEVELOPMENT
function prepareDev(done) {
	globalVars.createDistFolder();
	globalVars.stageBuild = false;
	globalVars.productionBuild = false;
	done();
}
gulp.task('build-dev', gulp.series(prepareDev, getWebFolder, 'hbs', 'iconfont', 'sasslint', 'css', 'js', 'assets', copyFavicon));

// build all files for STAGING
function prepareStage(done) {
	globalVars.createDistFolder();
	globalVars.stageBuild = true;
	globalVars.productionBuild = false;
	fs.copyFileSync('robots.txt', 'dist/robots.txt');
	done();
}
gulp.task('build-stage', gulp.series(prepareStage, getWebFolder, 'hbs', 'iconfont', 'css', 'assets', copyFavicon));


// build all files for PRODUCTION
function prepareProd(done) {
	globalVars.createDistFolder();
	globalVars.isBeta = false;
	globalVars.stageBuild = true;
	globalVars.productionBuild = true;
	done();

}
gulp.task('build-prod', gulp.series(prepareProd, getWebFolder, 'hbs', 'iconfont', 'css', 'assets', copyFavicon));


// default task, builds everything and then watch
function prepareDefault(done) {
	globalVars.createDistFolder();
	globalVars.productionBuild = false;
	globalVars.stageBuild = false;
	done();
}
gulp.task('default', gulp.series(prepareDefault, getWebFolder, 'hbs', 'css', 'watch'));


// clear dist folder
gulp.task('reset-dev', (done) => {
	fs.remove('./dist').then(() => {
		console.log('DIST IS REMOVED!');
		done();
	});
});