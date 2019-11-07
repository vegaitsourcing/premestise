const contact = {

	initializeContactSubmit: function() {
		$('.js-submit').on('click', (e) => {
			e.preventDefault();
			console.log('wow');
			const email = document.getElementById('email').value;
			const message = document.getElementById('message').value;
			$.post('/api/contact/', {email, message});
		});
	},

	init: function() {
		this.initializeContactSubmit();
	}
};

export default contact;
