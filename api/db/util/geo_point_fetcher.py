from geopy.geocoders import Nominatim

class GeoPoint(object):
    def __init__(self, location):
        self.latitude = location.latitude
        self.longitude = location.longitude

    def __repr__(self):
        return f'{self.latitude} {self.longitude}'

def getGeoPointFor(address):
    geolocator = Nominatim(user_agent = "premesti-se")
    location = geolocator.geocode(address)
    if location is not None:
        return GeoPoint(location)
