/*==============================================================*/
/* Database name:  Database_1                                   */
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2015/4/20 17:34:46                           */
/*==============================================================*/


drop database YueRen
go

/*==============================================================*/
/* Database: YueRen*/
/*==============================================================*/
create database YueRen
go

use YueRen
go

/*==============================================================*/
/* Table: YueRen_BandID                                         */
/*==============================================================*/
create table YueRen_BandID (
   BandID               int                  not null,
   BandName             nvarchar(30)         not null,
   CountryID            int                  not null,
   ProvinceID           int                  not null,
   CityID               int                  not null,
   BandStatus           int                  not null,
   constraint PK_YUEREN_BANDID primary key (BandID)
)
go

/*==============================================================*/
/* Table: YueRen_EventID                                        */
/*==============================================================*/
create table YueRen_EventID (
   EventID              bigint               not null,
   EventType            int                  not null,
   EventContent         nvarchar(max)        not null,
   OccurtingTime        datetime             not null,
   constraint PK_YUEREN_EVENTID primary key (EventID)
)
go

/*==============================================================*/
/* Table: YueRen_MemberIntro                                    */
/*==============================================================*/
create table YueRen_MemberIntro (
   BandID               int                  not null,
   MemberName           nvarchar(20)         not null,
   MemberPositionID     int                  not null,
   Intro                nvarchar(max)        null,
   constraint PK_YUEREN_MEMBERINTRO primary key (BandID)
)
go

/*==============================================================*/
/* Table: YueRen_School                                         */
/*==============================================================*/
create table YueRen_School (
   UserID               bigint               not null,
   SchoolTypeID         int                  not null,
   SchoolName           nvarchar(50)         not null,
   EnterTime            datetime             not null,
   constraint PK_YUEREN_SCHOOL primary key (UserID)
)
go

/*==============================================================*/
/* Table: YueRen_User                                           */
/*==============================================================*/
create table YueRen_User (
   UserID               bigint               identity,
   Name                 nvarchar(20)         not null,
   GraduateSchool       nvarchar(50)         null,
   Gender               int                  not null,
   Country              int                  null,
   City                 int                  null,
   Province             int                  null,
   District             nvarchar(10)         null,
   Address              nvarchar(50)         null,
   Age                  int                  null,
   CreatedTime          datetime             not null,
   UpdatedTime          datetime             not null,
   Head_Image           nvarchar(max)        not null,
   constraint PK_YUEREN_USER primary key (UserID)
)
go

/*==============================================================*/
/* Table: YueRen_UserLog                                        */
/*==============================================================*/
create table YueRen_UserLog (
   UserID               bigint               not null,
   Log                  nvarchar(Max)        null,
   CreatedTime          datetime             not null,
   UpdatedTime          datetime             not null,
   constraint PK_YUEREN_USERLOG primary key (UserID)
)
go

/*==============================================================*/
/* Table: YueRen_UserRelation                                   */
/*==============================================================*/
create table YueRen_UserRelation (
   UserID               bigint               not null,
   UserID2              bigint               not null,
   Relation             int                  not null,
   constraint PK_YUEREN_USERRELATION primary key (UserID, UserID2)
)
go

/*==============================================================*/
/* Table: YueRen_UserStatus                                     */
/*==============================================================*/
create table YueRen_UserStatus (
   UserID               bigint               not null,
   Status               nvarchar(255)        null,
   CreatedTime          datetime             not null,
   constraint PK_YUEREN_USERSTATUS primary key (UserID)
)
go

/*==============================================================*/
/* Table: YueRen_VisitLog                                       */
/*==============================================================*/
create table YueRen_VisitLog (
   UserID               bigint               not null,
   EventTime            datetime             not null,
   constraint PK_YUEREN_VISITLOG primary key (UserID)
)
go

/*==============================================================*/
/* Table: Yueren_UserInstrumentRelation                         */
/*==============================================================*/
create table Yueren_UserInstrumentRelation (
   UserID               bigint               not null,
   InstrumentID         int                  not null,
   constraint PK_YUEREN_USERINSTRUMENTRELATI primary key (UserID)
)
go

alter table YueRen_MemberIntro
   add constraint FK_YUEREN_M_FK_BANDME_YUEREN_B foreign key (BandID)
      references YueRen_BandID (BandID)
go

alter table YueRen_School
   add constraint FK_YUEREN_S_FK_USERSC_YUEREN_U foreign key (UserID)
      references YueRen_User (UserID)
go

alter table YueRen_UserLog
   add constraint FK_YUEREN_U_FK_USERLO_YUEREN_U foreign key (UserID)
      references YueRen_User (UserID)
go

alter table YueRen_UserRelation
   add constraint FK_YUEREN_U_FK_USERRE_YUEREN_U foreign key (UserID)
      references YueRen_User (UserID)
go

alter table YueRen_UserStatus
   add constraint FK_YUEREN_U_FK_USERST_YUEREN_U foreign key (UserID)
      references YueRen_User (UserID)
go

alter table YueRen_VisitLog
   add constraint FK_YUEREN_V_FK_VISITE_YUEREN_U foreign key (UserID)
      references YueRen_User (UserID)
go

alter table Yueren_UserInstrumentRelation
   add constraint FK_YUEREN_U_FK_USERIN_YUEREN_U foreign key (UserID)
      references YueRen_User (UserID)
go

