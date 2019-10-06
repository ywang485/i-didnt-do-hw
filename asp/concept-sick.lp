% Action get_sick
action(get_sick).

%% Precondition
can_happen(get_sick, T) :- holds_at(can_get_sick, T).

%% Effect
holds_at(no_energy, T+1) :- exec(get_sick, T).