# -*- coding: utf-8 -*-
import codecs
from transliterate import translit

# from cyrillic to latin
lines = codecs.open('obdanista_csv_cyrillic.csv', 'r', 'utf-8-sig').readlines()
with codecs.open('obdanista_csv_latin.csv', 'a', 'utf-8-sig') as final:
    for item in lines:
        if '&#39' in item:
            print('OLD: ' + item)
            new_item = item.replace('ч', 'č').replace('ћ', 'ć').replace('&#39;', '\'')
            print('NEW: ' + new_item)

        new_item = item.replace('ч', 'č').replace('ћ', 'ć').replace('&#39;', '\'')
        final.write(translit(new_item, "sr", reversed=True))