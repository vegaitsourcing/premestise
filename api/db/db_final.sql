CREATE DATABASE premestise;

USE premestise;


CREATE TABLE kindergarden (
  id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  municipality VARCHAR(50),
  government VARCHAR(50),
  city VARCHAR(50),
  name VARCHAR(50),
  department VARCHAR(50),
  street VARCHAR(50),
  street_number VARCHAR(50),
  postal_code VARCHAR(50),
  location_type BIT,
  longitude DECIMAL(9, 7),
  latitude DECIMAL(9, 7)
);

CREATE TABLE pending_request (
  id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  submitted_at DATETIME2,
  email VARCHAR(255),
  parent_name VARCHAR(50),
  phone_number VARCHAR(50),
  child_name VARCHAR(50),
  child_birth_date DATETIME2,
  from_kindergarden_id INT,
  verified BIT
);

CREATE TABLE pending_request_wishes (
  id  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  pending_request_id INT,
  kindergarden_wish_id INT
);

CREATE TABLE matched_request (
  id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  submitted_at DATETIME2,
  email VARCHAR(255),
  parent_name VARCHAR(50),
  phone_number VARCHAR(50),
  child_name VARCHAR(50),
  child_birth_date DATETIME2,
  from_kindergarden_id INT
);

CREATE TABLE matched_request_wishes (
  id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  matched_request_id INT,
  kindergarden_wish_id INT
);

CREATE TABLE matches (
  id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  first_matched_request int,
  second_matched_request int,
  matched_at DATETIME2,
  status INT
);

ALTER TABLE pending_request ADD FOREIGN KEY (from_kindergarden_id) REFERENCES kindergarden (id);

ALTER TABLE pending_request_wishes ADD FOREIGN KEY (pending_request_id) REFERENCES pending_request (id);

ALTER TABLE pending_request_wishes ADD FOREIGN KEY (kindergarden_wish_id) REFERENCES kindergarden (id);

ALTER TABLE matched_request ADD FOREIGN KEY (from_kindergarden_id) REFERENCES kindergarden (id);

ALTER TABLE matched_request_wishes ADD FOREIGN KEY (matched_request_id) REFERENCES matched_request (id);

ALTER TABLE matched_request_wishes ADD FOREIGN KEY (kindergarden_wish_id) REFERENCES kindergarden (id);

ALTER TABLE matches ADD FOREIGN KEY (first_matched_request) REFERENCES matched_request (id);

ALTER TABLE matches ADD FOREIGN KEY (second_matched_request) REFERENCES matched_request (id);
