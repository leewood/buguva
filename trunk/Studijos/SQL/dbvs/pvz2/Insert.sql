INSERT INTO IM.Sritys (Srities_ID,Sritis,Prioritetas)
	VALUES (0,'OOP',1)#
	
INSERT INTO IM.Sritys (Srities_ID,Sritis,Prioritetas)
	VALUES (0,'Duomenu_bazes',1)#
	
INSERT INTO IM.Sritys (Srities_ID,Sritis,Prioritetas)
	VALUES (0,'Matematika',3)#
	
INSERT INTO IM.Sritys (Srities_ID,Sritis,Prioritetas)
	VALUES (0,'Programavimas_Java',1)#
	
	

INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-03-01','penktadienis')#
	
INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-03-20','treciadienis')#
	
INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-03-15','penktadienis')#
	
INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-04-10','treciadienis')#
	
INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-04-06','sestadienis')#

INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-05-29','treciadienis')#
	
INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-04-03','treciadienis')#
	
INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-04-04','ketvirtadienis')#
	
INSERT INTO IM.Diena (Data,Savaites_diena)
	VALUES ('2002-04-05','penktadienis')#
	
	

INSERT INTO IM.Darbai (Darbo_ID,Darbas,Ar_atliktas,Pradzia,Pabaiga)
	VALUES (0,'Multiple_dispatching','taip','2002-03-01','2002-04-10')#
	
INSERT INTO IM.Darbai (Darbo_ID,Darbas,Ar_atliktas,Pradzia,Pabaiga)
	VALUES (0,'Logistinis_modelis','taip','2002-03-20','2002-04-06')#

INSERT INTO IM.Darbai (Darbo_ID,Darbas,Ar_atliktas,Pradzia,Pabaiga)
	VALUES (0,'Duomenu_baze','ne','2002-03-15','2002-05-29')#

	
	
INSERT INTO IM.Dirbimas (Dirbimo_ID,Laikas_val,Srities_ident,Darbo_ident)
	VALUES (0,3.3,0,0) #
	
INSERT INTO IM.Dirbimas (Dirbimo_ID,Laikas_val,Srities_ident,Darbo_ident)
	VALUES (0,5,1,2) #
	
INSERT INTO IM.Dirbimas (Dirbimo_ID,Laikas_val,Srities_ident,Darbo_ident)
	VALUES (0,10.5,2,1) #
	
INSERT INTO IM.Dirbimas (Dirbimo_ID,Laikas_val,Srities_ident,Darbo_ident)
	VALUES (0,1,3,NULL) #
	
	
INSERT INTO IM.Fiksavimas(Fiksavimo_ID,Dirbimo_ident,Datos_ident)
	VALUES(0,0,'2002-04-03')#
	
INSERT INTO IM.Fiksavimas(Fiksavimo_ID,Dirbimo_ident,Datos_ident)
	VALUES(0,1,'2002-04-03')#
	
INSERT INTO IM.Fiksavimas(Fiksavimo_ID,Dirbimo_ident,Datos_ident)
	VALUES(0,2,'2002-04-04')#
	
INSERT INTO IM.Fiksavimas(Fiksavimo_ID,Dirbimo_ident,Datos_ident)
	VALUES(0,3,'2002-04-05')#
	
	

INSERT INTO IM.Priklausymas(Srities_ident,Darbo_ident)
	VALUES(0,0)#
	
INSERT INTO IM.Priklausymas(Srities_ident,Darbo_ident)
	VALUES(2,2)#
	
INSERT INTO IM.Priklausymas(Srities_ident,Darbo_ident)
	VALUES(1,1)#
	
	
SELECT * FROM IM.DIENA #
SELECT * FROM IM.SRITYS #
SELECT * FROM IM.DARBAI #	
SELECT * FROM IM.DIRBIMAS #
SELECT * FROM IM.FIKSAVIMAS #
SELECT * FROM IM.PRIKLAUSYMAS #      