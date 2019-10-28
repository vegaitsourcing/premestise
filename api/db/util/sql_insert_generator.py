import pandas as pd
import geo_point_fetcher
import codecs

class Kindergarden(object):
    def __init__(self, *args, **kwargs):
        asd = []
        asd = list(args[0])
        asd.extend([args[1]])
        asd.extend([args[2]])
        args = asd
        self.uprava = args[0]
        self.opstina = args[1]
        self.naselje = args[2]
        self.ustanova = args[3]
        self.naziv = args[4]
        self.ulica = str(args[5]).capitalize()
        self.broj = args[6]
        self.postanski_broj = args[7]
        self.location_type = 1 if args[8] == 'Izdvojena lokacija' else 0
        self.latitude = str(args[9])
        self.longitude = str(args[10])

    def __str__(self):
        return f"INSERT INTO kindergarden (city, municipality, government, department, name, street, street_number, postal_code, location_type, longitude, latitude) VALUES ('{self.uprava}', '{self.opstina}', '{self.naselje}', '{self.ustanova}', '{self.naziv}', '{self.ulica}', '{self.broj}', '{self.postanski_broj}', {self.location_type}, {self.latitude}, {self.longitude});"
        
# Ne sme error bad lines, gube se redovi, ali za sad radi ovo, mozda moze Header=1
obdanista = pd.read_csv('obdanista_csv_latin.csv', delimiter = ',', error_bad_lines=False)
dbRow = obdanista[['uprava','opstina','naselje','ustanova','naziv','ulica','broj','postanski_broj', 'tip_lokacije']]
kindergardenAddress = obdanista[['ulica', 'broj', 'naselje']]
with codecs.open('insert.sql', "a", "utf-8-sig") as f:
    for i in range(0, len(dbRow)-1):
        adressLine = kindergardenAddress.iloc[i].to_string(header=False, index=False).strip()
        point = geo_point_fetcher.getGeoPointFor(adressLine)
        kindergarden = Kindergarden(dbRow.iloc[i], point.latitude if point is not None else "null", point.longitude if point is not None else "null")
        print(i, kindergarden)
        print(kindergarden, file=f)