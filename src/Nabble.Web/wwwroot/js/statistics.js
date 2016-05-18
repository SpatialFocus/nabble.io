(function refreshBadgeStatistics() {
	$.ajax({
		url: "/api/v1/statistics",
		type: "GET",
		success: function (data) {
			if (data && data.Projects !== undefined) {
				$("#stat-projects").html(data.Projects);
			}
			if (data && data.Builds !== undefined) {
				$("#stat-builds").html(data.Builds);
			}
			if (data && data.Requests !== undefined) {
				$("#stat-requests").html(data.Requests);
			}
		},
		dataType: "json",
		complete: setTimeout(function () { refreshBadgeStatistics() }, 3000),
		timeout: 1000
	});
})();
