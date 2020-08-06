CREATE DATABASE MedicalService;
GO

use MedicalService;

GO

DROP TABLE IF EXISTS tblPatient;
DROP TABLE IF EXISTS tblDoctor;
DROP TABLE IF EXISTS tblApplication;

 CREATE TABLE tblDoctor (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	FirstName varchar(50),
	LastName varchar(50),
    JMBG varchar(50),
    BankAccountNumber varchar(50),
	UserName varchar(50),
	Password varchar(50) 
);

 CREATE TABLE tblPatient (
    ID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
    FirstName varchar(50),
	LastName varchar(50),
	JMBG varchar(50),
	MedicalInsuranceNumber varchar(50),	
	UserName varchar(50),
	Password varchar(50),
	SelectedDoctor int FOREIGN KEY REFERENCES tblDoctor(ID)
);


 CREATE TABLE tblApplication (
    ApplicationID int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	DateOfApplication date,
	CauseForApplication varchar(50),
	EmployerFirm varchar(50),
	Emergency bit,
	ApplicationStatus varchar(50),
	PatientID int FOREIGN KEY REFERENCES tblPatient(ID),
	DoctorID int FOREIGN KEY REFERENCES tblDoctor(ID)
);