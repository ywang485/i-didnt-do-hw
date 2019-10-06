% Action parent_lost_job
action(parent_lost_job).

%% Precondition
can_happen(parent_lost_job, T) :- holds_at(parents_can_lose_job, T), astep(T).

%% Effect
holds_at(parents_lost_job, T+1) :- exec(parent_lost_job, T).