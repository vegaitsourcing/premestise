const request = {
	notifyMe: function() {
		$('.form-buttons__right').click((e)=>{
			e.preventDefault();
			const parentName = document.getElementsByClassName('input parent-input')[0].value;
			const childName = document.getElementsByClassName('input child-input')[0].value;
			const email = document.getElementsByClassName('input input__email')[0].value;
			const phoneNumber = document.getElementsByClassName('input input__phone')[0].value;
			const dateOfBirth = document.getElementsByClassName('input input__date')[0].value;
		});
	},
	init: function() {
		this.notifyMe();
	}
};


export default request;



