% Action laptop_stolen
action(laptop_stolen).

%% Precondition
can_happen(laptop_stolen, T) :- holds_at(not_careful, T).
can_happen(laptop_stolen, T) :- holds_at(crowded, T).

%% Effect
holds_at(can_lose_laptop, T+1) :- exec(laptop_stolen, T).