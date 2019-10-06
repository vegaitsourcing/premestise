# -*- coding: utf-8 -*-
import codecs

def strToCyrillic(text):
    return text.replace('lj', 'љ').replace('nj', 'њ').replace('e', 'е').replace('r', 'р').replace('t', 'т').replace('z', 'з').replace('u', 'у').replace('i', 'и').replace('a', 'а').replace('s', 'с').replace('d', 'д').replace('f', 'ф').replace('g', 'г').replace('h', 'х').replace('j', 'ј').replace('k', 'к').replace('l', 'л').replace('č', 'ч').replace('ć', 'ћ').replace('ž', 'ж').replace('c', 'ц').replace('v', 'в').replace('b', 'б').replace('n', 'н').replace('m', 'м').replace('o', 'о').replace('p', 'п').replace('š', 'ш').replace('đ', 'ђ').replace('dž', 'џ').replace('lj'.upper(), 'љ'.upper()).replace('nj'.upper(), 'њ'.upper()).replace('e'.upper(), 'е'.upper()).replace('r'.upper(), 'р'.upper()).replace('t'.upper(), 'т'.upper()).replace('z'.upper(), 'з'.upper()).replace('u'.upper(), 'у'.upper()).replace('i'.upper(), 'и'.upper()).replace('a'.upper(), 'а'.upper()).replace('s'.upper(), 'с'.upper()).replace('d'.upper(), 'д'.upper()).replace('f'.upper(), 'ф'.upper()).replace('g'.upper(), 'г'.upper()).replace('h'.upper(), 'х'.upper()).replace('j'.upper(), 'ј'.upper()).replace('k'.upper(), 'к'.upper()).replace('l'.upper(), 'л'.upper()).replace('č'.upper(), 'ч'.upper()).replace('ć'.upper(), 'ћ'.upper()).replace('ž'.upper(), 'ж'.upper()).replace('c'.upper(), 'ц'.upper()).replace('v'.upper(), 'в'.upper()).replace('b'.upper(), 'б'.upper()).replace('n'.upper(), 'н'.upper()).replace('m'.upper(), 'м'.upper()).replace('o'.upper(), 'о'.upper()).replace('p'.upper(), 'п'.upper()).replace('š'.upper(), 'ш'.upper()).replace('đ'.upper(), 'ђ'.upper()).replace('dž'.upper(), 'џ'.upper())

if __name__ == '__main__':
    file_name = input('Унесите име фајла: ')
    final_file = file_name + '_cyrillic'
    lines = codecs.open(file_name, 'r', 'utf-8-sig').readlines()
    with codecs.open(final_file, 'a', 'utf-8-sig') as final:
        for item in lines:
            final.write(strToCyrillic(item))
