const openMenu = {

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

	scrollTo: function() {
		$('.js-scroll-to').on('click', (e)=> {
			const $this = $(e.currentTarget);
			const clickedElement = $this.data('section-name-link');
			const clickedSection = $('section[data-section-name="' + clickedElement + '"]');

			$('html,body').animate({
				scrollTop: clickedSection.offset().top
			}, 1500);
		});
	},

	mobileScroll: function() {
		$('.js-scroll-to-mobile').on('click', (e)=> {
			const $this = $(e.currentTarget);
			const clickedElement = $this.data('section-mobile');
			const clickedSection = $('section[data-section-name="' + clickedElement + '"]');

			this.mobileNav.removeClass(this.openClass);
			this.menuBtn.removeClass(this.openedMenuClass);
			this.body.removeClass(this.openClass);

			setTimeout(() => {
				$('html,body').animate({
					scrollTop: clickedSection.offset().top
				}, 1500);
			}, 500);
		});
	},

	init: function() {
		this.toggleMenu();
		this.scrollTo();
		this.mobileScroll();
	}
};

export default openMenu;
