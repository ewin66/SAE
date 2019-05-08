const gulp = require('gulp');
const babel = require('gulp-babel');
const jxsPath = "wwwroot/js/**/*.js";

gulp.task('default', function () {
    return gulp.src(jxsPath)
               .pipe(babel())
               .pipe(gulp.dest('wwwroot/dist/js'))
});