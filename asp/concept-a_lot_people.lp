% Action a_lot_people
action(went_to_crowded_place).

%% Precondition
can_happen(went_to_crowded_place, T) :- astep(T).

%% Effect
holds_at(crowded, T+1) :- exec(went_to_crowded_place, T).