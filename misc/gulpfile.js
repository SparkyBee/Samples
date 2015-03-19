var gulp = require('gulp'),
  rimraf = require('gulp-rimraf'),
  concat = require('gulp-concat'),
  uglify = require('gulp-uglify'),
  minifycss = require('gulp-minify-css'),
  minifyhtml = require('gulp-minify-html');
//  sass = require('gulp-sass'),
//  sftp = require('gulp-sftp');


gulp.task('default', ['clean'], function() {
  return gulp.start('build');
});

//gulp.task('watch', function() {
//  gulp.watch(['src/sass**', 'src/angular/**'], ['build']);
//});

gulp.task('clean', function() {
  return gulp.src([
      'deploy/public/css',
      'deploy/public/css/vnd',
      'deploy/public/js',
      'deploy/public/js/vnd',
      'deploy/public/docs',
      'deploy/public/img',
      'deploy/view',
      'deploy/admin'], { read: false })
    .pipe(rimraf());
});

gulp.task('files_vnd_js',function() {
    return gulp.src('site_root/public/js/vnd/**')
        .pipe(gulp.dest('deploy/public/js/vnd'));
});

gulp.task('files_vnd_css',function() {
    return gulp.src('site_root/public/css/vnd/**')
        .pipe(gulp.dest('deploy/public/css/vnd'));
});

gulp.task('files_img',function() {
    return gulp.src('site_root/public/img/**')
        .pipe(gulp.dest('deploy/public/img'));
});

gulp.task('files_docs',function() {
    return gulp.src('site_root/public/docs/**')
        .pipe(gulp.dest('deploy/public/docs'));
});



gulp.task('css', function() {
  return gulp.src('site_root/public/css/*')
    .pipe(minifycss())
    .pipe(gulp.dest('deploy/public/css'));
});

gulp.task('js', function() {
  return gulp.src([ // pass individual files in array instead of glob to ensure proper order in bundle
    'site_root/public/js/app.js','site_root/public/js/appAdmin.js'])
    .pipe(uglify())
    //.pipe(concat('main.js'))
    .pipe(gulp.dest('deploy/public/js'));
});

gulp.task('templates', function() {
  return gulp.src('site_root/view/**')
    .pipe(minifyhtml({ empty: true, spare: true, conditionals: true }))
    .pipe(gulp.dest('deploy/view'));
});

gulp.task('html', function() {
  return gulp.src('site_root/*.html')
    .pipe(minifyhtml({ empty: true, spare: true, conditionals: true }))
    .pipe(gulp.dest('deploy/'));
});

gulp.task('htmlAdmin', function() {
    return gulp.src('site_root/admin/index.html')
        .pipe(minifyhtml({ empty: true, spare: true, conditionals: true }))
        .pipe(gulp.dest('deploy/admin'));
});

gulp.task('build', ['clean'], function() {
  gulp.start('clean','files_vnd_js','files_vnd_css','files_docs','files_img','css', 'js', 'templates', 'html', 'htmlAdmin', 'watch');
});

gulp.task('watch', function() {
  gulp.watch('site_root/**', ['css', 'js', 'templates', 'html', 'htmlAdmin']);

});
