-- *******************************************************
-- ***   Create Table ATDTRN(受講トラン)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS ATDTRN;
CREATE TABLE ATDTRN (
    DATKB                          VARCHAR(1)   NOT NULL,
    LESSONDT                       VARCHAR(8)   NOT NULL,
    TIMEZONE			   INT          NOT NULL,
    TANNO                          VARCHAR(3)   NOT NULL,
    LINNO                          VARCHAR(3)   NOT NULL,
    SCLMANNO                       VARCHAR(6),
    SCLKBN                         VARCHAR(1)   NOT NULL,
    MANNO                          VARCHAR(8),
    LESSONKBN                      VARCHAR(3),
    ATDPO                          INT,
    ATDTKTSU                       INT,
    COMENT                         VARCHAR(100),
    RSVDT                          VARCHAR(8),
    RSVTIMEZONE                    INT,
    RSVTANNO                       VARCHAR(3),
    RSVLINNO                       VARCHAR(3),
    RSVSTRTM                       VARCHAR(5),
    RSVENDTM                       VARCHAR(5),
    RSVPO                          INT,
    SEISANKBN                      VARCHAR(1),
    UPDDTM                         TIMESTAMP,
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(DATKB,LESSONDT,TIMEZONE,TANNO,LINNO,SCLKBN));

-- **********************************************************
-- ***   Create Table CSTPTRN(スクールカード停止トラン)【スクール】
-- **********************************************************

DROP TABLE IF EXISTS CSTPTRN;
CREATE TABLE CSTPTRN (
    SCLMANNO                       VARCHAR(6)   NOT NULL,
    MANNO                          VARCHAR(10),
    STOPFLG                        VARCHAR(1),
    SCLKBN                         VARCHAR(1),
    KSBKBN                         VARCHAR(1),
    STPDT                          VARCHAR(8),
    RIYU                           VARCHAR(100),
    UPDDTM                         TIMESTAMP,
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(SCLMANNO));

-- **********************************************************
-- ***   Create Table DENTRA(伝票トラン)
-- **********************************************************

DROP TABLE IF EXISTS DENTRA;
CREATE TABLE DENTRA (
    DATKB                          VARCHAR(1)  NOT NULL,
    UDNDT                          VARCHAR(8)  NOT NULL,
    UDNNO                          INT         NOT NULL,
    LINNO                          INT         NOT NULL,
    BMNCD                          VARCHAR(3),
    BUNCDA                         VARCHAR(3),
    BUNCDB                         VARCHAR(3),
    BUNCDC                         VARCHAR(3),
    TKTKBN                         VARCHAR(3),
    TKTNMA                         VARCHAR(20),
    UDNKN                          INT,
    TKTSU                          INT,
    SMADT                          VARCHAR(8),
    MANNO                          VARCHAR(8),
    KSBKB                          VARCHAR(2),
    SCLMANNO                       VARCHAR(6),
    SCLKBN                         VARCHAR(1),
    INSDTMSTR                      VARCHAR(20),
    INSDTM                         TIMESTAMP        NOT NULL,
    UDNAKN                         INT,
    UDNBKN                         INT,
    UDNZKN                         INT,
    HINZEIKB                       VARCHAR(1),
    POINT                          INT,
    KOTEIKBN                       VARCHAR(1),
    POZEIKB                        VARCHAR(1),
    UDNKBN                         VARCHAR(1),
    ZENUDNDT                       VARCHAR(8),
    ZENUDNNO                       INT,
    ZENINSDTM                      TIMESTAMP,
    CPAYKBN                        VARCHAR(1),
    HOSTNAME                       VARCHAR(20),
    DRAKBN                         VARCHAR(1),
    HINNMA                         VARCHAR(20),
    PREMKN                         INT,
    PRIMARY KEY(DATKB,UDNDT,UDNNO,LINNO,INSDTM));

-- **********************************************************
-- ***   Create Table DRATRN(ドロア管理トラン)
-- **********************************************************

DROP TABLE IF EXISTS DRATRN;
CREATE TABLE DRATRN (
    HOSTNAME			  VARCHAR(20)	NOT NULL,
    UDNDT			  VARCHAR(8)	NOT NULL,
    UDNNO			  INT		NOT NULL,
    FSTKBN			  VARCHAR(1),
    ASU			          INT,
    BSU			          INT,
    CSU			          INT,
    DSU			          INT,
    ESU			          INT,
    FSU			          INT,
    GSU			          INT,
    HSU			          INT,
    ISU			          INT,
    JSU			          INT,
    TOTALKN                       INT,
    CHKKBN                        VARCHAR(1),
    INSDTM			  TIMESTAMP,
    UPDDTM			  TIMESTAMP,
    PRIMARY KEY(HOSTNAME,UDNDT,UDNNO));

-- **********************************************************
-- ***   Create Table DUDNTRN(売上トラン)
-- **********************************************************

