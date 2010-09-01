CREATE TABLE Jole.Darbuotojai (
  ID           SMALLINT        NOT NULL,
  Pavarde      CHAR (20)       NOT NULL,
  Gime         DATE            NOT NULL,
  Telefonas    CHAR (15),
  Kortele      CHAR (20)       NOT NULL,
  Korteles_Nr  CHAR (16)       NOT NULL,
  Pareigos     CHAR (20)       NOT NULL,
  Alga         DECIMAL (8, 2)  NOT NULL  CHECK (Alga >= 0),
  Dirba_Nuo    DATE            NOT NULL,

  PRIMARY KEY (ID) )
#

CREATE INDEX Jole.Darb_Pavarde ON Jole.Darbuotojai (Pavarde)
#

CREATE UNIQUE INDEX Jole.Darb_Kortele
ON Jole.Darbuotojai (Kortele, Korteles_Nr)
#

CREATE TABLE Jole.Paslaugos (
  Pavadinimas  CHAR (15)       NOT NULL,
  Kaina        DECIMAL (5, 2)  NOT NULL  CHECK (Kaina >= 0),
  Aptarnauja   SMALLINT        NOT NULL,

  PRIMARY KEY (Pavadinimas),

  FOREIGN KEY Kas_Aptarnauja (Aptarnauja)
  REFERENCES Jole.Darbuotojai
    ON DELETE RESTRICT
    ON UPDATE RESTRICT )
#

CREATE TABLE Jole.Klientai (
  ID           SMALLINT   NOT NULL,
  Pavarde      CHAR (20)  NOT NULL,
  Kortele      CHAR (20)  NOT NULL,
  Korteles_Nr  CHAR (16)  NOT NULL,
  Telefonas    CHAR (15),
  Salis        CHAR (15)  NOT NULL,
  Registravo   SMALLINT,
  Data         DATE       NOT NULL,

  PRIMARY KEY (ID),

  FOREIGN KEY Kas_Registravo (Registravo)
  REFERENCES Jole.Darbuotojai
    ON DELETE SET NULL
    ON UPDATE RESTRICT )
#

CREATE INDEX Jole.KlientoPavarde ON Jole.Klientai (Pavarde)
#

CREATE UNIQUE INDEX Jole.KlientoKortele
ON Jole.Klientai (Kortele, Korteles_Nr)
#

CREATE TABLE Jole.Kambariai (
  Nr      SMALLINT        NOT NULL  CHECK (Nr > 0),
  Vietos  SMALLINT        NOT NULL  CHECK ((Vietos > 0) AND (Vietos <= 4)),
  Kaina   DECIMAL (5, 2)  NOT NULL  CHECK (Kaina > 0),
  Tvarko  SMALLINT        NOT NULL,

  PRIMARY KEY (Nr),

  FOREIGN KEY Kas_Tvarko (Tvarko)
  REFERENCES Jole.Darbuotojai
    ON DELETE RESTRICT
    ON UPDATE RESTRICT )
#

CREATE TABLE Jole.Gyventojai (
  Kambarys  SMALLINT,
  ID        SMALLINT        NOT NULL,
  Nuo       TIMESTAMP       NOT NULL,
  Iki       TIMESTAMP       NOT NULL,
  Sumoketa  DECIMAL (6, 2)  NOT NULL  CHECK (Sumoketa >= 0),   

  PRIMARY KEY (ID, Nuo, Iki),

  FOREIGN KEY Kas_Gyvena (ID)
  REFERENCES Jole.Klientai
    ON DELETE CASCADE
    ON UPDATE RESTRICT )
#

CREATE TABLE Jole.Rezervavimas (
  ID         SMALLINT   NOT NULL,
  Vietos     SMALLINT   NOT NULL  CHECK (Vietos > 0),
  Nuo        DATE       NOT NULL,
  Iki        DATE       NOT NULL,
  Rezervavo  SMALLINT,
  Data       DATE       NOT NULL,

  PRIMARY KEY (ID, Nuo, Iki),

  FOREIGN KEY Kas_Uzrezervavo (Rezervavo)
  REFERENCES Jole.Darbuotojai
    ON DELETE SET NULL
    ON UPDATE RESTRICT,

  FOREIGN KEY Kas_Uzsirezervavo (ID)
  REFERENCES Jole.Klientai
    ON DELETE CASCADE
    ON UPDATE RESTRICT )
#

CREATE VIEW Jole.Saskaita AS (
  SELECT G.ID, Pavarde, Kortele, Korteles_Nr, Kambarys, Nuo, Iki,
         DAY (Iki - Nuo) AS Dienos, DAY (Iki - Nuo) * Kaina AS Moketi, Sumoketa
  FROM Jole.Gyventojai AS G, Jole.Kambariai, Jole.Klientai AS K
  WHERE Nr = Kambarys AND K.ID = G.ID
UNION
  SELECT G.ID, Pavarde, Kortele, Korteles_Nr, Kambarys, Nuo, Iki, DAY (Iki - Nuo) AS Dienos, Kambarys AS Moketi, Sumoketa
  FROM Jole.Gyventojai AS G, Jole.Kambariai, Jole.Klientai AS K
  WHERE Kambarys IS NULL AND K.ID = G.ID )
#

CREATE VIEW Jole.LaisvosVietos AS
  SELECT Nr AS Kambarys, Vietos,  Vietos - (COALESCE ( (SELECT COUNT (ID) FROM Jole.Gyventojai
                                                        WHERE Iki >= CURRENT TIMESTAMP AND Kambarys = Nr), 0) ) AS Laisvos_Vietos, Kaina
  FROM Jole.Kambariai
#