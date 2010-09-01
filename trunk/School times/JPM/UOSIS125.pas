{Sprendimo id�ja:
  Visu pirma susidarome metoda patikrinti ar du �od�iai pakeitus vien� raid� juose tampa lyg�s. Tada gr��imo metodo pagalba formuojame visas galimas metagram� grandines ir sustojame jei suformuojame 
  sqlyg� tenkinan�i� grandin�. Formavimas vyksta taip
   Vis� pirma imame pat� pirm�j� �od� ir dedame � grandin�. Po to imame visus �od�ius nuo 2 iki n ir tikriname ar jie yra metagrama su pirmuoju �od�iu. Jei yra �odis pa�ymimas kaip panaudotas
   ir �dedamas � grandin�. Tada jau imame visi �od�iai nuo 2 iki n ir bandoma ar jie metagrama su tuo prie� tai paimtu �odiu ir t.t. Kai visi variantai patikrinami, gr��tama vienu �ingsniu atgal, imamas 
  kitas �odis ir v�l tikrinami visi variantai.}   

program uzd5;
var n: integer; {�od�i� skai�ius}
    zodziai: array[1..20] of string; {Saugo duotus �od�ius}
    rastas: boolean;    {Saugo ar jau rasta s�lyg� tenkinanti metagram� grandin�}
    jauPaimti: set of 1..20; {Saugo kokie �od�iai jau buvo paimti}    
	
{Patikrina ar du duoti �od�iai skiriasi tik viena raide}
  function ArMetagrama(zodis1, zodis2: string): boolean;
  var pozicija:integer;
      laikZodis1, laikZodis2: string;
	  rezultatas: boolean;
  begin
    rezultatas := false;
    pozicija := 1;
	{B�game per visas �od�io raides} 
    while pozicija <= length(zodis1) and not rezultatas do
    begin
      laikZodis1 := zodis1;
      laikZodis2 := zodis2;
	  {Pa�aliname vien� raid� ir �i�rime ar �od�iai tapo lyg�s} 
      Delete(laikZodis1, pozicija, 1);
      Delete(laikZodis2, pozicija, 1);
      if laikZodis1 = laikZodis2 then
        rezultatas := true;
	  pozicija := pozicija + 1; 
    end;
	ArMetagrama := rezultatas;
  end;

  {Atieka metagram� grandin�s pailginim� vienu elementu}
  procedure ImtiZodi(priesTaiPaimtas, imamas: integer);
  var tikrinami: integer;
  begin
    {Jei dar nebuvo rasta s�lyg� tenkinanti metagra� grandin�}
    if not rastas 
	then
	  {Jei toks �odis dar nebuvo paimtas ir sudaro metagram� su prie� tai paimtu �od�iu}
      if not (imamas in jauPaimti) and (ArMetagrama(zodziai[priesTaiPaimtas], zodziai[imamas])) 
	  then begin
	    {Pa�ymime �od� kaip paimt�}
        jauPaimti := jauPaimti + [imamas];
		{Jei jau pa�mem antr�j� �od�, vadinasi metagram� grandin� susidare ir spendimas baigtas}
        if imamas = 2 
		then rastas := true
        else
		{Kitu atveju bandome prie jau suformuotos grandin�s taikyti kitus �od�ius}
          for tikrinami := 2 to n do
            ImtiZodi(imamas, tikrinami);
        jauPaimti := jauPaimti - [imamas];
      end;
  end;

  {Nuskaito pradinius duomenis}
  procedure SkaitytiPradiniusDuomenis(failoVardas: string);
  var indeksas: integer;
      failas: text;
  begin
    Assign(failas, failoVardas);
    Reset(failas);
    ReadLn(failas, n);
    for indeksas := 1 to n do
      ReadLn(failas, zodziai[indeksas]);
    Close(failas);
	jauPaimti := [];
	rastas := false;
  end;  

  {I�veda rezultatus}
  procedure IsvestiRezultatus(failoVardas: string);
  var failas: text;
  begin
    Assign(failas, failoVardas);
    ReWrite(failas);
    if rastas = true 
	  then Write(failas, 'GALIMA')
      else Write(failas, 'NEGALIMA');
    Close(failas);
  end;
  
begin
  SkaitytiPradiniusDuomenis('duom.txt'); 
  ImtiZodi(1, 1);
  IsvestiRezultatus('rez.txt');
end.
