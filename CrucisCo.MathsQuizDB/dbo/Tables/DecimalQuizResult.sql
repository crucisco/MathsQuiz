CREATE TABLE [dbo].[DecimalQuizResult]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[QuizUser] [varchar](150) NOT NULL,
	[DecimalRange] [varchar](100) NOT NULL,
	[QuizDate] [datetime] NOT NULL,
	[Answered] [int] NOT NULL,
	[Correct] [int] NOT NULL,
 CONSTRAINT [PK_DecimalQuizResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]