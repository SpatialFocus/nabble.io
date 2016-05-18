
var http = require("http");
var url = require("url");
var shields = require("./shields/badge.js");
var svg2img = require("./shields/svg-to-img.js");

var port = process.env.port || 8080;

http.createServer(function(request, response) {
	try {
		handleRequest(request, response);
	}
	catch (exception) {
		console.log(exception);

		response.writeHead(500);
		response.end();
	}
}).listen(port);

function handleRequest(request, response) {

	var data = parseParameter(request.url);

	var format = data.format;
	var contentType = getContentType(format);

	var isImage = ["png", "gif", "jpg"].indexOf(format) !== -1;

	shields(data, function(data) {
		response.writeHead(200, { "Content-Type": contentType });

		if (isImage) {
			svg2img(data, format, response);
		} else {
			response.end(data);
		}
	});
}

function getContentType(format) {
	switch (format) {
		case "json":
			return "application/json";

		case "png":
		case "jpg":
		case "gif":
			return "image/" + format;

		default:
			return "image/svg+xml";
	}
}

var validFormats = ["svg", "png", "gif", "jpg", "json"];
var validTemplates = ["default", "plastic", "flat", "flat-square", "social"];

function parseParameter(requestUrl) {
	var urlParts = url.parse(requestUrl, true);
	var query = urlParts.query;

	var data = {};

	data.text = [];

	data.text[0] = query.label;
	data.text[1] = query.status;

	if (sixHex(query.color)) {
		data.colorB = "#" + query.color;
	} else {
		data.colorscheme = query.color;
	}

	if (query.style && validTemplates.indexOf(query.style) !== -1) {
		data.template = query.style;
	};

	if (query.format && validFormats.indexOf(query.format) !== -1) {
		data.format = query.format;
	};

	if (query.logo !== undefined && !/^data:/.test(query.logo)) {
		data.logo = "data:" + query.logo;
	}

	data.logoWidth = query.logoWidth;
	data.links = query.link;

	return data;
}

function sixHex(string) {
	return /^[0-9a-fA-F]{6}$/.test(string);
}