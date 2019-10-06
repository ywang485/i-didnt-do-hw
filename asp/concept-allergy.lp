% Action severe_allergy
action(severe_allergy).

%% Precondition
can_happen(severe_allergy, T) :- holds_at(air_polluted, T).
can_happen(severe_allergy, T) :- holds_at(pollen, T).

%% Effect
holds_at(can_get_sick, T+1) :- exec(severe_allergy, T).