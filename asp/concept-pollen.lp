% Action pollen_spread
action(pollen_spread).

%% Precondition
can_happen(pollen_spread, T) :- holds_at(spring, T).

%% Effect
holds_at(pollen, T+1) :- exec(pollen_spread, T).