const tabs = {
	test: function() {
		$('ul.tabs li').click(function() {
			const tabid = $(this).attr('data-tab');

			$('ul.tabs li').removeClass('current');
			$('.tab-content').removeClass('current');

			$(this).addClass('current');
			$('#' + tabid).addClass('current');
		});
	},

	init: function() {
		this.test();
	}
};

export default tabs;
