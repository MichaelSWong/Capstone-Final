use DemoDB;

BEGIN TRANSACTION;

INSERT INTO users VALUES('tom@yahoo.com','password1','salt','user','Tom','Anderson');
INSERT INTO users VALUES('ratt@yahoo.com','password1','salt','user','Matt','George');
INSERT INTO users VALUES('patt@yahoo.com','password1','salt','user','Matt','Mason');
INSERT INTO users VALUES('matt@yahoo.com','password1','salt','user','Matt','Ryan');
INSERT INTO users VALUES('mike@yahoo.com','password1','salt','user','Mike','Kaiser');
INSERT INTO users VALUES('todd@yahoo.com','password1','salt','user','Jason','Todd');
INSERT INTO users VALUES('bruce@yahoo.com','password1','salt','user','Bruce','Wayne');
INSERT INTO users VALUES('tim@yahoo.com','password1','salt','user','Tim','Drake');

INSERT INTO tournament VALUES('Racing', 1, GETDATE() + MONTH(15), GETDATE(), 2, '', '');
INSERT INTO tournament VALUES('Football', 1, GETDATE() + MONTH(9), GETDATE(), 4, '', '');
INSERT INTO tournament VALUES('Golf', 1, GETDATE() + MONTH(25), GETDATE(), 8, '', '');
INSERT INTO tournament VALUES('Ping-Pong', 1, GETDATE() + MONTH(60), GETDATE(), 0, '', '');
INSERT INTO tournament VALUES('Starcraft 2', 1, GETDATE() + MONTH(51), GETDATE(), 0, '', '');
INSERT INTO tournament VALUES('Overwatch', 1, GETDATE() + MONTH(43), GETDATE(), 0, '', '');
INSERT INTO tournament VALUES('CS:GO', 1, GETDATE() + MONTH(5), GETDATE(), 0, '', '');

INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(1,null,null, 0); --1
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(1,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(1, null,null, 0); --3

UPDATE slot SET player_id=1,nextslot_id=3 where id=1;
UPDATE slot SET player_id=2,nextslot_id=3 where id=2;

INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(2,null,null, 0); --4
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(2,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(2,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(2,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(2, null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(2,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(2, null,null, 0); --10

UPDATE slot SET player_id=1,nextslot_id=8 where id=4;
UPDATE slot SET player_id=2,nextslot_id=8 where id=5;
UPDATE slot SET player_id=3,nextslot_id=9 where id=6;
UPDATE slot SET player_id=4,nextslot_id=9 where id=7;
UPDATE slot SET				nextslot_id=10 where id=8;
UPDATE slot SET				nextslot_id=10 where id=9;

INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3,null,null, 0); --11
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);

INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3,null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);

INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);
INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0);

INSERT INTO slot(tournament_id, player_id, nextslot_id, score) VALUES(3, null,null, 0); --26

UPDATE slot SET player_id=1,nextslot_id=16 where id=11;
UPDATE slot SET player_id=2,nextslot_id=16 where id=12;
UPDATE slot SET player_id=3,nextslot_id=17 where id=13;
UPDATE slot SET player_id=4,nextslot_id=17 where id=14;
UPDATE slot SET player_id=5,nextslot_id=18 where id=15;
UPDATE slot SET player_id=6,nextslot_id=18 where id=16;
UPDATE slot SET player_id=7,nextslot_id=19 where id=17;
UPDATE slot SET player_id=8,nextslot_id=19 where id=18;
UPDATE slot SET				nextslot_id=20 where id=19;
UPDATE slot SET				nextslot_id=20 where id=20;
UPDATE slot SET				nextslot_id=21 where id=21;
UPDATE slot SET				nextslot_id=21 where id=22;
UPDATE slot SET				nextslot_id=22 where id=23;
UPDATE slot SET				nextslot_id=22 where id=24;

COMMIT TRANSACTION;

SELECT * FROM slot;