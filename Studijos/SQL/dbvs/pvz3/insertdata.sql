--						ID	Vardas		Pavarde		Dirba Nuo	Alga		Pareigos		Telefonas	El. pastas	

INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(1,	'Juozas',	'Martinaitis',	'1999-12-14',	3123.20,	'Vyr. gydytojas',	'+37066622288',	'juozas.martinaitis@psicho.org'	)#
INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(2,	'Antanas',	'Petrusis',	'2000-03-09',	1005.95,	'Kardiologas',		'+37013245584',	'antanas.petrusis@psicho.org'	)#
INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(3,	'Jonas',	'Kairys',	'2004-02-01',	1600.00,	'Psichiatras',		'+37012345655',	NULL				)#
INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(4,	'Mantas',	'Baubas',	'2002-03-22',	2550.00,	'Venerologas',		NULL,		'baubas@venero.com'		)# 
INSERT INTO aNdRoJdAz.Gydytojai		VALUES(5,	'Povilas',	'Kalimas',	'2001-01-18',	2000.00,	'Pcichologas',		NULL,		'povilas.kalimas@yahoo.com'	)# 
INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(6,	'Karolis',	'Juskus',	'2003-05-30',	1550.00,	'Felceris',		NULL,		NULL				)#
INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(7,	'Martynas',	'Juskus',	'2001-02-06',	3510.00,	'Chirurgas',		'+37062153211',	'chirurgas@atb-music.com'	)# 
INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(8,	'Jeronimas',	'Kavaliunas',	'2002-09-12',	1850.50,	'Psichoterapeutas',	NULL,		'kavaliunas@gmail.com'		)# 


--						ID	Vardas		Pavarde			Dirba Nuo	Alga	Telefonas

INSERT INTO aNdRoJdAz.Sesutes		VALUES(1,	'Karolina',	'Iesmantaviciute',	'2003-12-20',	800.12,	'+37064111242'	)#
INSERT INTO aNdRoJdAz.Sesutes		VALUES(2,	'Milda',	'Skaistute',		'2006-01-03',	550.00,	'+37053251122'	)#
INSERT INTO aNdRoJdAz.Sesutes		VALUES(3,	'Laura',	'Krisciunaite',		'2001-06-16',	900.50,	'+37061155321'	)#
INSERT INTO aNdRoJdAz.Sesutes		VALUES(4,	'Natasa',	'Miliute',		'1999-05-03',	1000.00,'+37062165342'	)#
INSERT INTO aNdRoJdAz.Sesutes		VALUES(5,	'Viktorija',	'Karaliunaite',		'2000-10-30',	950.43,	'+37063264212'	)#


--						Nr	Vietos	Sesutes ID

