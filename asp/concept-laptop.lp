% Action lost_laptop
action(lost_laptop).

%% Precondition
can_happen(lost_laptop, T) :- 
    holds_at(can_lose_laptop, T).

%% Effect
holds_at(no_equipment, T+1) :- exec(lost_laptop, T).