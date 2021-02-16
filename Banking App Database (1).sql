create database BankingApp;
use BankingApp;


create table UserDetails(
Id int primary key identity(10000, 1),
Title varchar(5) not null,
FirstName varchar(20) not null,
MiddleName varchar(25),
LastName varchar(25) not null,
FatherName varchar(50) not null,
MobileNumber varchar(10) not null,
Email varchar(25),
AadharNumber varchar(16) not null,
DOB Date not null,
ResidentialAddressLine1 varchar(30) not null,
ResidentialAddressLine2 varchar(30)not null,
ResidentialLandmark varchar(30),
ResidentState varchar(30) not null,
ResidentialCity varchar(30) not null,
ResidentialPinCode varchar(6) not null,
PermanentAddressLine1 varchar(30) not null,
PermanentAddressLine2 varchar(30) not null,
PermanentLandmark varchar(30),
PermanentState varchar(30) not null,
PermanentCity varchar(30) not null,
PermanentPinCode varchar(6) not null,
OccupationalType varchar(20) not null,
SourceOfIncome varchar(20) not null,
GrossAnnualIncome varchar(10) not null,
IsNetBanking varchar(5) not null
)

alter table UserDetails add DebitCard varchar(5) not null

select * from UserDetails;


create table Customer(
AccountNumber bigint primary key not null,
CustomerId int unique not null foreign key references UserDetails(Id),
LoginPassword varchar(30) not null,
TransactionPassword varchar(30),
Balance bigint not null,
ReferenceId bigint unique not null,
Status varchar(10) not null,
Otp bigint
)

select * from Customer


create table Beneficiary(
BId int identity primary key,
HolderAccountNumber bigint foreign key references Customer(AccountNumber) on delete cascade not null,
BeneficiaryAccountNumber bigint foreign key references Customer(AccountNumber) not null,
BeneficiaryId int foreign key references UserDetails(Id) ,
Nickname varchar(10))


select * from Beneficiary


create table Transactions(
TId int identity primary key not null,
FromAccountNumber bigint not null foreign key references Customer(AccountNumber),
ToAccountNumber bigint not null foreign key references Customer(AccountNumber) on delete cascade,
TransactionType varchar(5) not null,
Amount int not null,
MaturityInstruction varchar(20),
Remark varchar(30),
TransactionDate DateTime not null
)

select * from Transactions;


-- Trigger to update balancee

go
create trigger balance_update
on [dbo].[Transactions]
for insert 
as
begin
	update c set Balance=Balance+i.Amount
	from dbo.Customer c inner join inserted i
	on c.AccountNumber=i.ToAccountNumber
	update c set Balance=Balance-i.Amount
	from dbo.Customer c inner join inserted i
	on c.AccountNumber=i.FromAccountNumber
end


-- inserting

insert into UserDetails values('Ms.','Laya','','Srivastava','Sanjay kumar srivastava','9123114358','layaav@gmail.com','712595306136','08/12/1998','ROAD NO. 5','abc','near bb','jharkhand','dhanbad','828109','ROAD NO. 5','abc','near bb','jharkhand','dhanbad','828109','service','job','500000','Y','Y')
insert into UserDetails values('Mr.','Abhjit','','Kumar','Vipin Kumar','9123110000','xyzbcd@gmail.com','712547306137','04/08/1994','ROAD NO. 7','yn street','ab road','delhi','delhi','828100','ROAD NO. 7','yn street','ab road','delhi','delhi','828100','service','job','500000','Y','N')
insert into customer values(1000004254,10002,'abc12345','abc678',50000,450021,'Approved',14524),(10457804254,10000,'ab345','abuyt8',500000,660021,'not Appr',14524)
insert into beneficiary values(1000004254,10457804254,10000,'ANU'),(10457804254,1000004254,10002,'Abhi')
insert into Transactions values(1000004254,10457804254,'IMPS','','MOVIE','05-12-2020',2500),(10457804254,1000004254,'IMPS','','FOOD','06-06-2019',500)
insert into UserDetails values('Ms.','Mamta','','Singh','Sujoy Singh','9150124785','mamv@gmail.com','717854316136','08/11/1978','GT ROAD NO','near kg','sp cottage','UP','Mirzapur','824709','GT ROAD NO','near kg','sp cottage','UP','Mirzapur','824709','service','job','560000','Y','Y'),('Mr.','Ajit','','Kumar','Siddharth Kumar','8423114500','xyzytd@gmail.com','712578456137','04/08/1996','ROAD NO. 14','park street','gt road','MP','INDORE','614100','ROAD NO. 14','park street','gt road','MP','INDORE','614100','service','teacher','20000','Y','N');
insert into Customer values(1000004715,10003,'abc48345','784678',56000,450022,'Approved',45724),(10454784254,10004,'cde345','ykuyt8',60000,660471,'Approved',14584)
insert into beneficiary values(1000004254,10454784254,10004,'Ria'),(1000004254,10457804254,10000,'ali'),(1000004715,10454784254,10004,'Ria'),(1000004715,10457804254,10000,'Ali'),(10454784254,1000004254,10002,''),(10454784254,10457804254,10003,'neha')
insert into Transactions values(1000004254,10457804254,'IMPS','','MOVIE','05-11-2020',500),(10457804254,1000004254,'RTGS','','FOOD','12-06-2019',1500),(10457804254,1000004254,'IMPS','','travel','07-11-2020',2500),(10454784254,10457804254,'RTGS','','FOOD','12-06-2019',1500)
insert into beneficiary values(1000004254,10457804254,null,'ANU')
insert into UserDetails values('Mr.','Namana','','Kumaiior','Vipin Kumar','912390000','xyzbcd@gmai.com','71254306137','04/08/1994','ROAD NO. 7','yn street','ab road','delhi','delhi','828100','ROAD NO. 7','yn street','ab road','delhi','delhi','828100','service','job','500000','Y','N')
insert into Customer values(100909079,11020,'mailchecknow','checkmaiuil',5590,486502,108804, 1, 0, 0)

