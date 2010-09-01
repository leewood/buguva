unit xint;
interface
uses x_arg, x_proc, x_duom;
var line: string;
    cur_line, end_line: integer;
implementation

function findc(pr: integer; c: char; s: string): integer;
  var w: integer;
  begin
    w := pr;
    while (s[w] <> c)and(w <= length(s)) do
      w := w + 1;
    findc := w;
  end;

procedure argsplit(st, en: integer; s: string);
  var x, n, kur: integer;
  begin
    n := 0;
    x := st;
    while x <= en do
      begin
        kur := findc(x, ',', s);
        arg[n + 1] := copy(s, x, kur - x);
        x := kur + 1;
        n := n + 1;
      end;
    argn := n;
  end;


procedure exec_proc(libname, procname: string);
  begin
  end;

function get_line(line_number: integer): string;
begin
end;

function find_end(cur_line: integer): integer;
  var n, x: integer;
      s: string;
  begin
    n := 1;
    x := cur_line + 1;
    while n > 0 do
      begin
        s := get_line(x);
        if (s[1] = 'I') or (s[1] = 'W') or (s[1] = 'P') or (s[1] = 'E') then n := n + 1;
        if (s[1] = '}') then n := n - 1;
        x := x + 1;
      end;
    find_end := x - 1;
  end;


function rasti_sk(x: integer; exp: string): integer;
  var y, n: integer;
  begin
    y := x + 1;
    n := 1;
    while n > 0 do
      begin
        if exp[y] = '(' then n := n + 1;
        if exp[y] = ')' then n := n - 1;
        y := y + 1;
      end;
    rasti_sk := y - 1;
  end;


function op_num(name: string): integer;
var a: integer;
begin
if name = 'not' then a := 91;
if name = 'and' then a := 81;
if name = 'or' then a := 82;
if name = '*' then a := 71;
if name = '/' then a := 72;
if name = 'div' then a := 73;
if name = 'mod' then a := 74;
if name = '+' then a := 61;
if name = '-' then a := 62;
if name = '+s' then a := 63;
if name = '>' then a := 51;
if name = '<' then a := 52;
if name = '>=' then a := 53;
if name = '<=' then a := 54;
if name = '<>' then a := 55;
if name = '=' then a := 56;
op_num := a;
end;


function galingesne(op1, op2: string): boolean;
begin
if op_num(op1) div 10 > op_num(op2) div 10 then galingesne := true
                                          else galingesne := false;
end;


function operacija(a, b, op: string): string;
var num: integer;
    val: string;
begin
num := op_num(op);
if num = 56 then
if a = b then val := 'TRUE' else val := 'FALSE';


{Pabaigti--------------------}

end;

function vykdyti(exp: string): string;
  var x, y, kur: integer;
      name, a, b, op1: string;
  begin
    x := 1;
    while x <= length(exp) do
    begin
    if exp[x] = '$' then begin
      kur := findc(x + 1, ' ', exp);
      name := copy(exp, x + 1, kur - x - 1);
      a := kint[find_name(name)].value;
      x := kur + 1;
    end else if exp[x] = '(' then
      begin kur := rasti_sk(x, exp);
        name := copy(exp, x + 1, kur - x - 2);
        x := kur + 2;
        a := vykdyti(name);
      end else
        begin
          kur := findc(x, ' ', exp);
          a := copy(exp, x, kur - x);
          x := kur + 1;
        end;
     kur := findc(x, ' ', exp);
     name := copy(exp, x, kur - x);
     op1 := name;
     x := kur + 1;
     y := x;
    if exp[x] = '$' then begin
      kur := findc(x + 1, ' ', exp);
      name := copy(exp, x + 1, kur - x - 1);
      b := kint[find_name(name)].value;
      x := kur + 1;
    end else if exp[x] = '(' then
      begin kur := rasti_sk(x, exp);
        name := copy(exp, x + 1, kur - x - 2);
        x := kur + 2;
        b := vykdyti(name);
      end else
        begin
          kur := findc(x, ' ', exp);
          b := copy(exp, x, kur - x);
          x := kur + 1;
        end;
     kur := findc(x, ' ', exp);
     name := copy(exp, x, kur - x);
     x := kur + 1;
     if galingesne(name, op1) then
        begin
        b := vykdyti(copy(exp, y, length(exp) - y + 1));
        x := length(exp);
        end;
     name := operacija(a, b, op1);
     if x <= length(exp) then begin
        y := 1;
        op1 := copy(exp, x, length(exp) - x + 1);
        op1 := name + ' ' + op1;
        exp := op1;
        x := 1;
        end else exp := name;
     end;
    vykdyti := exp;
  end;

