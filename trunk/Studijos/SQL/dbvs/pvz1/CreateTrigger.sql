-- sukuria darbuotojo nr.
CREATE TRIGGER Jole.NaujasDarbuotojoNr
  NO CASCADE BEFORE INSERT ON Jole.Darbuotojai
  REFERENCING NEW AS NaujasDarbuotojas
  FOR EACH ROW MODE DB2SQL
  SET NaujasDarbuotojas.ID = (SELECT COALESCE (MAX (ID), 0) + 1 
                              FROM Jole.Darbuotojai)
#

-- sukuria kliento nr.
CREATE TRIGGER Jole.NaujasKlientoNr
  NO CASCADE BEFORE INSERT ON Jole.Klientai
  REFERENCING NEW AS NaujasKlientas
  FOR EACH ROW MODE DB2SQL
  SET NaujasKlientas.ID = (SELECT COALESCE (MAX (ID), 0) + 1
                           FROM Jole.Klientai)
#

-- salinant kambarius, lenteleje 'Gyventojai' irasomi NULL
CREATE TRIGGER Jole.PanaikintiKamb
  AFTER DELETE ON Jole.Kambariai
  REFERENCING OLD AS SeniKambariai
  FOR EACH ROW MODE DB2SQL
  UPDATE Jole.Gyventojai
  SET Jole.Gyventojai.Kambarys = NULL
      WHERE Jole.Gyventojai.Kambarys = SeniKambariai.Nr
#

-- update'ina kambariu nr. lenteleje 'Gyventojai'
CREATE TRIGGER Jole.KurGyvena
  AFTER UPDATE OF Nr ON Jole.Kambariai
  REFERENCING OLD AS SeniKambariai
              NEW AS NaujiKambariai
  FOR EACH ROW MODE DB2SQL
  UPDATE Jole.Gyventojai
  SET Jole.Gyventojai.Kambarys = NaujiKambariai.Nr
      WHERE Jole.Gyventojai.Kambarys = SeniKambariai.Nr
#

-- klaida, jei priskiria NULL kambariui, kuriame kazkas gyvena
CREATE TRIGGER Jole.KambarioNr
  NO CASCADE BEFORE UPDATE OF Kambarys ON Jole.Gyventojai
  REFERENCING OLD AS SenasGyventojas
              NEW AS PakeistasGyvent
  FOR EACH ROW MODE DB2SQL
  WHEN ( (PakeistasGyvent.Kambarys IS NULL) AND ((SELECT Iki FROM Jole.Gyventojai
                                                  WHERE Jole.Gyventojai.ID = PakeistasGyvent.ID AND Jole.Gyventojai.Nuo = SenasGyventojas.Nuo) >= CURRENT TIMESTAMP) )
  SIGNAL SQLSTATE '99999' ('NEGALIMA PRISKIRTI "NULL" REIKSMES KAMBARIUI, KURIAME KAZKAS GYVENA!!!')
#

-- klaida, jei salinamajame kambaryje kazkas gyvena
CREATE TRIGGER Jole.KurGyvena2
  NO CASCADE BEFORE DELETE ON Jole.Kambariai
  REFERENCING OLD AS SeniKambariai
  FOR EACH ROW MODE DB2SQL
  WHEN ( (SELECT Iki FROM Jole.Gyventojai
          WHERE Kambarys = SeniKambariai.Nr) >= CURRENT TIMESTAMP)
  SIGNAL SQLSTATE '99999' ('KAMBARIO PASALINTI NEGALIMA, NES JAME KAZKAS GYVENA!!!')
#

-- tikrina ar kambary yra laisvu vietu //vykdant INSERT
CREATE TRIGGER Jole.NaujasGyventojas
  NO CASCADE BEFORE INSERT ON Jole.Gyventojai
  REFERENCING NEW AS NaujasZmogus
  FOR EACH ROW MODE DB2SQL
  WHEN ( (SELECT Laisvos_Vietos 
          FROM Jole.LaisvosVietos
          WHERE Jole.LaisvosVietos.Kambarys = NaujasZmogus.Kambarys) = 0)
  SIGNAL SQLSTATE '99999' ('SITAME KAMBARYJE NERA LAISVU VIETU!!!')
#

