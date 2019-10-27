const contact = {

	test: function() {
		$('.js-submit').on('click', (e) => {
			e.preventDefault();
			const email = document.getElementById('email').value;
			const message = document.getElementById('message').value;
			$.post('/api/contact/', {email, message});
		});
	},

	init: function() {
		this.test();
	}
};

export default contact;
