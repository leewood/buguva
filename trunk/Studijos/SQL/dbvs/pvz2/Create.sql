CREATE TABLE IM.Diena (
	Data		DATE	   NOT NULL,
	Savaites_diena	CHAR(15)   NOT NULL   CHECK(Savaites_diena IN
							('pirmadienis',
							 'antradienis',
							 'treciadienis',
							 'ketvirtadienis',
							 'penktadienis',
							 'sestadienis',
							 'sekmadienis')
						    ),
	PRIMARY KEY (Data)
	)#

CREATE TABLE IM.Sritys (
	Srities_ID	INTEGER    NOT NULL   CHECK(Srities_ID >= 0), 
	Sritis		CHAR(30)   NOT NULL, 	
	Prioritetas	SMALLINT   	      CHECK(Prioritetas BETWEEN 1 AND 3),
	PRIMARY KEY (Srities_ID)
	) # 
	
CREATE TABLE IM.Darbai (
	Darbo_ID	INTEGER	   NOT NULL    CHECK(Darbo_ID >= 0),
	Darbas		CHAR(30)   NOT NULL,
	Ar_atliktas	CHAR(5)	   DEFAULT 'ne'   CHECK(Ar_atliktas IN 
								('taip',
								 'ne')
							),
	Pradzia		DATE,
	Pabaiga		DATE,
	PRIMARY KEY	(Darbo_ID),
	FOREIGN KEY	Pradetas(Pradzia)
		REFERENCES IM.Diena
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
	FOREIGN KEY	Baigtas(Pabaiga)
		REFERENCES IM.Diena
		ON DELETE CASCADE
		ON UPDATE RESTRICT
	)#
	
CREATE TABLE IM.Dirbimas (
	Dirbimo_ID	INTEGER	   NOT NULL	CHECK(Dirbimo_ID >= 0),
	Laikas_val	SMALLINT   NOT NULL	CHECK(Laikas_val > 0  AND
						      Laikas_val <= 24),
	Srities_ident	INTEGER			CHECK(Srities_ident >= 0),
	Darbo_ident	INTEGER			CHECK(Darbo_ident >= 0),
	CONSTRAINT Korektiskumas 		CHECK(Srities_ident IS NOT NULL  OR
						      Darbo_ident IS NOT NULL),
	PRIMARY KEY	(Dirbimo_ID),
	FOREIGN KEY	Kokioje_srityje(Srities_ident)
		REFERENCES IM.Sritys
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
	FOREIGN KEY	Koks_darbas(Darbo_ident)
		REFERENCES IM.Darbai
		ON DELETE CASCADE
		ON UPDATE RESTRICT
	)#
	
CREATE TABLE IM.Fiksavimas (
	Fiksavimo_ID	INTEGER	   NOT NULL	CHECK(Fiksavimo_ID >= 0),
	Dirbimo_ident	INTEGER	   NOT NULL	CHECK(Dirbimo_ident >= 0),
	Datos_ident	DATE	   NOT NULL,
	PRIMARY KEY	(Fiksavimo_ID),
	FOREIGN KEY	Kas_dirbta(Dirbimo_ident)
		REFERENCES IM.Dirbimas
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
	FOREIGN KEY	Kada_dirbta(Datos_ident)
		REFERENCES IM.Diena
		ON DELETE CASCADE
		ON UPDATE RESTRICT
	)#
	
CREATE TABLE IM.Priklausymas(
	Srities_ident	INTEGER	   NOT NULL	CHECK(Srities_ident >= 0),
	Darbo_ident	INTEGER	   NOT NULL     CHECK(Darbo_ident >= 0),
	PRIMARY KEY	(Srities_ident,Darbo_ident),
	FOREIGN KEY	Kam_priklauso(Srities_ident)
		REFERENCES IM.Sritys
		ON DELETE CASCADE
		ON UPDATE RESTRICT,
	FOREIGN KEY	Kas_priklauso(Darbo_ident)
		REFERENCES IM.Darbai
		ON DELETE CASCADE
		ON UPDATE RESTRICT
	)#
	
	
	
CREATE TRIGGER IM.Kitas_srities_ID
	NO CASCADE BEFORE INSERT ON IM.Sritys
	REFERENCING NEW AS Kita_sritis
	FOR EACH ROW MODE DB2SQL
	BEGIN ATOMIC
	  SET Kita_sritis.Srities_ID = (SELECT MAX(Srities_ID)+1 FROM IM.Sritys);
	  SET Kita_sritis.Srities_ID = COALESCE(Kita_sritis.Srities_ID, 0);
	END#
	
