% Action lost_hw_files
action(lost_hw_files).

%% Precondition
can_happen(lost_hw_files, T) :- 
    holds_at(can_lose_hw_files, T).

%% Effect
holds_at(no_equipment, T+1) :- exec(lost_hw_files, T).