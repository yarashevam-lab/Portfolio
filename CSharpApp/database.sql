USE [Отдел_Образования]
GO

/****** Object:  Table [dbo].[Жители]    Script Date: 26.07.2026 23:11:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Жители](
	[id_Жителя] [int] NOT NULL,
	[ФИО] [nvarchar](100) NOT NULL,
	[Адрес] [nvarchar](100) NOT NULL,
	[Телефон] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Жители] PRIMARY KEY CLUSTERED 
(
	[id_Жителя] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [Отдел_Образования]
GO

/****** Object:  Table [dbo].[Образовательные_учреждения]    Script Date: 26.07.2026 23:12:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Образовательные_учреждения](
	[id_Образовательного_учреждения] [int] NOT NULL,
	[Название] [nvarchar](100) NOT NULL,
	[Адрес] [nchar](200) NOT NULL,
 CONSTRAINT [PK_Образовательные_учреждения] PRIMARY KEY CLUSTERED 
(
	[id_Образовательного_учреждения] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


USE [Отдел_Образования]
GO

/****** Object:  Table [dbo].[Обращения]    Script Date: 26.07.2026 23:12:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Обращения](
	[id_Обращения] [int] NOT NULL,
	[Текст] [nvarchar](500) NOT NULL,
	[Дата_создания] [date] NULL,
	[id_Сотрудника] [int] NOT NULL,
	[id_Статуса] [int] NOT NULL,
	[id_Образовательного_учреждения] [int] NOT NULL,
	[id_Жителя] [int] NOT NULL,
 CONSTRAINT [PK_Обращения] PRIMARY KEY CLUSTERED 
(
	[id_Обращения] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Обращения]  WITH CHECK ADD  CONSTRAINT [FK_Обращения_Жители] FOREIGN KEY([id_Жителя])
REFERENCES [dbo].[Жители] ([id_Жителя])
GO

ALTER TABLE [dbo].[Обращения] CHECK CONSTRAINT [FK_Обращения_Жители]
GO

ALTER TABLE [dbo].[Обращения]  WITH CHECK ADD  CONSTRAINT [FK_Обращения_Образовательные_учреждения] FOREIGN KEY([id_Образовательного_учреждения])
REFERENCES [dbo].[Образовательные_учреждения] ([id_Образовательного_учреждения])
GO

ALTER TABLE [dbo].[Обращения] CHECK CONSTRAINT [FK_Обращения_Образовательные_учреждения]
GO

ALTER TABLE [dbo].[Обращения]  WITH CHECK ADD  CONSTRAINT [FK_Обращения_Сотрудники] FOREIGN KEY([id_Сотрудника])
REFERENCES [dbo].[Сотрудники] ([id_Сотрудника])
GO

ALTER TABLE [dbo].[Обращения] CHECK CONSTRAINT [FK_Обращения_Сотрудники]
GO

ALTER TABLE [dbo].[Обращения]  WITH CHECK ADD  CONSTRAINT [FK_Обращения_Статусы] FOREIGN KEY([id_Статуса])
REFERENCES [dbo].[Статусы] ([id_Статуса])
GO

ALTER TABLE [dbo].[Обращения] CHECK CONSTRAINT [FK_Обращения_Статусы]
GO

USE [Отдел_Образования]
GO

/****** Object:  Table [dbo].[Пользователи]    Script Date: 26.07.2026 23:12:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Пользователи](
	[ID_пользователя] [int] IDENTITY(1,1) NOT NULL,
	[Логин] [nvarchar](50) NOT NULL,
	[Пароль] [nvarchar](50) NOT NULL,
	[Роль] [nvarchar](50) NOT NULL,
	[ID_сотрудника] [int] NULL,
 CONSTRAINT [PK__Пользова__B1AC0CDE8A361251] PRIMARY KEY CLUSTERED 
(
	[ID_пользователя] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Пользова__BC2217D3E4F194EE] UNIQUE NONCLUSTERED 
(
	[Логин] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Пользователи]  WITH CHECK ADD  CONSTRAINT [FK_Пользователи_Сотрудники] FOREIGN KEY([ID_сотрудника])
REFERENCES [dbo].[Сотрудники] ([id_Сотрудника])
GO

ALTER TABLE [dbo].[Пользователи] CHECK CONSTRAINT [FK_Пользователи_Сотрудники]
GO

ALTER TABLE [dbo].[Пользователи]  WITH CHECK ADD  CONSTRAINT [CK__Пользовате__Роль__70DDC3D8] CHECK  (([Роль]='Гость' OR [Роль]='Сотрудник' OR [Роль]='Администратор'))
GO

ALTER TABLE [dbo].[Пользователи] CHECK CONSTRAINT [CK__Пользовате__Роль__70DDC3D8]
GO

USE [Отдел_Образования]
GO

/****** Object:  Table [dbo].[Сотрудники]    Script Date: 26.07.2026 23:12:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Сотрудники](
	[id_Сотрудника] [int] NOT NULL,
	[ФИО] [nvarchar](100) NOT NULL,
	[Должность] [nvarchar](100) NOT NULL,
	[Телефон] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Сотрудники] PRIMARY KEY CLUSTERED 
(
	[id_Сотрудника] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [Отдел_Образования]
GO

/****** Object:  Table [dbo].[Статусы]    Script Date: 26.07.2026 23:12:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Статусы](
	[id_Статуса] [int] NOT NULL,
	[Название] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Статусы] PRIMARY KEY CLUSTERED 
(
	[id_Статуса] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


