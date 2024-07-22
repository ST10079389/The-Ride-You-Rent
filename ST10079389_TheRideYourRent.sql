-- 1. DATABASE CREATION
CREATE DATABASE TheRideYourRent;
USE TheRideYourRent;

-- 2. TABLE CREATION
CREATE TABLE CarBodyType(
  BodyTypeID INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
  [description] VARCHAR(60)
);

CREATE TABLE CarMake(
  MakeID INT IDENTITY (1,1) PRIMARY KEY NOT NULL,
  [description] VARCHAR(60) 
);

CREATE TABLE Car (
  CarID INT IDENTITY (1,1) PRIMARY KEY,
  CarNo VARCHAR(8) UNIQUE,
  Model VARCHAR(30),
  Kilometres_Travelled INT,
  Service_Kilometres INT,
  Available VARCHAR(5),
  [MakeID] INT FOREIGN KEY REFERENCES CarMake(MakeID),
  BodyTypeID INT FOREIGN KEY REFERENCES CarBodyType(BodyTypeID)
);

CREATE TABLE Inspector(
  InspectorID INT IDENTITY (1,1) PRIMARY KEY,
  Inspector_No VARCHAR(8) UNIQUE,
  [Name] VARCHAR(30),
  Email VARCHAR(50),
  Mobile INT
);

CREATE TABLE InspectorRegister(
  InspectorID INT IDENTITY (1,1) PRIMARY KEY,
  [Name] VARCHAR(30),
  [Password] VARCHAR(50)
);

CREATE TABLE Driver(
  DriverID INT IDENTITY (1,1) PRIMARY KEY,
  [Name] VARCHAR(30),
  [Address] VARCHAR(80),
  Email VARCHAR(50),
  Mobile INT
);

CREATE TABLE Rental(
  RentalID INT IDENTITY (1,1) PRIMARY KEY,
  Rental_Fee INT,
  [Start Date] DATE,
  [End Date] DATE,
  CarNo VARCHAR(8) FOREIGN KEY REFERENCES Car(CarNo),
  Inspector_No VARCHAR(8) FOREIGN KEY REFERENCES Inspector(Inspector_No),
  DriverID INT FOREIGN KEY REFERENCES Driver(DriverID),
  [MakeID] INT FOREIGN KEY REFERENCES CarMake(MakeID)
);

CREATE TABLE [Return](
  ReturnID INT IDENTITY (1,1) PRIMARY KEY,
  [Return Date] DATE,
  [Elapsed Date] INT,
  Fine INT,
  CarNo VARCHAR(8) FOREIGN KEY REFERENCES Car(CarNo),
  Inspector_No VARCHAR(8) FOREIGN KEY REFERENCES Inspector(Inspector_No),
  DriverId INT FOREIGN KEY REFERENCES Driver(DriverID),
  [MakeID] INT FOREIGN KEY REFERENCES CarMake(MakeID)
);

-- 3. TABLE ALTERATION


-- 4. TABLE INSERTION
INSERT INTO CarBodyType([description])
VALUES ('Hatchback'),('Sedan'),('Coupe'),('Suv');

INSERT INTO CarMake([description])
VALUES ('Hyundai'),('BMW'),('Mercedes Benz'),('Toyota'),('Ford');

-- 5. INSERT VALUES
INSERT INTO Car(CarNo, Model, MakeID, BodyTypeID, Kilometres_Travelled, Service_Kilometres, Available)
VALUES
('HYU001', 'Grand i10 1.0 Motion', 1, 1, 1500, 15000, 'Yes'),
('HYU002', 'i20 1.2 Fluid', 1, 1, 3000, 15000, 'Yes'),
('BMW001', '320d 1.2', 2, 2, 20000, 50000, 'Yes'),
('BMW002', '240d 1.4', 2, 2, 9500, 15000, 'Yes'),
('TOY001', 'Corolla 1.0', 4, 2, 15000, 50000, 'Yes'),
('TOY002', 'Avanza 1.0', 4, 4, 98000, 15000, 'Yes'),
('TOY003', 'Corolla Quest 1.0', 4, 2, 15000, 50000, 'Yes'),
('MER001', 'c180', 3, 2, 5200, 15000, 'Yes'),
('MER002', 'A200 Sedan', 3, 2, 4080, 15000, 'Yes'),
('FOR001', 'Fiesta 1.0', 5, 2, 7600, 15000, 'Yes');


