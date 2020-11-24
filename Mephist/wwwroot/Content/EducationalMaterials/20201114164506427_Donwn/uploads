n = int(input()) 
answer = set(range(1, n + 1)) 

i = input() 
while i != 'HELP': 
    set1 = set(map(int, i.split())) 
    result = input() 
    if result == 'YES': 
        answer &= set1 

    if result == 'NO': 
        answer -= set1 
    i = input() 
answer = sorted(list(answer)) 

print(*answer)

