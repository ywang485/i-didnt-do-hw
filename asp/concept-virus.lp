% Action virus_deleted_hw_file
action(virus_deleted_hw_file).

%% Precondition
can_happen(virus_deleted_hw_file, T) :-
     holds_at(had_virus, T), astep(T).

%% Effect
holds_at(can_lose_hw_files, T+1) :- exec(virus_deleted_hw_file, T).