-- tikrina ar bus laisvu vietu visam apgyvendinimo laikotarpiui //vykdant INSERT
CREATE TRIGGER Jole.NaujasGyventojas2
  NO CASCADE BEFORE INSERT ON Jole.Gyventojai
  REFERENCING NEW AS NaujasZmogus
  FOR EACH ROW MODE DB2SQL
  WHEN ( ((SELECT SUM (Vietos) FROM Jole.Kambariai) -
          (COALESCE( (SELECT SUM (Vietos) FROM Jole.Rezervavimas
                      WHERE Jole.Rezervavimas.Nuo <= DATE (NaujasZmogus.Iki) AND Jole.Rezervavimas.Iki >= DATE (NaujasZmogus.Nuo)), 0) +
           COALESCE( (SELECT COUNT (ID) FROM Jole.Gyventojai
                      WHERE Jole.Gyventojai.Iki >= NaujasZmogus.Nuo), 0) ) ) < 1)
  SIGNAL SQLSTATE '99999' ('TOKIAM LAIKO TARPUI NERA LAISVU VIETU!!!')
#

-- tikrina ar galima apgyvendinti klienta
CREATE TRIGGER Jole.NaujasGyventojas3
  NO CASCADE BEFORE INSERT ON Jole.Gyventojai
  REFERENCING NEW AS NaujasZmogus
  FOR EACH ROW MODE DB2SQL
  WHEN ( (SELECT COUNT (Iki) FROM Jole.Gyventojai AS G
          WHERE G.ID = NaujasZmogus.ID AND G.Iki > CURRENT TIMESTAMP) > 0 )
  SIGNAL SQLSTATE '99999' ('SITAS KLIENTAS JAU GYVENA!!!')
#

-- apgyvendinant lauke 'Nuo' ivedama dabartine data ir laikas
CREATE TRIGGER Jole.NaujasGyventojas4
  NO CASCADE BEFORE INSERT ON Jole.Gyventojai
  REFERENCING NEW AS NaujasZmogus
  FOR EACH ROW MODE DB2SQL
  SET NaujasZmogus.Nuo = TIMESTAMP (CURRENT DATE, TIME (NaujasZmogus.Iki))
#

-- tikrina ar kambary yra laisvu vietu //vykdant UPDATE
CREATE TRIGGER Jole.AtnaujintiGyvent
  NO CASCADE BEFORE UPDATE OF Kambarys ON Jole.Gyventojai
  REFERENCING OLD AS SenasGyventojas
              NEW AS PakeistasGyvent
  FOR EACH ROW MODE DB2SQL
  WHEN ( ((SELECT Laisvos_Vietos FROM Jole.LaisvosVietos
           WHERE Jole.LaisvosVietos.Kambarys = PakeistasGyvent.Kambarys) = 0) AND
         ((SELECT Iki FROM Jole.Gyventojai
           WHERE Jole.Gyventojai.ID = PakeistasGyvent.ID AND Jole.Gyventojai.Nuo = SenasGyventojas.Nuo) >= CURRENT TIMESTAMP) AND
          (PakeistasGyvent.Kambarys <> SenasGyventojas.Kambarys) )
  SIGNAL SQLSTATE '99999' ('SITAME KAMBARYJE NERA LAISVU VIETU!!!')
#

-- tikrina ar bus laisvu vietu visam apgyvendinimo laikotarpiui //vykdant UPDATE
CREATE TRIGGER Jole.AtnaujintiGyvent2
  NO CASCADE BEFORE UPDATE OF Nuo, Iki ON Jole.Gyventojai
  REFERENCING NEW AS PakeistasGyvent
  FOR EACH ROW MODE DB2SQL
  WHEN ( ((SELECT SUM (Vietos) FROM Jole.Kambariai) -
          (COALESCE( (SELECT SUM (Vietos) FROM Jole.Rezervavimas
                      WHERE Jole.Rezervavimas.Nuo <= DATE (PakeistasGyvent.Iki) AND Jole.Rezervavimas.Iki >= DATE (PakeistasGyvent.Nuo)), 0) +
           COALESCE( (SELECT COUNT (ID) FROM Jole.Gyventojai
                      WHERE Jole.Gyventojai.Iki >= PakeistasGyvent.Nuo), 0) ) ) < 1)
  SIGNAL SQLSTATE '99999' ('TOKIAM LAIKO TARPUI NERA LAISVU VIETU!!!')
#

