				---------------------------------------------------
				-- ==++>> Dalykines taisykles (trigeriai) <<++== --
				---------------------------------------------------


-- Dalykine taisykle, leidzianti sesutei priziuret ne daugiau kaip 5 palatas [Naudojama iterpiant duomenis]

--Blogas sakinys:
INSERT INTO aNdRoJdAz.Palatos		VALUES(52,	2,	2	)#
--Geras sakinys:
INSERT INTO aNdRoJdAz.Palatos		VALUES(52,	2,	4	)#


-- Dalykine taisykle, leidzianti sesutei priziuret ne daugiau kaip 5 palatas [Naudojama atnaujinant duomenis]

--Blogas sakinys:
UPDATE aNdRoJdAz.Palatos
	SET Sesute = 2
	WHERE Nr = 10
#
--Geras sakinys:
UPDATE aNdRoJdAz.Palatos
	SET Sesute = 5
	WHERE Nr = 10
#


-- Dalykine taisykle, neleidzianti ligoniui buti priskirtam palatai, kurioje nera laisvu vietu [Naudojama iterpiant duomenis]

--Blogas sakinys:
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(77,	'Andrius',	'Semiolkinas',	'38605031234',	'XX777777',	'2006-04-10',	6,	11,	'Kybartai',	'Stabili',	'Neteistas'	)#
--Geras sakinys:
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(77,	'Andrius',	'Semiolkinas',	'38605031234',	'XX777777',	'2006-04-10',	6,	10,	'Kybartai',	'Stabili',	'Neteistas'	)#


-- Dalykine taisykle, neleidzianti ligoniui buti priskirtam palatai, kurioje nera laisvu vietu [Naudojama atnaujinant duomenis]

--Blogas sakinys:
UPDATE aNdRoJdAz.Ligoniai
	SET Palata = 22
	WHERE Vardas = 'Mykolas' AND Pavarde = 'Gagelis'
#
--Geras sakinys:
UPDATE aNdRoJdAz.Ligoniai
	SET Palata = 24
	WHERE Sodros_Nr = 'EH3213232'
#


-- Dalykine taisykle, uztikrinanti, kad ligonio gydymo ciklo pradzia nebus ateityje (Paskutini karta testuota 2006 metais)

--Blogas sakinys:
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(77,	'Ausra',	'Murauskaite',	'48601113215',	'FD542198',	'2009-01-01',	3,	43,	'Klaipeda',	'Stabili',	'Neteistas'	)#
--Geras sakinys:
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(77,	'Ausra',	'Murauskaite',	'48601113215',	'FD542198',	'2004-01-01',	3,	43,	'Klaipeda',	'Stabili',	'Neteistas'	)#


-- Dalykine taisykle, uztikrinanti, kad receptas nebus israsytas pries atvezant ligoni i ligonine

--Blogas sakinys:
INSERT INTO aNdRoJdAz.Apsilankymai		VALUES(77,	'1998-08-28',	10,	6)#
--Geras sakinys:
INSERT INTO aNdRoJdAz.Apsilankymai		VALUES(77,	'2002-12-26',	10,	6)#
