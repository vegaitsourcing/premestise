const helpers = {
	$win: $(window),
	$html: $('html'),
	$body: $('body'),
	topScroll: 0,
	isScrollDisabled: false,
	disabledScrollClass: 'scroll-disabled',

	init: function() {
		// Check if touch device for hover functionality
		if(('ontouchstart' in window || navigator.msMaxTouchPoints > 0) && window.matchMedia('screen and (max-width: 1024px)').matches) {
			this.$html.addClass('touch');
		} else {
			this.$html.addClass('no-touch');
		}

		// Add loaded class to html, to enable transitions
		this.$win.on('load', () => {
			setTimeout(() => {
				this.$html.addClass('loaded');
			}, 10);
		});
	},

	// Disable window scroll when popups, navigation and similar are opened
	disableScroll: function() {
		if (!this.isScrollDisabled) {
			this.topScroll = this.$win.scrollTop();
			this.$body.css('top', -this.topScroll + 'px').addClass(this.disabledScrollClass);
			this.isScrollDisabled = true;
		}
	},

	// Enable back window scroll when closing the opened overlays
	enableScroll: function() {
		this.$body.removeAttr('style').removeClass(this.disabledScrollClass);
		this.$win.scrollTop(this.topScroll);
		this.isScrollDisabled = false;
	},

	// Equal heights function should be best used on window load, and with debounce function on window resize
	/**     $(window).on('load', () => {
	 *          this.setEqualHeights($('.js-equal-item'), 2);
     *      });
	 *
	 *      # This will wait for a tenth of a second, when the window has finished resizing, and then the function will be called
	 *      $(window).on('resize', this.debounce(() => {
	 *          this.setEqualHeights($('.js-equal-item'), 2);
	 *      }, 100));
	 *
	 *      # This function will be executed 4 times in a second during the event (without throttle, the function is called more than 100 times)
	 *      $(window).on('resize', this.throttle(() => {
	 *          this.setEqualHeights($('.js-equal-item'), 2);
	 *      }, 250));
	 *
	 *      # Recommended events to be throttled or debounced are window scroll, resize, mousemove
	 */
	setEqualHeights: function(arrayItems, count) {
		if(arrayItems !== undefined && arrayItems.length > 0) {
			arrayItems.removeAttr('style');
			if(this.$win.width() > 767) {
				let maxH = 0;

				if(count) {
					const arrays = [];
					while(arrayItems.length > 0) {
						arrays.push(arrayItems.splice(0, count));
					}

					for(let i = 0; i < arrays.length; i += 1) {
						const data = arrays[i];
						maxH = 0;
						for(let j = 0; j < data.length; j += 1) {
							const currentH = $(data[j]).outerHeight();
							if(currentH > maxH) {
								maxH = currentH;
							}
						}

						for(let k = 0; k < data.length; k += 1) {
							$(data[k]).css('height', maxH);
						}
					}
				} else {
					arrayItems.each(function() {
						const currentH2 = $(this).outerHeight();
						if(currentH2 > maxH) {
							maxH = currentH2;
						}
					});

					arrayItems.css('height', maxH);
				}
			}
		}
	},

	throttle: function(func, interval) {
		let timeout;
		return function() {
			const _this = this;
			const args = arguments;
			const later = function() {
				timeout = false;
			};
			if (!timeout) {
				func.apply(_this, args);
				timeout = true;
				setTimeout(later, interval || 250);
			}
		};
	},

	debounce: function(func, interval) {
		let timeout;
		return function() {
			const _this = this;
			const args = arguments;
			const later = function() {
				timeout = null;
				func.apply(_this, args);
			};
			clearTimeout(timeout);
			timeout = setTimeout(later, interval || 100);
		};
	}
};

export default helpers;
