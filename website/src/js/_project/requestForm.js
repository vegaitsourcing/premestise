const request = {

	getKindergarden: function() {

		$(document).ready(function(){
			$.get('localhost:50800/api/kindergarden', (data)=> {
			data.forEach(element => {
				$('#firstLocation').append(new Option(element.name, element.id));
				})
			});
		
		})
	},

	notifyMe: function() {
		$('.form-buttons__right').click((e)=>{
			e.preventDefault();
			const parentName = document.getElementsByClassName('input parent-input')[0].value;
			const childName = document.getElementsByClassName('input child-input')[0].value;
			const email = document.getElementsByClassName('input input__email')[0].value;
			const phoneNumber = document.getElementsByClassName('input input__phone')[0].value;
			const childBirthDate = document.getElementsByClassName('input input__date')[0].value;
			const fromKindergardenId = document.getElementById('firstLocation').value;

			const toKindergardenId = document.getElementById('secondLocation').value;

			const pendingRequest = {
				parentName,
				childName,
				email,
				phoneNumber,
				childBirthDate,
				fromKindergardenId,
				toKindergardenIds: [toKindergardenId]
			};

			$.post('/api/request', pendingRequest);
		});
	},
	init: function() {
		this.notifyMe();
	}
};




export default request;



