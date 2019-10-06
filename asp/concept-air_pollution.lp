% Action air_pollution
action(air_pollution).

%% Precondition
can_happen(air_pollution, T) :- astep(T).

%% Effect
holds_at(air_polluted, T+1) :- exec(air_pollution, T).