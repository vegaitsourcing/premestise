const gulp = require('gulp');
const fs = require('fs');
const path = require('path');
const svgo = require('svgo');
const iconfont = require('gulp-iconfont');
const iconfontCss = require('gulp-iconfont-css');
const globalVars = require('./_global-vars');
const gulpif = require('gulp-if');

const inputPath = __dirname;

/*----------------------------------------------------------------------------------------------
	SVGs / IconFont
----------------------------------------------------------------------------------------------*/
// SVG optimization
const svgPath = 'src/assets/svg';
const svgomg = new svgo({
	plugins: [
		{cleanupAttrs: true},
		{removeDoctype: true},
		{removeXMLProcInst: true},
		{removeComments: true},
		{removeMetadata: true},
		{removeTitle: true},
		{removeDesc: true},
		{removeUselessDefs: true},
		{removeEditorsNSData: true},
		{removeEmptyAttrs: true},
		{removeHiddenElems: true},
		{removeEmptyText: true},
		{removeEmptyContainers: true},
		{removeViewBox: false},
		{cleanupEnableBackground: true},
		{convertStyleToAttrs: true},
		{convertColors: true},
		{convertPathData: {
			noSpaceAfterFlags: false
		}},
		{convertTransform: true},
		{removeUnknownsAndDefaults: true},
		{removeNonInheritableGroupAttrs: true},
		{removeUselessStrokeAndFill: true},
		{removeUnusedNS: true},
		{cleanupIDs: true},
		{cleanupNumericValues: true},
		{moveElemsAttrsToGroup: true},
		{moveGroupAttrsToElems: true},
		{collapseGroups: true},
		{removeRasterImages: false},
		{mergePaths: true},
		{convertShapeToPath: true},
		{sortAttrs: true},
		{removeDimensions: true},
		{removeAttrs: {attrs: '(stroke|fill)'}},
		{
			addAttributesToSVGElement: {
				attributes: ['fill="currentColor"']
			}
		}
	]
});
const optimizeSVGs = [];

const renameSvgFiles = (file, filePath, allFiles) => {
	return new Promise((resolve) => {
		globalVars.rf(filePath, () => {
			let newFile;

			if (file.substring(0, 4) !== 'ico-') {
				newFile = file.toLowerCase();
				newFile = newFile.toLowerCase();
				newFile = newFile.replace('.svg', '').replace(/-/g, ' ').replace(/[^\w\s]/gi, '').replace(/ /g, '-');
				newFile = `ico-${newFile}.svg`;

				if (allFiles.includes(newFile)) {
					// delete file if already exists
					try {
						fs.unlinkSync(filePath);
						globalVars.logMSG(globalVars.warningTemp, `deleted '${filePath}' as file with same name already exists`);
					} catch (err) {
						console.log(err);
					}
				} else {
					// rename file
					fs.renameSync(filePath, path.join(svgPath, newFile));
				}
			} else {
				newFile = file;
			}

			resolve(newFile);
		});
	});
};

const optimizeSvgFiles = (filePath) => {
	return new Promise((resolve) => {
		globalVars.rf(filePath, (data) => {
			resolve(svgomg.optimize(data, {path: filePath}));
		});
	});
};

const saveSvgFiles = (filepath, data) => {
	return new Promise((resolve) => {
		fs.writeFile(filepath, data, err => {
			if (err) throw err;
			resolve(filepath);
		});
	});
};

gulp.task('optimize-svgs', (done) => {
	fs.readdirSync(svgPath).forEach((file, i, allFiles) => {
		const filePath = path.join(svgPath, file);

		// remove non svg files from folder
		if (path.extname(file) !== '.svg') {
			try {
				fs.unlinkSync(filePath);
				globalVars.logMSG(globalVars.warningTemp, `deleted '${filePath}' as it is not an SVG file`);
			} catch (err) {
				console.log(err);
			}
		} else {
			// svgo optimization
			const optimizeSVG = optimizeSvgFiles(filePath)
				.then(result => {
					// save optimized file
					return saveSvgFiles(filePath, result.data);
				})
				.then(() => {
					// clean file name and prefix it
					return renameSvgFiles(file, filePath, allFiles);
				});

			// store all optimization Promise functions
			optimizeSVGs.push(optimizeSVG);
		}
	});

	// build font icons when all optimization functions are done
	Promise.all(optimizeSVGs)
		.then(() => {
			done();
		})
		.catch(error => {
			console.log(error);
		});
});

// generate font icons from SVGs
gulp.task('iconfont-build', (done) => {
	gulp.src([`${svgPath}/*.svg`])
		.pipe(iconfontCss({
			fontName: 'svgicons',
			cssClass: 'font',
			path: 'src/config/icon-font.scss',
			cacheBuster: globalVars.productionBuild ? '' : `?v=${new Date().getTime()}`,
			targetPath: inputPath.replace('src\\config\\gulp-tasks', 'src\\scss') + '/layout/_icon-font.scss',
			fontPath: '../assets/fonts/'
		}))
		.pipe(iconfont({
			fontName: 'svgicons', // required
			prependUnicode: false, // recommended option
			formats: ['woff2', 'woff', 'ttf'], // default, 'woff2' and 'svg' are available
			normalize: true,
			centerHorizontally: true
		}))
		.on('glyphs', (glyphs, options) => {
			// CSS templating, e.g.
			let glyphsReport = '\nIcons Report:\n';

			glyphs.forEach(cur => {
				glyphsReport += `\n  Icon: ${cur.name}\n  - unicode: ${cur.unicode}\n  - color: ${cur.color}\n`;
			});

			glyphsReport += '\n\n Options Report:\n';

			for (const prop in options) {
				if (options.hasOwnProperty(prop)
				&& prop !== 'callback'
				&& prop !== 'log'
				&& prop !== 'error') {
					glyphsReport += `\n  - ${prop}: ${options[prop]}`;
				}
			}

			glyphsReport += '\n';

			globalVars.logMSG(globalVars.warningTemp, glyphsReport, 'green');
		})
		.pipe(gulpif(!!globalVars.webFolder, gulp.dest(`${globalVars.webFolder}/assets/fonts`)))
		.pipe(gulp.dest('dist/assets/fonts/'));

	done();
});

gulp.task('iconfont', gulp.series('optimize-svgs', 'iconfont-build'));