DROP TABLE IF EXISTS DUDNTRN;
CREATE TABLE DUDNTRN (
    DATKB                          VARCHAR(1) NOT NULL,
    UDNDT                          VARCHAR(8) NOT NULL,
    UDNNO                          INT        NOT NULL,
    BMNCD                          VARCHAR(3),
    BUNCDA                         VARCHAR(3),
    BUNCDB                         VARCHAR(3),
    BUNCDC                         VARCHAR(3),
    TKTKBN                         VARCHAR(3),
    UDNKN                          INT,
    TKTSU                          INT,
    SMADT                          VARCHAR(8),
    MANNO                          VARCHAR(8),
    KSBKB                          VARCHAR(2),
    SCLMANNO                       VARCHAR(6),
    SCLKBN                         VARCHAR(1),
    INSDTMSTR                      VARCHAR(20),
    INSDTM                         TIMESTAMP        NOT NULL,
    UDNAKN                         INT,
    UDNBKN                         INT,
    UDNZKN                         INT,
    HINZEIKB                       VARCHAR(1),
    POINT                          INT,
    NYUKN                          INT,
    TURIKN                         INT,
    KOTEIKBN                       VARCHAR(1), 
    POZEIKB                        VARCHAR(1), 
    UDNKBN                         VARCHAR(1),
    ZENUDNDT                       VARCHAR(8),
    ZENUDNNO                       INT,
    ZENINSDTM                      TIMESTAMP,
    CPAYKBN                        VARCHAR(1),
    HOSTNAME                       VARCHAR(20),
    DRAKBN                         VARCHAR(1),
    HINNMA                         VARCHAR(20),
    PREMKN                         INT,
    STFCODE			   VARCHAR(4),
    STFNAME			   VARCHAR(20),
    CARDLIMIT                      VARCHAR(8),
    PRIMARY KEY(DATKB,UDNDT,UDNNO,INSDTM));

-- *******************************************************
-- ***   Create Table ERRLOG(エラーログファイル)
-- *******************************************************

DROP TABLE IF EXISTS ERRLOG;
CREATE TABLE ERRLOG (
	ESQNO                           INT 	     NOT NULL,
	ERRNO                           VARCHAR(8),
	ERRNM                           VARCHAR(50),
	DATDT                           VARCHAR(8),
	DATNO                           INT,
	MANNO                           VARCHAR(8),
	MANCARDID                       INT,
	BNDNO                           VARCHAR(2),
	NODNO                           VARCHAR(8),
	IPADDRESS                       VARCHAR(15),
	EIGKB                           VARCHAR(1),
	RKNKB                           VARCHAR(2),	
	TIMCD                           VARCHAR(2),
	KSBKB                           VARCHAR(2),
	SCLMANNO                        VARCHAR(6),
	SCLCARDNO                       VARCHAR(6),
	PRGID                           VARCHAR(7),
	PRGNM                           VARCHAR(30),
	EXCKB                           VARCHAR(1),
	INSDTM                          TIMESTAMP,
    	PRIMARY KEY(ESQNO));

-- *******************************************************
-- ***   Create Table ERRSEIGOTRN(整合トラン)
-- *******************************************************

DROP TABLE IF EXISTS ERRSEIGOTRN;
CREATE TABLE ERRSEIGOTRN (
	DENDT                           VARCHAR(8)	NOT NULL,
	SEQNO                           INT      	NOT NULL,
	SCLMANNO                        VARCHAR(6),
	MANNO                           VARCHAR(8),
	ERRRSVPO                        INT,
	ERRATDPO                        INT,
	ERRTICHET                       INT,
	SYORIKB                         VARCHAR(1),
	REKBN                           VARCHAR(2),
	MOVEPO                          INT,
	GETTKTSU                        INT,
	INSDTM                          TIMESTAMP,
	NOWZANKIN                       INT,
	NOWPZANKIN                      INT, 
	NOWPOINT                        INT, 
	AFTZANKIN                       INT, 
	AFTPZANKIN                      INT,
	AFTPOINT                        INT, 
    	PRIMARY KEY(DENDT,SEQNO));

-- *******************************************************
-- ***   Create Table ERRTRN(整合トラン)
-- *******************************************************

DROP TABLE IF EXISTS ERRTRN;
CREATE TABLE ERRTRN (
	SEQNO                           INT          NOT NULL,
	SCLMANNO                        VARCHAR(6),
	MANNO                           VARCHAR(6),
	ERRPOINT                        INT,
	ERRTICHETSU                     INT,
	SYORIKB                         VARCHAR(1),
	REKBN                           VARCHAR(2),
	MOVEPO                          INT,
	GETTKTSU                        INT,
	UPDDTM                          TIMESTAMP,
	INSDTM                          TIMESTAMP,
    	PRIMARY KEY(SEQNO));

-- *******************************************************
-- ***   Create Table HINMTA(商品マスタ)
-- *******************************************************

DROP TABLE IF EXISTS HINMTA;
CREATE TABLE HINMTA (
	BMNCD                         VARCHAR(3)	NOT NULL,
	BUNCDA                        VARCHAR(3)	NOT NULL,
	BUNCDB                        VARCHAR(3)	NOT NULL,
	BUNCDC                        VARCHAR(3)	NOT NULL,
	HINCD                         VARCHAR(3)	NOT NULL,
	HINNMA                        VARCHAR(30),
	HINNMB                        VARCHAR(30),
	HINRN                         VARCHAR(10),
	HINKN                         VARCHAR(10),
	HINCLAKB                      VARCHAR(1),
	HINSIRCD                      VARCHAR(6),
	HINCMA                        VARCHAR(50),
	HINCMB                        VARCHAR(50),
	URIATK                        INT,   		/*:税込単価*/
	URIBTK                        INT,   		/*:税抜単価*/
	HINZEIKB                      VARCHAR(1), 	/*1:税抜,2:税込,9:非課税*/
	ZEIRT                         DECIMAL(3,2),
	ZEITK                         INT,   		/*:消費税*/
	HINKB                         VARCHAR(1),
	ZAIKB                         VARCHAR(1),
	UNTCD                         VARCHAR(2),
	IMGNM                         VARCHAR(100),
	UPDDTM                        TIMESTAMP,
	INSDTM                        TIMESTAMP,   
	PRIMARY KEY(BMNCD,BUNCDA,BUNCDB,BUNCDC,HINCD));

