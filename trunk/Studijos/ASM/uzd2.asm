;(C) Karolis Uosis 2006

.model small
.stack 1000
.data
raide db 'a$'
vardas db 'a.txt',0, 7 dup(0), '$'
vardas2 db 'b.txt', 0, 7 dup(0), '$'
klaida db 'Klaida dirbant su failu$'
eil1 db 'Naudojimas is komandines eilutes:', 13, 10, '$'
eil2 db 'myasm <input> <output> <raide>', 13, 10, '$'
eil3 db 'Input: $'
eil4 db 'Output: $'
eil5 db 'Tikrinama raide: $'
eil6 db 13, 10, '$'
eil7 db 'Atlikta$'
buf db 26, 49 dup(26)
handle db 4 dup(0)
handle2 db 4 dup(0)
ten db 10
.code

start proc
mov bx, ds
mov ax, @data
mov ds, ax
call prduom
call pranes
call sukurti
call openwrite
call openread   
toliau:
call read

cmp cx, 0
jne toliau


mov dx, offset eil7
mov ah, 09h
int 21h

mov si, offset handle
mov bx, [si]
call close
mov si, offset handle2
mov bx, [si]
call close

mov ah, 4ch                     ; baigiam programa ir griztam i os
int 21h     


ENDP

pranes proc
mov dx, offset eil1
mov ah, 09h
int 21h
mov dx, offset eil2
mov ah, 09h
int 21h
mov dx, offset eil3
mov ah, 09h
int 21h
mov dx, offset vardas
mov ah, 09h
int 21h
mov dx, offset eil6
mov ah, 09h
int 21h
mov dx, offset eil4
mov ah, 09h
int 21h
mov dx, offset vardas2
mov ah, 09h
int 21h
mov dx, offset eil6
mov ah, 09h
int 21h
mov dx, offset eil5
mov ah, 09h
int 21h
mov dx, offset raide
mov ah, 09h
int 21h
mov dx, offset eil6
mov ah, 09h
int 21h
ret
endp

prduom proc
mov es, bx
mov di, 80h
mov al, es:[di]
mov si, 81h
mov ch, 0
mov cl, ' '
mov ah, 0
dec si
tarpas:
inc si
inc ch
cmp ch, al
jg baik
cmp cl, es:[si]
je tarpas
cmp ah, 0
jne fv2
mov ah, 1
mov di, offset vardas
jmp c1
fv2:
cmp ah, 1
jne fv3
mov ah, 2
mov di, offset vardas2
c1:
mov bl, es:[si]
mov [di], bl
inc si
inc ch
inc di
cmp ch, al
jg baik
cmp es:[si], cl
je b2
jmp c1
b2:
mov [di], byte ptr 0
jmp tarpas 
fv3:
mov di, offset raide
mov bl, es:[si]
mov [di], bl
baik:
ret
endp

patikr proc
mov di, offset raide
mov al, [di]
mov di, offset buf
mov bl, ' '
mov cx, 1 ;how many
mov dx, 0 ;no
dec di
jmp tik

ok:
mov dx, 1
jmp pab

tik:
inc di
cmp [di], al
je ok
tarpai: 
inc di
inc cx
cmp cx, si
jge pab
cmp [di], bl
je tik
jmp tarpai 
pab:
cmp dx, 1
jne pab2
call write
pab2:
ret
endp

openread proc
mov ah, 3dh
mov al, 0
mov dx, offset vardas
int 21h
jnc pavyko
jmp galas
pavyko:
mov si, offset handle
mov [si], ax
ret
ENDP

openwrite proc
mov ah, 3dh
mov al, 1
mov dx, offset vardas2
int 21h
jnc pav
jmp galas
pav:
mov si, offset handle2
mov [si], ax
ret
ENDP

close proc
mov ah, 3eh
int 21h
jnc pavyko2
jmp galas
pavyko2:
ret
ENDP

read proc
mov dx, offset buf
mov di, dx
mov si, 0
cikl:
mov [di], byte ptr 26
inc di
inc si
cmp si, 25
jne cikl
mov si, offset handle
mov bx, [si]
mov ah, 3fh
mov dx, offset buf
mov di, dx
mov [di], byte ptr 26
mov si, 0
pradzia:
mov ah, 3fh
mov cx, 1
int 21h
jc galas
            ; uzrasome ant ekrano eil turini
;    mov ah, 09h                     ; 09h  spec. int 21h funkcija
 ;   int 21h 
inc si
;push si
;mov si, byte ptr ds:dx
;mov di, dx
;pop si
mov di, dx
cmp [di], byte ptr 10
je pabaiga
cmp [di], byte ptr 26
je pabaig2
inc dx
cmp si, 20
je pabaiga
jmp pradzia
jmp pabaiga
pabaig2:
mov [di], byte ptr 13
mov [di+1],  byte ptr 10
inc di
inc si
mov cx, 0
pabaiga:
push cx
call patikr
pop cx
ret
ENDP

write proc
;mov [si + 1], 13
;inc si
;mov [si + 1], 10
;inc si
mov ah, 40h
mov di, offset handle2
mov bx, [di]
mov cx, si
mov dx, offset buf
int 21h
ret
ENDP

sukurti proc
mov ah, 3ch
mov cx, 0
mov dx, offset vardas2
int 21h
ret
endp

galas:
    mov dx, offset klaida             ; uzrasome ant ekrano eil turini
    mov ah, 09h                     ; 09h  spec. int 21h funkcija
    int 21h      
    mov ah, 4ch                     ; baigiam programa ir griztam i os
    int 21h 
END start