CREATE TABLE Stud.Knyga (            
    ISBN        CHAR(13)    NOT NULL,
    Pavadinimas VARCHAR(32) NOT NULL,
    Leidykla    CHAR(12)    NOT NULL,
    Metai       SMALLINT    NOT NULL CONSTRAINT LeidimoMetai CHECK(Metai > 1900 AND Metai < 2010),
    Puslapiai   SMALLINT    NOT NULL CHECK(Puslapiai > 0 AND Puslapiai < 2000),
    Verte       DECIMAL(5,2) CHECK(Verte > 0 AND Verte < 1000),
    PRIMARY KEY (ISBN)
);

CREATE TABLE Stud.Autorius (             
    ISBN        CHAR(13)    NOT NULL,
    Vardas      CHAR(12)    NOT NULL,
    Pavarde     VARCHAR(20) NOT NULL,
    PRIMARY KEY (ISBN,Vardas,Pavarde),
    FOREIGN KEY Knygai(ISBN) REFERENCES Stud.Knyga ON DELETE CASCADE ON UPDATE RESTRICT
);

CREATE TABLE Stud.Skaitytojas (             
    Nr          INTEGER     NOT NULL,
    AK          CHAR(11)    NOT NULL,
    Vardas      CHAR(12)    NOT NULL,
    Pavarde     VARCHAR(20) NOT NULL,
    Gimimas     DATE        NOT NULL,
    Adresas     VARCHAR(32),
    PRIMARY KEY (Nr)
);

CREATE TABLE Stud.Egzempliorius (             
    Nr          INTEGER     NOT NULL,
    ISBN        CHAR(13)    NOT NULL,
    Skaitytojas INTEGER,
    Paimta      DATE,
    Grazinti    DATE,
    PRIMARY KEY (Nr),
    FOREIGN KEY Knygai(ISBN)             REFERENCES Stud.Knyga       ON DELETE CASCADE ON UPDATE RESTRICT,
    FOREIGN KEY Skaitytojui(Skaitytojas) REFERENCES Stud.Skaitytojas ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT  GrazintiVeliau CHECK(Grazinti >= Paimta) 
);

