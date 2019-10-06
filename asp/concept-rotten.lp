% Action food_rotten
action(food_rotten(F)) :- rottenable_food(F).
rottenable_food(apple; pear; strawberry).

%% Precondition
can_happen(food_rotten(F), T) :- rottenable_food(F), astep(T).

%% Effect
holds_at(food_bad(F), T+1) :- exec(food_rotten(F), T).

%% Effect of actions persists
holds_at(food_bad(F), T+1) :- holds_at(food_bad(F), T), astep(T).