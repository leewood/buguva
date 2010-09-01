-- Informacija apie likusias laisvas vietas palatose

CREATE VIEW aNdRoJdAz.LaisvosVietos AS
	SELECT Nr AS 	Palata,
			Vietu_Sk, 
			Vietu_Sk - (COALESCE ((SELECT COUNT (ID)
					       FROM aNdRoJdAz.Ligoniai
					       WHERE Palata = Nr), 0)) AS "Laisvos Vietos"
	FROM aNdRoJdAz.Palatos
#


-- Apsilankymu israsai

CREATE VIEW aNdRoJdAz.SkiriamiVaistai AS
	SELECT 	Ligonis					AS "Ligonio ID",
		aNdRoJdAz.Ligoniai.Vardas 		AS "Ligonio Vardas",
		aNdRoJdAz.Ligoniai.Pavarde 		AS "Ligonio Pavarde",
		aNdRoJdAz.Receptai.Kodas		AS "Vaistu Kodas",
		aNdRoJdAz.Medikamentai.Pavadinimas	AS "Vastu Pavadinimas",
		aNdRoJdAz.Gydytojai.Vardas		AS "Skyrusio Gydytojo Vardas",
		aNdRoJdAz.Gydytojai.Pavarde		AS "Skyrusio Gydytojo Pavarde",
		Data
	FROM aNdRoJdAz.Apsilankymai, aNdRoJdAz.Ligoniai, aNdRoJdAz.Gydytojai, aNdRoJdAz.Medikamentai, aNdRoJdAz.Receptai
	WHERE aNdRoJdAz.Receptai.Kodas = aNdRoJdAz.Medikamentai.Kodas AND aNdRoJdAz.Apsilankymai.ID = aNdRoJdAz.Receptai.ID AND aNdRoJdAz.Apsilankymai.Gydytojas_Skyre = aNdRoJdAz.Gydytojai.ID AND aNdRoJdAz.Apsilankymai.Ligonis = aNdRoJdAz.Ligoniai.ID
#


-- Detalus ligonio israsas is jo korteles

CREATE VIEW aNdRoJdAz.LigonioInfo AS
	SELECT	aNdRoJdAz.Ligoniai.ID			AS "Ligonio ID",
		aNdRoJdAz.Ligoniai.Vardas 		AS "Ligonio Vardas",
		aNdRoJdAz.Ligoniai.Pavarde 		AS "Ligonio Pavarde",
		Nr					AS "Palatos Nr",
		AK,
		Sodros_Nr,
		aNdRoJdAz.Gydytojai.Vardas		AS "Gydytojo Vardas",
		aNdRoJdAz.Gydytojai.Pavarde		AS "Gydytojo Pavarde",
		aNdRoJdAz.Sesutes.Vardas		AS "Sesutes Vardas",
		aNdRoJdAz.Sesutes.Pavarde		AS "Sesutes Pavarde",
		COALESCE ((SELECT COUNT (DISTINCT Kodas)
			   FROM aNdRoJdAz.Receptai, aNdRoJdAz.Apsilankymai
			   WHERE aNdRoJdAz.Apsilankymai.ID = aNdRoJdAz.Receptai.ID AND aNdRoJdAz.Ligoniai.ID = aNdRoJdAz.Apsilankymai.Ligonis), 0)	AS "Skirti Vaistai",
		COALESCE ((SELECT COUNT (Kodas)
			   FROM aNdRoJdAz.Receptai, aNdRoJdAz.Apsilankymai
			   WHERE aNdRoJdAz.Apsilankymai.ID = aNdRoJdAz.Receptai.ID AND aNdRoJdAz.Ligoniai.ID = aNdRoJdAz.Apsilankymai.Ligonis), 0)	AS "Is Viso Skirta"

	FROM aNdRoJdAz.Ligoniai, aNdRoJdAz.Palatos, aNdRoJdAz.Sesutes, aNdRoJdAz.Gydytojai
	WHERE aNdRoJdAz.Ligoniai.Gydytojas_Gydo = aNdRoJdAz.Gydytojai.ID AND aNdRoJdAz.Ligoniai.Palata = aNdRoJdAz.Palatos.Nr AND aNdRoJdAz.Palatos.Sesute = aNdRoJdAz.Sesutes.ID
#