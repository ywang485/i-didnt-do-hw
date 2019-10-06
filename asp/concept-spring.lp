% Action spring_come
action(spring_come).

%% Precondition
can_happen(spring_come, T) :- astep(T).

%% Effect
holds_at(spring, T+1) :- exec(spring_come, T).