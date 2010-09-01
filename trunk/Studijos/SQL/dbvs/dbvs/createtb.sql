CREATE TABLE kauo4157.Card (
    Nr          INTEGER       NOT NULL 
                              GENERATED ALWAYS AS IDENTITY
                              (START WITH 0, INCREMENT BY 1),
    Name        VARCHAR(20)   NOT NULL ,
    Description VARCHAR(1000), 
    Attack      SMALLINT,
    Defense     SMALLINT,
    Type        SMALLINT,
    Stars       SMALLINT,
    Use_Times   SMALLINT      NOT NULL CHECK(Use_Times > 0),
    PRIMARY KEY (Nr)
);

CREATE TABLE kauo4157.User (
   ID          INTEGER       NOT NULL 
                             GENERATED ALWAYS AS IDENTITY
                             (START WITH 0, INCREMENT BY 1),
   Username    VARCHAR(10)   NOT NULL,
   Password    VARCHAR(15)   NOT NULL,
   Level       SMALLINT      NOT NULL CHECK (Level >0 AND Level < 100),
   X           INTEGER       NOT NULL CHECK (Pos_X >= 0),
   Y           INTEGER       NOT NULL CHECK (Pos_Y >= 0),
   MapNr       INTEGER       NOT NULL CHECK (Pos_Map >= 0),
   PRIMARY KEY (ID)
);

CREATE TABLE kauo4157.Inventory (
   ID          INTEGER       NOT NULL 
                             GENERATED ALWAYS AS IDENTITY
                             (START WITH 0, INCREMENT BY 1),
   Type        SMALLINT      NOT NULL CHECK (Type > 0),
   Size        SMALLINT      NOT NULL CHECK (Size > 0 AND Size < 5000),
   PRIMARY KEY (ID)
);

CREATE TABLE kauo4157.Item (
   Nr          INTEGER       NOT NULL 
                             GENERATED ALWAYS AS IDENTITY
                             (START WITH 0, INCREMENT BY 1),
   Position    SMALLINT      NOT NULL CHECK (Position > 0),
   Usage       SMALLINT      NOT NULL CHECK (Usage >= 0) DEFAULT 0,
   Card        INTEGER       NOT NULL,
   Inventory   INTEGER,
   PRIMARY KEY (Nr),
   FOREIGN KEY FCard(Card) REFERENCES kauo4157.Card ON DELETE CASCADE ON UPDATE RESTRICT,
   FOREIGN KEY FOwn(Inventory) REFERENCES kauo4157.Inventory ON DELETE CASCADE ON UPDATE RESTRICT
);

CREATE TABLE kauo4157.UserVsInventory (
   ID          INTEGER       NOT NULL 
                             GENERATED ALWAYS AS IDENTITY
                             (START WITH 0, INCREMENT BY 1),
   Inventory   INTEGER,
   User        INTEGER,
   PRIMARY KEY (ID),
   FOREIGN KEY FInv(Inventory) REFERENCES kauo4157.Inventory ON DELETE CASCADE ON UPDATE RESTRICT,
   FOREIGN KEY FUser(User) REFERENCES kauo4157.User ON DELETE CASCADE ON UPDATE RESTRICT
);