INSERT INTO HINMTA VALUES('001','001','001','001','001','基本チケット',null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,NOW(),NOW());
INSERT INTO HINMTA VALUES('001','001','002','001','002','お試しチケット',null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,NOW(),NOW());
INSERT INTO HINMTA VALUES('001','001','003','001','003','割引チケット',null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,NOW(),NOW());

-- *******************************************************
-- ***   Create Table HINMTB(商品分類マスタ)
-- *******************************************************

DROP TABLE IF EXISTS HINMTB;
CREATE TABLE HINMTB (
	BMNCD				VARCHAR(3)	NOT NULL,
	BUNCDA				VARCHAR(3)	NOT NULL,
	BUNCDB				VARCHAR(3)	NOT NULL,
	BUNCDC				VARCHAR(3)	NOT NULL,
	BUNNMA				VARCHAR(20),
	BUNNMB				VARCHAR(20),
	BUNNMC				VARCHAR(20),
	KOTEIKBN			VARCHAR(1),
	UPDDTM				TIMESTAMP,
	INSDTM				TIMESTAMP,
    PRIMARY KEY(BMNCD,BUNCDA,BUNCDB,BUNCDC));

INSERT INTO HINMTB VALUES('001','001','001','001','チケット','基本チケット',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('001','001','002','001','チケット','お試しチケット',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('001','001','003','001','チケット','割引チケット',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('001','002','001','001','チケット','基本チケット(束)',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('001','002','002','001','チケット',null,null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('001','002','003','001','チケット',null,null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','001','001','001','商品販売','商品１',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','001','002','001','商品販売','商品２',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','001','003','001','商品販売','商品３',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','001','004','001','商品販売','商品４',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','001','005','001','商品販売','商品５',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','001','006','001','商品販売','商品６',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','002','001','001','レンタル','商品',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','003','001','001','入金','～9,999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','003','002','001','入金','～99,999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','003','003','001','入金','',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','004','001','001','サービス入金','～999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','004','002','001','サービス入金','～9,999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','004','003','001','サービス入金','～99,999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','005','001','001','入場料','入場料',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','006','001','001','カード支出','～999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','006','002','001','カード支出','～9,999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','006','003','001','カード支出','～99,999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','007','001','001','ポイント還元','～999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','007','002','001','ポイント還元','～9,999円',null,'1',NOW(),NOW());
INSERT INTO HINMTB VALUES('002','007','003','001','ポイント還元','～99,999円',null,'1',NOW(),NOW());

-- *******************************************************
-- ***   Create Table LESSONMST(基本レッスンマスタ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS LESSONMST;
CREATE TABLE LESSONMST (
	LESSONKBN                       VARCHAR(3)	NOT NULL,
	LESSONNM                        VARCHAR(20),
	RSVMAXSU                        INT,
	WAIMAXSU                        INT,
	RSVPO                           INT,
	ATDPO                           INT,
	ATDTKTSU                        INT,
	PNLTKTSU                        INT,
	CONTENTSA                       VARCHAR(20),
	CONTENTSB                       VARCHAR(20),
	IMGNM                           VARCHAR(100),
	PLACE                           VARCHAR(20),
	UPDDTM                          TIMESTAMP,
	INSDTM                          TIMESTAMP,
	TRYMAXSU                        INT,
	TRYTKTSU                        INT,
        LSNTIME                         INT,
    PRIMARY KEY(LESSONKBN));

INSERT INTO LESSONMST VALUES('001', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('002', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('003', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('004', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('005', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('006', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('007', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('008', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('009', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);
INSERT INTO LESSONMST VALUES('010', null,0, 0, 0, 0, 0, 0, null, null, null, null, NOW(), NOW(),null, null,null);

-- *******************************************************
-- ***   Create Table LESSONTABLE(レッスンカレンダーテーブル)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS LESSONTABLE;
CREATE TABLE LESSONTABLE (
	LESSONDT                        VARCHAR(8)	NOT NULL,
	TANNO                           VARCHAR(3)	NOT NULL,
	TIMEZONE                        INT		NOT NULL,
	LESSONKBN                       VARCHAR(3),
	LESSONNM                        VARCHAR(10),
	STRTM                           VARCHAR(5),
	ENDTM                           VARCHAR(5),
	RSVMAXSU                        INT,
	WAIMAXSU                        INT,
	RSVPO                           INT,
	ATDPO                           INT,
	ATDTKTSU                        INT,
	PNLTKTSU                        INT,
	CONTENTSA                       VARCHAR(20),
	CONTENTSB                       VARCHAR(20),
	COMENTA                         VARCHAR(100),
	COMENTB                         VARCHAR(100),
	COMENTC                         VARCHAR(100),
	IMGNM                           VARCHAR(100),
	RSVCANFLG                       VARCHAR(1),
	ATDCANFLG                       VARCHAR(1),
	PLACE                           VARCHAR(20),
	UPDDTM                          TIMESTAMP,
	INSDTM                          TIMESTAMP,
	TRYMAXSU                        INT,
	NOKORISU                        INT,
	ATDTKTTK                        INT,
	TRYTKTSU                        INT,   
	PRIMARY KEY(LESSONDT,TANNO,TIMEZONE));

-- *******************************************************
-- ***   Create Table MANMST(スクール生マスタ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS MANMST;
CREATE TABLE MANMST (
	MANNO                           VARCHAR(8)	NOT NULL,
	MANCARDID                       VARCHAR(10),
	MANNM                           VARCHAR(20),
	MANKN                           VARCHAR(20),
	KSBKB                           VARCHAR(2),
	MANSEX                          VARCHAR(1),
	MANINFO                         VARCHAR(20),
	MANZIP                          VARCHAR(7),
	MANADDA                         VARCHAR(50),
	MANADDB                         VARCHAR(50),
	MANTELNO                        VARCHAR(15),
	MANFAXNO                        VARCHAR(15),
	MANKTELNO                       VARCHAR(15),
	MANKBN                          VARCHAR(1),
	MANBITH                         VARCHAR(8),
	MANLMTDATE                      VARCHAR(8),
	MANINSDATE                      VARCHAR(8),
	MANRESDATE                      VARCHAR(8),
	MANRETDATE                      VARCHAR(8),
	MANENDDATE                      VARCHAR(8),
	MANMAIL                         VARCHAR(100),
	SCLMANNO                        VARCHAR(6)	NOT NULL,
	SCLKBN                          VARCHAR(1),
	SCLINFO                         VARCHAR(40),
	SCLLMTDATE                      VARCHAR(8),
	SCLTRYDATE                      VARCHAR(8),
	SCLINSDATE                      VARCHAR(8),
	SCLRESDATE                      VARCHAR(8),
	SCLRETDATE                      VARCHAR(8),
	SCLENDDATE                      VARCHAR(8),
	SCLCOMMENT                      VARCHAR(40),
	SCLCARDNO                       VARCHAR(6),
	MAILKBN                         VARCHAR(1),
	PAYKBN                          VARCHAR(1),
	LEFTKBN                         VARCHAR(1),
	UPDDTM                          TIMESTAMP,
	INSDTM                          TIMESTAMP,
	COMENT                          VARCHAR(100),   
	PRIMARY KEY(MANNO,SCLMANNO));


-- *******************************************************
-- ***   Create Table POINTMST(ポイントマスタ)
-- *******************************************************

DROP TABLE IF EXISTS POINTMST;
CREATE TABLE POINTMST (
	BMNCD			VARCHAR(3)	NOT NULL,
	POINTNO			VARCHAR(3)	NOT NULL,
	POINTNM			VARCHAR(30),
	POINT			INT,
	POZEIKB			VARCHAR(1),
	UPDDTM			TIMESTAMP,
	INSDTM			TIMESTAMP,
    PRIMARY KEY(BMNCD,POINTNO));

INSERT INTO POINTMST VALUES('001','001','チケット購入 ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('001','002','チケット購入 誕生日ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('001','003','チケット購入 誕生月ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','001','入場 ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','002','入場 誕生日ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','003','入場 誕生月ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','004','入金 ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','005','入金 誕生日ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','006','入金 誕生月ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','007','商品購入 ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','008','商品購入 誕生日ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','009','商品購入 誕生月ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','010','打ち放題ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','011','打ち放題 誕生日ポイント',0,'2',NOW(),NOW());
INSERT INTO POINTMST VALUES('002','012','打ち放題 誕生月ポイント',0,'2',NOW(),NOW());

-- *******************************************************
-- ***   Create Table POINTSMA(スクールポイントサマリ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS POINTSMA;
CREATE TABLE POINTSMA (
	SCLMANNO		VARCHAR(6)	NOT NULL,
	MANNO			VARCHAR(8),
	RSVPO			INT,
	ATDPO			INT,
	UPDDTM			TIMESTAMP,
	INSDTM			TIMESTAMP,
    PRIMARY KEY(SCLMANNO));

-- *******************************************************
-- ***   Create Table REPOMST(スクールポイント還元マスタ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS REPOMST;
CREATE TABLE REPOMST (
    REKBN                          VARCHAR(2) NOT NULL,
    SEQNO                          INT NOT NULL,
    REPOINTNM                      VARCHAR(20),
    SREPOINT                       INT,
    RETKTSU                        INT,
    DREPOINT                       INT,
    REDKIN                         INT,
    ALLREKBN                       VARCHAR(1),
    POKIN                          INT,
    RESCLPO                        INT,
    RESRTPO                        INT,
    ALLMVPKBN                      VARCHAR(1),
    SCLPO                          INT,
    SRTPO                          INT,
    UPDDTM                         TIMESTAMP,
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(REKBN,SEQNO));

-- *******************************************************
-- ***   Create Table RSVTRN(予約トラン)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS RSVTRN;
CREATE TABLE RSVTRN (
	DATKB                           VARCHAR(1)	NOT NULL,
	LESSONDT                        VARCHAR(8)	NOT NULL,
	TIMEZONE                        INT		NOT NULL,
	TANNO                           VARCHAR(3)	NOT NULL,
	LINNO                           VARCHAR(3)	NOT NULL,
	SCLMANNO                        VARCHAR(6),
	SCLKBN                          VARCHAR(1)	NOT NULL,
	MANNO                           VARCHAR(8),
	LESSONKBN                       VARCHAR(3),
	STATEFLG                        VARCHAR(1),
	RSVPO                           INT,
	PNLTKTSU                        INT,
	COMENT                          VARCHAR(100),
	STRTM   	                VARCHAR(5),
	ENDTM                           VARCHAR(5),
	SEISANKBN                       VARCHAR(1),
	UPDDTM                          TIMESTAMP,
	INSDTM                          TIMESTAMP,
	CARDFLG                         VARCHAR(1), 
    	PRIMARY KEY(DATKB,LESSONDT,TIMEZONE,TANNO,LINNO,SCLKBN));

-- *******************************************************
-- ***   Create Table STPTRN(休会トラン)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS STPTRN;
CREATE TABLE STPTRN (
	DENDT                          VARCHAR(8)	NOT NULL,
	DENNO                          INT		NOT NULL,
	SCLMANNO                       VARCHAR(6),
	SCLKBN                         VARCHAR(1),
	STPDT                          VARCHAR(8),
	RETNDT                         VARCHAR(8),
	RIYU                           VARCHAR(20),
	SONOTA                         VARCHAR(100),
	BIKO                           VARCHAR(100),
	MAILKBN                        VARCHAR(1),
	RETNKBN                        VARCHAR(1),
	STSFLG                         VARCHAR(1),
	INSDTM                         TIMESTAMP,
	RETNKDT                        VARCHAR(8),
	RSTSFLG                        VARCHAR(1), 
	PRIMARY KEY(DENDT,DENNO));

-- *******************************************************
-- ***   Create Table SYSTBA(ユーザー情報テーブル(スクール))【スクール】
-- *******************************************************

DROP TABLE IF EXISTS SYSTBA;
CREATE TABLE SYSTBA (
	USRID                           VARCHAR(3)	NOT NULL,
	USRNMA                          VARCHAR(30),
	USRNMB                          VARCHAR(30),
	USRRN                           VARCHAR(20),
	USRNK                           VARCHAR(10),
	USRZP                           VARCHAR(8),
	USRADA                          VARCHAR(50),
	USRADB                          VARCHAR(50),
	USRTL                           VARCHAR(15),
	USRFX                           VARCHAR(15),
	USRBOSNM                        VARCHAR(30),
	GYMSTTDT                        VARCHAR(8),
	DAYUPDDT                        VARCHAR(8),
	MONUPDDT                        VARCHAR(8),
	YERUPDDT                        VARCHAR(8),
	ZEIRT                           DECIMAL(3,2),
	HINRPSKB                        VARCHAR(1),
	HINZRNKB                        VARCHAR(1),
	MINDSPCP                        VARCHAR(8),
	PASSNO                          VARCHAR(4),
	SCLMANNO                        VARCHAR(6),
	LSTHOLDT                        VARCHAR(6),
	MACNM                           VARCHAR(20),
	LMTTIME                         INT,
	USRMAIL                         VARCHAR(30),
	MAKELSNTABLE                    INT,
	TIMEMAXSU                       INT,
	MOVEPORT                        INT,
	MOVETKTRT                       INT,
	AKAKBN                          VARCHAR(1),
	PNLTTIME                        INT,
	TRYMAXSU                        INT,
	INSDTM                          TIMESTAMP,
	UPDDTM                          TIMESTAMP,
	NOKORISU                        INT,
	SELTANKB                        VARCHAR(1),
	SELLSNKB                        VARCHAR(1),
	SCLLMTTIME                      INT,
	TRYPOKB                         VARCHAR(1),
	TRNHOJIMON                      INT,
	STARTTM                         INT,
	RSVMAXSU                        INT,
	SCLCMTKB                        VARCHAR(1),
	RSVPOLMT			INT,
        PRIMARY KEY(USRID));

INSERT INTO SYSTBA VALUES('001',null,null,null,null,null,null,null,null,null,null,'20000101','20000101',null,null,null
,null,null,null,'golf',null,null,null,null,null,29,10,9,null,null,null,4
,NOW(),NOW(),null,null,null,null,null,960,null,null,null,0);

-- *******************************************************
-- ***   Create Table SYSTBL(排他テーブル)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS SYSTBL;
CREATE TABLE SYSTBL (
	UPDFLG				VARCHAR(1),
	SYSFLG				VARCHAR(1),
	UPDDTM				TIMESTAMP);

INSERT INTO SYSTBL VALUES(null, null, NOW());

-- *******************************************************
-- ***   Create Table STAFFMTA(スタッフマスタ)
-- *******************************************************

DROP TABLE IF EXISTS STAFFMTA;
CREATE TABLE STAFFMTA (
	STFCODE				VARCHAR(4)	NOT NULL,
	STFNAME				VARCHAR(20),
	SCRTYCODE			VARCHAR(30),
	GAMENCLA			VARCHAR(50),
	GAMENCLB			VARCHAR(50),
	INSDTM				TIMESTAMP,
	UPDDTM				TIMESTAMP,
        PRIMARY KEY(STFCODE));

INSERT INTO STAFFMTA VALUES('0000','管理者','1111111111','11111111111111111111111111111111111111111111111111','11111111111111111111111111111111111111111111111111',NOW(),NOW());

-- *******************************************************
-- ***   Create Table TANLSNMST(担当者別レッスンマスタ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS TANLSNMST;
CREATE TABLE TANLSNMST (
	TANNO                           VARCHAR(3)  NOT NULL,
	DAYWEEK                         INT         NOT NULL,
	TIMEZONE                        INT         NOT NULL,
	LESSONKBN                       VARCHAR(3),
	LESSONNM                        VARCHAR(10),
	STRTM                           VARCHAR(5),
	ENDTM                           VARCHAR(5),
	RSVMAXSU                        INT,
	WAIMAXSU                        INT,
	RSVPO                           INT,
	ATDPO                           INT,
	ATDTKTSU                        INT,
	PNLTKTSU                        INT,
	CONTENTSA                       VARCHAR(20),
	CONTENTSB                       VARCHAR(20),
	COMENTA                         VARCHAR(100),
	COMENTB                         VARCHAR(100),
	COMENTC                         VARCHAR(100),
	IMGNM                           VARCHAR(100),
	PLACE                           VARCHAR(20),
	LSNSTDT                         VARCHAR(8)  NOT NULL,
	UPDDTM                          TIMESTAMP,
	INSDTM                          TIMESTAMP,
	TRYMAXSU                        INT,
	TRYTKTSU                        INT,      
	PRIMARY KEY(TANNO,DAYWEEK,TIMEZONE,LSNSTDT));

-- *******************************************************
-- ***   Create Table TANMST(担当者マスタ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS TANMST;
CREATE TABLE TANMST (
	TANNO                           VARCHAR(3)	NOT NULL,
	TANNM                           VARCHAR(20),
	TANRN                           VARCHAR(20),
	TANKN                           VARCHAR(20),
	TANZP                           VARCHAR(8),
	TANADA                          VARCHAR(50),
	TANADB                          VARCHAR(50),
	TANTL                           VARCHAR(15),
	TANFX                           VARCHAR(15),
	SEXKB                           VARCHAR(1),
	BIRTHDT                         VARCHAR(8),
	ENTDT                           VARCHAR(8),
	COMENT                          VARCHAR(20),
	COMENTA                         VARCHAR(100),
	COMENTB                         VARCHAR(100),
	COMENTC                         VARCHAR(100),
	IMGNM                           VARCHAR(100),
	UPDDTM                          TIMESTAMP,
	INSDTM                          TIMESTAMP,
	PRINTSEQ                        INT,
	IFORECOLOR                      VARCHAR(50),
	IBACKCOLOR                      VARCHAR(50),    
	PRIMARY KEY(TANNO));

-- *******************************************************
-- ***   Create Table TANWORK(担当者状況テーブル)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS TANWORK;
CREATE TABLE TANWORK (
	SCLMANNO			VARCHAR(6),
	YEAR				INT,
	MONTH				INT,
	TANNO				VARCHAR(3),
	INSDTM				TIMESTAMP,
    PRIMARY KEY(SCLMANNO,YEAR,MONTH));

-- *******************************************************
-- ***   Create Table TDYSTPTRN(自動休会トラン)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS TDYSTPTRN;
CREATE TABLE TDYSTPTRN (
	DENDT                          VARCHAR(8)	NOT NULL,
	DENNO                          INT		NOT NULL,
	SCLMANNO                       VARCHAR(6)	NOT NULL,
	SCLKBN                         VARCHAR(1),
	STPDT                          VARCHAR(8),
	RETNDT                         VARCHAR(8),
	RIYU                           VARCHAR(20),
	SONOTA                         VARCHAR(100),
	BIKO                           VARCHAR(100),
	MAILKBN                        VARCHAR(1),
	RETNKBN                        VARCHAR(1),
	STSFLG                         VARCHAR(1),
	INSDTM                         TIMESTAMP,
	RETNKDT                        VARCHAR(8),
	RSTSFLG                        VARCHAR(1),
	PRIMARY KEY(DENDT,DENNO));

-- *******************************************************
-- ***   Create Table TELOP(テロップ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS TELOP;
CREATE TABLE TELOP (
	STRTELOP					   VARCHAR(100)			,
    COMENT						   VARCHAR(40)			,
    COMPICPATHA                    VARCHAR(100)         ,
    COMPICPATHB                    VARCHAR(100)         ,
    COMPICPATHC                    VARCHAR(100)         ,
    UPDDTM                         TIMESTAMP                 ,
    DCOMENT                        VARCHAR(40));
    INSERT INTO TELOP VALUES('','','','','',NOW(),'');

-- *******************************************************
-- ***   Create Table TICHETSMA(チケットサマリ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS TICHETSMA;
CREATE TABLE TICHETSMA (
	SCLMANNO		VARCHAR(6)	NOT NULL,
	MANNO			VARCHAR(8),
	TICHET			INT,
	UPDDTM			TIMESTAMP,
	INSDTM			TIMESTAMP,
    PRIMARY KEY(SCLMANNO));

-- *******************************************************
-- ***   Create Table TKTBMST(チケット購入マスタ(旧))【スクール】
-- *******************************************************

DROP TABLE IF EXISTS TKTBMST;
CREATE TABLE TKTBMST (
	TKTKBN                         VARCHAR(3)	NOT NULL,
	SEQNO                          INT		NOT NULL,
	REPOINTNM                      VARCHAR(20),
	RETKTSU                        INT,
	RETKTAKN                       INT,
	UPDDTM                         TIMESTAMP,
	INSDTM                         TIMESTAMP,
	RETKTBKN                       INT,
	REZEIKN                        INT,
	POINT                          INT,
	PRETKTSU                       INT,
	KOTEIKBN                       VARCHAR(1),
	PRIMARY KEY(TKTKBN,SEQNO));

-- *******************************************************
-- ***   Create Table TKTTBMST(チケット購入マスタ)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS TKTTBMST;
CREATE TABLE TKTTBMST (
	TKTKBN                         VARCHAR(3)	NOT NULL,
	SEQNO                          INT		NOT NULL,
	REPOINTNM                      VARCHAR(20),
	RETKTSU                        INT,
	RETKTAKN                       INT,
	UPDDTM                         TIMESTAMP,
	INSDTM                         TIMESTAMP,
	RETKTBKN                       INT,
	REZEIKN                        INT,
	POINT                          INT,
	PRETKTSU                       INT,
	KOTEIKBN                       VARCHAR(1),
	HINZEIKB                       VARCHAR(1),
	PRIMARY KEY(TKTKBN,SEQNO));

-- *******************************************************
-- ***   Create Table UDNTRN(売上トラン)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS UDNTRN;
CREATE TABLE UDNTRN (
	UDNDT                          VARCHAR(8)	NOT NULL,
	UDNNO                          INT		NOT NULL,
	TKTKBN                         VARCHAR(3),
	UDNKN                          INT,
	TKTSU                          INT,
	SMADT                          VARCHAR(8),
	SCLMANNO                       VARCHAR(6),
	SCLKBN                         VARCHAR(1),
	INSDTM                         TIMESTAMP		NOT NULL,
	BMNCD                          VARCHAR(3),
	BUNCDA                         VARCHAR(3),
	BUNCDB                         VARCHAR(3),
	BUNCDC                         VARCHAR(3),
	UDNAKN                         INT,
	UDNBKN                         INT,
	UDNZKN                         INT,
	HINZEIKB                       VARCHAR(1),
	POINT                          INT,
	NYUKN                          INT,
	TURIKN                         INT,
	KOTEIKBN                       VARCHAR(1), 
	POZEIKB                        VARCHAR(1), 
	UDNKBN                         VARCHAR(1),
	HOSTNAME                       VARCHAR(20),
	DRAKBN                         VARCHAR(1),
	SVCKBN                         VARCHAR(1),
	INSDTMSTR                      VARCHAR(20),
	CPAYKBN                        VARCHAR(1),
	DATKB                          VARCHAR(1)	NOT NULL,
	HINNMA                         VARCHAR(20),
	STFCODE		               VARCHAR(4),
	STFNAME			       VARCHAR(20),
	PRIMARY KEY(DATKB,UDNDT,UDNNO,INSDTM));

-- *******************************************************
-- ***   Create Table WAITTRN(キャンセル待ちトラン)【スクール】
-- *******************************************************

DROP TABLE IF EXISTS WAITTRN;
CREATE TABLE WAITTRN (
	DATKB                           VARCHAR(1)	NOT NULL,
	LESSONDT                        VARCHAR(8)	NOT NULL,
	TIMEZONE                        INT		NOT NULL,
	TANNO                           VARCHAR(3)	NOT NULL,
	LINNO                           VARCHAR(3)	NOT NULL,
	SCLMANNO                        VARCHAR(6),
	SCLKBN                          VARCHAR(1)	NOT NULL,
	MANNO                           VARCHAR(8),
	LESSONKBN                       VARCHAR(3),
	STATEFLG                        VARCHAR(1),
	COMENT                         VARCHAR(100),
	RSVDT                           VARCHAR(8),
	RSVTIMEZONE                     INT,
	RSVTANNO                        VARCHAR(3),
	RSVLINNO                        VARCHAR(3),
	RSVSTRTM                        VARCHAR(5),
	RSVENDTM                        VARCHAR(5),
	UPDDTM                          TIMESTAMP,
	INSDTM                          TIMESTAMP,
	CARDFLG                         VARCHAR(1),
        PRIMARY KEY(DATKB,LESSONDT,TIMEZONE,TANNO,LINNO,SCLKBN));

-- *******************************************************
-- ***   Create Table PRGMTA(プログラムマスタ)【ベンダー】
-- *******************************************************

DROP TABLE IF EXISTS PRGMTA;
CREATE TABLE PRGMTA (
	PRGID                   VARCHAR(20) NOT NULL,
        SYSKBN                  VARCHAR(1) NOT NULL,
    	PRGNAME                 VARCHAR(20),
    	SCRTYKBN                VARCHAR(1),
    	GAMENKBN                VARCHAR(1),
    	SORTNO                  INT,
    	INSDTM                  TIMESTAMP,
    	UPDDTM                  TIMESTAMP,
    	PRIMARY KEY(PRGID,SYSKBN));

INSERT INTO PRGMTA VALUES('Ent001','1','入場料精算','0','1',1,NOW(),NOW());
INSERT INTO PRGMTA VALUES('CardLimit','1','カード金額クリア処理','0','1',2,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Hin001','1','商品引落し','0','1',3,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Nkn001','1','入金処理','0','1',4,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Nkn002','1','サービス入金処理','0','1',5,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Nkn003','1','カード支出','0','1',6,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Pot020','1','ポイント還元','0','1',7,NOW(),NOW());
INSERT INTO PRGMTA VALUES('BCDH','1','カード発行','0','1',8,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Dra001','1','ドロワ管理','0','1',9,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Src001','1','顧客/ｽｸｰﾙ検索','0','1',10,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst960','1','ユーザ情報登録','0','2',1,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst810','1','顧客登録','0','2',2,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst880','1','営業カレンダー登録','0','2',3,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst860','1','営業情報登録','0','2',4,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst820','1','顧客種別登録','0','2',5,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst920','1','商品単価登録','0','2',6,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst840','1','入金マスタ登録','0','2',7,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst850','1','サービス入金マスタ登録','0','2',8,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst851','1','カード支出マスタ登録','0','2',9,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst890','1','カゴ単価マスタ登録','0','2',10,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst870','1','打ち放題マスタ登録','0','2',11,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst852','1','ポイント還元マスタ登録','0','2',12,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst950','1','ポイント付与率マスタ登録','0','2',13,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst951','1','月間来場ポイントマスタ登録','0','2',14,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn061','1','顧客一覧','0','3',1,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn970','1','売上管理帳票(日報)','0','3',2,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn980','1','売上管理帳票(月報)','0','3',3,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn990','1','月間来場者集計','0','3',4,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Tkt002','2','チケット購入','0','4',1,NOW(),NOW());
INSERT INTO PRGMTA VALUES('CDH','2','カード発行','0','4',2,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Pot010','2','ポイント還元','0','4',3,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Scl010','2','予約・受講画面','0','4',4,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Src001','2','顧客/ｽｸｰﾙ検索','0','4',5,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst960','2','ユーザ情報登録','0','5',1,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst990','2','チケット購入マスタ登録','0','5',2,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst920','2','商品単価登録','0','5',3,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst010','2','インストラクター登録','0','5',4,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst020','2','レッスン種別登録','0','5',5,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst030','2','レッスン登録','0','5',6,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst040','2','レッスン修正(日別)','0','5',7,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst910','2','スクール生登録','0','5',8,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst940','2','ポイント還元マスタ','0','5',9,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Mst950','2','ポイント付与率マスタ登録','0','5',10,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn030','2','予約・受講状況表','0','6',1,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn060','2','スクール会員一覧','0','6',2,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn930','2','お試しフォローアップ情報','0','6',3,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn090','2','休会者リスト','0','6',4,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn100','2','スクール生存調査表','0','6',5,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn020','2','スクールデータ集計表','0','6',6,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn080','2','利用率日計表','0','6',7,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn920','2','担当生徒情報一覧表','0','6',8,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn960','2','休会・復会処理済一覧表','0','6',9,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn110','2','入会総計','0','6',10,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn950','2','自動休・復会予定一覧表','0','6',11,NOW(),NOW());
INSERT INTO PRGMTA VALUES('Prn940','2','本日の自動休会・復会情報','0','6',12,NOW(),NOW());

-- *******************************************************
-- ***   Create Table SYSTBB(ユーザー情報テーブル(打席))【ベンダー】
-- *******************************************************

DROP TABLE IF EXISTS SYSTBB;
CREATE TABLE SYSTBB (
	USRID                           VARCHAR(8)	NOT NULL,
	STRCARDNO                       VARCHAR(10),
	ENDCARDNO                       VARCHAR(10),
	FLOSU                           INT,
	BLKSU                           INT,
	SEATSU                          INT,
	BNDNO                           INT,
	PASSNO                          VARCHAR(4),
	MANNO                           VARCHAR(8),
	GYMSTTDT                        VARCHAR(8),
	DAYUPDDT                        VARCHAR(8),
	MONUPDDT                        VARCHAR(8),
	YERUPDDT                        VARCHAR(8),
	SMAMM                           VARCHAR(2),
	SMADD                           VARCHAR(2),
	SMAMONDD                        VARCHAR(2),
	MINSPCCP                        VARCHAR(8),
	MINDSPCP                        VARCHAR(8),
	MONUPDSC                        VARCHAR(2),
	YERUPDSC                        VARCHAR(2),
	ZEIRT                           DECIMAL(3,2),
	HINRPSKB                        VARCHAR(1),
	HINZRNKB                        VARCHAR(1),
	BALRPSKB                        VARCHAR(1),
	BALZRNKB                        VARCHAR(1),
	KAGRPSKB                        VARCHAR(1),
	KAGZRNKB                        VARCHAR(1),
	LSTHOLDT                        VARCHAR(6),
	MACNM                           VARCHAR(20),
	SDOWNKB                         VARCHAR(1),
	ENTFKB                          VARCHAR(1),
	EIGKB                           VARCHAR(1),
	TIMMAXSU                        INT,
	LMTTIME                         INT,
	USRMAIL                         VARCHAR(100),
	MOVEPORT                        INT,
	MOVETKTRT                       INT,
	RSHEETKBN                       VARCHAR(1),
	NONOKBN                         INT,
	HTTP                            VARCHAR(100),
	INSDTM                          TIMESTAMP,
	UPDDTM                          TIMESTAMP,
	SCLFLG                          INT,
	POWRFLG                         INT,
	CDWRKBN                         INT,
        PRIMARY KEY(USRID));

INSERT INTO SYSTBB VALUES(
'001','0000000000','9999999999',null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null
,null,null,null,null,null,null,null,null,null,null,null,10,null,null,null,null,null,null,null,NOW(),NOW(),1,1,1);

