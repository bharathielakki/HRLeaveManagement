USE [HRLeaveDB]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DeptId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[DeptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NULL,
	[Email] [nvarchar](150) NULL,
	[DeptId] [int] NULL,
	[ManagerId] [int] NULL,
	[IsActive] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_Employees]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_Employees]
AS
SELECT 
E.EmpId,
E.FullName,
E.Email,
E.DeptId,
D.Name AS DepartmentName,
E.ManagerId,
E.IsActive
FROM Employees E
LEFT JOIN Departments D ON E.DeptId = D.DeptId
GO
/****** Object:  Table [dbo].[LeaveTypes]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveTypes](
	[LeaveTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[DefaultDaysPerYear] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LeaveTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveRequests]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveRequests](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[LeaveTypeId] [int] NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[BusinessDays] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[ManagerComment] [nvarchar](300) NULL,
	[CreatedAt] [datetime] NULL,
	[ReviewedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_LeaveRequests]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_LeaveRequests]
AS
SELECT 
LR.RequestId,
LR.EmpId,
E.FullName,
LT.Name AS LeaveType,
LR.StartDate,
LR.EndDate,
LR.BusinessDays,
LR.Status,
LR.ManagerComment,
LR.CreatedAt,
LR.ReviewedAt
FROM LeaveRequests LR
JOIN Employees E ON LR.EmpId = E.EmpId
JOIN LeaveTypes LT ON LR.LeaveTypeId = LT.LeaveTypeId
GO
/****** Object:  Table [dbo].[LeaveBalances]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveBalances](
	[BalanceId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[LeaveTypeId] [int] NULL,
	[Year] [int] NULL,
	[TotalDays] [int] NULL,
	[UsedDays] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BalanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[RequestId] [int] NULL,
	[EmpId] [int] NULL,
	[Amount] [decimal](10, 2) NULL,
	[Gateway] [nvarchar](100) NULL,
	[GatewayRef] [nvarchar](200) NULL,
	[Status] [nvarchar](50) NULL,
	[PaidAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[Username] [nvarchar](100) NULL,
	[PasswordHash] [nvarchar](200) NULL,
	[Role] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[USP_AddEmployee]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_AddEmployee]
(
    @FullName NVARCHAR(150),
    @Email NVARCHAR(150),
    @DeptId INT,
    @ManagerId INT

)
AS
BEGIN

INSERT INTO Employees(FullName,Email,DeptId,ManagerId,IsActive)
VALUES(@FullName,@Email,@DeptId,@ManagerId,1)

DECLARE @EmpId INT = SCOPE_IDENTITY()
DECLARE @Year INT = YEAR(GETDATE())

INSERT INTO LeaveBalances(EmpId,LeaveTypeId,Year,TotalDays,UsedDays)
SELECT @EmpId, LeaveTypeId, @Year, DefaultDaysPerYear, 0
FROM LeaveTypes


END
GO
/****** Object:  StoredProcedure [dbo].[USP_ApproveLeaveRequest]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_ApproveLeaveRequest]
    @RequestId INT,
    @ManagerId INT,
    @Comment NVARCHAR(300) = NULL,
    @BusinessDays INT,
    @EmpId INT,
    @LeaveTypeId INT
AS
BEGIN
    UPDATE LeaveRequests
    SET Status = 'Approved',
        ManagerComment = @Comment,
        ReviewedAt = GETDATE()
    WHERE RequestId = @RequestId AND Status = 'Pending'


 UPDATE LeaveBalances
SET UsedDays = UsedDays + @BusinessDays
WHERE EmpId = @EmpId
AND LeaveTypeId = @LeaveTypeId
AND Year = YEAR(GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CancelLeaveRequest]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CancelLeaveRequest]
    @RequestId INT,
    @EmpId INT
AS
BEGIN

    UPDATE LeaveRequests SET Status = 'Cancelled', ReviewedAt = GETDATE()
    WHERE RequestId = @RequestId AND EmpId = @EmpId AND Status = 'Pending';
END
GO
/****** Object:  StoredProcedure [dbo].[USP_CheckLeaveOverlap]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_CheckLeaveOverlap]
    @EmpId INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    -- Count overlapping approved leaves
    IF EXISTS (
        SELECT 1
        FROM LeaveRequests
        WHERE EmpId = @EmpId
          AND Status = 'Approved'
          AND (
               (@StartDate BETWEEN StartDate AND EndDate) OR
               (@EndDate BETWEEN StartDate AND EndDate) OR
               (StartDate BETWEEN @StartDate AND @EndDate)
              )
    )
    BEGIN
        -- Overlap exists
        SELECT 1 AS IsOverlap
    END
    ELSE
    BEGIN
        SELECT 0 AS IsOverlap
    END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_DeleteEmployee]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_DeleteEmployee]
@EmpId INT
AS
BEGIN

BEGIN TRY

BEGIN TRANSACTION

UPDATE  Employees set IsActive=0
WHERE EmpId=@EmpId

COMMIT TRANSACTION

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION

DECLARE @ErrorMessage NVARCHAR(4000)
SET @ErrorMessage = ERROR_MESSAGE()

RAISERROR(@ErrorMessage,16,1)

END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[USP_Department]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_Department]
AS
BEGIN

SELECT * FROM Departments

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetEmployeeById]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetEmployeeById]
@EmpId INT
AS
BEGIN

SELECT * FROM vw_Employees
WHERE EmpId=@EmpId

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetEmployeeLeaveBalance]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetEmployeeLeaveBalance]
    @EmpId INT,
    @Year INT
AS
BEGIN
    SELECT 
        lb.EmpId,
        lt.LeaveTypeId,
        lt.Name AS LeaveType,
        lb.TotalDays,
        lb.UsedDays,
        (lb.TotalDays - lb.UsedDays) AS RemainingDays
    FROM LeaveBalances lb
    JOIN LeaveTypes lt ON lb.LeaveTypeId = lt.LeaveTypeId
    WHERE lb.EmpId = @EmpId AND lb.Year = @Year
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetEmployeeLeaveRequests]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_GetEmployeeLeaveRequests]
@EmpId INT
AS
BEGIN

SELECT * 
FROM LeaveRequests
WHERE EmpId=@EmpId

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetEmployees]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[USP_GetEmployees]
AS
BEGIN

SELECT * FROM vw_Employees where IsActive=1

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetLeaveReport]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetLeaveReport]
    @DeptId INT = NULL,
    @EmpId INT = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @Status NVARCHAR(50) = NULL
AS
BEGIN

SELECT 
    lr.RequestId,
    e.FullName,
    d.Name AS Department,
    lt.Name AS LeaveType,
    lr.StartDate,
    lr.EndDate,
    lr.BusinessDays,
    lr.Status,
    lr.CreatedAt
FROM LeaveRequests lr
JOIN Employees e ON lr.EmpId = e.EmpId
JOIN Departments d ON e.DeptId = d.DeptId
JOIN LeaveTypes lt ON lr.LeaveTypeId = lt.LeaveTypeId

WHERE
(@DeptId IS NULL OR e.DeptId = @DeptId)
AND (@EmpId IS NULL OR lr.EmpId = @EmpId)
AND (@StartDate IS NULL OR lr.StartDate >= @StartDate)
AND (@EndDate IS NULL OR lr.EndDate <= @EndDate)
AND (@Status IS NULL OR lr.Status = @Status)

ORDER BY lr.CreatedAt DESC

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetManagers]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[USP_GetManagers]
AS
BEGIN

SELECT 
e.EmpId,
e.FullName
FROM Employees e
JOIN Users u ON e.EmpId = u.EmpId
WHERE u.Role = 'Manager'
AND e.IsActive = 1

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetPendingRequests]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetPendingRequests]
    @DeptId INT = NULL
AS
BEGIN
    SELECT 
lr.RequestId,
lr.EmpId,
e.FullName AS EmployeeName,
lr.LeaveTypeId,
lt.Name AS LeaveTypeName,
lr.StartDate,
lr.EndDate,
lr.BusinessDays,
e.DeptId
FROM LeaveRequests lr
JOIN Employees e ON lr.EmpId = e.EmpId
JOIN LeaveTypes lt ON lr.LeaveTypeId = lt.LeaveTypeId

WHERE lr.Status = 'Pending'
    --AND (@DeptId IS NULL OR e.DeptId = @DeptId)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetTeamLeaveCalendar]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_GetTeamLeaveCalendar]
    @Month INT,
    @Year INT,
    @DeptId INT
AS
BEGIN

    SELECT 
        lr.StartDate,
        lr.EndDate,
        e.FullName AS Employee,
        lt.Name AS LeaveType,
        lr.Status
    FROM LeaveRequests lr
    JOIN Employees e ON lr.EmpId = e.EmpId
    JOIN LeaveTypes lt ON lr.LeaveTypeId = lt.LeaveTypeId
    WHERE 
        MONTH(lr.StartDate) = @Month
        AND YEAR(lr.StartDate) = @Year
        AND e.DeptId = @DeptId
        AND lr.Status = 'Approved'

END
GO
/****** Object:  StoredProcedure [dbo].[USP_LoginUser]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_LoginUser]
@Username NVARCHAR(100)
AS
BEGIN
    SELECT UserId,EmpId,Username,PasswordHash,Role
    FROM Users
    WHERE Username=@Username
END
GO
/****** Object:  StoredProcedure [dbo].[USP_RejectLeaveRequest]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_RejectLeaveRequest]
    @RequestId INT,
    @ManagerId INT,
    @Comment NVARCHAR(300)
AS
BEGIN
    UPDATE LeaveRequests
    SET Status = 'Rejected',
        ManagerComment = @Comment,
        ReviewedAt = GETDATE()
    WHERE RequestId = @RequestId AND Status = 'Pending'
END
GO
/****** Object:  StoredProcedure [dbo].[USP_SubmitLeaveRequest]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_SubmitLeaveRequest]
@EmpId INT,
@LeaveTypeId INT,
@StartDate DATE,
@EndDate DATE,
@BusinessDays INT
AS
BEGIN

BEGIN TRY

BEGIN TRANSACTION

INSERT INTO LeaveRequests
(EmpId,LeaveTypeId,StartDate,EndDate,BusinessDays,Status,CreatedAt)
VALUES
(@EmpId,@LeaveTypeId,@StartDate,@EndDate,@BusinessDays,'Pending',GETDATE())

COMMIT TRANSACTION

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION

DECLARE @ErrorMessage NVARCHAR(4000)
SET @ErrorMessage = ERROR_MESSAGE()

RAISERROR(@ErrorMessage,16,1)

END CATCH

END
GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateEmployee]    Script Date: 11/03/2026 9:57:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[USP_UpdateEmployee]
@EmpId INT,
@FullName NVARCHAR(150),
@Email NVARCHAR(150),
@DeptId INT,
@ManagerId INT,
@IsActive BIT
AS
BEGIN

BEGIN TRY

BEGIN TRANSACTION

UPDATE Employees
SET
FullName=@FullName,
Email=@Email,
DeptId=@DeptId,
ManagerId=@ManagerId
WHERE EmpId=@EmpId

COMMIT TRANSACTION

END TRY

BEGIN CATCH

ROLLBACK TRANSACTION

DECLARE @ErrorMessage NVARCHAR(4000)
SET @ErrorMessage = ERROR_MESSAGE()

RAISERROR(@ErrorMessage,16,1)

END CATCH

END
GO
IF NOT EXISTS (SELECT 1 FROM Departments)
BEGIN
    INSERT INTO Departments(Name)
    VALUES ('HR'),('IT'),('Finance');
END
GO


IF NOT EXISTS (SELECT 1 FROM Employees)
BEGIN
    INSERT INTO Employees (FullName, Email, DeptId, ManagerId, IsActive)
    VALUES 
    ('Alice','alice.manager@gmail.com',1,0,1),  
    ('Bob','bob.hr@gmail.com',2,1,1),               
    ('Charlie','charlie.employee@gmail.com',3,1,1)
END
GO


IF NOT EXISTS (SELECT 1 FROM Users)
BEGIN
    INSERT INTO Users (EmpId, Username, PasswordHash, Role)
    VALUES
    (1,'alice.manager@gmail.com','alice@123','Manager'),
    (2,'bob.hr@gmail.com','bob@123','HRAdmin'),
    (3,'charlie.employee@gmail.com','charlie@123','Employee')
END
GO


IF NOT EXISTS (SELECT 1 FROM LeaveTypes)
BEGIN
    INSERT INTO LeaveTypes(Name, DefaultDaysPerYear)
    VALUES ('Annual',20),('Sick',10),('Unpaid',0),('Emergency',5)
END
GO
