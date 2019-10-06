const scrollTo = {

	body: $('body'),
	menuBtn: $('.js-menu-btn'),
	mobileNav: $('.nav__mobile'),
	openedMenuClass: 'mobile-icon--open',
	openClass: 'menu-open',

	toggleMenu: function() {

		this.menuBtn.on('click', (e) =>{
			const $this = $(e.currentTarget);

			this.mobileNav.toggleClass(this.openClass);
			$this.toggleClass(this.openedMenuClass);
			this.body.toggleClass(this.openClass);
		});
	},

	init: function() {
		this.toggleMenu();
	}
};

export default scrollTo;
