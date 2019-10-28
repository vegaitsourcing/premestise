# -*- coding: utf-8 -*-
import codecs

def cyrToLat(text):
    return text.replace('ч', 'č').replace('ћ', 'ć')

if __name__ == '__main__':
    cyrillic_file = 'obdanista_csv_cyrillic.csv'
    latin_file = 'obdanista_csv_latin.csv'
    lines = codecs.open(cyrillic_file, 'r', 'utf-8-sig').readlines()
    with codecs.open(latin_file, 'a', 'utf-8-sig') as final:
        for item in lines:
            final.write(cyrToLat(item))
