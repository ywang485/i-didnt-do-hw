% Action grandparente_disease
action(grandparent_disease).

%% Precondition
can_happen(grandparent_disease, T) :- astep(T).

%% Effect
holds_at(parents_need_money, T+1) :- exec(grandparent_disease, T).
holds_at(parents_busy, T+1) :- exec(grandparent_disease, T).