procedure run_exp(exp: string);
  begin
  end;

procedure save_to_var(name, value: string);
  begin
    kint[find_name(name)].value := value;
  end;

procedure make_new(name, value, typename: string);
  begin
    n_var := n_var + 1;
    kint[n_var].name := name;
    kint[n_var].value := value;
    kint[n_var].typename := typename;
  end;

procedure detect;
  var back, f1, f2: integer;
      libname, procname, exp: string;
  begin
    case line[1] of
     '@': back := 1; {var definition}
     'T': back := 2; {type definition}
     'C': back := 3; {const definition}
     '}': back := 4; {goto sign}
     '$': back := 5; {expression}
     'I': back := 6; {IF}
     'E': back := 7; {Else}
     'W': back := 8; {While}
     'P': back := 9; {Procedure definition}
     '#': back := 10; {Procedure call}
    else back := 11; {Comment}
    end;

    if back = 10 then begin
    {Procedure name must be #libname.procname(arg1, arg2, ..., arg3)}
      libname := copy(line, 2, findc(2, '.', line) - 2);
      procname := copy(line, findc(2, '.', line) + 1, findc(2, '(', line) - findc(2, '.', line));
      argsplit(findc(2, '(', line) + 1, length(line) - 1, line);
      exec_proc(libname, procname);
      end_line := cur_line + 1;
    end;

    if back = 9 then begin
       {Procedure def: PROC proc_name}
       procname := copy(line, 6, length(line) - 5);
       end_line := find_end(cur_line);
       proc[0].s := proc[0].s + 1;
       proc[proc[0].s].s := cur_line + 1;
       proc[proc[0].s].e := end_line;
       proc[proc[0].s].name := procname;
       end_line := end_line + 1;
    end;

    if back = 8 then begin
       {While def: While (BOOL exp)}
       exp := copy(line, 8, length(line) - 8);
       run_exp(exp);
       if rez[1] = 'TRUE' then begin
         backl[0] := backl[0] + 1;
         backl[backl[0]] := cur_line;
         end_line := end_line + 1;
       end
       else end_line := find_end(cur_line) + 1;
    end;

    if back = 7 then
       {Else def:
             Else
             ...
             }
             {}
       end_line := find_end(cur_line) + 1;

    if back = 6 then
       {IF def:
       IF (exp)
       }
       begin
         exp := copy(line, 4, length(line) - 5);
         run_exp(exp);
         if rez[1] = 'TRUE' then begin
           end_line := end_line + 1;
         end
         else begin
           backl[0] := backl[0] + 1;
           backl[backl[0]] := find_end(cur_line) + 1;
           end_line := find_end(cur_line) + 2;
         end;
       end;

    if back = 5 then begin
    {DEF: $var_name := expresion}
      libname := copy(line, 2, findc(2, ' ', line) - 2);
      exp := copy(line, findc(2, '=', line) + 2, length(line) - findc(2, '=', line) - 1);
      run_exp(exp);
      save_to_var(libname, rez[1]);
    end;

    if back = 4 then begin
      backl[0] := backl[0] - 1;
      end_line := backl[0] + 1;
    end;

    if back = 3 then begin
    {def: Const name = value}
      f1 := findc(7, ' ', line);
      libname := copy(line, 7, f1 - 7);
      exp := copy(line, f1 + 3, length(line) - f1 - 2);
      make_new(libname, exp, 'CONST');
    end;

    if back = 1 then begin
    {Def: @type name = exp}
      f1 := findc(2, ' ', line);
      f2 := findc(2, '=', line);
      if f2 > length(line) then begin
        exp := '';
        f2 := length(line);
      end
      else
        begin
          exp := copy(line, f2 + 2, length(line) - f2 - 1);
          f2 := f2 - 2;
        end;
      libname := copy(line, f1 + 1, f2 - f1);
      procname := copy(line, 2, f1 - 2);
      {Make_new(var_name, var_value, var_type);}
      make_new(libname, exp, procname);
    end;

  end;


end.