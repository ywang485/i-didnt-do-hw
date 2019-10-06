% Action parents_dont_know_tech
action(parents_dont_know_tech).

%% Precondition
can_happen(parents_dont_know_tech, T) :-
     astep(T).

%% Effect
holds_at(parents_dont_know_tech, T+1) :- exec(parents_dont_know_tech, T).