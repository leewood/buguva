unit x_duom;
interface
type var_type = record name, value, typename: string; end;
var kint: array[0..50] of var_type;
    n_var: integer;
function find_name(name: string): integer;
implementation
function find_name(name: string): integer;
var x: integer;
begin
x := 1;
while (kint[x].name <> name) and (x <= n_var) do
begin
x := x + 1;
end;
if x > n_var then x := 0;
find_name := x;
end;

end.