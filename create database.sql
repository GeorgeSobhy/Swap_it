CREATE DATABASE SwapIt; 
 
GO 

USE SwapIt;
GO 
 

CREATE TABLE [dbo].[City](
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[Name] [nvarchar](100) NULL, 
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]  
GO

 
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[Name] [nvarchar](100) NULL, 
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO


CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[Name] [nvarchar](100) NULL, 
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO




CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] not NULL,
	[Name] [nvarchar](100) not NULL,
	[Email] [varchar](200) not NULL,
	[Password] [nvarchar](500) not NULL,	
	[Address] [nvarchar](500) NULL,
	[CityId] [int] NULL,
	[CountryId] [int] NULL,
	[Comment] [nvarchar](500) NULL,
	[ImageUrl] [varchar](300) NULL,
	[Active] [Bit] not NULL default(0),
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_Country] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Country] ([id])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Country]
GO

ALTER TABLE [dbo].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_City] FOREIGN KEY([CityId])
REFERENCES [dbo].[City] ([id])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_City]
GO
 

GO
ALTER TABLE [dbo].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([id])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO



--========================

 
CREATE TABLE [dbo].[ServiceType](
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[Name] [nvarchar](100) NULL, 
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO

 
CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[ServiceTypeId] [int] not NULL, 
	[Name] [nvarchar](100) not NULL, 
	[Description] [nvarchar](2000) NULL, 
	[ProviderId] [int] not NULL, 
	[Price] [float] not NULL, 
	[ExecutionTime] [float] not NULL, 
	[Active] [Bit] not NULL default(0),
	[CreationDate] [datetime] NOT NULL, 
	 
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO

GO
ALTER TABLE [dbo].[Service]  WITH NOCHECK ADD  CONSTRAINT [FK_Service_ServiceType] FOREIGN KEY([ServiceTypeId])
REFERENCES [dbo].[ServiceType] ([id])
GO

ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceType]
GO

GO
ALTER TABLE [dbo].[Service]  WITH NOCHECK ADD  CONSTRAINT [FK_Service_Provider] FOREIGN KEY([ProviderId])
REFERENCES [dbo].[User] ([id])
GO

ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Provider]
GO



 
CREATE TABLE [dbo].[ServiceImage](
	[Id] [int] IDENTITY(1,1) NOT NULL,  
	[ServiceId] [int] not NULL,  
	[ImageUrl] [varchar](300) not NULL,   
	[CreationDate] [datetime] NOT NULL, 
	 
 CONSTRAINT [PK_ServiceImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO


GO
ALTER TABLE [dbo].[ServiceImage]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceImage_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([id])
GO

ALTER TABLE [dbo].[ServiceImage] CHECK CONSTRAINT [FK_ServiceImage_Service]
GO


-- =======================
CREATE TABLE [dbo].[ServiceStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[Name] [nvarchar](100) NULL, 
 CONSTRAINT [PK_ServiceStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO
 
CREATE TABLE [dbo].[PaymentStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL, 
	[Name] [nvarchar](100) NULL, 
 CONSTRAINT [PK_PaymentStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO
 

 
CREATE TABLE [dbo].[ServiceRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,  
	[ServiceId] [int] not NULL,   
	[CustomerId] [int] not NULL,  
	[StatusId] [int] not NULL,
	[PaymentStatusId] [int]  NULL,
	[Notes] [nvarchar] (1000)  NULL,
	[Feedback] [nvarchar] (2000)  NULL,
	[Rating] [int]   NULL,
	[CreationDate] [datetime] NOT NULL, 
	 
 CONSTRAINT [PK_ServiceRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 
GO
 

GO
ALTER TABLE [dbo].[ServiceRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceRequest_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[User] ([id])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_ServiceRequest_Customer]
GO

GO
ALTER TABLE [dbo].[ServiceRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceRequest_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([id])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_ServiceRequest_Service]
GO

GO
ALTER TABLE [dbo].[ServiceRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceRequest_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[ServiceStatus] ([id])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_ServiceRequest_Status]
GO


GO
ALTER TABLE [dbo].[ServiceRequest]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceRequest_PaymentStatus] FOREIGN KEY([PaymentStatusId])
REFERENCES [dbo].[PaymentStatus] ([id])
GO

ALTER TABLE [dbo].[ServiceRequest] CHECK CONSTRAINT [FK_ServiceRequest_PaymentStatus]
GO

--========================

 
CREATE TABLE [dbo].[ServiceBookmark](
	[Id] [int] IDENTITY(1,1) NOT NULL,  
	[ServiceId] [int] not NULL,   
	[CustomerId] [int] not NULL,   
	[CreationDate] [datetime] NOT NULL, 
	 
 CONSTRAINT [PK_ServiceBookmark] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

GO
ALTER TABLE [dbo].[ServiceBookmark]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceBookmark_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([id])
GO

ALTER TABLE [dbo].[ServiceBookmark] CHECK CONSTRAINT [FK_ServiceBookmark_Service]
GO


GO
ALTER TABLE [dbo].[ServiceBookmark]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceBookmark_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[User] ([id])
GO

ALTER TABLE [dbo].[ServiceBookmark] CHECK CONSTRAINT [FK_ServiceBookmark_Customer]
GO

---======================

CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,  
	[ServiceRequestId] [int] NULL,     
	[UserId] [int] not NULL,   
	[Content] [nvarchar] (1000)  NULL,
	[CreationDate] [datetime] NOT NULL, 
	 
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

GO

GO
ALTER TABLE [dbo].[Notification]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceBookmark_ServiceRequest] FOREIGN KEY([ServiceRequestId])
REFERENCES [dbo].[ServiceRequest] ([id])
GO

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_ServiceBookmark_ServiceRequest]
GO

GO
ALTER TABLE [dbo].[Notification]  WITH NOCHECK ADD  CONSTRAINT [FK_ServiceBookmark_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([id])
GO

ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_ServiceBookmark_User]
GO


--========================


CREATE TABLE [dbo].[CustomerBalance](
	[Id] [int] IDENTITY(1,1) NOT NULL,     
	[CustomerId] [int] not NULL,   
	[Points] [int] not NULL default(0), 
	[CreationDate] [datetime] NOT NULL, 
	 
 CONSTRAINT [PK_CustomerBalance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] 

GO

GO
ALTER TABLE [dbo].[CustomerBalance]  WITH NOCHECK ADD  CONSTRAINT [FK_CustomerBalance_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[User] ([id])
GO

ALTER TABLE [dbo].[CustomerBalance] CHECK CONSTRAINT [FK_CustomerBalance_Customer]
GO

 
 --=====
 -- Insert User to login
 go
 IF(Not exists(select 1 from [Role] where Name = 'Admin'))
 begin
	Insert into [Role]  values('Admin')
 END
 go
  IF(Not exists(select 1 from [Role] where Name = 'Provider'))
 begin
	Insert into [Role]  values('Provider')
 END
 go
  IF(Not exists(select 1 from [Role] where Name = 'Customer'))
 begin
	Insert into [Role]  values('Customer')
 END
 go
  IF(Not exists(select 1 from [User] where [Email] = 'admin@swapit.com'))
 begin
	Insert into [User] (RoleId,
	[Name],
	[Email],
	[Password], 
	Active,
	CreationDate) values(1,'Administrator','admin@swapit.com','Pass123',1,getdate()) 
 END
 go
 --=========================
 -- Tables to add to code
--City
--Country
--CustomerBalance
--Notification
--PaymentStatus 
--ServiceBookmark
--ServiceImage
--ServiceRequest
--ServiceStatus
 
 
  