INSERT INTO Inspector(Inspector_No, [Name], Email, Mobile)
VALUES
('I101', 'Bud Barnes', 'bud@therideyourent.com', '0821585359'),
('I102', 'Tracy Reeves', 'tracy@therideyourent.com', '0822889988'),
('I103', 'Sandra Goodwin', 'sandra@therideyourent.com', '0837695468'),
('I104', 'Shannon Burke', 'shannon@therideyourent.com', '083680251');

INSERT INTO Driver ([Name], [Address], Email, Mobile)
VALUES
('Gabrielle Clarke', '917 Heuvel St Botshabelo Free State 9781', 'gorix10987@macauvpn.com', 0837113269),
('Geoffrey Franklin', '1114 Dorp St Paarl Western Cape 7655', 'noceti8743@drlatvia.com', 0847728052),
('Fawn Cooke', '2158 Prospect St Garsfontein Gauteng 0042', 'yegifav388@enamelme.com', 0821966584),
('Darlene Peters', '2529 St. John Street Somerset West Western Cape 7110', 'mayeka4267@macauvpn.com', 0841221244),
('Vita Soto', '1474 Wolmarans St Sundra Mpumalanga 2200', 'wegog55107@drlatvia.com', 0824567924),
('Opal Rehbein', '697 Thutlwa St Letaba Limpopo 0870', 'yiyow34505@enpaypal.com', 0826864938),
('Vernon Hodgson', '1935 Thutlwa St Letsitele Limpopo 0885', 'gifeh11935@enamelme.com', 0855991446),
('Crispin Wheatly', '330 Sandown Rd Cape Town Western Cape 8018', 'likon78255@macauvpn.com', 0838347945),
('Melanie Cunningham', '616 Loop St Atlantis Western Cape 7350', 'sehapeb835@macauvpn.com', 0827329001),
('Kevin Peay', '814 Daffodil Dr Elliotdale Eastern Cape 5118', 'xajic53991@enpaypal.com', 0832077149);

INSERT INTO Rental (CarNo, Inspector_No, DriverID, Rental_Fee, [Start Date], [End Date], [MakeID])
VALUES
('HYU001', 'I101', 1, 5000, '2021-08-30', '2021-08-31', 1),
('HYU002', 'I101', 1, 5000, '2021-09-01', '2021-09-10', 1),
('FOR001', 'I101', 2, 6500, '2021-09-01', '2021-09-10', 5),
('BMW002', 'I102', 5, 7000, '2021-09-20', '2021-09-25', 2),
('TOY002', 'I102', 4, 5000, '2021-10-03', '2021-10-31', 4),
('MER001', 'I103', 4, 8000, '2021-10-05', '2021-10-15', 3),
('HYU002', 'I104', 7, 5000, '2021-12-01', '2022-02-10', 1),
('TOY003', 'I104', 9, 5000, '2021-08-10', '2021-08-30', 4);

INSERT INTO [Return] (CarNo, Inspector_No, DriverID, [Return Date], [Elapsed Date], Fine, [MakeID])
VALUES
('HYU001', 'I101', 1, '2021-08-31', '0', '0', 1),
('HYU002', 'I101', 1, '2021-09-10', '0', '0', 1),
('FOR001', 'I101', 2, '2021-09-10', '0', '0', 5),
('BMW002', 'I102', 5, '2021-09-30', '5', '2500', 2),
('TOY002', 'I102', 4, '2021-10-31', '2', '1000', 4),
('MER001', 'I103', 4, '2021-10-15', '1', '500', 3),
('HYU002', 'I104', 7, '2022-02-10', '0', '0', 1),
('TOY003', 'I104', 9, '2021-08-31', '0', '0', 4);

INSERT INTO InspectorRegister([Name], [Password])
VALUES
('Bud Barnes', 'barney101'),
('Tracy Reeves', 'tracyiscool'),
('Sandra Goodwin', 'password'),
('Shannon Burke', 'helloworld');

select*from CarMake
select*from CarBodyType
select*from Car
select*from Driver
select*from Inspector
select*from InspectorRegister
select*from [Rental]
select*from [Return]







