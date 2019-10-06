% Domain definition
astep(0..m-1).
step(0..m).
boolean(t; f).

family_members(dad; mon; older_sister; younger_sister; older_brother; younger_brother; cat; dog; grandmother; grandfather).

%% Choice rule
{exec(Act, T)} :- action(Act), astep(T).

%% An action cannot be executed if is precondition is not met
:- exec(Act, T), not can_happen(Act, T), action(Act).

%% At most one action can happen at each time step
:- not {exec(Act, T) : action(Act)}1, astep(T).

% Action didnt_do_hw
action(didnt_do_hw).

can_happen(didnt_do_hw, T) :- holds_at(no_equipment, T).
can_happen(didnt_do_hw, T) :- holds_at(no_time, T).
can_happen(didnt_do_hw, T) :- holds_at(no_energy, T).
can_happen(didnt_do_hw, T) :- holds_at(not_in_mood, T).

%% didnt_do_hw can happen at final time step
{exec(didnt_do_hw, m)}.

#show exec/2.
%#show can_happen/2.

% Testing
:- not exec(didnt_do_hw, m).