select * from UserDetails;
select * from Customer;
select * from Beneficiary
select * from Transactions;
select * from Customer;

insert into transactions values (1000004715, 10454784254, 'IMPS', 6000, 'Check', 'Success', '05-12-2020');


create table AdminModule(AdminId int identity(20000,1) primary key,AdminPassword varchar(30) not null)
insert into AdminModule values('PASSWORD')
select * from AdminModule

alter table customer drop column status
alter table customer add IsApproved int
alter table customer add Status int
alter table customer add TotalCnt int

insert into customer values(1000009715,10001,'namana123','74567',56500,1234987,45002,1,1,0);

--Email Configuration

GO
SP_CONFIGURE 'show advanced options', 1
RECONFIGURE WITH OVERRIDE
GO
 
SP_CONFIGURE 'Database Mail XPs', 1
RECONFIGURE WITH OVERRIDE
GO
 
SP_CONFIGURE 'show advanced options', 0
RECONFIGURE WITH OVERRIDE
GO

EXEC msdb.dbo.sysmail_add_account_sp
    @account_name = 'Customer_Mail_Account'
   ,@description = 'Send emails using SQL Server Stored Procedure'
   ,@email_address = 'bankingapp2@gmail.com'
   ,@display_name = 'bank app'
   ,@replyto_address = 'bankingapp2@gmail.com'
   ,@mailserver_name = 'smtp.gmail.com'
   ,@username = 'bankingapp2@gmail.com'
   ,@password = 'bankapp@15'
   ,@port = 587
   ,@enable_ssl = 1
GO


EXEC msdb.dbo.sysmail_add_profile_sp
    @profile_name = 'Customer_Email_Profile'
   ,@description = 'Send emails using SQL Server Stored Procedure'
GO


EXEC msdb.dbo.sysmail_add_profileaccount_sp
    @profile_name = 'Customer_Email_Profile'
   ,@account_name = 'Customer_Mail_Account'
   ,@sequence_number = 1
GO


EXEC msdb.dbo.sp_send_dbmail
    @profile_name = 'Customer_Email_Profile'
   ,@recipients = 'bankingapp2@gmail.com'
   ,@subject = 'Email from SQL Server'
   ,@body = 'This is my First Email sent from SQL Server :)'
   ,@importance ='HIGH'
GO

-- Trigger for email

go
alter trigger SendEmail 
on [dbo].[Customer]
after insert
as 
begin
    Declare @Cid int
	Declare @AcctNo int
	Declare @LogPsw varchar(10)
	Declare @TrPsw varchar(10)
	Declare @otp int
    Select @Cid= Inserted.CustomerId,@AcctNo=Inserted.AccountNumber,@LogPsw=Inserted.LoginPassword,@TrPsw=Inserted.TransactionPassword,@otp=Inserted.Otp from Inserted
	declare @body varchar(500)='Dear User, Your Account Has Been Activated!! \\n' +
	'Your Customer ID is: ' + CAST(@CId AS VARCHAR(6)) + '\\n'
	+'Your Account Number: '+ CAST(@AcctNo AS VARCHAR(10))+ '\\n'
	+'Your LoginPassword: '+@LogPsw+ '\\n' +
	'Your Transaction Password: '+@TrPsw+ '\\n'+
	'Your OTP is: '+CAST(@otp AS VARCHAR(5)) + '\\n'+ 
	'Confidential Information. Please Do Not Share.'
	EXEC msdb.dbo.sp_send_dbmail
			@profile_name = 'Customer_Email_Profile',
			@recipients = 'bankingapp2@gmail.com'
           ,@subject = 'New Customer Record'
           ,@body = @body
           ,@importance ='HIGH'
END


-- Procedure for Login

create proc
 [dbo].[UserLogin](@Id int,@pwd varchar(30))
as
begin
select * from customer where CustomerId=@Id and LoginPassword=@pwd
end


-- Insertion

