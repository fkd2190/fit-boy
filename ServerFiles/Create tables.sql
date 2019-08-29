CREATE TABLE users(
    user_id INT(5) PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(25),
    password VARCHAR(255),
    email VARCHAR(255),
    xp INT(5),
    level INT(5)
);

CREATE TABLE quests(
    quest_id INT(10) PRIMARY KEY AUTO_INCREMENT,
    quest_name VARCHAR(25),
    quest_distance FLOAT(4,1),
    start_time DATETIME,
    end_time DATETIME,
    start_lat DECIMAL(9,6),
    start_long DECIMAL(9,6),
    end_lat DECIMAL(9,6),
    end_long DECIMAL(9,6)
);

CREATE TABLE user_quests(
    user_id INT(5),
    quest_id INT(10),
    CONSTRAINT PRIMARY KEY(user_id, quest_id),
    CONSTRAINT FOREIGN KEY(user_id) REFERENCES users(user_id),
    CONSTRAINT FOREIGN KEY(quest_id) REFERENCES quests(quest_id)
);