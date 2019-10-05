const accordions = {

	accordionBtn: $('.js-open-accordion'),
	openedAccordionClass: 'accordion--opened',


	openAccordion: function() {
		this.accordionBtn.on('click', (e) => {
			const $this = $(e.currentTarget);

			$this.toggleClass(this.openedAccordionClass);
			$this.next().slideToggle();
		});
	},

	init: function() {
		this.openAccordion();
	}
};

export default accordions;
