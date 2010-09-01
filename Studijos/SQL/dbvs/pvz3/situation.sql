
-- Ligoniui paliekanciam ligonine suteikiama bukle 'SVEIKAS'

UPDATE aNdRoJdAz.Ligoniai
	SET Bukle = 'Sveikas'
	WHERE Palata = 30
#


-- I duomenu baze itraukiamas naujas medikamentas

INSERT INTO aNdRoJdAz.Medikamentai	VALUES('POAS-94153',	'Maltodekstrinas')#


-- Priimamas dirbti naujas gydytojas

INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(9,	'Arunas',	'Kairys',	CURRENT DATE,	1000.00,	'Psichoterapeutas',	'+37061845974',	'kairys@psicho.org')# 


-- Ligonis gulejes 33-ioje palatoje perkeliamas i 34-a palata

UPDATE aNdRoJdAz.Ligoniai
	SET Palata = 34
	WHERE Palata = 33
#


-- I duomenu baze itraukiamas naujasis ligonis. Jis paguldomas i 33-ia palata. Ji gydo naujasis gydytojas

INSERT INTO aNdRoJdAz.Ligoniai		VALUES(19,	'Mantas',	'Peciulionis',	'36402021258',	'AS987456',	CURRENT DATE,	9,	33,	'Kybartai',	'Nestabili',	'Neteistas')#


-- Ligonis lankosi pas gydytoja

INSERT INTO aNdRoJdAz.Apsilankymai	VALUES(32,	CURRENT DATE,	19,	9)#


-- Israsomas receptas apsilankymo metu naujam medikamentui

INSERT INTO aNdRoJdAz.Receptai		VALUES(32,	'POAS-94153')#


-- Priimama dirbti nauja sesute

INSERT INTO aNdRoJdAz.Sesutes		VALUES(6,	'Liana',	'Ausyte',	CURRENT DATE,	800.00,	NULL)#


-- Sesute imasi priziureti 33-iaja ir kitas iseinancios is darbo sesutes palatas

UPDATE aNdRoJdAz.Palatos
	SET Sesute = 6
	WHERE Sesute = 3
#


-- Sesute palieka darba

DELETE FROM aNdRoJdAz.Sesutes WHERE ID = 3#