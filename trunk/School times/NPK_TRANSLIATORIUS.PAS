program transl(input, output);
  label 55;
  const
        bsk = 30; vrdilg = 10; lmax = 100; kodmax = 200;
        tmax = 255; kmax = 21; stdydis = 100;
  type simbolis = (skai, vard, tekst, plius, minus, zvaigzdute,
                   dalsv, dalliek, sauktukas, kskliaust, dskliaust,
                   kablelis, kabliatask, taskas, dvitask, priskyrimas,
                   programa, pradzia, pabaiga, kint, jei, tai, kitaip,
                   kol, atlikti, skait, atsak, rom, lyg, nelyg, daug,
                   maz, daugl, mazl, logi, int, taip, ne);
       vardas = string [vrdilg]; tipas = (nera, romen, log, svei);
       sima = set of simbolis (*simbolitf aibe*);
       komanda = packed record f: 0..kmax; y: integer end;
  var simb: simbolis;
      vrd: vardas;
      vieta: integer;
      reiksme: integer;
      kiekz: integer;
      ilg: integer; (*nagti?iejamos eilutes ilgis*)
      zsk: integer; (*zenklt( skaitliukas*)
      preil: array [1..60] of char;
      ch: char;
      psk: integer;
      kl: boolean;
      progvard: vardas;
      bloksimb, daugsimb, saksimb: sima;
      baz: array [1..bsk] of vardas;
      pran: array[1..22] of string;
      bsi: array [1..bsk] of simbolis;
      sps: array [char] of simbolis;
      lent, tx: integer;
      lentele: array [0..lmax] of packed record
                                         pavadin: vardas;
                                         tip: tipas;
                                         adr: integer
                                   end;
      tlent: string [tmax];
      kodas: array [0..kodmax] of komanda;
procedure zenklas;
  begin
   if zsk = ilg then begin
     if eof (input) then begin
        writeln;
        writeln ('programa nebaigta');
        halt(0);
     end;
     ilg:= 1;
     zsk := 0;
     while not eoln(input) do begin
       ilg := ilg+1; read(ch); preil[ilg]:= ch
     end;
     ilg:= ilg + 1;
     read (preil[ilg])
   end;
   zsk:= zsk + 1;
   ch:= preil[zsk];
  { write(ch)}
 end (*zenklas*);

procedure klaida(n: integer);
  var k: integer;
  begin
    k := 1;
    if kl then kl:= false;
    while k <> n do
      k := k + 1;
    write ('???', pran[k], '???')
  end;

function pozic (var ch: char): integer;
  var r, a1, a2, a3, kiek: integer;
  begin
    r := 0; a1 := 1; a2 := 0; a3 := 0; kiek := 0;
    while ch in ['I', 'V', 'X', 'L', 'C', 'D', 'M'] do begin
      if (kiek <= 3)and ((a2 >= a3) or (a2 <> a1) or (a1 = 0)) then begin
         a1 := a2; a2 := a3;
         case ch of
          'M': a3 := 1000;
          'D': a3 := 500;
          'C': a3 := 100;
          'L': a3 := 50;
          'X': a3 := 10;
          'V': a3:=5;
          'I': a3:=1
         end (*case*);
         if a1 = a3 then kiek := kiek + 1 else kiek := 1;
         if (a2 < a3) then r := r - a2 else r := r + a2;
         zenklas
      end else
        while ch in ['I', 'V', 'X', 'L', 'C', 'D', 'M'] do zenklas end;
      if (kiek> 3) or ((a2 < a3) and (a2 = a1) and (a2 <> 0))
         then pozic := 0 else begin r := r + a3; pozic:= r end
    end; (*/>or/c*)

procedure leksema;
  label 5;
  var j, x, code: integer;
  begin (*leksema*)
  5:
    while ch = '' do zenklas;
    if ch in ['I', 'V',  'X', 'L', 'C', 'D', 'M'] then
      begin
        reiksme := pozic(ch);
        simb := skai;
        if reiksme = 0 then klaida (1)
      end else
        if (ch in ['a'.. 'z']) then begin
           VRD := ch;
           zenklas;
           while (CH in ['a'.. 'z',  '0' .. '9']) do begin
             if length(vrd) < vrdilg then vrd := vrd + ch; zenklas
           end;
           while length(vrd) < vrdilg do VRD := vrd + '  ';
           j := 1;
           while (baz[j] <> VRD) and (j <  bsk) do j :=j + 1;
           if  baz[j] = vrd then simb := bsi[J] else simb := vard
        end else if (ch in ['0' .. '9']) then begin
           reiksme := 0;
           while (ch in ['0' .. '9']) do
             begin
               val(''+ch,x, code);
               reiksme := reiksme * 10 + x;
               zenklas;
             end;
           simb := skai;
        end else
        case ch of
        ''''   :   begin
                 reiksme := length (tlent); j := 0;
                 repeat
                   zenklas;
                   tx := length (tlent);
                   if ch <> '''' then begin
                     if tx + 1 = tmax then klaida(2)
                                      else begin tlent:= tlent + ch;
                                                 j := j + 1
                                      end
                     end
                 until ch = '''';
                 simb := tekst;
                 kiekz :=j;
                 zenklas
                end;
        ':' :   begin
                  zenklas;
                  if ch = '=' then begin
                     simb := priskyrimas; zenklas
                  end
                  else simb := dvitask
                end;
        '<':  begin
                Zenklas;
                if ch = '=' then
                   begin simb := mazl; zenklas end
                   else
                     if ch = '  > ' then
                        begin simb := nelyg; zenklas end
                        else simb := maz
               end;
        '>':  begin
                Zenklas;
                if ch = '=' then
                   begin simb := daugl; zenklas end
                   else simb := daug
              end;
        '+', '-', '*', '$', '#', '(', ')', '.', ',', ';', '=', '!':
           begin simb := sps[ch]; zenklas end
        else zenklas;
        goto 5
      end (*c«.s<?*)
  end (*leksema*);

procedure gen (fv: integer);
  begin
    if psk = kodmax then klaida(3)
                    else kodas[psk].f := fv;
    psk := psk+ 1
  end (*-gen ?*);

procedure gen1 (fv, a : integer);
  begin
    if psk = kodmax then klaida(3)
      else
        with kodas [psk] do
          begin f :=fv; y := a end;
    psk := psk + 1
  end (*£c//l*);


procedure spausdinti;
  var j: integer;
      k: komanda;
  begin
    writeln;
    writeln ('ÉÍÍÍÍËkintamasisËÍtipasËÍÍÍadresas»');
    for j := 1 to lent do
      with lentele[j] do
        writeln('º',j:2, '  º' , pavadin: 10, 'º   ',  ord(tip):1, '  º    ' , adr:3, '   º');
    writeln ('ÈÍÍÍÍÊÍÍÍÍÍÍÍÍÍÍÊÍÍÍÍÍÍÊÍÍÍÍÍÍÍÍÍÍ¼');
    writeln ('    kodas:');
    for  j := 0 to psk-1 do
      begin
        if j mod 5 = 0 then begin writeln; write(j: 5) end;
        k := kodas[j]; write(k.f: 5);
        if (k.f < 3) or (k.f = 16) or (k.f = 17) or (k.f= 19)
           then write(k.y: 2) else write (' ');
        write (' ,')
      end;
    writeln
  end (*spausdinti*);

procedure blokas (sustok: sima);

procedure persokti (sustok: sima; n: integer);
  begin
    klaida (n);
    while not (simb in sustok) do leksema
  end (*persokti*);

procedure ieskoti (s1, s2: sima; n: integer);
  begin
    if not (simb in s1) then persokti (s1 + s2, n)
  end (*ieskoti*);

procedure iterpti(vrd: vardas);
  var j: integer;
  begin
    if lent = lmax then klaida(5)
      else begin
        lentele[0].pavadin := vrd (*sargas*);
        j := lent;
        while lentele[j].pavadin <> vrd do j :=j-1;
        if j <> 0 then klaida(5) else
          begin
            lent := lent + 1;
            with lentele[lent] do
              begin pavadin:= vrd; tip := nera; adr := 0 end
          end
      end
  end (*jterpti*);

function kelintas (vrd: vardas): integer;
  var j: integer;
  begin
    lentele[0].pavadin := vrd (*sargas*);
    j := lent;
    while lentele[j].pavadin <> vrd do
      j := j-1;
    if j = 0 then klaida(6);
    kelintas := j
  end (*kelintas*);

procedure aprasas;
  var l0, l1: integer;
      tp: tipas;
  begin
    leksema;
    while simb = vard do begin
      l0 := lent;
      iterpti(vrd);
      leksema;
      while simb = kablelis do begin
        leksema;
        if  simb = vard then iterpti(vrd) else klaida(7);
        leksema
      end;
      if simb = dvitask then leksema else klaida(8);
      l1 := lent;
      ieskoti ([rom, logi, int], sustok, 9);
      if (simb <> rom) and (simb <> logi) and (simb <> int) then tp := nera
        else begin
          if simb = rom then tp:= romen else if simb = int then tp := svei
             else tp := log;
          leksema
        end;
      while l0 < l1 do begin
        l0 := l0 + 1;
        with lentele[l0] do
          begin tip := tp; adr := vieta; vieta := vieta + 1   end
        end;
      if simb = kabliatask then leksema else klaida(10)
    end
  end (*aprasas*);

procedure sakinys (sustok : sima);
  var j: integer;

  function reztipas(a, b: tipas): tipas;
    begin
      if ((a = romen)or(a=svei)) and ((b = romen)or(b=svei))
         then reztipas:= romen
         else reztipas := nera
    end (*reztipas *);

procedure lreiskinys (sustok: sima; var z: tipas);
  var y: tipas; op: simbolis;
procedure reiskinys (sustok: sima; var z: tipas);
  var op: simbolis;
      y: tipas;
procedure demuo (sustok: sima; var z: tipas);
  var  op: simbolis;
       y: tipas;

procedure laipsnis(sustok: sima; var z: tipas);

procedure daugiklis(sustok: sima; var z: tipas);
  var j: integer;
  begin
    z := nera;
    ieskoti(daugsimb, sustok, 11);
    while simb in daugsimb do begin
      if simb = vard then begin
        j := kelintas(vrd);
        leksema; z:= lentele[j].tip;
        gen1(0, lentele[j]. adr)
      end else if simb = skai then
        begin gen1(1, reiksme); leksema; z:= romen
        end else if simb = kskliaust then begin
          leksema; reiskinys (sustok+ [dskliaust] , z);
          if simb = dskliaust then leksema else klaida(12)
         end;
      ieskoti (sustok, daugsimb, 13)
    end (*WHILE*)
  end (*daugiklis*);

 begin (*laipsnis*)
   daugiklis (sustok+[sauktukas], z);
   if simb = sauktukas then begin
     leksema;
     if simb <> skai then begin klaida(14); z := nera end
        else gen1 (2, reiksme); leksema end
     end (*laipsnis*);

 begin (*d emuo*)
   laipsnis (sustok+[zvaigzdute, dalsv, dalliek], z);
   while simb in [zvaigzdute, dalsv, dalliek] do begin
     op := simb; leksema;
     laipsnis(sustok+ [zvaigzdute, dalsv, dalliek], y);
     if op = zvaigzdute then begin
       z := reztipas(z, y);
       if ((z = romen) or (z = svei)) then gen(3)
       end
       else begin
         if ((z = romen) or (z = svei)) and ((y = romen) or (y = svei))
            then if op = dalsv then gen(4)
                               else gen (5)
            else z := nera
       end
     end
 end (*demuo*);

 begin (*reiskinys*)
   if simb in [plius, minus] then begin
      op := SIMB; leksema;
      demuo(sustok + [plius, minus], z);
      if op = minus then gen(6)
   end
   else demuo(sustok + [plius, minus], z);
   while simb in [plius, minus] do begin
     op :=simb; leksema;
     demuo(sustok + [plius, minus], y);
     z := reztipas (z, y);
     if (z = romen)or(z = svei) then if op = plius then gen(7) else gen(8)
   end
 end (*rciskinys*);

 begin (*lreiskinys*)
   reiskinys(sustok+[lyg, nelyg, maz, daug, mazl, daugl, taip, ne], z);
   if simb in [lyg, nelyg, maz, daug, mazl, daugl, taip, ne] then begin
     op :=simb; leksema; reiskinys(sustok,y);
     if (z = y)or(((z=svei)or(z=romen))and((y=svei)or(y=romen))) then
       case op of
         lyg:      gen(9);
         nelyg:   gen(10);
         maz:     gen(11);
         daug:   gen(12);
         mazl:    gen(13);
         daugl:  gen(14);
         taip: gen(23);
         ne: gen(24);
       end;
     z := log
   end
 end (*lreiskinys*);

procedure priskirti (ad: integer);
  var z, y: tipas;
  begin
    z := lentele[j].tip;
    gen1(1, ad);
    if simb = priskyrimas then leksema
       else begin klaida(15); if simb = lyg then leksema
       end;
    lreiskinys (sustok, y);
    if (z = y) or ((z = svei) and (y = romen)) or ((z = romen) and (y = svei)) then gen(15)
             else if (z <> nera) and (y <> nera) then klaida(15)
  end (*priskirti*);

procedure sudsak;
  begin
    leksema;
    sakinys ([kabliatask, pabaiga]+sustok);
    while simb in [kabliatask]+saksimb do begin
      if simb = kabliatask then leksema else klaida(10);
      sakinys ([kabliatask, pabaiga] + sustok)
    end;
    if simb = pabaiga then leksema else klaida(16)
  end (*sudsak*);

procedure jeisak;
  var z: tipas; psk1, psk2: integer;
  begin
    leksema; lreiskinys(sustok+[tai], z);
    if not(z in [log, nera]) then klaida(17);
    psk1 := psk;
    gen(16) (*sqlygiiiis nukrcipimas*);
    if simb = tai then leksema else klaida(18);
    sakinys(sustok+ [kitaip]);
    if simb = kitaip then begin
      leksema; psk2:= psk; gen(17);
      kodas[psk1].y := psk;
      sakinys(sustok);
      kodas[psk2].y := psk
    end
    else kodas[psk1].y := psk
  end (*jcisak*);

procedure kolsak;
  var z: tipas; psk1, psk2: integer;
  begin
    leksema;
    psk1 := psk;
    lreiskinys(sustok + [atlikti], z);
    if not (z in [log, nera]) then klaida(17);
    psk2 :=psk; gen(16) (*sqlyginis nukrcipimas-':*);
    if simb = atlikti then leksema else klaida(19);
    sakinys (sustok);
    gen1(17, psk1 ); kodas[psk2].y := psk
  end (*ko/sak*);

procedure skaitsak;
  var z: tipas; j: integer;
  begin
    leksema;
    if simb = kskliaust
      then begin
        repeat
          leksema;
          if simb <> vard
            then klaida(20)
            else begin
              j := kelintas(vrd);
              if j <> 0 then begin
                z := lentele[j].tip;
                gen1(1, lentele[j].adr);
                if (z = romen) or (z = svei) then begin gen( 18); gen( 15) end;
                leksema
              end
            end
        until simb <> kablelis;
       if simb = dskliaust then leksema else klaida(12)
     end
  end (*skaitsak*);

procedure atsaksak;
  var z: tipas;
  begin
    leksema;
    if simb = kskliaust then
      begin
        repeat
          leksema;
          if simb = tekst then
            begin gen1(1, kiekz); gen1(19, reiksme); leksema end
            else begin
              lreiskinys(sustok+[kablelis, dskliaust], z);
              if z = romen then gen(20) else if z = svei then gen(22)
              else if z = log then gen(25)
            end
        until simb <> kablelis;
        if simb = dskliaust then leksema else klaida(12)
      end
  end (*atsaksah*);

  begin (*sakinys*)
    if simb in saksimb + [vard] then
      case simb of
        vard:	begin
                  j := kelintas(vrd); leksema;
                  if  j <> 0 then priskirti(lentele[j].adr)
                end;
        pradzia:      sudsak;
        jei:	jeisak;
        kol:	kolsak;
        skait:	skaitsak;
        atsak:	atsaksak
     end (*case*)
  end (*sakinys*);

  begin (*blokas*)
    vieta := 3;
    ieskoti ([kabliatask], sustok, 10);
    leksema;
     if simb = kint then aprasas;
     if simb = pradzia then
       begin
         leksema;
         sakinys([kabliatask, pabaiga] + sustok);
         while simb in [kabliatask] + saksimb do
           begin
             if simb = kabliatask then leksema else klaida(10);
             sakinys ([kabliatask, pabaiga]+ sustok)
           end;
         if simb = pabaiga then leksema else klaida(16);
       end;
     ieskoti(sustok + [taskas], [], 13)
  end (*blokas*);

procedure interpret;
  var kom: komanda;
      k, p, vs, h1, h2, x, code: integer;
      eilute: string[tmax];
      bus: (gerai, stperpild, dalklaida, skaitkl, viskas);
      s: array [1.. stdydis] of record
                                case tipas of
                                  romen: (j: integer);
                                  log:  ( lg: boolean)
                                end;

  procedure roma(s: integer; alfa, beta, gama: char);
    begin
      while h1 >= 10 * s do
        begin write(alfa); h1 := h1-10*s end;
      if h1 >= 9*s then
        begin write(gama, alfa); h1 := h1 - 9 * s end;
      if h1 >= 5 * s then begin write(beta); h1 := h1 - 5 * s end;
      if h1 >= 4 * s then
        begin write(gama, beta); h1 := h1 - 4 * S end
    end (*roma*);

  begin (*interpret*)
    bus := gerai;
    p := 0;
    s[1].lg := true;
    s[2].lg := false;
    vs := vieta - 1;
    repeat
      kom := kodas[p];
      p := p + 1;
      case kom.f of
        0: begin (*kintamojo reiksmes jrasymas / stekq*) vs := VS+ 1;
             if vs > stdydis then bus := stperpild else s[vs].j := s[kom.y].j
           end;
        1: begin (*skaiciaus reiksmes ar kintamojo adreso jrasymas i stekq*)
             vs := vs + 1;
             if vs > stdydis then bus := stperpild else s[vs].j := kom.y
           end;
(*aritmeti?ies operacijos*)
        2: begin (*kelimas laipsntit*)
             h1 := s[vs].j; h2 := kom.y;
             repeat
               s[vs].j := s[vs].j * h1;
               h2 := h2 - 1
             until h2 = 1
           end;
        3: begin
             vs := vs - 1;
             s[vs].j := s[vs].j * s[vs+1].j
           end;
        4: begin
             vs := vs - 1;
             If s[vs + 1].j = 0
                then bus := dalklaida else s[vs].j := s[VS].J div s[vs+1].j
           end;
        5: begin
             vs := vs - 1;
             if s[vs+1].j = 0
               then bus := dalklaida else s[vs].j := s[vs].j mod s[vs + 1].j
           end;
        6: s[vs].j := -s[vs].j;
        7: begin
             VS := vs - 1; s[vs].j := s[VS].j + s[vs + 1].j
           end;
        8: begin
             vs := vs -1; s[vs].j := s[vs].j - s[vs + 1].j
           end;
    (*lyginimo operacijos*)
        9: begin
             vs := vs - 1; s[vs].lg := s[vs].j = s[vs + 1].j
           end;
        10: begin
              vs := vs - 1; s[vs].lg := s[vs].j <> s[vs + 1].j
            end;
        11: begin
              vs := vs - 1; s[vs].lg := s[vs].j < s[vs + 1].j
            end;
        12: begin
              vs := vs - 1; s[vs].lg := s[vs].j > s[vs + 1].j end;
        13: begin
              vs := vs - 1; s[vs].lg := s[vs].j <= s[vs + 1].j end;
        14: begin
              vs := vs - 1; s[vs].lg := s[vs].j >= s[vs + 1].j end;
        15: begin (*reiksmes irasymas adresu, esanciu steke*)
              s[s[vs-1].j].j :=s[vs].j;
              vs := vs - 2
            end;
        16: begin (*sqlyginis suolis *)
              if not s[vs].lg then p:= kom.y; vs := vs - 1
            end;
        17: p := kom.y (*besqlyginis suolis*);
        18: begin (*romeiiiskais skaitmcmmis parasyto skaiciaus reiksmes apskaiciavimas*)
              ch := #0;
              while (ch = '') or (ch = #0) or (ch = #10) do
                zenklas;
              vs := vs + 1;
              if eof(input) then bus := skaitkl else
                 if ch in ['I', 'V', 'X', 'L', 'C',  'D',  'M'] then begin
                    s[vs].j := pozic(ch);
                    if s[vs].j = 0 then bus := skaitkl
                 end
                 else if ch in ['0' .. '9'] then begin
                    val(''+ch, x, code);
                    s[vs].j := 0;
                    while ch in ['0' .. '9'] do
                      begin
                        val(''+ch, x, code);
                        s[vs].j := s[vs].j * 10 + x;
                        zenklas
                      end;
                 end else bus := skaitkl
        end;
(*spausdinimas*)
        19: begin (*teksto spausdinimas*)
              h1 := s[vs].j;
              vs := vs - 1;
              eilute := copy(tlent, kom.y, h1);
              write(eilute)
            end;
        20: begin (*skaiciaus spausdinimas romeniskais skaitmemmis*)
              h1 := s[vs].j;
              if h1 = 0 then write ('0') else begin
                 if h1 <0 then write ('-');
                 if (h1< -3999) or (h1 > 3999) then write ('****') else begin
                    roma (100, 'M',  'D', 'C'); roma(10, 'C', 'L', 'X');
                    roma(1,'X', 'V', 'I');
                    while h1 >= 1 do
                      begin write('I'); h1 := h1 - 1 end
                 end
              end
         end;
         21:  bus := viskas;
         22: begin
               h1 := s[vs].j;
               write(h1);
             end;
         23: begin vs := vs - 1; s[vs].lg := true end;
         24: begin vs := vs - 1; s[vs].lg := false end;
         25: begin write(s[vs].lg); vs := vs - 1; end;
        end (*case*);
     until bus <> gerai;
     if bus <> viskas then begin
        writeln;
        write(' sustabdyta nuo', p: 5, ' nes ');
        case bus of
          stperpild: writeln('perpildyta atmintis');
          dalklaida: writeln('dalyba is 0');
          skaitkl:      writeln('rasta skaitymo klaida')
        end (*case*);
        k := lent;
        while k <> 0 do (*spausdinamc visu kintamujif reiksmes*)
          with lentele[k] do begin
            write(' ', pavadin,  '=');
            if (tip = romen)or(tip = svei) then writeln(s[adr].j) else writeln(s[adr].lg);
            k := k - 1 (*siuo atveju kintainum rdkSmes isspausdiname desimtaine skaiciavimo sistcmu*)
          end
        end
     end (*interprct*);

  begin (*pagrindine programa*)
  writeln('NPL interpretator ready:');

baz[1] := 'programa  ';
baz[2] := 'pradzia   ';
baz[3] := 'pabaiga   ';
baz[4] := 'kint      ';
baz[5] := 'jei       ';
baz[6] := 'tai       ';
baz[7] := 'kitaip    ';
baz[8] := 'kol       ';
baz[9] := 'atlikti   ';
baz[10] := 'skait     ';
baz[11] := 'atsak     ';
baz[12] := 'romen     ';
baz[13] := 'program   ';
baz[14] := 'begin     ';
baz[15] := 'end       ';
baz[16] := 'var       ';
baz[17] := 'if        ';
baz[18] := 'then      ';
baz[19] := 'else      ';
baz[20] := 'while     ';
baz[21] := 'do        ';
baz[22] := 'read      ';
baz[23] := 'write     ';
baz[24] := 'div       ';
baz[25] := 'mod       ';
baz[26] := 'log       ';
baz[27] := 'boolean   ';
baz[28] := 'integer   ';
baz[29] := 'true1     ';
baz[30] := 'false1    ';
bsi[1] := programa;
bsi[2] := pradzia;
bsi[3] := pabaiga;
bsi[4] := kint;
bsi[5] :=jei;
bsi[6] := tai;
bsi[7]  := kitaip;
bsi[8]  := kol;
bsi[9]  := atlikti;
bsi[10] := skait;
bsi[11] := atsak;
bsi[12] := rom;
bsi[13] := programa;
bsi[14] := pradzia;
bsi[15] := pabaiga;
bsi[16] := kint;
bsi[17] :=jei;
bsi[18] := tai;
bsi[19]  := kitaip;
bsi[20]  := kol;
bsi[21]  := atlikti;
bsi[22] := skait;
bsi[23] := atsak;
bsi[24] := dalsv;
bsi[25] := dalliek;
bsi[26] := logi;
bsi[27] := logi;
bsi[28] := int;
bsi[29] := taip;
bsi[30] := ne;
sps['+'] := plius;
sps['-'] := minus;
sps['*'] := zvaigzdute;
sps['$'] := dalsv;
sps['#'] := dalliek;
sps['('] := kskliaust;
sps[')'] := dskliaust;
sps[','] := kablelis;
sps[';'] := kabliatask;
sps['.'] := taskas;
sps['='] := lyg;
sps['!'] := sauktukas;
pran[2] := 'ilgas tekstas';
pran[4] := 'ilga klentele';
pran[6] := 'neapibreztas kintamasis ';
pran[8] := 'nera dvitaskio';
pran[10] := 'nera kabliataskio';
pran[12] := 'nera desinio skliausto';
pran[14] := 'nera skaiciaus';
pran[16] := 'nera pabaigos';
pran[18] := 'nera tai';
pran[1] := 'blogas skaiciaus formatas';
pran[3] := 'ilgas kodas';
pran[5] := 'antra karta apibreztas';
pran[7] := 'nera vardo';
pran[9] := 'nera tipo';
pran[11] := 'daugiklis pries';
pran[13] := 'neteisingas simbolis';
pran[15] := 'blogas priskyrimas';
pran[17] := 'nera lreiskinio';
pran[19] := 'nera atlikti';
pran[20] := 'nera kintamojo vardo';
pran[21] := 'nera programa ';
pran[22] := 'nera tasko';
bloksimb := [kint, pradzia];
daugsimb := [skai, vard, kskliaust];
saksimb := [pradzia, jei, kol, skait, atsak];
ilg := 0;
zsk := 0;
psk := 0;
tlent := '';
ch := ' ' ;
kl := true;
leksema;
lent := 2;
vieta := 3;
lentele[1].pavadin := 'true      ';
lentele[2].pavadin := 'false     ';
lentele[1].tip := log;
lentele[2].tip := log;
lentele[1].adr := 1;
lentele[2].adr := 2;
if simb <> programa then klaida(21) else
  begin
   leksema;
  { if (simb <> vrd) then klaida(7) else}
     begin progvard := vrd; leksema end;
  end;
blokas(bloksimb + saksimb);
if simb <> taskas then klaida(22);
gen(21);
if kl then begin spausdinti; interpret end;
55: end (*pagrindineprograma*).

