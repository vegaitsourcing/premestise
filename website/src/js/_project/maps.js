const Map = {

    init: function () {
        console.log('aa')
        const map = L.map('mapid').setView([45.23, 19.845], 14);
    
        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoidGVzdGFwcGNmYWMiLCJhIjoiY2sxZXRjazlsMGw0ZzNvdW1mdTh4ZHA0eCJ9.7RLE9_R1Z-SOpPw9WrRJCA', {
            maxZoom: 18,
            id: 'mapbox.streets'
        }).addTo(map);
        
        const pin1 = {
            'name': 'Паличица',
            'longitude': 45.267136,
            'latitude': 19.833549
        }
        
        const pin2 = {
            'name': 'Зубић вила',
            'longitude': 45.27,
            'latitude': 19.845
        }
        
        const list = [pin1, pin2];
        
        list.forEach(pin => {
            L.marker([pin.longitude, pin.latitude]).addTo(map)
            .bindPopup(`<b>${pin.name}</b><br/><button onClick=alert('Кликнуто')>Изабери на мапи</button>`);
        });
        
        const drawLineBeetwenPines = (pin1, pin2) => {
            const longLat = [[pin1.longitude, pin1.latitude], [pin2.longitude, pin2.latitude]];
            L.polyline(longLat).addTo(map);
        }
        
        drawLineBeetwenPines(...list);
    }
}

export default Map;
