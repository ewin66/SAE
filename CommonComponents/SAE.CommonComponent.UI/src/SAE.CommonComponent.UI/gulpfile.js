const gulp = require('gulp');
const babel = require('gulp-babel');
const jxsPath = "wwwroot/js/**/*.js";
const storagePath = "wwwroot/storage/**/*.js";

gulp.task('default', function () {

    gulp.src(storagePath)
        .pipe(babel())
        .pipe(gulp.dest('wwwroot/dist'))

    return gulp.src(jxsPath)
               .pipe(babel())
               .pipe(gulp.dest('wwwroot/dist/js'))
});