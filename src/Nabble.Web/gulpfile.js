/// <binding BeforeBuild='less, min' Clean='clean' />
"use strict";

var gulp = require("gulp"),
	rimraf = require("rimraf"),
	concat = require("gulp-concat"),
	cssmin = require("gulp-cssmin"),
	less = require("gulp-less"),
	path = require("path"),
	uglify = require("gulp-uglify");

var paths = {
	webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.less = paths.webroot + "css/**/*.less";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

gulp.task("clean:js", function(cb) {
	rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function(cb) {
	rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("less", function() {
	return gulp.src(paths.less)
		.pipe(less({
			paths: [path.join(paths.less, "less", "includes")]
		}))
		.pipe(gulp.dest(paths.webroot + "css/"));
});

gulp.task("min:js", function() {
	return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
		.pipe(concat(paths.concatJsDest))
		.pipe(uglify())
		.pipe(gulp.dest("."));
});

gulp.task("min:css", function() {
	return gulp.src([paths.css, "!" + paths.minCss])
		.pipe(concat(paths.concatCssDest))
		.pipe(cssmin())
		.pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);