INSERT INTO aNdRoJdAz.Palatos		VALUES(10,	2,	1	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(11,	2,	1	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(12,	2,	1	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(13,	2,	1	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(14,	2,	1	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(20,	3,	2	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(21,	3,	2	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(22,	3,	2	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(23,	3,	2	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(24,	3,	2	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(30,	1,	3	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(31,	1,	3	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(32,	1,	3	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(33,	1,	3	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(34,	1,	3	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(40,	1,	4	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(41,	2,	4	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(42,	2,	4	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(43,	2,	4	)#
INSERT INTO aNdRoJdAz.Palatos		VALUES(44,	3,	5	)#


--						Bukle

INSERT INTO aNdRoJdAz.Bukles		VALUES('Stabili')#
INSERT INTO aNdRoJdAz.Bukles		VALUES('Nestabili')#
INSERT INTO aNdRoJdAz.Bukles		VALUES('Sveikas')#
INSERT INTO aNdRoJdAz.Bukles		VALUES('Sunki')#
INSERT INTO aNdRoJdAz.Bukles		VALUES('Tramdomas')#
INSERT INTO aNdRoJdAz.Bukles		VALUES('Siauteja')#


--						Teistumas

INSERT INTO aNdRoJdAz.Teistumai		VALUES('Teistas')#
INSERT INTO aNdRoJdAz.Teistumai		VALUES('Neteistas')#
INSERT INTO aNdRoJdAz.Teistumai		VALUES('Kali')#


--						ID	Vardas		Pavarde		Asmens Kodas	Sordos paz. Nr	Gydosi Nuo	Gydancio Gydytojo ID	Palata	Miestas		Bukle		Teistumas

INSERT INTO aNdRoJdAz.Ligoniai		VALUES(1,	'Bronius',	'Asmena',	'36012306542',	'AS3216547',	'2000-01-05',	2,			33,	'Kaunas',	'Tramdomas',	'Teistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(2,	'Arunas',	'Maciulis',	'34311131332',	'KF1513295',	'2005-06-19',	6,			11,	'Panevezys',	'Stabili',	'Neteistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(3,	'Ligitas',	'Auksiejus',	'38412211515',	'SG2135486',	'2000-12-25',	1,			30,	'Kaunas',	'Tramdomas',	'Kali'		)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(4,	'Marius',	'Marcelijus',	'36001264645',	'HF8947654',	'2006-01-18',	5,			13,	'Siauliai',	'Stabili',	'Neteistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(5,	'Arturas',	'Asmena',	'37911164621',	'BC3321245',	'2002-01-05',	2,			11,	'Kaunas',	'Siauteja',	'Teistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(6,	'Konstantinas',	'Lobynas',	'34901274651',	'WT2215552',	'1986-10-17',	1,			23,	'Marijampole',	'Nestabili',	'Neteistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(7,	'Paulius',	'Ragutis',	'35304261322',	'EF3949492',	'1996-01-20',	3,			12,	'Kaunas',	'Sunki',	'Teistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(8,	'Mykolas',	'Gagelis',	'37001241221',	'EH3213232',	'2005-09-30',	3,			23,	'Kaunas',	'Stabili',	'Kali'		)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(9,	'Saulius',	'Psichodelijus','38605030656',	'FB8415482',	'1991-08-16',	2,			23,	'Radviliskis',	'Tramdomas',	'Kali'		)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(10,	'Aleksas',	'Mircius',	'35605171351',	'HD8948945',	'1999-07-05',	2,			24,	'Kaunas',	'Stabili',	'Kali'		)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(11,	'Beata',	'Kisko',	'47005191351',	'KJ8321844',	'1995-06-01',	4,			22,	'Kybartai',	'Stabili',	'Neteistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(12,	'Tatjana',	'Chlapciuk',	'48105251351',	'LD8752321',	'1997-01-17',	4,			22,	'Vilnius',	'Sunki',	'Neteistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(13,	'Goda',		'Sakalauskaite','46405301351',	'PS3213211',	'1989-11-25',	1,			22,	'Kybartai',	'Sunki',	'Neteistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(14,	'Migle',	'Sakalauskaite','47402132122',	'AS3213255',	'1989-11-25',	1,			41,	'Vilkaviskis',	'Stabili',	'Teistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(15,	'Ramune',	'Sakalauskaite','46208123215',	'PB8421027',	'1989-11-25',	1,			41,	'Alytus',	'Stabili',	'Neteistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(16,	'Mila',		'Chlapciuk',	'48012120325',	'RN1321322',	'1999-02-12',	4,			42,	'Kaunas',	'Stabili',	'Kali'		)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(17,	'Juste',	'Mazute',	'46906041232',	'MH8472100',	'2000-05-01',	7,			43,	'Siauliai',	'Stabili',	'Neteistas'	)#
INSERT INTO aNdRoJdAz.Ligoniai		VALUES(18,	'Aukse',	'Maciulaityte',	'48912303215',	'WK9843212',	'2000-03-05',	8,			44,	'Kaunas',	'Tramdomas',	'Neteistas'	)#


--						Kodas		Pavadinimas

INSERT INTO aNdRoJdAz.Medikamentai	VALUES('AAGD-21542',	'Metandrostenolonas'	)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('DAGF-95121',	'Retabolilas 250'	)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('KAHS-32135',	'Kardioalamandarginas'	)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('SDGW-03245',	'Nalpreksonas'		)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('ASRE-53212',	'Chlorpromazinas'	)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('QWER-21000',	'Haloperidolis'		)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('IAUS-32132',	'Paradolginas'		)#	
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('ASDF-32142',	'Meladoksinas'		)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('LKJN-98874',	'Kontatubinas'		)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('KHGV-13254',	'Karmadoksinas'		)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('HRDK-21541',	'Pelimandrinas'		)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('KJVH-86421',	'Kartodoksenas'		)#
INSERT INTO aNdRoJdAz.Medikamentai	VALUES('PIUT-32415',	'Medochlorinas'		)#


--							ID	Data		Ligonio ID	Skyrusio Gydytojo ID

INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(1,	'1997-02-13',	7,		1)#	
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(2,	'1999-09-26',	7,		3)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(3,	'2004-01-20',	7,		2)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(4,	'2004-01-20',	7,		6)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(5,	'1998-10-21',	7,		3)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(6,	'1998-10-21',	12,		4)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(7,	'1995-05-03',	9,		4)#		
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(8,	'2000-12-25',	3,		4)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(9,	'2001-04-19',	1,		2)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(10,	'2001-04-19',	1,		2)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(11,	'2001-04-19',	1,		2)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(12,	'2005-07-25',	2,		4)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(13,	'2006-03-01',	4,		6)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(14,	'2003-12-20',	5,		3)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(15,	'1987-03-01',	6,		8)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(16,	'1989-11-15',	6,		2)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(17,	'2005-10-20',	8,		5)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(18,	'2000-03-02',	10,		8)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(19,	'1989-11-25',	13,		1)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(20,	'1993-09-05',	13,		6)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(21,	'1996-08-24',	11,		3)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(22,	'1990-07-17',	14,		8)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(23,	'2000-12-19',	14,		7)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(24,	'2001-09-05',	18,		6)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(25,	'2003-01-29',	17,		8)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(26,	'2000-08-21',	16,		7)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(27,	'2001-12-10',	16,		6)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(28,	'2002-11-30',	16,		8)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(29,	'1990-04-03',	15,		7)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(30,	'1995-06-13',	15,		1)#
INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(31,	'2000-10-18',	15,		6)#


--						ID	Kodas
	
INSERT INTO aNdRoJdAz.Receptai		VALUES(1,	'AAGD-21542')#	
INSERT INTO aNdRoJdAz.Receptai		VALUES(2,	'AAGD-21542')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(3,	'AAGD-21542')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(4,	'DAGF-95121')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(5,	'DAGF-95121')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(6,	'DAGF-95121')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(7,	'KAHS-32135')#		
INSERT INTO aNdRoJdAz.Receptai		VALUES(8,	'KAHS-32135')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(9,	'SDGW-03245')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(10,	'ASRE-53212')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(11,	'QWER-21000')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(12,	'SDGW-03245')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(13,	'ASRE-53212')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(14,	'QWER-21000')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(15,	'KJVH-86421')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(16,	'KHGV-13254')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(17,	'KJVH-86421')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(18,	'LKJN-98874')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(19,	'PIUT-32415')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(20,	'LKJN-98874')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(21,	'KHGV-13254')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(22,	'PIUT-32415')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(23,	'HRDK-21541')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(24,	'KJVH-86421')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(25,	'ASDF-32142')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(26,	'KJVH-86421')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(27,	'ASDF-32142')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(28,	'PIUT-32415')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(29,	'IAUS-32132')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(30,	'PIUT-32415')#
INSERT INTO aNdRoJdAz.Receptai		VALUES(31,	'HRDK-21541')#

