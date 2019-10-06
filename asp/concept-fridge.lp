% Action didnt_fridge
action(didnt_fridge(F)) :- fridgeable_food(F).
fridgeable_food(burger; sandwitch).

%% Precondition
can_happen(didnt_fridge(F), T) :- fridgeable_food(F), astep(T).

%% Effect
holds_at(food_bad(F), T+1) :- exec(didnt_fridge(F), T).

%% Effect of actions persists
holds_at(food_bad(F), T+1) :- holds_at(food_bad(F), T), astep(T).