CREATE TRIGGER IM.Kitas_darbo_ID
	NO CASCADE BEFORE INSERT ON IM.Darbai
	REFERENCING NEW AS Kitas_darbas
	FOR EACH ROW MODE DB2SQL
	BEGIN ATOMIC
	  SET Kitas_darbas.Darbo_ID = (SELECT MAX(Darbo_ID)+1 FROM IM.Darbai);
	  SET Kitas_darbas.Darbo_ID = COALESCE(Kitas_darbas.Darbo_ID, 0);
	END#
	
CREATE TRIGGER IM.Kitas_dirbimo_ID
	NO CASCADE BEFORE INSERT ON IM.Dirbimas
	REFERENCING NEW AS Kitas_dirbimas
	FOR EACH ROW MODE DB2SQL
	BEGIN ATOMIC
	  SET Kitas_dirbimas.Dirbimo_ID = (SELECT MAX(Dirbimo_ID)+1 FROM IM.Dirbimas);
	  SET Kitas_dirbimas.Dirbimo_ID = COALESCE(Kitas_dirbimas.Dirbimo_ID, 0);
	END#
	
CREATE TRIGGER IM.Kitas_fiksavimo_ID
       NO CASCADE BEFORE INSERT ON IM.Fiksavimas
       REFERENCING NEW AS Kitas_fiksavimas
       FOR EACH ROW MODE DB2SQL
       BEGIN ATOMIC
	SET Kitas_fiksavimas.Fiksavimo_ID=(SELECT MAX(Fiksavimo_ID)+1 FROM IM.Fiksavimas);
	SET Kitas_fiksavimas.Fiksavimo_ID = COALESCE(Kitas_fiksavimas.Fiksavimo_ID, 0);
       END#
       
   
   
CREATE VIEW IM.Veikla_Laikas (Dirbimo_ID,Data,Savaites_diena,Laikas_val,Sritis,Darbas)
AS
SELECT	Dirbimo_ID,Data,Savaites_diena,Laikas_val,Sritis,
	CASE WHEN Darbo_ident IS NOT NULL THEN Darbas ELSE NULL END AS Darbas
FROM	IM.Fiksavimas,IM.Diena,IM.Dirbimas,IM.Sritys,IM.Darbai
WHERE 	Datos_ident = Data AND
	Dirbimo_ident = Dirbimo_ID AND
	Srities_ident = Srities_ID AND
	Darbo_ident = Darbo_ID  	
UNION
SELECT	Dirbimo_ID,Data,Savaites_diena,Laikas_val,Sritis,
	CASE WHEN Darbo_ident IS NOT NULL THEN Darbas ELSE NULL END AS Darbas
FROM	IM.Fiksavimas,IM.Diena,IM.Dirbimas,IM.Sritys,IM.Darbai
WHERE	Datos_ident = Data AND
	Dirbimo_ident = Dirbimo_ID AND
	Srities_ident = Srities_ID AND
	Darbo_ident IS NULL 
UNION
SELECT	Dirbimo_ID,Data,Savaites_diena,Laikas_val,
	CASE WHEN Srities_ident IS NOT NULL THEN Sritis ELSE NULL END AS Sritis,
	Darbas
FROM	IM.Fiksavimas,IM.Diena,IM.Dirbimas,IM.Sritys,IM.Darbai
WHERE	Datos_ident = Data AND
	Dirbimo_ident = Dirbimo_ID AND
	Srities_ident IS NULL AND
	Darbo_ident = Darbo_ID #
	
	
CREATE VIEW IM.Prioritetai_Laikas (Sritis,Darbas,Prioritetas,Laikas_val)
AS
SELECT	Sritis,
	CASE WHEN Darbo_ident IS NOT NULL THEN Darbas ELSE NULL END AS Darbas,
	Prioritetas,Laikas_val
FROM	IM.Dirbimas,IM.Sritys,IM.Darbai
WHERE	Srities_ident = Srities_ID AND
	Darbo_ident = Darbo_ID 
UNION	
SELECT	Sritis,
	CASE WHEN Darbo_ident IS NOT NULL THEN Darbas ELSE NULL END AS Darbas,
	Prioritetas,Laikas_val
FROM	IM.Dirbimas,IM.Sritys,IM.Darbai
WHERE	Srities_ident = Srities_ID AND
	Darbo_ident IS NULL #
	
CREATE UNIQUE INDEX Raktas_Sritis ON IM.Sritys(Sritis) #

CREATE UNIQUE INDEX Raktas_Darbas ON IM.Darbai(Darbas) #
	
