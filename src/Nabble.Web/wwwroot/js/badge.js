var availableData = {
	vendorValues: ["AppVeyor"],
	analyzerValues: ["StyleCop", "FxCop", "Custom"],
	styleValues: ["Default", "Plastic", "Flat", "FlatSquare", "Social"],
	formatValues: ["svg", "png", "jpg", "gif"],
	colors: ["Brightgreen", "Green", "Yellow", "Yellowgreen", "Orange", "Red", "Blue", "Gray", "Lightgray"]
};

var defaultData = {
	vendor: "AppVeyor",
	analyzer: "StyleCop",
	style: "Default",
	format: "svg",
	successColor: "Brightgreen",
	warningColor: "Yellow",
	errorColor: "Red",
	infoColor: "Yellowgreen",
	inaccessibleColor: "Lightgray"
};

// The 'trimmed' extension
// Source: http://stackoverflow.com/a/13295290
ko.subscribable.fn.trimmed = function () {
	return ko.computed({
		read: function () {
			return this().trim();
		},
		write: function (value) {
			this(value.trim());
			this.valueHasMutated();
		},
		owner: this
	});
};
// Note that `valueHasMutated` seems to be required for `<input>` elements to reflect changes. At least on Chrome.

// Legacy browser support
if (!String.prototype.trim) {

	// ReSharper disable once NativeTypePrototypeExtending
	String.prototype.trim = function () {
		return this.replace(/^\s+|\s+$/g, "");
	};
}

var BadgeModel = function (badgeData, badgeDefaults) {
	var self = this;

	var baseUrl = "https://nabble.io/api/v1/";

	function queryStringBuilder() {
		var query = Array();

		if (self.style() !== badgeDefaults.style) {
			query.push("style=" + self.style());
		}

		if (self.format() !== badgeDefaults.format) {
			query.push("format=" + self.format());
		}

		if (self.aggregate() === "true") {
			query.push("aggregateValues=true");
		}

		if (self.countInfos() === "true") {
			query.push("countInfos=true");
		}

		if (self.customAnalyzer() && self.label() !== "") {
			query.push("label=" + self.label());
		}

		if (self.customAnalyzer() && self.rules() !== "") {
			query.push("rules=" + self.rules());
		}

		if (self.successColor() !== badgeDefaults.successColor) {
			query.push("colorSuccess=" + self.successColor());
		}
		if (self.successText() !== "") {
			query.push("statusTemplateSuccess=" + self.successText());
		}

		if (self.warningColor() !== badgeDefaults.warningColor) {
			query.push("colorWarning=" + self.warningColor());
		}
		if (self.warningText() !== "") {
			query.push("statusTemplateWarning=" + self.warningText());
		}

		if (self.errorColor() !== badgeDefaults.errorColor) {
			query.push("colorError=" + self.errorColor());
		}
		if (self.errorText() !== "") {
			query.push("statusTemplateError=" + self.errorText());
		}

		if (self.infoColor() !== badgeDefaults.infoColor) {
			query.push("colorInfo=" + self.infoColor());
		}
		if (self.infoText() !== "") {
			query.push("statusTemplateInfo=" + self.infoText());
		}

		if (self.aggregateText() !== "") {
			query.push("statusTemplateAggregate=" + self.aggregateText());
		}

		if (self.inaccessibleColor() !== badgeDefaults.inaccessibleColor) {
			query.push("colorInaccessible=" + self.inaccessibleColor());
		}
		if (self.inaccessibleText() !== "") {
			query.push("statusTemplateInaccessible=" + self.inaccessibleText());
		}

		if (query.length > 0) {
			return "?" + query.join("&");
		}

		return "";
	}

	self.vendorValues = ko.observable(badgeData.vendorValues);
	self.account = ko.observable("").trimmed();
	self.project = ko.observable("").trimmed();
	self.branch = ko.observable("").trimmed();
	self.analyzerValues = ko.observable(badgeData.analyzerValues);
	self.analyzer = ko.observable(badgeDefaults.analyzer);
	self.styleValues = ko.observable(badgeData.styleValues);
	self.style = ko.observable(badgeDefaults.style);
	self.formatValues = ko.observable(badgeData.formatValues);
	self.format = ko.observable(badgeDefaults.format);
	self.aggregate = ko.observable("false");
	self.countInfos = ko.observable("false");

	self.colors = ko.observable(badgeData.colors);
	self.successColor = ko.observable(badgeDefaults.successColor);
	self.successText = ko.observable("").trimmed();
	self.warningColor = ko.observable(badgeDefaults.warningColor);
	self.warningText = ko.observable("").trimmed();
	self.errorColor = ko.observable(badgeDefaults.errorColor);
	self.errorText = ko.observable("").trimmed();
	self.infoColor = ko.observable(badgeDefaults.infoColor);
	self.infoText = ko.observable("").trimmed();
	self.aggregateText = ko.observable("").trimmed();
	self.inaccessibleColor = ko.observable(badgeDefaults.inaccessibleColor);
	self.inaccessibleText = ko.observable("").trimmed();

	self.label = ko.observable("").trimmed();
	self.rules = ko.observable("").trimmed();
	self.customAnalyzer = ko.computed(function () {
		return self.analyzer() === "Custom";
	});

	self.url = ko.computed(function () {
		if (self.account() === "" || self.project() === "") {
			return "Account name and project name is required.";
		}

		var url = baseUrl;

		url += self.vendorValues() +
			"/" + self.account() +
			"/" + self.project();

		if (self.branch() !== "") {
			url += "/" + self.branch();
		}

		url += "/" + self.analyzer();

		url += queryStringBuilder();

		return url;
	}, this);

	self.showPreview = ko.computed(function () {
		return self.url().substring(0, "http".length) === "http";
	});

	self.successUrl = ko.computed(function () {
		var url = baseUrl + "preview/success";
		url += "/" + self.analyzer();
		url += queryStringBuilder();

		return url;
	});
	self.warningUrl = ko.computed(function () {
		var url = baseUrl + "preview/warning";
		url += "/" + self.analyzer();
		url += queryStringBuilder();

		return url;
	});
	self.errorUrl = ko.computed(function () {
		var url = baseUrl + "preview/error";
		url += "/" + self.analyzer();
		url += queryStringBuilder();

		return url;
	});
	self.infoUrl = ko.computed(function () {
		var url = baseUrl + "preview/info";
		url += "/" + self.analyzer();
		url += queryStringBuilder();

		return url;
	});
	self.aggregateUrl = ko.computed(function () {
		var url = baseUrl + "preview/aggregate";
		url += "/" + self.analyzer();
		url += queryStringBuilder();

		return url;
	});
	self.inaccessibleUrl = ko.computed(function () {
		var url = baseUrl + "preview/inaccessible";
		url += "/" + self.analyzer();
		url += queryStringBuilder();

		return url;
	});

	return self;
};

var BadgeContainerModel = function (badgeData, badgeDefaults) {
	var self = this;

	self.badgeModel = ko.observable(BadgeModel(badgeData, badgeDefaults));

	self.reset = function () {
		self.badgeModel(BadgeModel(badgeData, badgeDefaults));
	};

	return self;
};

ko.applyBindings(new BadgeContainerModel(availableData, defaultData));