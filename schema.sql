
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the DemoDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='DemoDB')
DROP DATABASE DemoDB;
GO

-- Create a new DemoDB Database
CREATE DATABASE DemoDB;
GO

-- Switch to the DemoDB Database
USE DemoDB
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('user'),
	first_name  varchar(64) not null,
	last_name	varchar(64) not null,

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
	playerString varchar(500) not null,
	scoresString varchar(500) not null,

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
	x_pos			float		null,
	y_pos			float		null,
	score			int			not null,

	constraint pk_slot primary key(id),
	constraint fk_tournament_id foreign key(tournament_id) references tournament(id),
	constraint fk_player_id foreign key (player_id) references users (id),
	constraint fk_nextslot_id foreign key(nextslot_id) references slot(id)

);

COMMIT TRANSACTION;