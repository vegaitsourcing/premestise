const isBeta = {
	$betaButton: $('.js-is-beta-button'),
	$betaContent: $('.js-is-beta-content'),

	init: function() {
		this.$betaButton.on('click', () => {
			this.$betaContent.fadeToggle();
		});
	}
};

export default isBeta;