-- klaida, jei keiciamas jau nebegyvenancio gyventojo 'Iki'
CREATE TRIGGER Jole.SenasGyventojas
  NO CASCADE BEFORE UPDATE OF Iki ON Jole.Gyventojai
  REFERENCING OLD AS SenasGyventojas
              NEW AS PakeistasGyvent
  FOR EACH ROW MODE DB2SQL
  WHEN ( (SELECT Iki FROM Jole.Gyventojai
          WHERE Jole.Gyventojai.ID = PakeistasGyvent.ID AND Jole.Gyventojai.Nuo = SenasGyventojas.Nuo) < CURRENT TIMESTAMP)
  SIGNAL SQLSTATE '99999' ('SIO GYVENTOJO "Iki" KEISTI NEGALIMA!!!')
#

-- klaida, jei keiciamas 'Nuo'
CREATE TRIGGER Jole.Gyventojas
  NO CASCADE BEFORE UPDATE OF Nuo ON Jole.Gyventojai
  FOR EACH ROW MODE DB2SQL
  SIGNAL SQLSTATE '99999' ('GYVENTOJO "Nuo" KEISTI NEGALIMA!!!')
#

-- tikrina ar bus laisvu vietu visam rezervavimo laikotarpiui //vykdant INSERT
CREATE TRIGGER Jole.NaujasRezervavimas
  NO CASCADE BEFORE INSERT ON Jole.Rezervavimas
  REFERENCING NEW AS NaujasRez
  FOR EACH ROW MODE DB2SQL
  WHEN ( ((SELECT SUM (Vietos) FROM Jole.Kambariai) -
          (COALESCE( (SELECT SUM (Vietos) FROM Jole.Rezervavimas
                       WHERE Jole.Rezervavimas.Nuo <= NaujasRez.Iki AND Jole.Rezervavimas.Iki >= NaujasRez.Nuo), 0) +
           COALESCE( (SELECT COUNT (ID) FROM Jole.Gyventojai
                       WHERE DATE (Jole.Gyventojai.Iki) >= NaujasRez.Nuo), 0) ) ) < NaujasRez.Vietos)
  SIGNAL SQLSTATE '99999' ('TOKIAM LAIKO TARPUI NERA TIEK LAISVU VIETU!!!')
#

-- tikrina ar rezervuojant gerai nurode 'Nuo' ir 'Iki'
CREATE TRIGGER Jole.NaujasRezervav
  NO CASCADE BEFORE INSERT ON Jole.Rezervavimas
  REFERENCING NEW AS NaujasRez
  FOR EACH ROW MODE DB2SQL
  WHEN ( (NaujasRez.Nuo < CURRENT DATE) OR (NaujasRez.Iki <= CURRENT DATE) )
  SIGNAL SQLSTATE '99999' ('REZERVUOJANT NETEISINGAI NURODYTA "Nuo" ARBA "Iki"!!!')
#

-- tikrina ar bus laisvu vietu visam rezervavimo laikotarpiui //vykdant UPDATE
CREATE TRIGGER Jole.AtnaujintiRezerv
  NO CASCADE BEFORE UPDATE OF Vietos, Nuo, Iki ON Jole.Rezervavimas
  REFERENCING OLD AS SenasRez
              NEW AS PakeistasRez
  FOR EACH ROW MODE DB2SQL
  WHEN ( ((SELECT SUM (Vietos) FROM Jole.Kambariai) -
          (COALESCE( (SELECT SUM (Vietos) FROM Jole.Rezervavimas
                      WHERE Jole.Rezervavimas.Nuo <= PakeistasRez.Iki AND Jole.Rezervavimas.Iki >= PakeistasRez.Nuo), 0) +
           COALESCE( (SELECT COUNT (ID) FROM Jole.Gyventojai
                      WHERE DATE (Jole.Gyventojai.Iki) >= PakeistasRez.Nuo), 0) )
           + SenasRez.Vietos ) < PakeistasRez.Vietos)
  SIGNAL SQLSTATE '99999' ('TOKIAM LAIKO TARPUI NERA TIEK LAISVU VIETU!!!')
#

CREATE TRIGGER Jole.Sumoketa
  NO CASCADE BEFORE UPDATE OF Sumoketa ON Jole.Gyventojai
  REFERENCING NEW AS PakeistasGyvent
              OLD AS SenasGyvent
  FOR EACH ROW MODE DB2SQL
  WHEN ( PakeistasGyvent.Sumoketa > (SELECT Moketi FROM Jole.Saskaita AS S
                                     WHERE S.ID = SenasGyvent.ID AND S.Nuo = SenasGyvent.Nuo AND S.Iki = SenasGyvent.Iki) )
  SIGNAL SQLSTATE '99999' ('IVESTA PER DIDELE SUMA!!!')
