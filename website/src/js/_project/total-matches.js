const totalMatches = {
	getTotalMatches: function() {
		return $.get('/api/request/matched/count');
	},

	init: function() {
		this.getTotalMatches().done(count => {
			$('.succeess-banner__number').text(count);
		});
	}
};

export default totalMatches;