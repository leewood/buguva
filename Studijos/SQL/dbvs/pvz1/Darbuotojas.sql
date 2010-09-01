-- Sakiniai, skirti darbuotojams:
-- 1. Klient� registravimas:
-- a) info apie klientus
SELECT K.ID, K.Pavarde, K.Kortele, K.Korteles_Nr, K.Telefonas, Salis, D.Pavarde AS Registravo, Registravo AS Darb_ID, Data
FROM Jole.Klientai AS K, Jole.Darbuotojai AS D WHERE Registravo = D.ID
#
-- b) info apie klient�
SELECT K.ID, K.Pavarde, K.Kortele, K.Korteles_Nr, K.Telefonas, Salis, D.Pavarde AS Registravo, Registravo AS Darb_ID, Data
FROM Jole.Klientai AS K, Jole.Darbuotojai AS D WHERE Registravo = D.ID AND K.ID = 1
#
-- c) u�registruoti nauj� klient�
INSERT INTO Jole.Klientai
VALUES (0, 'Kuzmickas', 'Visa Electron', '2000200020002008', '+37061060008', 'Lietuva', 3, CURRENT DATE)
#
-- d) pakeisti info apie klient�
UPDATE Jole.Klientai SET Telefonas = '+37067020200' WHERE ID = 8
#
-- e) pa�alinti klient�
DELETE FROM Jole.Klientai WHERE ID = 8
#

-- 2. Klient� apgyvendinimas:
-- a) dabar gyvenantys klientai
SELECT Kambarys, G.ID, Pavarde, Nuo, Iki
FROM Jole.Gyventojai AS G, Jole.Klientai AS K
WHERE G.ID = K.ID AND Iki >= CURRENT TIMESTAMP
#
-- b) klientai, gyven� anks�iau
SELECT Kambarys, G.ID, Pavarde, Nuo, Iki
FROM Jole.Gyventojai AS G, Jole.Klientai AS K
WHERE G.ID = K.ID AND Iki < CURRENT TIMESTAMP
#
-- c) apgyvendinti klient�
INSERT INTO Jole.Gyventojai 
VALUES (24, 6, CURRENT TIMESTAMP, CURRENT TIMESTAMP + 10 DAYS, 0)
#
-- d) pakeisti info apie gyventoj�
UPDATE Jole.Gyventojai SET Iki = Iki - 3 DAYS
WHERE ID = 6 AND DATE (Nuo) = CURRENT DATE
#
-- e) pa�alinti gyventoj�
DELETE FROM Jole.Gyventojai WHERE ID = 6 AND DATE (Nuo) = CURRENT DATE
AND DATE (Iki) = CURRENT DATE + 7 DAYS
#
-- f) dabartin� s�skaita
SELECT ID, Pavarde, Kortele, Korteles_Nr, Kambarys, Nuo, Iki, Dienos, Moketi, Sumoketa
FROM Jole.Saskaita WHERE Iki >= CURRENT TIMESTAMP
#
-- g) ankstesn� s�skaita
SELECT ID, Pavarde, Kortele, Korteles_Nr, Kambarys, Nuo, Iki, Dienos, Moketi, Sumoketa
FROM Jole.Saskaita WHERE Iki < CURRENT TIMESTAMP
#
-- h) gyventojo s�skaita
SELECT ID, Pavarde, Kortele, Korteles_Nr, Kambarys, Nuo, Iki, Dienos, Moketi, Sumoketa
FROM Jole.Saskaita WHERE ID = 1
#
-- j) apmok�ti gyventojo s�skait�
UPDATE Jole.Gyventojai SET Sumoketa = (SELECT Moketi FROM Jole.Saskaita WHERE ID = 1)
WHERE ID = 1 AND DATE (Nuo) = CURRENT DATE AND DATE (Iki) = (CURRENT DATE + 3 DAYS)
#

