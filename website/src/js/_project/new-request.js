let kindergardens = [];
// let displayedKindergardens = [];

const fetchKindergardens = () => {
	return $.get('/api/kindergarden/');
};

// const filterSelectedKindergardens = () => {
// 	const selectedKindergardens = [
// 		document.getElementById('fromKindergardenSelect').value,
// 		document.getElementById('toKindergarden1Select').value,
// 		document.getElementById('toKindergarden2Select').value,
// 		document.getElementById('toKindergarden3Select').value
// 	];

// 	displayedKindergardens = kindergardens.filter(k => !selectedKindergardens.includes(k.id));
// 	console.log(kindergardens.length !== displayedKindergardens.length);
// 	initializeKindergardensInDropdowns();
// };

// const initializeOnDropdownChange = () => {
// 	$(document).on('change', '#fromKindergardenSelect', filterSelectedKindergardens);
// 	$(document).on('change', '#toKindergarden1Select', filterSelectedKindergardens);
// 	$(document).on('change', '#toKindergarden2Select', filterSelectedKindergardens);
// 	$(document).on('change', '#toKindergarden3Select', filterSelectedKindergardens);
// };

const initializeKindergardensInDropdowns = () => {
	const options = kindergardens.map(kindergarden =>
		`<option value='${kindergarden.id}'>${kindergarden.name}</option>`
	);
	$('#newRequestFromKindergardenSelect').append(options);
	$('#newRequestToKindergarden1Select').append(options);
	$('#newRequestToKindergarden2Select').append(options);
	$('#newRequestToKindergarden3Select').append(options);
};


const initializeNewRequestSubmit = () => {
	$('#submit-new-request').on('click', (e) => {
		e.preventDefault();
		const email = document.getElementById('newRequestEmail').value;
		const parentName = document.getElementById('newRequestParentName').value;
		const phoneNumber = document.getElementById('newRequestPhoneNumber').value;
		const childName = document.getElementById('newRequestChildName').value;
		const childBirthDate = document.getElementById('newRequestChildBirthDate').value;
		const fromKindergardenId = document.getElementById('newRequestFromKindergardenSelect').value;
		const toKindergardenId1 = document.getElementById('newRequestToKindergarden1Select').value;
		const toKindergardenId2 = document.getElementById('newRequestToKindergarden2Select').value;
		const toKindergardenId3 = document.getElementById('newRequestToKindergarden3Select').value;

		$.ajax('/api/request/', {
			data: JSON.stringify({
				email,
				parentName,
				phoneNumber,
				childName,
				childBirthDate,
				fromKindergardenId,
				toKindergardenIds: [
					toKindergardenId1 === '' ? undefined : toKindergardenId1,
					toKindergardenId2 === '' ? undefined : toKindergardenId2,
					toKindergardenId3 === '' ? undefined : toKindergardenId3
				].filter(a => a)
			}),
			contentType: 'application/json',
			type: 'POST'
		});
	});
};


const newRequest = {
	init: function() {
		fetchKindergardens().done(data => {
			kindergardens = data,
			// filterSelectedKindergardens();
			initializeKindergardensInDropdowns();
			// initializeOnDropdownChange();
		});
		initializeNewRequestSubmit();
	}
};

export default newRequest;
