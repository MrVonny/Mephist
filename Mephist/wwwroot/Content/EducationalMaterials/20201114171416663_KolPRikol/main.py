n = input()
can = set(range(1, int(n) + 1))
cant = set()

while True:
    inpFriends = input()
    if inpFriends == "HELP":
        break
    ans = input()
    inpFriends = set(map(int, inpFriends.split()))
    if ans == 'YES':
        can = can.intersection(inpFriends)
    else:
        cant = cant.union(inpFriends)

output = list()
for r in sorted(can - cant):
    output.append(str(r))

print(' '.join(output))