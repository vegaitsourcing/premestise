const map = {

	init: function() {
		const L = window.L;
		const map = L.map('mapid').setView([45.23, 19.845], 14);

		L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoidGVzdGFwcGNmYWMiLCJhIjoiY2sxZXRjazlsMGw0ZzNvdW1mdTh4ZHA0eCJ9.7RLE9_R1Z-SOpPw9WrRJCA', {
			maxZoom: 18,
			id: 'mapbox.streets'
		}).addTo(map);

		const pin1 = {
			'name': 'Вртић Златна греда',
			'longitude': 45.249762,
			'latitude': 19.830369
		};

		const pin2 = {
			'name': 'Maштоленд',
			'longitude': 45.247820,
			'latitude': 19.8408017
		};

		const list = [pin1, pin2];
		list.forEach(pin => {
			L.marker([pin.longitude, pin.latitude]).addTo(map).bindPopup(`<b>${pin.name}</b>`);
		});
		const drawLineBeetwenPines = (pin1, pin2) => {
			const longLat = [[pin1.longitude, pin1.latitude], [pin2.longitude, pin2.latitude]];
			L.polyline(longLat).addTo(map);
		};
		drawLineBeetwenPines(...list);

		const pin3 = {
			'name': 'Весели вртић',
			'longitude': 45.2551141,
			'latitude': 19.811248
		};
		const pin4 = {
			'name': 'Maштоленд',
			'longitude': 45.2494525,
			'latitude': 19.8472819
		};
		const list2 = [pin3, pin4];
		list2.forEach(pin => {
			L.marker([pin.longitude, pin.latitude]).addTo(map).bindPopup(`<b>${pin.name}</b>`);
		});
		drawLineBeetwenPines(...list2);
	}
};

export default map;