-- Sakiniai, skirti klientams:
-- 1. Klientø registravimas:
-- a) klientai
SELECT Pavarde, Salis FROM Jole.Klientai
#
-- b) info apie klientà
SELECT K.ID, K.Pavarde, K.Kortele, K.Korteles_Nr, K.Telefonas, Salis, D.Pavarde AS Registravo, Registravo AS Darb_ID, Data
FROM Jole.Klientai AS K, Jole.Darbuotojai AS D
WHERE Registravo = D.ID AND K.ID = 1
#

-- 2. Kliento apgyvendinimas:
-- a) apgyvendinimas dabar
SELECT Kambarys, G.ID, Pavarde, Nuo, Iki
FROM Jole.Gyventojai AS G, Jole.Klientai AS K
WHERE G.ID = K.ID AND Iki >= CURRENT TIMESTAMP AND G.ID = 1
#
-- b) apgyvendinimas anksèiau
SELECT Kambarys, G.ID, Pavarde, Nuo, Iki
FROM Jole.Gyventojai AS G, Jole.Klientai AS K
WHERE G.ID = K.ID AND Iki < CURRENT TIMESTAMP AND G.ID = 1
#
-- c) kliento sàskaita
SELECT ID, Pavarde, Kortele, Korteles_Nr, Kambarys, Nuo, Iki, Dienos, Moketi, Sumoketa
FROM Jole.Saskaita
WHERE ID = 1
#

-- 3. Kliento rezervuotos vietos
SELECT R.ID, K.Pavarde, Vietos, Nuo, Iki, D.Pavarde AS Rezervavo, Rezervavo AS Darb_ID, Data
FROM Jole.Rezervavimas AS R, Jole.Klientai AS K, Jole.Darbuotojai AS D
WHERE R.ID = K.ID AND Rezervavo = D.ID AND K.ID = 1
#

-- 4. Paslaugos
SELECT Pavadinimas, Kaina FROM Jole.Paslaugos
#

-- 5. Kambariai:
-- a) laisvos vietos kambariuose
SELECT Kambarys, Vietos, Laisvos_Vietos, Kaina FROM Jole.LaisvosVietos
#
-- b) laisvø vietø sk. tam tikram laikotarpiui
SELECT SUM (Vietos) -
       (COALESCE( (SELECT SUM (Vietos) FROM Jole.Rezervavimas
                   WHERE Jole.Rezervavimas.Nuo <= CURRENT DATE + 3 DAYS AND Jole.Rezervavimas.Iki >= CURRENT DATE), 0) +
        COALESCE( (SELECT COUNT (ID) FROM Jole.Gyventojai
                   WHERE DATE (Jole.Gyventojai.Iki) >= CURRENT DATE), 0) ) AS Laisvos_Vietos
FROM Jole.Kambariai
#