var gulp = require("gulp");
var sass = require("gulp-sass");
var rename = require("gulp-rename");
var path = require('path');

gulp.task('sass', function () {

  return gulp.src('src/*.scss')
    .pipe(sass().on('error', sass.logError))

    .pipe(rename(function (path) {
      if (path.basename == "specialFile") {
        path.dirname = "Special";
      }
    }))

    .pipe(gulp.dest('Public/Css'))

  //   .pipe(gulp.dest(function(file) {
  //     var temp = file.path.split(path.sep);
  //     var baseName = temp[temp.length - 1].split('.')[0];
  //     console.log(baseName);
  //     if (baseName == "specialFile") {
  //       return 'Public/Css/Special';
  //     }
  //     else return 'Public/Css';
  // }))

});

gulp.task('default', ['sass']);