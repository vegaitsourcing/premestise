const gulp = require('gulp');
const fs = require('fs');
const argv = require('yargs').argv;
const globalVars = require('./_global-vars');
const tap = require('gulp-tap');

/*----------------------------------------------------------------------------------------------
	Create/Read/Update Files
 ----------------------------------------------------------------------------------------------*/
function createFiles(arg, type) {
	const directory = `src/html/${type}s/${arg}`;

	function create(file, lang) {
		let temp = `${type}-hbs-temp.txt`;
		let filename;

		// console.log(arg);

		// detect which file to create
		if (file === 'scss') {
			temp = 'module-scss-temp.txt';
			filename = `_${arg}.scss`;
		} else if (file === 'json') {
			temp = `${type}-json-temp.txt`;
			filename = lang ? lang + '.data.json' : 'data.json';
		} else if (type === 'template') {
			filename = 'template.hbs';
		} else {
			filename = `${arg}.hbs`;
		}

		const readDir = file === 'style' ? 'src/scss/style.scss' : `src/config/cf-templates/${temp}`;
		let writeDir;

		if (file === 'style') {
			writeDir = 'src/scss/style.scss';
		} else if (file === 'scss') {
			writeDir = `src/scss/modules/${filename}`;
		} else {
			writeDir = `${directory}/${filename}`;
		}

		globalVars.rf(readDir, (data) => {
			const output = file === 'style' ? (data + `\n@import 'modules/${arg}';`) : data.replace(new RegExp(`@{${type}}`, 'g'), arg);
			fs.writeFileSync(writeDir, output);
		});
	}

	// create if template or module doesn't exists
	if (!fs.existsSync(directory)) {
		fs.mkdirSync(directory);

		create('hbs');
		if(globalVars.isMultilanguage) {
			globalVars.languages.map(l => {
				create('json', l);
			});
			create('json');
		} else {
			create('json');
		}

		if (type === 'module') {
			create('scss');
			create('style');
		}

		globalVars.logMSG(`src/config/cf-templates/${type}-log-temp.txt`, arg, 'green');
	} else {
		globalVars.logMSG(globalVars.warningTemp, `ERROR: ${type} '${arg}' already exists`);
	}
}

gulp.task('cf', (done) => {
	if (argv.t && typeof argv.t === 'string') {
		// create template HBS and JSON files
		createFiles(argv.t.toLowerCase(), 'template');
	} else if (argv.m && typeof argv.m === 'string') {
		// create module HBS and JSON files
		createFiles(argv.m.toLowerCase(), 'module');
	} else {
		globalVars.logMSG(globalVars.warningTemp, 'ERROR: no parameters were passed');
	}
	done();
});

