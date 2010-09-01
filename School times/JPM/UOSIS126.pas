{Sprendimo id�ja:
  Sprendimo id�ja remiasi gr��imo metodu. Bandome visus �enkl� sud�liojimo variantus ir jei gaunamas pageidaujamas rezultatas, tai j� i�vedame ir u�baigiame skai�iavimus.
   Tam, kad suma�intume veiksm� skai�i�, tarpiniai skai�iavimai atliekami dar prie� atliekant sekan�io �enklo parinkim�, o ne u�pild�ius visus �enklus. Tam realizuoti naudojami
   dukintaieji demuo ir reiskinys. Demuo atitinka sumavimo/atimties d�men�, t.y. jei einamoji operacija yra daugyba, dalyba arba liekana, tai atnaujinamas kintamasis duomuo, o reiskinys reik�m�
   nekei�iama. Ji pakei�iama tik tada kai einamoji operacija yra sud�tis arba atimtis. Tokiu b�du pasiekiame, kad daugyba, dalyba ir atimtis b�t� atliekami anks�iau nei sudtis ir atimtis.
   Visa kita yra papras�iausias vis� variant� perrinkimas}
program uzd6;
var  indeksas, r:integer;     
     ivedimas, isvedimas: text;
     rastas: boolean;
     zenklai: array[2..5] of string;
     skaiciai: array[1..5] of integer;

  {Padeda �enkl� � nurodyt� viet� ir atlieka su tuo reikalingus skai�iavimus}
  procedure DetiZenkla(demuo, reiskinys : int64;
                       zenklas : char;
                       zenkloVieta : integer);
  var naujasReiskinys: int64;
      i: 2..5;
  begin
    {Jei dar joks teisingas �enkl� i�d�stymas nebuvo rastas}
    if not rastas 
	then begin
	  {Atliekame operacija pagal duot� �enkl�}
      if zenklas = '+'
        then naujasReiskinys := reiskinys + demuo
        else naujasReiskinys := reiskinys - demuo;
	  {Jei dar visi �enklai nebuvo u�pildyti}
      if zenkloVieta < 6
      then begin
	    {Bandome d�ti visus galimus �enkl� variantus}
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
	    {Jei jau visi �enklai buvo u�pildyti ir gautas norimas rezultatas}
        if naujasReiskinys = r
        then begin
		  {Pa�ymime, kad rezultatas rastas ir i�vedame rezultatus}
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
