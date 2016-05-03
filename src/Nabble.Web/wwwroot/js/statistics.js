var badgeStatistics = function() {
	$(document).ready(function() {
		var statAccount = 154;
		var statBadges = 362;
		var statRequests = 7117;

		function increment() {
			statAccount++;
			statBadges += 2;
			statRequests += 3;

			$("#stat-account").html(statAccount);
			$("#stat-badges").html(statBadges);
			$("#stat-requests").html(statRequests);

			setTimeout(function() {
				increment();
			}, 2000);
		}

		increment();
	});
}();