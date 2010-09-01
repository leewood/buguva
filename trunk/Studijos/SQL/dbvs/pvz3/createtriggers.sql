-- Dalykine taisykle, leidzianti sesutei priziuret ne daugiau kaip 5 palatas [Naudojama iterpiant duomenis]

CREATE TRIGGER aNdRoJdAz.MaxPalatSesuteiI
	NO CASCADE BEFORE INSERT ON aNdRoJdAz.Palatos
	REFERENCING NEW AS NaujaPalata
	FOR EACH ROW MODE DB2SQL
	WHEN ((SELECT COUNT(*) FROM aNdRoJdAz.Palatos
		WHERE aNdRoJdAz.Palatos.Sesute = NaujaPalata.Sesute) >= 5)
	SIGNAL SQLSTATE '80000'('ON INSERT: SI SESUTE DAUGIAU PALATU PRIZIURETI NEGALI!!!')
#


-- Dalykine taisykle, leidzianti sesutei priziuret ne daugiau kaip 5 palatas [Naudojama atnaujinant duomenis]

CREATE TRIGGER aNdRoJdAz.MaxPalatSesuteiU
	NO CASCADE BEFORE UPDATE OF Sesute ON aNdRoJdAz.Palatos
	REFERENCING NEW AS UpdPalata
	FOR EACH ROW MODE DB2SQL
	WHEN ((SELECT COUNT(*) FROM aNdRoJdAz.Palatos
		WHERE aNdRoJdAz.Palatos.Sesute = UpdPalata.Sesute) >= 5)
	SIGNAL SQLSTATE '80001'('ON UPDATE: SI SESUTE DAUGIAU PALATU PRIZIURETI NEGALI!!!')
#


-- Dalykine taisykle, neleidzianti ligoniui buti priskirtam palatai, kurioje nera laisvu vietu [Naudojama iterpiant duomenis]

CREATE TRIGGER aNdRojdAz.MaxVietuPalatojI
	NO CASCADE BEFORE INSERT ON aNdRoJdAz.Ligoniai
	REFERENCING NEW AS NaujasLigonis
	FOR EACH ROW MODE DB2SQL
	WHEN ((SELECT "Laisvos Vietos" FROM aNdRoJdAz.LaisvosVietos
		WHERE aNdRoJdAz.LaisvosVietos.Palata = NaujasLigonis.Palata) = 0)
	SIGNAL SQLSTATE '80002'('ON INSERT: SIOJE PALATOJE DAUGIAU NEBERA VIETU!!!')
#	


-- Dalykine taisykle, neleidzianti ligoniui buti priskirtam palatai, kurioje nera laisvu vietu [Naudojama atnaujinant duomenis]

CREATE TRIGGER aNdRojdAz.MaxVietuPalatojU
	NO CASCADE BEFORE UPDATE OF Palata ON aNdRoJdAz.Ligoniai
	REFERENCING NEW AS NaujasLigonis
	FOR EACH ROW MODE DB2SQL
	WHEN ((SELECT "Laisvos Vietos" FROM aNdRoJdAz.LaisvosVietos
		WHERE aNdRoJdAz.LaisvosVietos.Palata = NaujasLigonis.Palata) = 0)
	SIGNAL SQLSTATE '80003'('ON UPDATE: SIOJE PALATOJE DAUGIAU NEBERA VIETU!!!')
#


-- Automatinis tapatumo pozymio priskyrimas naujam gydytojui

CREATE TRIGGER aNdRojdAz.NaujasisGydytojas
	NO CASCADE BEFORE INSERT ON aNdRoJdAz.Gydytojai
	REFERENCING NEW AS NaujasGydytojas
	FOR EACH ROW MODE DB2SQL	
	SET NaujasGydytojas.ID = (SELECT COALESCE(MAX(ID),0)+1
					FROM aNdRoJdAz.Gydytojai)
#


-- Automatinis tapatumo pozymio priskyrimas naujam ligoniui

CREATE TRIGGER aNdRojdAz.NaujasisLigonis
	NO CASCADE BEFORE INSERT ON aNdRoJdAz.Ligoniai
	REFERENCING NEW AS NaujasLigonis
	FOR EACH ROW MODE DB2SQL	
	SET NaujasLigonis.ID = (SELECT COALESCE(MAX(ID),0)+1
					FROM aNdRoJdAz.Ligoniai)
#


-- Automatinis tapatumo pozymio priskyrimas naujai sesutei

CREATE TRIGGER aNdRojdAz.NaujojiSesute
	NO CASCADE BEFORE INSERT ON aNdRoJdAz.Sesutes
	REFERENCING NEW AS NaujaSesute
	FOR EACH ROW MODE DB2SQL	
	SET NaujaSesute.ID = (SELECT COALESCE(MAX(ID),0)+1
					FROM aNdRoJdAz.Sesutes)
#


-- Automatinis tapatumo pozymio priskyrimas naujam apsilankymui

CREATE TRIGGER aNdRojdAz.NaujasisApsilan
	NO CASCADE BEFORE INSERT ON aNdRoJdAz.Apsilankymai
	REFERENCING NEW AS NaujasApsilan
	FOR EACH ROW MODE DB2SQL	
	SET NaujasApsilan.ID = (SELECT COALESCE(MAX(ID),0)+1
					FROM aNdRoJdAz.Apsilankymai)
#

-- Dalykine taisykle, uztikrinanti, kad ligonio gydymo ciklo pradzia nebus ateityje

CREATE TRIGGER aNdRojdAz.LigonioAtvezimas
	NO CASCADE BEFORE INSERT ON aNdRoJdAz.Ligoniai
	REFERENCING NEW AS NaujasLigonis
	FOR EACH ROW MODE DB2SQL
	WHEN (NaujasLigonis.Gydosi_Nuo > CURRENT DATE)
	SIGNAL SQLSTATE '80004' ('LIGONIO ATVEZIMO DATA NEGALI BUTI VELESNE NEI SIOS DIENOS!!!')
#


-- Dalykine taisykle, uztikrinanti, kad apsilankymas ligoniui nebus paskirtas pries atvezant ji i ligonine

CREATE TRIGGER aNdRoJdAz.VaistuSkyrData
	NO CASCADE BEFORE INSERT ON aNdRoJdAz.Apsilankymai
	REFERENCING NEW AS NaujasVaistuSkyrimas
	FOR EACH ROW MODE DB2SQL
	WHEN ((SELECT DISTINCT Gydosi_Nuo FROM aNdRoJdAz.Apsilankymai, aNdRoJdAz.Ligoniai WHERE NaujasVaistuSkyrimas.Ligonis = aNdRoJdAz.Ligoniai.ID) > NaujasVaistuSkyrimas.Data)
	SIGNAL SQLSTATE '80005' ('LIGONIUI SKIRIAMU VAISTU DATA NEGALI BUTI ANKSTESNE NEI JO ATVEZIMO DATA!!!')
#	