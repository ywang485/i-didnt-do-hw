% Action had_to_do_part_time_job
action(had_to_do_part_time_job).

%% Precondition
can_happen(had_to_do_part_time_job, T) :- holds_at(extra_amount_work, T), astep(T).

%% Effect
holds_at(no_time, T+1) :- exec(had_to_do_part_time_job, T).