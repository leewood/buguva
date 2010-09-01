				-------------------------------------------------------
				-- ==++>> Deklaratyvus reikalavimai duomenims <<++== --
				-------------------------------------------------------

				-----------------------------------------------------------
				-- ==++>> Automatiniai tapatumo pozymiu priskyrimai: <<++==
				-----------------------------------------------------------
				--	Naujam gydytojui
				--	Naujam ligoniui 
				--	Naujai sesutei
				--	Naujam apsilankymui
				-----------------------------------------------------------



-- CONSTRAINT MinimaliAlga CHECK (Alga >= 550)

-- @Sesutes [Automatinis tapatumo pozymio priskyrimas naujai sesutei]:

--Blogas sakinys:
INSERT INTO aNdRoJdAz.Sesutes		VALUES(77,	'Veronika',	'Blauzdziunaite',	'2004-11-10',	500,	NULL	)#
--Geras sakinys:
INSERT INTO aNdRoJdAz.Sesutes		VALUES(77,	'Veronika',	'Blauzdziunaite',	'2004-11-10',	600,	NULL	)#

-- @Gydytojai [Automatinis tapatumo pozymio priskyrimas naujam gydytojui]:

--Blogas sakinys:
INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(77,	'Karolis',	'Antanaitis',	'2003-10-23',	549.99,		NULL,	NULL,	NULL	)# 
--Geras sakinys:
INSERT INTO aNdRoJdAz.Gydytojai 	VALUES(77,	'Karolis',	'Antanaitis',	'2003-10-23',	1240.69,	NULL,	NULL,	NULL	)# 


-- CHECK (Nr > 0)

--Blogas sakinys:
INSERT INTO aNdRoJdAz.Palatos		VALUES(0,	1,	5	)#
--Geras sakinys:
INSERT INTO aNdRoJdAz.Palatos		VALUES(50,	1,	5	)#


-- CONSTRAINT PalatuDydi CHECK ((Vietu_Sk >= 1) AND (Vietu_Sk <= 6))

--Blogas sakinys:
INSERT INTO aNdRoJdAz.Palatos		VALUES(51,	0,	5	)#
--Geras sakinys:
INSERT INTO aNdRoJdAz.Palatos		VALUES(51,	2,	5	)#


-- Automatinis tapatumo pozymio priskyrimas naujam apsilankymui:

INSERT INTO aNdRoJdAz.Apsilankymai		VALUES(77,	'2005-12-18',	1,	1)#