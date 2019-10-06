% Action not_careful
action(not_careful).

%% Precondition
can_happen(not_careful, T) :- astep(T).

%% Effect
holds_at(not_careful, T+1) :- exec(not_careful, T).