#

CREATE TRIGGER Jole.DienuSkaicius
  NO CASCADE BEFORE INSERT ON Jole.Gyventojai
  REFERENCING NEW AS NaujasZmogus
  FOR EACH ROW MODE DB2SQL
  WHEN ( (DAYS (NaujasZmogus.Iki) - DAYS (NaujasZmogus.Nuo)) < 1 )
  SIGNAL SQLSTATE '99999' ('NEGALIMA APGYVENDINTI KLIENTO MAZIAU NEI 1 DIENAI!!!')
#

CREATE TRIGGER Jole.DienuSkaicius2
  NO CASCADE BEFORE INSERT ON Jole.Rezervavimas
  REFERENCING NEW AS NaujasRez
  FOR EACH ROW MODE DB2SQL
  WHEN ( (DAYS (NaujasRez.Iki) - DAYS (NaujasRez.Nuo)) < 1 )
  SIGNAL SQLSTATE '99999' ('NEGALIMA REZERVUOTI MAZIAU NEI 1 DIENAI!!!')
#

CREATE TRIGGER Jole.DienuSkaicius3
  NO CASCADE BEFORE UPDATE OF Iki ON Jole.Gyventojai
  REFERENCING NEW AS PakeistasGyvent
  FOR EACH ROW MODE DB2SQL
  WHEN ( (DAYS (PakeistasGyvent.Iki) - DAYS (PakeistasGyvent.Nuo)) < 1 )
  SIGNAL SQLSTATE '99999' ('NEGALIMA APGYVENDINTI KLIENTO MAZIAU NEI 1 DIENAI!!!')
#

CREATE TRIGGER Jole.DienuSkaicius4
  NO CASCADE BEFORE UPDATE OF Nuo, Iki ON Jole.Rezervavimas
  REFERENCING NEW AS PakeistasRez
  FOR EACH ROW MODE DB2SQL
  WHEN ( (DAYS (PakeistasRez.Iki) - DAYS (PakeistasRez.Nuo)) < 1 )
  SIGNAL SQLSTATE '99999' ('NEGALIMA REZERVUOTI MAZIAU NEI 1 DIENAI!!!')
#

CREATE TRIGGER Jole.DirbaNuo
  NO CASCADE BEFORE INSERT ON Jole.Darbuotojai
  REFERENCING NEW AS NaujasDarb
  FOR EACH ROW MODE DB2SQL
  SET NaujasDarb.Dirba_Nuo = CURRENT DATE
#

CREATE TRIGGER Jole.DirbaNuo2
  NO CASCADE BEFORE UPDATE OF Dirba_Nuo ON Jole.Darbuotojai
  FOR EACH ROW MODE DB2SQL
  SIGNAL SQLSTATE '99999' ('NEGALIMA KEISTI IDARBINIMO DATOS!!!')
#

CREATE TRIGGER Jole.KlientoReg
  NO CASCADE BEFORE INSERT ON Jole.Klientai
  REFERENCING NEW AS NaujasKlientas
  FOR EACH ROW MODE DB2SQL
  SET NaujasKlientas.Data = CURRENT DATE
#

CREATE TRIGGER Jole.KlientoReg2
  NO CASCADE BEFORE UPDATE OF Data ON Jole.Klientai
  FOR EACH ROW MODE DB2SQL
  SIGNAL SQLSTATE '99999' ('NEGALIMA KEISTI REGISTRAVIMO DATOS!!!')
#

CREATE TRIGGER Jole.Rezervavimas
  NO CASCADE BEFORE INSERT ON Jole.Rezervavimas
  REFERENCING NEW AS NaujasRez
  FOR EACH ROW MODE DB2SQL
  SET NaujasRez.Data = CURRENT DATE
#

CREATE TRIGGER Jole.Rezervavimas2
  NO CASCADE BEFORE UPDATE OF ID, Vietos, Nuo, Iki, Rezervavo, Data ON Jole.Rezervavimas
  REFERENCING NEW AS PakeistRez
  FOR EACH ROW MODE DB2SQL
  SET PakeistRez.Data = CURRENT DATE
#