insert into UserDetails values('Mr.','Ram','','Kumar','Sanjay Kumar','9123114358','bankingapp2@gmail.com','714585306136','08-12-1998','House NO.2','Sector 23','Near huda market','Haryana','Gurgaon','122017','ROAD NO.2','Sector 23','Near huda market','Haryana','Gurgaon','122017','Science','Private','500000','Y','Y')
insert into UserDetails values('Ms.','Rita','Rajesh','Kumar','Rajesh Kumar','9257118358','bankingapp2@gmail.com','857495196136','12-11-1997','House NO.3','Sector 20','Near Bandra station','Maharashtra','Mumbai','230532','ROAD NO.5','Sector 24','Near GP Cottage','Maharashtra','Mumbai','230532','Manufacturing','Private','400000','N','Y')
insert into UserDetails values('Mr.','Ved','','Kaur','Jay Kaur','9123114342','bankingapp2@gmail.com','452194206136','04-12-1990','House NO.25','Sector 53','Near Katraj','Maharashtra','Pune','122037','House NO.25','Sector 53','Near Katraj','Maharashtra','Pune','122037','Technology','Private','700000','N','Y')
insert into UserDetails values('Mrs.','Neeta','','Mathur','Aditya kumar','7723344358','bankingapp2@gmail.com','789496206136','07-10-1994','House NO.21','Sector 21','Near Hooda Sweets','Haryana','Gurgaon','122017','ROAD NO.2','Sector 23','Near huda market','Haryana','Gurgaon','122017','Science','Public','600000','Y','Y')
insert into UserDetails values('Mr.','Ramesh','','Kumar','Sanjay kumar','8521114358','bankingapp2@gmail.com','423692006136','09-11-1988','ROAD NO.20','Sector 16','Near huda market','Chandigarh','Punjab','121117','ROAD NO.20','Sector 16','Near huda market','Chandigarh','Punjab','121117','Technology','Private','500000','N','N')



insert into Customer values(1000425790,11025,'abc12345','ab@c678',5000,450821,172819,1,0,0),(1000478490,11026,'abc1u@45','cd@c678',5000,456721,189819,1,0,0),(1000893490,11027,'abt#u@45','c$@c678',40000,498721,189019,1,0,0),(1000666490,11028,'ab89$u@45','cd@*618',5000,458821,189219,1,0,0)
insert into Customer values(1000425788,11029,'Abcd@1234','Qwer@1234',5000,457621,174819,1,0,0)

insert into Beneficiary values(1000425788,1000478490,11026,'RITU'),(1000425788,1000666490,11028,'RAMU'),(1000478490,1000893490,11027,'NITU'),(1000478490,1000425490,11025,'RIA'),(1000893490,1000478490,11026,'NIA'),(1000666490,1000478490,11026,'ANNE')
insert into Beneficiary values(1000425788,1000478490,11026,'RITU'),(1000425788,1000666490,11028,'RAMU')

insert into Transactions values(1000425788,1000478490,'IMPS',200,'CLOSE ON','Movie','05-11-2020'),(1000425788,1000478490,'RTGS',680,'CLOSE ON','Food','05-11-2020'),(1000425788,1000478490,'IMPS',400,'PAYOUT','Food','09-07-2020'),(1000425788,1000478490,'NEFT',100,'PAYOUT','Travel','01-11-2020'),(1000666490,1000478490,'NEFT',7000,'PAYOUT','Travel','01-11-2016'),(1000425490,1000478490,'RTGS',9000,'PAYOUT','Food','01-11-2020'),(1000478490,1000425490,'NEFT',3000,'PAYOUT','Food','02-12-2020')

select * from UserDetails
select * from Customer
select * from beneficiary
select * from transactions



insert into Customer values(100000425490,10002,'abc12345','ab@c678',50000,450021,172819,1,0,0),(100000478490,10001,'abc1u@45','cd@c678',60000,456721,189819,0,1,1),(100000893490,10003,'abt#u@45','c$@c678',40000,498721,189019,1,1,0),(100000666490,10004,'ab89$u@45','cd@*618',90000,458821,189219,0,0,1),(100000555490,10000,'ab78$u@45','cd@%8678',20000,416721,109819,0,1,0)

insert into Beneficiary values(100000425490,100000478490,10001,'RITU'),(100000893490,100000666490,10004,'RAMU'),(100000478490,100000893490,10003,'NITU')

insert into Transactions values(100000425490,100000478490,'IMPS',2000,'CLOSE ON','Movie','05-11-2020'),(100000478490,100000893490,'RTGS',5000,'CLOSE ON','Food','05-11-2018'),(100000893490,100000666490,'IMPS',4000,'PAYOUT','Food','09-07-2020'),(100000666490,100000478490,'NEFT',7000,'PAYOUT','Travel','01-11-2016'),(100000666490,100000478490,'NEFT',7000,'PAYOUT','Travel','01-11-2016'),(100000425490,100000478490,'RTGS',9000,'PAYOUT','Food','01-11-2020')