-- 3. Viet� rezervavimas:
-- a) rezervuotos vietos
SELECT R.ID, K.Pavarde, Vietos, Nuo, Iki, D.Pavarde AS Rezervavo, Rezervavo AS Darb_ID, R.Data
FROM Jole.Rezervavimas AS R, Jole.Klientai AS K, Jole.Darbuotojai AS D
WHERE R.ID = K.ID AND Rezervavo = D.ID
#
-- b) kliento rezervuotos vietos
SELECT R.ID, K.Pavarde, Vietos, Nuo, Iki, D.Pavarde AS Rezervavo, Rezervavo AS Darb_ID, R.Data
FROM Jole.Rezervavimas AS R, Jole.Klientai AS K, Jole.Darbuotojai AS D
WHERE R.ID = K.ID AND Rezervavo = D.ID AND K.ID = 1
#
-- c) naujas rezervavimas
INSERT INTO Jole.Rezervavimas
VALUES (6, 4, CURRENT DATE + 7 DAYS, CURRENT DATE + 14 DAYS, 3, CURRENT DATE)
#
-- d) pakeisti rezervavim�
UPDATE Jole.Rezervavimas SET Iki = (CURRENT DATE + 10 DAYS)
WHERE ID = 6 AND Nuo = (CURRENT DATE + 7 DAYS) AND Iki = (CURRENT DATE + 14 DAYS)
#
-- e) panaikinti rezervavim�
DELETE FROM Jole.Rezervavimas
WHERE ID = 6 AND Nuo = (CURRENT DATE + 7 DAYS) AND Iki = (CURRENT DATE + 10 DAYS)
#

-- 4. Paslaugos
-- a) teikiamos paslaugos
SELECT Pavadinimas, Kaina, Pavarde AS Aptarnauja, Aptarnauja AS Darb_ID
FROM Jole.Paslaugos, Jole.Darbuotojai WHERE Aptarnauja = ID
#
-- b) nauja paslauga
INSERT INTO Jole.Paslaugos VALUES ('Uzkandis', 5, 5)
#
-- c) pakeisti paslaug�
UPDATE Jole.Paslaugos SET Kaina = 6 WHERE Pavadinimas = 'Uzkandis'
#
-- d) panaikinti paslaug�
DELETE FROM Jole.Paslaugos WHERE Pavadinimas = 'Uzkandis'
#

-- 5. Kambariai:
-- a) info apie kambarius
SELECT Nr, Vietos, Kaina, Pavarde AS Tvarko, Tvarko AS Darb_ID
FROM Jole.Kambariai, Jole.Darbuotojai WHERE Tvarko = ID
#
-- b) laisvos vietos kambariuose
SELECT Kambarys, Vietos, Laisvos_Vietos, Kaina FROM Jole.LaisvosVietos
#
-- c) laisv� viet� sk. tam tikram laikotarpiui
SELECT SUM (Vietos) -
       (COALESCE( (SELECT SUM (Vietos) FROM Jole.Rezervavimas
                   WHERE Jole.Rezervavimas.Nuo <= CURRENT DATE + 3 DAYS AND Jole.Rezervavimas.Iki >= CURRENT DATE), 0) +
        COALESCE( (SELECT COUNT (ID) FROM Jole.Gyventojai
                   WHERE DATE (Jole.Gyventojai.Iki) >= CURRENT DATE), 0) ) AS Laisvos_Vietos
FROM Jole.Kambariai
#
-- d) naujas kambarys
INSERT INTO Jole.Kambariai VALUES (20, 1, 100, 6)
#
-- e) pakeisti info apie kambar�
UPDATE Jole.Kambariai SET Tvarko = 7 WHERE Nr = 20
#
-- f) panaikinti kambar�
DELETE FROM Jole.Kambariai WHERE Nr = 20
#

-- 6. Darbuotojai:
-- a) info apie darbuotojus
SELECT ID, Pavarde, Gime, Telefonas, Kortele, Korteles_Nr, Pareigos, Alga, Dirba_Nuo
FROM Jole.Darbuotojai
#
-- b) info apie darbuotoj�
SELECT ID, Pavarde, Gime, Telefonas, Kortele, Korteles_Nr, Pareigos, Alga, Dirba_Nuo
FROM Jole.Darbuotojai WHERE ID = 1
#
-- c) naujas darbuotojas
INSERT INTO Jole.Darbuotojai
VALUES (0, 'Kuzmickas', '1952.06.29', '+37061120009', 'Visa Electron', '1000100010001009', 'Virejas', 1000, CURRENT DATE)
#
-- d) pakeisti info apie darbuotoj�
UPDATE Jole.Darbuotojai SET Alga = 1100 WHERE ID = 9
#
-- e) pa�alinti darbuotoj�
DELETE FROM Jole.Darbuotojai WHERE ID = 9
#