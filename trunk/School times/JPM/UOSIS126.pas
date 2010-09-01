{Sprendimo idëja:
  Sprendimo idëja remiasi gráþimo metodu. Bandome visus þenklø sudëliojimo variantus ir jei gaunamas pageidaujamas rezultatas, tai já iðvedame ir uþbaigiame skaièiavimus.
   Tam, kad sumaþintume veiksmø skaièiø, tarpiniai skaièiavimai atliekami dar prieð atliekant sekanèio þenklo parinkimà, o ne uþpildþius visus þenklus. Tam realizuoti naudojami
   dukintaieji demuo ir reiskinys. Demuo atitinka sumavimo/atimties dëmená, t.y. jei einamoji operacija yra daugyba, dalyba arba liekana, tai atnaujinamas kintamasis duomuo, o reiskinys reikðmë
   nekeièiama. Ji pakeièiama tik tada kai einamoji operacija yra sudëtis arba atimtis. Tokiu bûdu pasiekiame, kad daugyba, dalyba ir atimtis bûtø atliekami anksèiau nei sudtis ir atimtis.
   Visa kita yra paprasèiausias visø variantø perrinkimas}
program uzd6;
var  indeksas, r:integer;     
     ivedimas, isvedimas: text;
     rastas: boolean;
     zenklai: array[2..5] of string;
     skaiciai: array[1..5] of integer;

  {Padeda þenklà á nurodytà vietà ir atlieka su tuo reikalingus skaièiavimus}
  procedure DetiZenkla(demuo, reiskinys : int64;
                       zenklas : char;
                       zenkloVieta : integer);
  var naujasReiskinys: int64;
      i: 2..5;
  begin
    {Jei dar joks teisingas þenklø iðdëstymas nebuvo rastas}
    if not rastas 
	then begin
	  {Atliekame operacija pagal duotà þenklà}
      if zenklas = '+'
        then naujasReiskinys := reiskinys + demuo
        else naujasReiskinys := reiskinys - demuo;
	  {Jei dar visi þenklai nebuvo uþpildyti}
      if zenkloVieta < 6
      then begin
	    {Bandome dëti visus galimus þenklø variantus}
        zenklai[zenkloVieta] := '+';
        DetiZenkla(skaiciai[zenkloVieta], naujasReiskinys, '+', zenkloVieta + 1);
        zenklai[zenkloVieta] := '-';
        DetiZenkla(skaiciai[zenkloVieta], naujasReiskinys, '-', zenkloVieta + 1);                                                     
        zenklai[zenkloVieta] := '*';
        DetiZenkla(demuo * skaiciai[zenkloVieta], reiskinys, zenklas, zenkloVieta + 1);
        zenklai[zenkloVieta] := 'div';
        DetiZenkla(demuo div skaiciai[zenkloVieta], reiskinys, zenklas, zenkloVieta + 1);
        zenklai[zenkloVieta] := 'mod';
        DetiZenkla(demuo mod skaiciai[zenkloVieta], reiskinys, zenklas, zenkloVieta + 1);
      end else
	    {Jei jau visi þenklai buvo uþpildyti ir gautas norimas rezultatas}
        if naujasReiskinys = r
        then begin
		  {Paþymime, kad rezultatas rastas ir iðvedame rezultatus}
          rastas := true;
          for i := 2 to 5 do 
		    Write(isvedimas, sk[i - 1],' ', t[i],' ');
          WriteLn(isvedimas, sk[5], ' = ', r);
        end;
    end;
  end;


begin
  Assign(ivedimas, 'duom.txt');
  Reset(ivedimas);
  for indeksas := 1 to 5 do
    Read(ivedimas, skaiciai[a]);
  end;
  Read(ivedimas, r);
  Close(ivedimas);
  Assign(isvedimas, 'rez.txt');
  ReWrite(isvedimas);
  DetiZenkla(skaiciai[1], 0, '+', 2);
  if not(rastas) 
    then WriteLn(isvedimas, 'negalima');
  Close(isvedimas);
end.
