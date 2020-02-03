import React, { Component } from 'react';
import { connect } from 'react-redux';
import { GetAllWishesForMap } from '../../Actions/NavActions/AllWishesActions';
class MapWishView extends Component {

    componentDidUpdate() {
        this.paintMapMarkers();
    }

    paintMapMarkers() {


        if(!this.props.allWishes || !this.props.allWishes.length) return;

        const L = window.L;
        const map = L.map('mapid').setView([45.23, 19.845], 14);

        const drawLineBeetwenPines = (pin1, pin2) => {
            const longLat = [[pin1.longitude, pin1.latitude], [pin2.longitude, pin2.latitude]];
            L.polyline(longLat).addTo(map);
        };

        L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoidGVzdGFwcGNmYWMiLCJhIjoiY2sxZXRjazlsMGw0ZzNvdW1mdTh4ZHA0eCJ9.7RLE9_R1Z-SOpPw9WrRJCA', {
            maxZoom: 18,
            id: 'mapbox.streets'
        }).addTo(map);


        this.props.allWishes.forEach(wish => {
            if (wish.fromKindergarden.longitude == null || wish.fromKindergarden.latitude == null) return;

            const toKinderLen = wish.toKindergardens.length;

            let pinFrom = {}
            pinFrom.name = wish.fromKindergarden.name;
            pinFrom.longitude = wish.fromKindergarden.longitude;
            pinFrom.latitude = wish.fromKindergarden.latitude;

            let pinTo = {}

            for (let i = 0; i < toKinderLen; i++) {
                if (wish.toKindergardens[i].longitude == null || wish.toKindergardens[i].latitude == null) return;

                pinTo.name = wish.toKindergardens[i].name;
                pinTo.longitude = wish.toKindergardens[i].longitude;
                pinTo.latitude = wish.toKindergardens[i].latitude;

                let list = [pinFrom, pinTo];
                
                list.forEach((pin, index) => {
                L.marker([pin.longitude, pin.latitude]).addTo(map).bindPopup(`<b>${index===0?'Iz: ':'U: '} ${pin.name}</b>`);
                });

                drawLineBeetwenPines(...list)
            }

        });


    }


     componentDidMount() {
         this.props.getAllWishes();
    }


    render() {
        return (
            <div id="mapid" style={{ height: 500 }}></div>
        );
    }
}


const mapStateToProps = (state) => {
    return {
        allWishes: state.allWishesForMap,
    }
}


const mapDispatchToProps = (dispatch) => {

    return {
        getAllWishes: () => { dispatch(GetAllWishesForMap()) },
    }
}

export default connect(mapStateToProps, mapDispatchToProps)(MapWishView);