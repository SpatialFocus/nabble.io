var initialData = {
	vendorValues: ["AppVeyor"],
	analyzerValues: ["StyleCop", "FxCop", "Custom"],
	styleValues: ["Default", "Plastic", "Flat", "FlatSquare", "Social"],
	formatValues: ["svg", "png", "jpg", "gif", "json"]
};

var BadgeModel = function (badge) {
	var self = this;

	self.vendorValues = ko.observable(badge.vendorValues);
	self.account = ko.observable();
	self.project = ko.observable();
	self.branch = ko.observable();
	self.analyzerValues = ko.observable(badge.analyzerValues);
	self.analyzer = ko.observable(badge.analyzerValues[0]);
	self.styleValues = ko.observable(badge.styleValues);
	self.style = ko.observable(badge.styleValues[0]);
	self.formatValues = ko.observable(badge.formatValues);
	self.format = ko.observable(badge.formatValues[0]);
	self.aggregate = ko.observable("false");
	self.countInfos = ko.observable("false");

	self.label = ko.observable("");
	self.enableLabel = ko.computed(function () {
		return self.analyzer() === "Custom";
	});

	self.url = ko.computed(function () {
		if (self.account() === undefined || self.project() === undefined) {
			return "";
		}

		var url = "https://nabble.io/api/v1/";

		url += self.vendorValues() +
			"/" + self.account() +
			"/" + self.project();

		if (self.branch() !== undefined) {
			url += "/" + self.branch();
		}
		url += "/" + self.analyzer();

		var query = Array();

		if (self.style() !== badge.styleValues[0]) {
			query.push("style=" + self.style());
		}

		if (self.format() !== badge.formatValues[0]) {
			query.push("format=" + self.format());
		}

		if (self.aggregate() === "true") {
			query.push("aggregateValues=true");
		}

		if (self.countInfos() === "true") {
			query.push("countInfos=true");
		}

		if (self.enableLabel() && self.label().trim() !== "") {
			query.push("label=" + self.label().trim());
		}

		if (query.length > 0) {
			url += "?" + query.join("&");
		}

		return url;
	}, this);
};

ko.applyBindings(new BadgeModel(initialData));