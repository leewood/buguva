{Sprendimo idëja:
  Visu pirma susidarome metoda patikrinti ar du þodþiai pakeitus vienà raidæ juose tampa lygûs. Tada gráþimo metodo pagalba formuojame visas galimas metagramø grandines ir sustojame jei suformuojame 
  sqlygà tenkinanèià grandinæ. Formavimas vyksta taip
   Visø pirma imame patá pirmàjá þodá ir dedame á grandinæ. Po to imame visus þodþius nuo 2 iki n ir tikriname ar jie yra metagrama su pirmuoju þodþiu. Jei yra þodis paþymimas kaip panaudotas
   ir ádedamas á grandinæ. Tada jau imame visi þodþiai nuo 2 iki n ir bandoma ar jie metagrama su tuo prieð tai paimtu þodiu ir t.t. Kai visi variantai patikrinami, gráþtama vienu þingsniu atgal, imamas 
  kitas þodis ir vël tikrinami visi variantai.}   

program uzd5;
var n: integer; {Þodþiø skaièius}
    zodziai: array[1..20] of string; {Saugo duotus þodþius}
    rastas: boolean;    {Saugo ar jau rasta sàlygà tenkinanti metagramø grandinë}
    jauPaimti: set of 1..20; {Saugo kokie þodþiai jau buvo paimti}    
	
{Patikrina ar du duoti þodþiai skiriasi tik viena raide}
  function ArMetagrama(zodis1, zodis2: string): boolean;
  var pozicija:integer;
      laikZodis1, laikZodis2: string;
	  rezultatas: boolean;
  begin
    rezultatas := false;
    pozicija := 1;
	{Bëgame per visas þodþio raides} 
    while pozicija <= length(zodis1) and not rezultatas do
    begin
      laikZodis1 := zodis1;
      laikZodis2 := zodis2;
	  {Paðaliname vienà raidæ ir þiûrime ar þodþiai tapo lygûs} 
      Delete(laikZodis1, pozicija, 1);
      Delete(laikZodis2, pozicija, 1);
      if laikZodis1 = laikZodis2 then
        rezultatas := true;
	  pozicija := pozicija + 1; 
    end;
	ArMetagrama := rezultatas;
  end;

  {Atieka metagramø grandinës pailginimà vienu elementu}
  procedure ImtiZodi(priesTaiPaimtas, imamas: integer);
  var tikrinami: integer;
  begin
    {Jei dar nebuvo rasta sàlygà tenkinanti metagraø grandinë}
    if not rastas 
	then
	  {Jei toks þodis dar nebuvo paimtas ir sudaro metagramà su prieð tai paimtu þodþiu}
      if not (imamas in jauPaimti) and (ArMetagrama(zodziai[priesTaiPaimtas], zodziai[imamas])) 
	  then begin
	    {Paþymime þodá kaip paimtà}
        jauPaimti := jauPaimti + [imamas];
		{Jei jau paëmem antràjá þodá, vadinasi metagramø grandinë susidare ir spendimas baigtas}
        if imamas = 2 
		then rastas := true
        else
		{Kitu atveju bandome prie jau suformuotos grandinës taikyti kitus þodþius}
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

  {Iðveda rezultatus}
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
