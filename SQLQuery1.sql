--create table query

CREATE TABLE [USER](
	user_id int NOT NULL PRIMARY KEY,
	first_name nvarchar(50) NOT NULL,
	last_name nvarchar(50) NULL,
	email nvarchar(50) NOT NULL,
	password nvarchar(50) NOT NULL,
	mobile nvarchar(50) NULL)
	GO;

CREATE TABLE BOOK(
	book_id int NOT NULL PRIMARY KEY,
	book_name nvarchar(50) NOT NULL,
	--categoryid nvarchar(50) NOT NULL,
	category_id int NOT NULL FOREIGN KEY REFERENCES [CATEGORY](categoryid),
	author nvarchar(50)  NULL,
	publisher nvarchar(50)  NULL,
	price decimal(8,2) NOT NULL)
	--categoryid int FOREIGN KEY REFERENCES tbl_CATEGORY(categoryid)
	GO;

CREATE TABLE CATEGORY(
    category_id int IDENTITY (1,1) NOT NULL PRIMARY KEY,
	category_name nvarchar(50)NOT NULL)
	GO;

--select query

SELECT * FROM BOOK;
SELECT * FROM CATEGORY;
SELECT * FROM [USER];

