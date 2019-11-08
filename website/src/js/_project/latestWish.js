import translate from './translate';

const latestWish = {

	onChangeLatestWish: function() {
		$('#changeLatestWishButton').on('click', () => {
			$('#newRequestFromKindergardenSelect').val($('#latestWishFrom').data('id'));
			$('#newRequestToKindergarden1Select').val($('#latestWishTo').data('id'));

			// redirect to first tab ?
			$('ul.tabs li').removeClass('current');
			$('#tab-2').removeClass('current');
			$('[data-tab="tab-1"]').addClass('current');
			$('#tab-1').addClass('current');
		});
	},

	getLatestWish: function() {
		return $.get('/api/request/latest');
	},

	// imena pojedinih vrtica su previse duga da bi ih stavili na ui
	shortenName: function(name, numOfWords = 4) {
		const nameTokens = name.split(' ');
		let shortenedName = '';

		for (let i = 0; i < numOfWords; i++) {
			if (nameTokens[i] !== undefined) {
				shortenedName += `${nameTokens[i]} `;
			}
		}

		return shortenedName;
	},

	init: function() {
		// get latest wish
		this.getLatestWish().done(wish => {
			const latestWishFrom = translate(wish.fromKindergarden.name);
			const latestWishTo = translate(wish.toKindergardens[0].name);

			$('#latestWishFrom').prepend(this.shortenName(latestWishFrom))
				.data('id', wish.fromKindergarden.id);

			$('#latestWishTo').prepend(this.shortenName(latestWishTo))
				.data('id', wish.toKindergardens[0].id);
		});

		// register event listener
		this.onChangeLatestWish();

	}
};

export default latestWish;