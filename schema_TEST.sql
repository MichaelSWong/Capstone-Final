
-- Switch to the system (aka master) database
USE master;
--GO

-- Delete the DemoDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='DemoDB')
DROP DATABASE DemoDB_TEST;
--GO

-- Create a new DemoDB Database
CREATE DATABASE DemoDB_TEST;
--GO

-- Switch to the DemoDB Database
USE DemoDB_TEST
--GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('user'),

	constraint pk_users primary key (id)
);

CREATE TABLE tournament
(
	id			int			identity(1,1),
	name		varchar(64) not null,
	active		bit			not null,
	start_date	datetime	null,
	create_date	datetime	not null,
	playertotal int			null,

	constraint pk_tournament primary key(id)
);

CREATE TABLE tournament_players
(
	player_id		int			not null,
	tournament_id int		not null,

	constraint fk_tournamentuser_playerid foreign key (player_id) references users (id),
	constraint fk_tournamentuser_tournamentid foreign key (tournament_id) references tournament (id) 
);

CREATE TABLE tournament_organizer
(
	player_id		int			not null,
	tournament_id int		not null,

	constraint fk_tournamentorganizer_playerid foreign key (player_id) references users (id),
	constraint fk_tournamentorganizer_tournamentid foreign key (tournament_id) references tournament (id) 
);

CREATE TABLE tournament_admins
(
	player_id		int			not null,
	tournament_id int		not null,

	constraint fk_tournamentadmins_playerid foreign key (player_id) references users (id),
	constraint fk_tournamentadmins_tournamentid foreign key (tournament_id) references tournament (id) 
);

CREATE TABLE slot
(
	id				int			identity(1,1),
	tournament_id	int			not null,
	player_id		int			null,
	nextslot_id		int			null,

	constraint pk_slot primary key(id),
	constraint fk_tournament_id foreign key(tournament_id) references tournament(id),
	constraint fk_player_id foreign key (player_id) references users (id),
	constraint fk_nextslot_id foreign key(nextslot_id) references slot(id)

);

INSERT INTO users VALUES('PlayerOne', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerTwo', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerThree', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerFour', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerFive', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerSix', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerSeven', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerEight', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerNine', 'password', 'salt', 'user');
INSERT INTO users VALUES('PlayerTen', 'password', 'salt', 'user');

INSERT INTO tournament VALUES('Racing', 1, GETDATE() + MONTH(15), GETDATE(), null);
INSERT INTO tournament VALUES('Football', 1, GETDATE() + MONTH(9), GETDATE(), null);
INSERT INTO tournament VALUES('Golf', 1, GETDATE() + MONTH(25), GETDATE(), null);
INSERT INTO tournament VALUES('Ping-Pong', 1, GETDATE() + MONTH(60), GETDATE(), null);
INSERT INTO tournament VALUES('Starcraft 2', 1, GETDATE() + MONTH(51), GETDATE(), null);
INSERT INTO tournament VALUES('Overwatch', 1, GETDATE() + MONTH(43), GETDATE(), null);
INSERT INTO tournament VALUES('CS:GO', 1, GETDATE() + MONTH(5), GETDATE(), null);

INSERT INTO slot VALUES(1, NULL, NULL);
INSERT INTO slot VALUES(1, NULL, NULL);
INSERT INTO slot VALUES(1, NULL, NULL);
INSERT INTO slot VALUES(1, NULL, NULL);
INSERT INTO slot VALUES(1, NULL, NULL);
INSERT INTO slot VALUES(1, NULL, NULL);
INSERT INTO slot VALUES(1, NULL, NULL);
UPDATE slot SET player_id = 1, nextslot_id = 5 WHERE id = 1;
UPDATE slot SET player_id = 2, nextslot_id = 5 WHERE id = 2;
UPDATE slot SET player_id = 3, nextslot_id = 6 WHERE id = 3;
UPDATE slot SET player_id = 4, nextslot_id = 6 WHERE id = 4;
UPDATE slot SET				   nextslot_id = 7 WHERE id = 5;
UPDATE slot SET				   nextslot_id = 7 WHERE id = 6;

COMMIT TRANSACTION;