USE [master]
GO
/****** Object:  Database [OrenairTraining]    Script Date: 09.12.2013 10:15:01 ******/
CREATE DATABASE [OrenairTraining]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OrenairTraining', FILENAME = N'D:\Bases\OrenairTraining.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'OrenairTraining_log', FILENAME = N'D:\Bases\OrenairTraining_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [OrenairTraining] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OrenairTraining].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OrenairTraining] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OrenairTraining] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OrenairTraining] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OrenairTraining] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OrenairTraining] SET ARITHABORT OFF 
GO
ALTER DATABASE [OrenairTraining] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OrenairTraining] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [OrenairTraining] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OrenairTraining] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OrenairTraining] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OrenairTraining] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OrenairTraining] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OrenairTraining] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OrenairTraining] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OrenairTraining] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OrenairTraining] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OrenairTraining] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OrenairTraining] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OrenairTraining] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OrenairTraining] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OrenairTraining] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OrenairTraining] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OrenairTraining] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OrenairTraining] SET RECOVERY FULL 
GO
ALTER DATABASE [OrenairTraining] SET  MULTI_USER 
GO
ALTER DATABASE [OrenairTraining] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OrenairTraining] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OrenairTraining] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OrenairTraining] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [OrenairTraining]
GO
/****** Object:  User [testsyst]    Script Date: 09.12.2013 10:15:01 ******/
CREATE USER [testsyst] FOR LOGIN [testsyst] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [NT AUTHORITY\СИСТЕМА]    Script Date: 09.12.2013 10:15:01 ******/
CREATE USER [NT AUTHORITY\СИСТЕМА] FOR LOGIN [NT AUTHORITY\СИСТЕМА] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [testsyst]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\СИСТЕМА]
GO
/****** Object:  Table [dbo].[answer]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[answer](
	[answer_id] [int] IDENTITY(1,1) NOT NULL,
	[session_id] [int] NOT NULL,
	[question_id] [int] NOT NULL,
	[useranswer] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_answer] PRIMARY KEY CLUSTERED 
(
	[answer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[container]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[container](
	[container_id] [int] IDENTITY(1,1) NOT NULL,
	[container_name] [nvarchar](255) NOT NULL,
	[ancestor_id] [int] NULL,
	[type_id] [int] NOT NULL,
	[deleted] [bit] NULL,
 CONSTRAINT [PK_container] PRIMARY KEY CLUSTERED 
(
	[container_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[containertodepartment]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[containertodepartment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[container_id] [int] NOT NULL,
	[department_id] [int] NOT NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_containertodepartment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[containertype]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[containertype](
	[containertype_id] [int] IDENTITY(1,1) NOT NULL,
	[containertype_name] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_containertype] PRIMARY KEY CLUSTERED 
(
	[containertype_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[department]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[department](
	[department_id] [int] IDENTITY(1,1) NOT NULL,
	[department_name] [nvarchar](255) NOT NULL,
	[deleted] [bit] NULL,
 CONSTRAINT [PK_department] PRIMARY KEY CLUSTERED 
(
	[department_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ipaddress]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ipaddress](
	[ipaddress_id] [int] IDENTITY(1,1) NOT NULL,
	[ipaddress_name] [nchar](10) NOT NULL,
	[deleted] [bit] NULL,
 CONSTRAINT [PK_ipaddress] PRIMARY KEY CLUSTERED 
(
	[ipaddress_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[log]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[log](
	[note_id] [int] IDENTITY(1,1) NOT NULL,
	[datetime] [datetime] NOT NULL,
	[operation_code] [nvarchar](10) NOT NULL,
	[user_id] [int] NULL,
	[objectcode_id] [int] NULL,
	[object_id] [int] NULL,
	[ip] [nvarchar](50) NULL,
 CONSTRAINT [PK_log] PRIMARY KEY CLUSTERED 
(
	[note_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[material]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[material](
	[material_id] [int] IDENTITY(1,1) NOT NULL,
	[material_name] [nvarchar](255) NOT NULL,
	[container_id] [int] NOT NULL,
	[deleted] [bit] NULL,
	[material_content] [varbinary](max) NOT NULL,
	[file_name] [nvarchar](255) NULL,
	[file_size] [float] NULL,
 CONSTRAINT [PK_material] PRIMARY KEY CLUSTERED 
(
	[material_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[objectcode]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[objectcode](
	[objectcode_id] [int] IDENTITY(1,1) NOT NULL,
	[objectcode_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_objectcode] PRIMARY KEY CLUSTERED 
(
	[objectcode_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[question]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[question](
	[question_id] [int] IDENTITY(1,1) NOT NULL,
	[text] [nvarchar](max) NOT NULL,
	[image] [varbinary](max) NULL,
	[answer] [nvarchar](max) NOT NULL,
	[type_id] [int] NOT NULL,
	[container_id] [int] NOT NULL,
	[material_id] [int] NULL,
	[time] [time](7) NULL,
	[deleted] [bit] NULL,
 CONSTRAINT [PK_question] PRIMARY KEY CLUSTERED 
(
	[question_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[questiontype]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[questiontype](
	[questiontype_id] [int] IDENTITY(1,1) NOT NULL,
	[questiontype_name] [nvarchar](255) NOT NULL,
	[deleted] [bit] NOT NULL,
 CONSTRAINT [PK_questiontype] PRIMARY KEY CLUSTERED 
(
	[questiontype_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[role]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](255) NOT NULL,
	[deleted] [bit] NULL,
 CONSTRAINT [PK_role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[session]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[session](
	[session_id] [int] IDENTITY(1,1) NOT NULL,
	[datetime] [datetime] NOT NULL,
	[user_id] [int] NOT NULL,
	[testconfig_id] [int] NOT NULL,
	[ipaddress] [nvarchar](255) NULL,
	[deleted] [bit] NULL,
	[result] [int] NULL,
 CONSTRAINT [PK_session] PRIMARY KEY CLUSTERED 
(
	[session_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[testconfig]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[testconfig](
	[testconf_id] [int] IDENTITY(1,1) NOT NULL,
	[testconf_name] [nvarchar](255) NOT NULL,
	[time] [time](7) NULL,
	[deleted] [bit] NULL,
	[themes] [nvarchar](255) NULL,
	[questions] [nvarchar](max) NULL,
	[criterion] [int] NULL,
 CONSTRAINT [PK_testconfig] PRIMARY KEY CLUSTERED 
(
	[testconf_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[testtouser]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[testtouser](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[testconf_id] [int] NOT NULL,
	[is_completed] [bit] NULL,
	[date_of_assignment] [datetime] NULL,
	[deleted] [bit] NULL,
 CONSTRAINT [PK_testtouser] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user]    Script Date: 09.12.2013 10:15:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](255) NOT NULL,
	[tab_num] [int] NULL,
	[password] [nvarchar](255) NULL,
	[firstname] [nvarchar](255) NOT NULL,
	[surname] [nvarchar](255) NOT NULL,
	[patronymic] [nvarchar](255) NULL,
	[fio] [nvarchar](255) NULL,
	[birthdate] [date] NULL,
	[department_id] [int] NULL,
	[regdate] [date] NULL,
	[loggedon] [bit] NULL,
	[deleted] [bit] NULL,
	[last_activity_date] [date] NULL,
	[role_id] [int] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[answer]  WITH CHECK ADD  CONSTRAINT [FK_answer_question] FOREIGN KEY([question_id])
REFERENCES [dbo].[question] ([question_id])
GO
ALTER TABLE [dbo].[answer] CHECK CONSTRAINT [FK_answer_question]
GO
ALTER TABLE [dbo].[answer]  WITH CHECK ADD  CONSTRAINT [FK_answer_session] FOREIGN KEY([session_id])
REFERENCES [dbo].[session] ([session_id])
GO
ALTER TABLE [dbo].[answer] CHECK CONSTRAINT [FK_answer_session]
GO
ALTER TABLE [dbo].[container]  WITH CHECK ADD  CONSTRAINT [FK_container_container] FOREIGN KEY([ancestor_id])
REFERENCES [dbo].[container] ([container_id])
GO
ALTER TABLE [dbo].[container] CHECK CONSTRAINT [FK_container_container]
GO
ALTER TABLE [dbo].[container]  WITH CHECK ADD  CONSTRAINT [FK_container_containertype] FOREIGN KEY([type_id])
REFERENCES [dbo].[containertype] ([containertype_id])
GO
ALTER TABLE [dbo].[container] CHECK CONSTRAINT [FK_container_containertype]
GO
ALTER TABLE [dbo].[containertodepartment]  WITH CHECK ADD  CONSTRAINT [FK_containertodepartment_container] FOREIGN KEY([container_id])
REFERENCES [dbo].[container] ([container_id])
GO
ALTER TABLE [dbo].[containertodepartment] CHECK CONSTRAINT [FK_containertodepartment_container]
GO
ALTER TABLE [dbo].[containertodepartment]  WITH CHECK ADD  CONSTRAINT [FK_containertodepartment_department] FOREIGN KEY([department_id])
REFERENCES [dbo].[department] ([department_id])
GO
ALTER TABLE [dbo].[containertodepartment] CHECK CONSTRAINT [FK_containertodepartment_department]
GO
ALTER TABLE [dbo].[log]  WITH CHECK ADD  CONSTRAINT [FK_log_objectcode] FOREIGN KEY([objectcode_id])
REFERENCES [dbo].[objectcode] ([objectcode_id])
GO
ALTER TABLE [dbo].[log] CHECK CONSTRAINT [FK_log_objectcode]
GO
ALTER TABLE [dbo].[log]  WITH CHECK ADD  CONSTRAINT [FK_log_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[log] CHECK CONSTRAINT [FK_log_user]
GO
ALTER TABLE [dbo].[material]  WITH CHECK ADD  CONSTRAINT [FK_material_container] FOREIGN KEY([container_id])
REFERENCES [dbo].[container] ([container_id])
GO
ALTER TABLE [dbo].[material] CHECK CONSTRAINT [FK_material_container]
GO
ALTER TABLE [dbo].[question]  WITH CHECK ADD  CONSTRAINT [FK_question_container] FOREIGN KEY([container_id])
REFERENCES [dbo].[container] ([container_id])
GO
ALTER TABLE [dbo].[question] CHECK CONSTRAINT [FK_question_container]
GO
ALTER TABLE [dbo].[question]  WITH CHECK ADD  CONSTRAINT [FK_question_material] FOREIGN KEY([material_id])
REFERENCES [dbo].[material] ([material_id])
GO
ALTER TABLE [dbo].[question] CHECK CONSTRAINT [FK_question_material]
GO
ALTER TABLE [dbo].[question]  WITH CHECK ADD  CONSTRAINT [FK_question_questiontype] FOREIGN KEY([type_id])
REFERENCES [dbo].[questiontype] ([questiontype_id])
GO
ALTER TABLE [dbo].[question] CHECK CONSTRAINT [FK_question_questiontype]
GO
ALTER TABLE [dbo].[session]  WITH CHECK ADD  CONSTRAINT [FK_session_testconfig] FOREIGN KEY([testconfig_id])
REFERENCES [dbo].[testconfig] ([testconf_id])
GO
ALTER TABLE [dbo].[session] CHECK CONSTRAINT [FK_session_testconfig]
GO
ALTER TABLE [dbo].[session]  WITH CHECK ADD  CONSTRAINT [FK_session_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[session] CHECK CONSTRAINT [FK_session_user]
GO
ALTER TABLE [dbo].[testtouser]  WITH CHECK ADD  CONSTRAINT [FK_testtouser_testconfig] FOREIGN KEY([testconf_id])
REFERENCES [dbo].[testconfig] ([testconf_id])
GO
ALTER TABLE [dbo].[testtouser] CHECK CONSTRAINT [FK_testtouser_testconfig]
GO
ALTER TABLE [dbo].[testtouser]  WITH CHECK ADD  CONSTRAINT [FK_testtouser_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[testtouser] CHECK CONSTRAINT [FK_testtouser_user]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_department] FOREIGN KEY([department_id])
REFERENCES [dbo].[department] ([department_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_department]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_role] FOREIGN KEY([role_id])
REFERENCES [dbo].[role] ([role_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_role]
GO
USE [master]
GO
ALTER DATABASE [OrenairTraining] SET  READ_WRITE 
GO
