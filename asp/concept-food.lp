% Action eat
action(eat(F)) :- food(F).
food(apple; pear; strawberry; burger; sandwitch).

%% Precondition
can_happen(eat(F), T) :- food(F), astep(T).

%% Effect
holds_at(eatten(F), T+1) :- exec(eat(F), T).

% Action food_poison
action(food_poison).

%% Precondition
can_happen(food_poison, T) :- holds_at(eatten(F), T), holds_at(food_bad(F), T).

%% Effect
holds_at(can_get_sick, T+1) :- exec(food_poison, T).