/* smooth scrolling sections */
$("a[href='#service']").click(function() {
	if (location.pathname.replace(/^\//, "") == this.pathname.replace(/^\//, "") && location.hostname == this.hostname) {
		var target = $(this.hash);
		target = target.length ? target : $("[name=" + this.hash.slice(1) + "]");
		if (target.length) {
			$("html, body").animate({
				scrollTop: target.offset().top - 50
			}, 800);

			if (this.hash == "#section1") {
				$(".scroll-up").hide();
			} else {
				$(".scroll-up").show();
			}


			// activte animations in this section
			target.find(".animate").delay(1200).addClass("animated");
			setTimeout(function() {
				target.find(".animated").removeClass("animated");
			}, 2000);

			return false;
		}
	}
});