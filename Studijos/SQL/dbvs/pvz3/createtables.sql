CREATE TABLE aNdRoJdAz.Sesutes (

	ID		SMALLINT	NOT NULL	CHECK (ID > 0),
	Vardas		CHAR(15)	NOT NULL,
	Pavarde		VARCHAR(20)	NOT NULL,
	Dirba_Nuo	DATE		NOT NULL,
	Alga		DECIMAL(7,2)	NOT NULL	CONSTRAINT MinimaliALga
							  CHECK (Alga >= 550),
	Telefonas	CHAR(15),

	PRIMARY KEY (ID)
)#

CREATE TABLE aNdRoJdAz.Palatos (

	Nr		SMALLINT	NOT NULL	CHECK (Nr > 0),
	Vietu_Sk	SMALLINT	NOT NULL	CONSTRAINT PalatuDydis
							  CHECK ((Vietu_Sk >= 1) AND (Vietu_Sk <= 6)),
	Sesute		SMALLINT	NOT NULL,

	PRIMARY KEY (Nr),

	FOREIGN KEY Priziuri (Sesute)
	  REFERENCES aNdRoJdAz.Sesutes
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT
)#

CREATE TABLE aNdRoJdAz.Gydytojai (

	ID		SMALLINT	NOT NULL	CHECK (ID > 0),
	Vardas		CHAR(15)	NOT NULL,
	Pavarde		VARCHAR(20)	NOT NULL,
	Dirba_Nuo	DATE		NOT NULL,
	Alga		DECIMAL(7,2)	NOT NULL	CONSTRAINT MinimaliALga
							  CHECK (Alga >= 550),
	Pareigos	VARCHAR(20),
	Telefonas	CHAR(15),
	El_Pastas	VARCHAR(30),

	PRIMARY KEY (ID)
)#

CREATE TABLE aNdRoJdAz.Bukles (

	Bukle		CHAR(9)		NOT NULL,

	PRIMARY KEY (Bukle)
)#

CREATE TABLE aNdRoJdAz.Teistumai (


	Teistumas	CHAR(9)		NOT NULL,

	PRIMARY KEY (Teistumas)
)#


CREATE TABLE aNdRoJdAz.Ligoniai (

	ID		SMALLINT	NOT NULL	CHECK (ID > 0),
	Vardas		CHAR(15)	NOT NULL,
	Pavarde		VARCHAR(20)	NOT NULL,
	AK		CHAR(11)	NOT NULL,
	SoDros_Nr	CHAR(9)		NOT NULL,
	Gydosi_Nuo	DATE		NOT NULL,
	Gydytojas_Gydo	SMALLINT	NOT NULL,
	Palata		SMALLINT	NOT NULL,
	Miestas		VARCHAR(20),
	Bukle		CHAR(9)		NOT NULL,
	Teistumas	CHAR(9)		NOT NULL,

	PRIMARY KEY (ID),

	FOREIGN KEY Gydo (Gydytojas_Gydo)
	  REFERENCES aNdRoJdAz.Gydytojai
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT,

	FOREIGN KEY Guli (Palata)
	  REFERENCES aNdRoJdAz.Palatos
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT,

	FOREIGN KEY Bukles_Pav (Bukle)
	  REFERENCES aNdRoJdAz.Bukles
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT,

	FOREIGN KEY Teistumas_Pav (Teistumas)
	  REFERENCES aNdRoJdAz.Teistumai
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT
)#

CREATE TABLE aNdRoJdAz.Medikamentai (

	Kodas		CHAR(10)	NOT NULL,
	Pavadinimas	VARCHAR(25)	NOT NULL,

	PRIMARY KEY (Kodas)
)#

CREATE TABLE aNdRoJdAz.Apsilankymai (

	ID		SMALLINT	NOT NULL	CHECK (ID > 0),
	Data		DATE		NOT NULL,
	Ligonis		SMALLINT	NOT NULL,
	Gydytojas_Skyre	SMALLINT	NOT NULL,

	PRIMARY KEY (ID),

	FOREIGN KEY Skirti (Ligonis)
	  REFERENCES aNdRoJdAz.Ligoniai
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT,

	FOREIGN KEY Skiria (Gydytojas_Skyre)
	  REFERENCES aNdRoJdAz.Gydytojai
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT
)#

CREATE TABLE aNdRoJdAz.Receptai (

	ID		SMALLINT	NOT NULL	CHECK (ID > 0),	
	Kodas		CHAR(10)	NOT NULL,

	PRIMARY KEY (ID,Kodas),

	FOREIGN KEY Apsilank_ID (ID)
	  REFERENCES aNdRoJdAz.Apsilankymai
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT,	

	FOREIGN KEY Med_Kodas (Kodas)
	  REFERENCES aNdRoJdAz.Medikamentai
	  ON DELETE RESTRICT
	  ON UPDATE RESTRICT
)#
