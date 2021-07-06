USE premestise
GO

-- CREATE TABLE TO HOLD EMAIL FOR GROUP OF KINDERGARDENS
CREATE TABLE kindergarden_central (
  id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
  name NVARCHAR(220),
  email NVARCHAR(120),
);
GO

INSERT INTO kindergarden_central (name, email) VALUES (N'ПУ Радосно детињство', N'predskolska.novisad@gmail.com')
GO

-- ADD COLUMN TO SHOW IF KINDERGARDEN IS ACTIVE
ALTER TABLE kindergarden
ADD IsActive BIT NOT NULL
DEFAULT (0);
GO

-- ADD CONNECTION TO PARENT 
ALTER TABLE kindergarden 
ADD central INT;
GO

UPDATE kindergarden
SET central = 1,
IsActive =  1
WHERE city like N'Нови Сад'
GO

ALTER TABLE kindergarden ADD FOREIGN KEY (central) REFERENCES kindergarden (id);
GO

-- ADD AGE GROUP TO MATCH REQUST
ALTER TABLE matched_request
ADD age_group INT;