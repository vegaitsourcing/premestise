const contact = {

	test: function() {
		$('.js-submit').on('click', (e) => {
			e.preventDefault();
			const email = document.getElementById('email').value;
			const emailText = document.getElementById('emailText').value;
			const receivedEmail = {email, emailText};
			$.post('/api/contact/', receivedEmail);
		});
	},

	init: function() {
		this.test();
	}
};

export default contact;
