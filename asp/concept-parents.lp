% Action confisticated_laptop
action(confisticated_laptop).

%% Precondition
can_happen(confisticated_laptop, T) :-
     holds_at(too_much_video_game, T), astep(T).

can_happen(confisticated_laptop, T) :-
     holds_at(parents_dont_know_tech, T), astep(T).

%% Effect
holds_at(can_lose_laptop, T+1) :- exec(confisticated_laptop, T).

% Action parents_deleted_hw_file
action(parents_deleted_hw_file).

%% Precondition
can_happen(parents_deleted_hw_file, T) :-
     holds_at(parents_dont_know_tech, T), astep(T).

%% Effect
holds_at(can_lose_hw_files, T+1) :- exec(parents_deleted_hw_file, T).

% Action parents_in_debt
action(parents_in_debt).

%% Precondition
can_happen(parents_in_debt, T) :-
     holds_at(parents_lost_job, T), astep(T).
	 
can_happen(parents_in_debt, T) :-
     holds_at(parents_need_money, T), astep(T).
	 
%% Effect
holds_at(extra_amount_work, T+1) :- exec(parents_in_debt, T).

% Action parents_no_time_to_work
action(parents_no_time_to_work).

%% Precondition
can_happen(parents_no_time_to_work, T) :-
     holds_at(parents_busy, T), astep(T).
	 
%% Effect
holds_at(extra_amount_work, T+1) :- exec(parents_no_time_to_work, T).
