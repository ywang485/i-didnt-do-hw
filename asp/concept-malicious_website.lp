% Action visited_malicious_website
action(visited_malicious_website).

%% Precondition
can_happen(visited_malicious_website, T) :-
     astep(T).

%% Effect
holds_at(had_virus, T+1) :- exec(visited_malicious_website, T).