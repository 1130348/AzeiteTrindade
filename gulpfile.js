var gulp = require('gulp'),
    sass = require('gulp-ruby-sass'),
    autoprefixer = require('gulp-autoprefixer'),
    minifycss = require('gulp-minify-css'),
    rename = require('gulp-rename'),
    imagemin = require('gulp-imagemin'),
    concat = require('gulp-concat'),
    uglify = require('gulp-uglify'),
    sourcemaps = require('gulp-sourcemaps'),
    pngquant = require('imagemin-pngquant');

var paths = {
  scripts: 'frontend-dev/js/**/*.js',
  sass:    'frontend-dev/sass/**/*.scss',
  images:  'frontend-dev/img/**/*'
};


//DEV TASKS
//Compile Sass
gulp.task('cssLDF', function() {
  return gulp.src('frontend-dev/sass/website/cssLDF.scss')
    .pipe(sourcemaps.init())
    .pipe(sass())
    .on('error', function (err) { console.log(err.message); })
    .pipe(sourcemaps.write())
    .pipe(rename({suffix: '.min'}))
    .pipe(gulp.dest('LusiadasPIM/LusiadasPIM/Content/css'));
});

//Concat Scripts w/ Sourcemaps
gulp.task('jsLDF', function() {
  return gulp.src(paths.scripts)
    .pipe(sourcemaps.init())
      .pipe(uglify())
      .pipe(concat('main.min.js'))
    .pipe(sourcemaps.write())
    .pipe(gulp.dest('LusiadasPIM/LusiadasPIM/Scripts'));
});




//Watch SASS Changes & Compile them
gulp.task('watch', function() {
  gulp.watch(paths.sass, ['cssLDF']);
  gulp.watch(paths.sass, ['jsLDF']);
});


//Development task
gulp.task('devLDF', ['cssLDF', 'jsLDF', 'watch']);