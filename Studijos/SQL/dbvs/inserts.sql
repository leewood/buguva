INSERT INTO Stud.Knyga VALUES('9998-01-101-9','Duomenu bazes',           'Baltoji',   2000, 212, NULL ) ;
INSERT INTO Stud.Knyga VALUES('9998-01-102-7','Programavimo kalbos',     'Baltoji',   2001, 401, 22.50) ;
INSERT INTO Stud.Knyga VALUES('9999-02-202-2','Operacines sistemos',     'Juodoji',   2001, 356, 19.90) ;
INSERT INTO Stud.Knyga VALUES('9999-02-203-0','Transliavimo metodai',    'Juodoji',   2002, 495, 30.00) ;
INSERT INTO Stud.Knyga VALUES('9998-01-103-5','Objektinis programavimas','Baltoji',   2001, 356, 19.90) ;
INSERT INTO Stud.Knyga VALUES('9997-03-303-5','Informacines sistemos',   'Raudonoji', 2001, 254, 15.50) ;
INSERT INTO Stud.Knyga VALUES('9998-01-104-3','Kompiuteriu tinklai',     'Baltoji',   2002, 455, 35.20) ;


INSERT INTO Stud.Autorius VALUES('9998-01-101-9', 'Jonas',   'Jonaitis'  ) ;
INSERT INTO Stud.Autorius VALUES('9998-01-101-9', 'Petras',  'Petraitis' ) ;
INSERT INTO Stud.Autorius VALUES('9998-01-102-7', 'Pijus',   'Jonaitis'  ) ;
INSERT INTO Stud.Autorius VALUES('9999-02-202-2', 'Onute',   'Jonaityte' ) ;
INSERT INTO Stud.Autorius VALUES('9999-02-202-2', 'Jonas',   'Petraitis' ) ;
INSERT INTO Stud.Autorius VALUES('9999-02-202-2', 'Jonas',   'Jonaitis'  ) ;
INSERT INTO Stud.Autorius VALUES('9999-02-203-0', 'Juozas',  'Juodakis'  ) ;
INSERT INTO Stud.Autorius VALUES('9999-02-203-0', 'Antanas', 'Antanaitis') ;
INSERT INTO Stud.Autorius VALUES('9998-01-103-5', 'Maryte',  'Grazulyte' ) ;
INSERT INTO Stud.Autorius VALUES('9998-01-103-5', 'Janina',  'Jonaityte' ) ;
INSERT INTO Stud.Autorius VALUES('9997-03-303-5', 'Simas',   'Simaitis'  ) ;
INSERT INTO Stud.Autorius VALUES('9997-03-303-5', 'Petras',  'Petraitis' ) ;
INSERT INTO Stud.Autorius VALUES('9997-03-303-5', 'Simas',   'Baltakis'  ) ;
INSERT INTO Stud.Autorius VALUES('9998-01-104-3', 'Jonas',   'Petraitis' ) ;


INSERT INTO Stud.Skaitytojas VALUES(1000, '38001010222', 'Jonas',   'Petraitis', '1987-01-01', 'Tiesioji 1-10' ) ;
INSERT INTO Stud.Skaitytojas VALUES(1001, '38002200102', 'Jonas',   'Onaitis',   '1987-02-28', 'Lenktoji 20' ) ;
INSERT INTO Stud.Skaitytojas VALUES(1010, '48103021111', 'Milda',   'Onaityte',  '1988-03-02', 'Didzioji 21-5' ) ;
INSERT INTO Stud.Skaitytojas VALUES(1015, '48206301011', 'Onute',   'Petraityte','1989-06-30', 'Didzioji 21-5' ) ;
INSERT INTO Stud.Skaitytojas VALUES(1021, '38105301031', 'Petras',  'Jonaitis',  '1988-05-30', 'Mazoji 1' ) ;
INSERT INTO Stud.Skaitytojas VALUES(1032, '38112310031', 'Tadas',   'Onaitis',   '1988-12-31', 'Tiesioji 12' ) ;
INSERT INTO Stud.Skaitytojas VALUES(1033, '48111300131', 'Grazina', 'Petraityte','1988-11-30', 'Tiesioji 1-10' ) ;


INSERT INTO Stud.Egzempliorius VALUES(32101, '9998-01-101-9', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32102, '9998-01-101-9', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32103, '9998-01-101-9', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32104, '9998-01-101-9', 1000, '2007-09-02', '2007-10-05' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32105, '9998-01-101-9', 1010, '2007-09-14', '2007-10-04' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32106, '9998-01-101-9', 1021, '2007-09-14', '2007-10-04' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32107, '9998-01-101-9', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32108, '9998-01-101-9', 1001, '2007-09-02', '2007-10-14' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32109, '9998-01-101-9', 1032, '2007-09-20', '2007-10-05' ) ;


INSERT INTO Stud.Egzempliorius VALUES(32200, '9998-01-102-7', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32201, '9998-01-102-7', 1021, '2007-09-02', '2007-10-04' ) ;

INSERT INTO Stud.Egzempliorius VALUES(32301, '9999-02-202-2', 1000, '2007-09-02', '2007-09-15' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32302, '9999-02-202-2', 1021, '2007-09-14', '2007-10-04' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32303, '9999-02-202-2', NULL, NULL, NULL ) ;


INSERT INTO Stud.Egzempliorius VALUES(32330, '9999-02-203-0', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32331, '9999-02-203-0', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32332, '9999-02-203-0', 1010, '2007-09-14', '2007-10-05' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32333, '9999-02-203-0', 1015, '2007-09-14', '2007-10-05' ) ;

INSERT INTO Stud.Egzempliorius VALUES(32401, '9998-01-103-5', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32402, '9998-01-103-5', 1032, '2007-09-20', '2007-10-05' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32403, '9998-01-103-5', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32404, '9998-01-103-5', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32405, '9998-01-103-5', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32406, '9998-01-103-5', 1010, '2007-09-14', '2007-10-04' ) ;

INSERT INTO Stud.Egzempliorius VALUES(32501, '9997-03-303-5', 1010, '2007-09-14', '2007-10-04' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32502, '9997-03-303-5', 1032, '2007-09-20', '2007-10-04' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32503, '9997-03-303-5', 1015, '2007-09-20', '2007-10-05' ) ;
INSERT INTO Stud.Egzempliorius VALUES(32504, '9997-03-303-5', 1001, '2007-09-06', '2007-10-05' ) ;

INSERT INTO Stud.Egzempliorius VALUES(32601, '9998-01-104-3', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32602, '9998-01-104-3', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32603, '9998-01-104-3', NULL, NULL, NULL ) ;
INSERT INTO Stud.Egzempliorius VALUES(32604, '9998-01-104-3', NULL, NULL, NULL ) ;

