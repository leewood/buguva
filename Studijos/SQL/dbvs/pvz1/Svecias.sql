-- Sakiniai, skirti sveèiams:
-- 1. Klientai
SELECT Pavarde, Salis FROM Jole.Klientai
#

-- 2. Paslaugos
SELECT Pavadinimas, Kaina FROM Jole.Paslaugos
#

-- 3. Kambariai:
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