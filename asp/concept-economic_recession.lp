% Action economic_recession
action(economic_recession).

%% Precondition
can_happen(economic_recession, T) :- astep(T).

%% Effect
holds_at(parents_can_lose_job, T+1) :- exec(economic_recession, T).