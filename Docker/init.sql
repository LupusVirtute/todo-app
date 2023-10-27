CREATE DATABASE db;
GO
USE db;
CREATE TABLE todos (
	id int IDENTITY(1,1) NOT NULL,
	title varchar(64) NOT NULL,
	description text NOT NULL,
	todo_date DATETIME NOT NULL,

);