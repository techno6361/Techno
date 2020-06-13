-- *******************************************************
-- ***   Create Table SEQTRN(連番取得用トラン)【共通】
-- *******************************************************

DROP TABLE IF EXISTS SEQTRN;
CREATE TABLE SEQTRN (
    CARDSEQ                        INT,
    DCARDSEQ                       INT,
    DENNOSEQ	         	   INT,
    MANNOSEQ			   INT,
    FREENOSEQ			   INT,
    SCLMANNOSEQ                    INT,
    TRYMANNOSEQ			   INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP);
INSERT INTO SEQTRN VALUES(0,0,0,0,10000000,0,0,NOW(),NOW());

-- *******************************************************
-- ***   Create Table KBMAST(顧客種別マスタ）
-- *******************************************************

DROP TABLE IF EXISTS KBMAST;
CREATE TABLE KBMAST (
    NKBNO                          INT          NOT NULL,
    CKBNAME			   VARCHAR(20),
    CKBRN                          VARCHAR(10),
    CKBLIMIT                       INT,
    CKBFEE                         INT,
    TAGNO                          INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(NKBNO));

INSERT INTO KBMAST VALUES(1,'会員',NULL,12,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(2,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(3,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(4,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(5,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(6,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(7,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(8,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(9,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(10,NULL,NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(11,'メンテナンス',NULL,0,0,0,NOW(),NOW());
INSERT INTO KBMAST VALUES(12,'打ち放題',NULL,0,0,0,NOW(),NOW());

-- **********************************************************
-- ***   Create Table CSMAST(顧客マスタ)【ベンダー】
-- **********************************************************

DROP TABLE IF EXISTS CSMAST;
CREATE TABLE CSMAST (
    NCSNO                          INT	          NOT NULL,
    NCARDID                        INT,
    CCSNAME                        VARCHAR(20),
    CCSKANA                        VARCHAR(20),
    NCSRANK                        INT,
    NSEX                           INT,
    MANINFO                        VARCHAR(40),
    NZIP                           INT,
    CADDRESS1                      VARCHAR(50),
    CADDRESS2                      VARCHAR(50),
    CTELEPHONE                     VARCHAR(15),
    CFAX                           VARCHAR(15),
    CPOTABLENUM                    VARCHAR(15),
    NMRKBN                         INT,
    DBIRTH                         TIMESTAMP,
    DMEMBER                        TIMESTAMP,
    RESDATE                        VARCHAR(8),
    RETDATE                        VARCHAR(8),
    ENDDATE                        VARCHAR(8),
    ZENENTDATE                     VARCHAR(8),
    CEMAIL                         VARCHAR(100),
    MANCOMMENT                     VARCHAR(100),
    DSCLMANNO                      VARCHAR(6),
    BDSCLMANNO                     VARCHAR(6),
    NFLAGDEL                       INT,
    CEMAILKBN                      VARCHAR(1),
    PASSNO                         VARCHAR(4),
    ENTDT                          VARCHAR(8),
    INSDTM                         TIMESTAMP,
    DUPDATE                       TIMESTAMP,
    DENTRY                         TIMESTAMP,
    BIKO1                          VARCHAR(50),
    BIKO2                          VARCHAR(50),
    BIKO3                          VARCHAR(50),
    ENTCNT                         INT,
    DLEFTKBN                       VARCHAR(1),
    ENTCNT2                        INT,
    BIRTHIMPART                    VARCHAR(6),
    CARDLIMIT                      VARCHAR(8),
    CARDADMINNO                    VARCHAR(7),
    MEMBERNO                       VARCHAR(8),
    KRKNO                          INT,
    HANDICAP                       INT,
    PRIMARY KEY(NCSNO));

-- *******************************************************
-- ***   Create Table SYSMTA(システム情報マスタ）
-- *******************************************************

DROP TABLE IF EXISTS SYSMTA;
CREATE TABLE SYSMTA (
    SHOPNO                         VARCHAR(4)            NOT NULL,
    SHOPNM                         VARCHAR(40),
    ADMNPW                         VARCHAR(10),
    SEPW                           VARCHAR(10),
    TAMATAX                        INT,
    TAX                            INT,
    TAXKBN                         INT,
    TAXHASUKBN                     INT,
    FLRSU                          INT,
    SEATSU                         INT,
    LSTNO1F                        INT,
    LSTNO2F                        INT,
    LSTNO3F                        INT,
    CALLSTDT                       VARCHAR(8),
    PASSKBN                        INT,
    TANCHKFLG                      INT,
    CALLMT                         INT,
    LRMULTI01                      INT,
    LRMULTI02                      INT,
    LRMULTI03                      INT,
    LRMULTI04                      INT,
    LRMULTI05                      INT,
    DISPMULTI                      INT,
    CLRENTMCNT                     INT,
    SITEIKBN                       INT,
    OSDOWNFLG                      INT,
    RECEIPTFLG                     INT,
    CHKPOINT                       INT,
    COMMENT                        VARCHAR(40),
    ZANMAX                         INT,
    CARDLIMIT                      INT,
    HINPREMPAYKBN                  INT,
    SHTPREMPAYKBN                  INT,
    PREMLIMIT                      INT,
    YUSENTANKA                     INT,
    MAXJDCNT                       INT,
    CARDFEE                        INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(SHOPNO));

INSERT INTO SYSMTA VALUES('7706',NULL,'12345','assist',0,0,0,0,0
,0,0,0,0,NULL,0,0,0,7,30,42,65,0,0,0,0,0,0,0,NULL,30000,6,0,0,0,0,0,0,NOW(),NOW());

-- *******************************************************
-- ***   Create Table CALMTA(カレンダーマスタ）
-- *******************************************************

DROP TABLE IF EXISTS CALMTA;
CREATE TABLE CALMTA (
    CALDT                          VARCHAR(8)    NOT NULL,
    RKNKB                          INT,
    YOUBIKB                        INT,
    TENKI                          VARCHAR(20),
    KION                           VARCHAR(5),
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(CALDT));

-- *******************************************************
-- ***   Create Table EIGMTA(営業情報マスタ(1球貸し)）
-- *******************************************************

DROP TABLE IF EXISTS EIGMTA;
CREATE TABLE EIGMTA (
    RKNKB                          INT           NOT NULL,
    NKBNO                          INT           NOT NULL,
    TIMEKB                         INT           NOT NULL,
    TIMENM                         VARCHAR(4),
    PASSCD                         VARCHAR(2),
    ENTKIN                         INT,
    POINT                          INT,
    POINTW                         INT,
    POINTS                         INT,
    BALLKIN1F                      INT,
    BALLKIN2F                      INT,
    BALLKIN3F                      INT,
    SITEIKBN                       INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(RKNKB,NKBNO,TIMEKB));

-- *******************************************************
-- ***   Create Table RKNMTA(料金体系マスタ）
-- *******************************************************

DROP TABLE IF EXISTS RKNMTA;
CREATE TABLE RKNMTA (
    RKNKB                          INT                  NOT NULL,
    RKNNM                          VARCHAR(20),
    CLRR                           INT,
    CLRG                           INT,
    CLRB                           INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(RKNKB));

INSERT INTO RKNMTA VALUES(1,'平日',0,0,255,NOW(),NOW());
INSERT INTO RKNMTA VALUES(2,'休日',255,0,0,NOW(),NOW());
INSERT INTO RKNMTA VALUES(3,'特日1',0,128,0,NOW(),NOW());
INSERT INTO RKNMTA VALUES(4,'特日2',255,128,0,NOW(),NOW());
INSERT INTO RKNMTA VALUES(5,'特日3',0,128,255,NOW(),NOW());
INSERT INTO RKNMTA VALUES(6,'特日4',0,128,280,NOW(),NOW());

-- *******************************************************
-- ***   Create Table EIGMTB(打ち放題マスタ）
-- *******************************************************

DROP TABLE IF EXISTS EIGMTB;
CREATE TABLE EIGMTB (
    RKNKB                          INT           NOT NULL,
    NKBNO                          INT           NOT NULL,
    JKNKB                          INT           NOT NULL,
    JKNMM                          INT,
    JKNNAME                        VARCHAR(30),
    JKNKIN                         INT,
    JKNTAX                         INT,
    POINT                          INT,
    JKNFLR                         INT,
    MAXBALLSU                      INT,
    CHARGEFLG                      INT,
    ENDTIME                        VARCHAR(4),
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(RKNKB,NKBNO,JKNKB));

-- *******************************************************
-- ***   Create Table EIGMTC(営業情報マスタ(カゴ貸し)）
-- *******************************************************

DROP TABLE IF EXISTS EIGMTC;
CREATE TABLE EIGMTC (
    RKNKB                          INT           NOT NULL,
    NKBNO                          INT           NOT NULL,
    FLRKB                          INT           NOT NULL,
    TIMEKB                         INT           NOT NULL,
    TIMENM                         VARCHAR(4),
    PASSCD                         VARCHAR(2),
    ENTKIN                         INT,
    POINT                          INT,
    BALLSU1                        INT,
    KAGO1AKN                       INT,
    KAGO1BKN                       INT,
    KAGO1ZEI                       INT,
    BALLSU2                        INT,
    KAGO2AKN                       INT,
    KAGO2BKN                       INT,
    KAGO2ZEI                       INT,
    BALLSU3                        INT,
    KAGO3AKN                       INT,
    KAGO3BKN                       INT,
    KAGO3ZEI                       INT,
    BALLSU4                        INT,
    KAGO4AKN                       INT,
    KAGO4BKN                       INT,
    KAGO4ZEI                       INT,
    BALLSU5                        INT,
    KAGO5AKN                       INT,
    KAGO5BKN                       INT,
    KAGO5ZEI                       INT,
    BALLSU6                        INT,
    KAGO6AKN                       INT,
    KAGO6BKN                       INT,
    KAGO6ZEI                       INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(RKNKB,NKBNO,FLRKB,TIMEKB));

-- *******************************************************
-- ***   Create Table KINSMA(金額サマリ)
-- *******************************************************

DROP TABLE IF EXISTS KINSMA;
CREATE TABLE KINSMA (
	MANNO				VARCHAR(8)	NOT NULL,
	SCLMANNO			VARCHAR(6),
	ZANKN				INT,
	PREZANKN			INT,
	UPDDTM				TIMESTAMP,
	INSDTM				TIMESTAMP,
    PRIMARY KEY(MANNO));

-- **********************************************************
-- ***   Create Table DPOINTSMA(打席ポイントサマリ)
-- **********************************************************

DROP TABLE IF EXISTS DPOINTSMA;
CREATE TABLE DPOINTSMA (
    MANNO                          VARCHAR(8)	 NOT NULL,
    SCLMANNO                       VARCHAR(6),
    SRTPO                          INT,
    UPDDTM                         TIMESTAMP,
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(MANNO));

-- *******************************************************
-- ***   Create Table SEATWORK(打席情報ワーク）
-- *******************************************************

DROP TABLE IF EXISTS SEATWORK;
CREATE TABLE SEATWORK (
    SEATDT                         VARCHAR(8)     NOT NULL,
    SEATNO                         INT            NOT NULL,
    LEFTKB                         INT            NOT NULL,
    JKNKB                          INT,
    FLRKB                          INT,
    TIMEKB                         INT,
    SEATSTATE                      INT,
    ALLBALLCNT                     INT,
    USEBALLCNT                     INT,
    STARTTIME                      INT,
    USETIME                        INT,
    NKBNO                          INT,
    BALLKIN                        INT,
    UPDFLG                         INT,
    RSVKBN                         INT,
    NCSNO                          VARCHAR(8),
    CCSNAME                        VARCHAR(40),
    SEISANKIN                      INT,
    ZANKN                          INT,
    PREZANKN                       INT,
    SRTPO                          INT,
    DBNCSNO                        VARCHAR(8),
    ENTDT                          VARCHAR(8),
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(SEATDT,SEATNO,LEFTKB));

-- *******************************************************
-- ***   Create Table SEATSMA(打席情報サマリA）
-- *******************************************************

DROP TABLE IF EXISTS SEATSMA;
CREATE TABLE SEATSMA (
    SEATDT                         VARCHAR(8)     NOT NULL,
    FLRKB                          INT,
    TIMEKB                         INT,
    NKB01BALL                      INT,
    NKB01KIN                       INT,
    NKB01TAXKIN                    INT,
    NKB01TAX                       INT,
    NKB01DOSU                      INT,
    NKB02BALL                      INT,
    NKB02KIN                       INT,
    NKB02TAXKIN                    INT,
    NKB02TAX                       INT,
    NKB02DOSU                      INT,
    NKB03BALL                      INT,
    NKB03KIN                       INT,
    NKB03TAXKIN                    INT,
    NKB03TAX                       INT,
    NKB03DOSU                      INT,
    NKB04BALL                      INT,
    NKB04KIN                       INT,
    NKB04TAXKIN                    INT,
    NKB04TAX                       INT,
    NKB04DOSU                      INT,
    NKB05BALL                      INT,
    NKB05KIN                       INT,
    NKB05TAXKIN                    INT,
    NKB05TAX                       INT,
    NKB05DOSU                      INT,
    NKB06BALL                      INT,
    NKB06KIN                       INT,
    NKB06TAXKIN                    INT,
    NKB06TAX                       INT,
    NKB06DOSU                      INT,
    NKB07BALL                      INT,
    NKB07KIN                       INT,
    NKB07TAXKIN                    INT,
    NKB07TAX                       INT,
    NKB07DOSU                      INT,
    NKB08BALL                      INT,
    NKB08KIN                       INT,
    NKB08TAXKIN                    INT,
    NKB08TAX                       INT,
    NKB08DOSU                      INT,
    NKB09BALL                      INT,
    NKB09KIN                       INT,
    NKB09TAXKIN                    INT,
    NKB09TAX                       INT,
    NKB09DOSU                      INT,
    NKB10BALL                      INT,
    NKB10KIN                       INT,
    NKB10TAXKIN                    INT,
    NKB10TAX                       INT,
    NKB10DOSU                      INT,
    NKB11BALL                      INT,
    NKB11KIN                       INT,
    NKB11TAXKIN                    INT,
    NKB11TAX                       INT,
    NKB11DOSU                      INT,
    NKB12BALL                      INT,
    NKB12KIN                       INT,
    NKB12TAXKIN                    INT,
    NKB12TAX                       INT,
    NKB12DOSU                      INT,
    NKB13BALL                      INT,
    NKB13KIN                       INT,
    NKB13TAXKIN                    INT,
    NKB13TAX                       INT,
    NKB13DOSU                      INT,
    NKB14BALL                      INT,
    NKB14KIN                       INT,
    NKB14TAXKIN                    INT,
    NKB14TAX                       INT,
    NKB14DOSU                      INT,
    NKB15BALL                      INT,
    NKB15KIN                       INT,
    NKB15TAXKIN                    INT,
    NKB15TAX                       INT,
    NKB15DOSU                      INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(SEATDT,FLRKB,TIMEKB));

-- *******************************************************
-- ***   Create Table SEATSMB(打席情報サマリB）
-- *******************************************************

DROP TABLE IF EXISTS SEATSMB;
CREATE TABLE SEATSMB (
    SEATDT                         VARCHAR(8)     NOT NULL,
    BALL001                        INT,DOSU001 INT,TIME001 INT,
    BALL002                        INT,DOSU002 INT,TIME002 INT,
    BALL003                        INT,DOSU003 INT,TIME003 INT,
    BALL004                        INT,DOSU004 INT,TIME004 INT,
    BALL005                        INT,DOSU005 INT,TIME005 INT,
    BALL006                        INT,DOSU006 INT,TIME006 INT,
    BALL007                        INT,DOSU007 INT,TIME007 INT,
    BALL008                        INT,DOSU008 INT,TIME008 INT,
    BALL009                        INT,DOSU009 INT,TIME009 INT,
    BALL010                        INT,DOSU010 INT,TIME010 INT,
    BALL011                        INT,DOSU011 INT,TIME011 INT,
    BALL012                        INT,DOSU012 INT,TIME012 INT,
    BALL013                        INT,DOSU013 INT,TIME013 INT,
    BALL014                        INT,DOSU014 INT,TIME014 INT,
    BALL015                        INT,DOSU015 INT,TIME015 INT,
    BALL016                        INT,DOSU016 INT,TIME016 INT,
    BALL017                        INT,DOSU017 INT,TIME017 INT,
    BALL018                        INT,DOSU018 INT,TIME018 INT,
    BALL019                        INT,DOSU019 INT,TIME019 INT,
    BALL020                        INT,DOSU020 INT,TIME020 INT,
    BALL021                        INT,DOSU021 INT,TIME021 INT,
    BALL022                        INT,DOSU022 INT,TIME022 INT,
    BALL023                        INT,DOSU023 INT,TIME023 INT,
    BALL024                        INT,DOSU024 INT,TIME024 INT,
    BALL025                        INT,DOSU025 INT,TIME025 INT,
    BALL026                        INT,DOSU026 INT,TIME026 INT,
    BALL027                        INT,DOSU027 INT,TIME027 INT,
    BALL028                        INT,DOSU028 INT,TIME028 INT,
    BALL029                        INT,DOSU029 INT,TIME029 INT,
    BALL030                        INT,DOSU030 INT,TIME030 INT,
    BALL031                        INT,DOSU031 INT,TIME031 INT,
    BALL032                        INT,DOSU032 INT,TIME032 INT,
    BALL033                        INT,DOSU033 INT,TIME033 INT,
    BALL034                        INT,DOSU034 INT,TIME034 INT,
    BALL035                        INT,DOSU035 INT,TIME035 INT,
    BALL036                        INT,DOSU036 INT,TIME036 INT,
    BALL037                        INT,DOSU037 INT,TIME037 INT,
    BALL038                        INT,DOSU038 INT,TIME038 INT,
    BALL039                        INT,DOSU039 INT,TIME039 INT,
    BALL040                        INT,DOSU040 INT,TIME040 INT,
    BALL041                        INT,DOSU041 INT,TIME041 INT,
    BALL042                        INT,DOSU042 INT,TIME042 INT,
    BALL043                        INT,DOSU043 INT,TIME043 INT,
    BALL044                        INT,DOSU044 INT,TIME044 INT,
    BALL045                        INT,DOSU045 INT,TIME045 INT,
    BALL046                        INT,DOSU046 INT,TIME046 INT,
    BALL047                        INT,DOSU047 INT,TIME047 INT,
    BALL048                        INT,DOSU048 INT,TIME048 INT,
    BALL049                        INT,DOSU049 INT,TIME049 INT,
    BALL050                        INT,DOSU050 INT,TIME050 INT,
    BALL051                        INT,DOSU051 INT,TIME051 INT,
    BALL052                        INT,DOSU052 INT,TIME052 INT,
    BALL053                        INT,DOSU053 INT,TIME053 INT,
    BALL054                        INT,DOSU054 INT,TIME054 INT,
    BALL055                        INT,DOSU055 INT,TIME055 INT,
    BALL056                        INT,DOSU056 INT,TIME056 INT,
    BALL057                        INT,DOSU057 INT,TIME057 INT,
    BALL058                        INT,DOSU058 INT,TIME058 INT,
    BALL059                        INT,DOSU059 INT,TIME059 INT,
    BALL060                        INT,DOSU060 INT,TIME060 INT,
    BALL061                        INT,DOSU061 INT,TIME061 INT,
    BALL062                        INT,DOSU062 INT,TIME062 INT,
    BALL063                        INT,DOSU063 INT,TIME063 INT,
    BALL064                        INT,DOSU064 INT,TIME064 INT,
    BALL065                        INT,DOSU065 INT,TIME065 INT,
    BALL066                        INT,DOSU066 INT,TIME066 INT,
    BALL067                        INT,DOSU067 INT,TIME067 INT,
    BALL068                        INT,DOSU068 INT,TIME068 INT,
    BALL069                        INT,DOSU069 INT,TIME069 INT,
    BALL070                        INT,DOSU070 INT,TIME070 INT,
    BALL071                        INT,DOSU071 INT,TIME071 INT,
    BALL072                        INT,DOSU072 INT,TIME072 INT,
    BALL073                        INT,DOSU073 INT,TIME073 INT,
    BALL074                        INT,DOSU074 INT,TIME074 INT,
    BALL075                        INT,DOSU075 INT,TIME075 INT,
    BALL076                        INT,DOSU076 INT,TIME076 INT,
    BALL077                        INT,DOSU077 INT,TIME077 INT,
    BALL078                        INT,DOSU078 INT,TIME078 INT,
    BALL079                        INT,DOSU079 INT,TIME079 INT,
    BALL080                        INT,DOSU080 INT,TIME080 INT,
    BALL081                        INT,DOSU081 INT,TIME081 INT,
    BALL082                        INT,DOSU082 INT,TIME082 INT,
    BALL083                        INT,DOSU083 INT,TIME083 INT,
    BALL084                        INT,DOSU084 INT,TIME084 INT,
    BALL085                        INT,DOSU085 INT,TIME085 INT,
    BALL086                        INT,DOSU086 INT,TIME086 INT,
    BALL087                        INT,DOSU087 INT,TIME087 INT,
    BALL088                        INT,DOSU088 INT,TIME088 INT,
    BALL089                        INT,DOSU089 INT,TIME089 INT,
    BALL090                        INT,DOSU090 INT,TIME090 INT,
    BALL091                        INT,DOSU091 INT,TIME091 INT,
    BALL092                        INT,DOSU092 INT,TIME092 INT,
    BALL093                        INT,DOSU093 INT,TIME093 INT,
    BALL094                        INT,DOSU094 INT,TIME094 INT,
    BALL095                        INT,DOSU095 INT,TIME095 INT,
    BALL096                        INT,DOSU096 INT,TIME096 INT,
    BALL097                        INT,DOSU097 INT,TIME097 INT,
    BALL098                        INT,DOSU098 INT,TIME098 INT,
    BALL099                        INT,DOSU099 INT,TIME099 INT,
    BALL100                        INT,DOSU100 INT,TIME100 INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(SEATDT));

-- *******************************************************
-- ***   Create Table NKNMST(入金マスタ)
-- *******************************************************

DROP TABLE IF EXISTS NKNMST;
CREATE TABLE NKNMST (
    STSFLG                         VARCHAR(1)     NOT NULL,
    NKNKBN                         VARCHAR(3)     NOT NULL,
    SEQNO                          INT            NOT NULL,
    NKNNM                          VARCHAR(20),
    NKNKN                          INT,
    PREMKN                         INT,
    POINT                          INT,
    NKNTAX                         INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(STSFLG,NKNKBN,SEQNO));

-- *******************************************************
-- ***   Create Table DREPOMST(ポイント還元マスタ(打席))
-- *******************************************************

DROP TABLE IF EXISTS DREPOMST;
CREATE TABLE DREPOMST (
    REKBN                          VARCHAR(2)     NOT NULL,
    RETAGKBN                       VARCHAR(3)     NOT NULL,
    SEQNO                          INT            NOT NULL,
    REPONM                         VARCHAR(20),
    REPOINT                        INT,
    TKTKBN                         INT,
    PREMKN                         INT,
    TKTSU                          INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(REKBN,RETAGKBN,SEQNO));

-- *******************************************************
-- ***   Create Table ETPMTA(月間来場ポイントマスタ)
-- *******************************************************

DROP TABLE IF EXISTS ETPMTA;
CREATE TABLE ETPMTA (
    ETPKBN                         VARCHAR(2)     NOT NULL,
    NKBNO                          INT            NOT NULL,
    ENTCNT                         INT            NOT NULL,
    POINT                          INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(ETPKBN,NKBNO));

-- *******************************************************
-- ***   Create Table NKNTRN(入金トラン)
-- *******************************************************

DROP TABLE IF EXISTS NKNTRN;
CREATE TABLE NKNTRN (
    DATKB                          VARCHAR(1)	NOT NULL,
    DENDT                          VARCHAR(8)	NOT NULL,
    DENNO                          INT		NOT NULL,
    MANNO                          VARCHAR(8),
    NKNNM                          VARCHAR(20),
    NKNKN                          INT,
    NKNAKN                         INT,
    NKNBKN                         INT,
    NZEIKN                         INT,
    POINT                          INT,
    PRERT                          DECIMAL(6,3),
    PREMKN                         INT,
    ZANAKN                         INT,
    ZANBKN                         INT,
    PREZANAKN                      INT,
    PREZANBKN                      INT,
    ZANAPO                         INT,
    ZANBPO                         INT,
    KOTEIKBN                       VARCHAR(1),
    STSFLG                         VARCHAR(1),  /*0:入金、9:サービス入金*/
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(DATKB,DENDT,DENNO));

-- *******************************************************
-- ***   Create Table DREPOTRN(ポイント還元トラン)
-- *******************************************************

DROP TABLE IF EXISTS DREPOTRN;
CREATE TABLE DREPOTRN (
    DATKB                          VARCHAR(1)	NOT NULL,
    DENDT                          VARCHAR(8)	NOT NULL,
    DENNO                          INT		NOT NULL,
    MANNO                          VARCHAR(8),
    REPONM                         VARCHAR(20),
    REPOINT                        INT,
    TKTKBN                         INT,
    REPOSU                         INT,
    ZANAKN                         INT,
    ZANBKN                         INT,
    PREZANAKN                      INT,
    PREZANBKN                      INT,
    ZANAPO                         INT,
    ZANBPO                         INT,
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(DATKB,DENDT,DENNO));

-- **********************************************************
-- ***   Create Table ENTTRA(入場トラン)
-- **********************************************************

DROP TABLE IF EXISTS ENTTRA;
CREATE TABLE ENTTRA (
	DATKB                           VARCHAR(1)	NOT NULL,
	ENTDT                           VARCHAR(8)	NOT NULL,
	ENTNO                           INT		NOT NULL,
	KSBKB                           VARCHAR(2),
	MANNO                           VARCHAR(8),
	EIGKB                           VARCHAR(1),
	RKNKB                           VARCHAR(2),
	TIMCD                           VARCHAR(2),
	TIMTM                           VARCHAR(4),
	PASSNO                          VARCHAR(4),
	ENTKN                           INT,
	ENTAKN                          INT,
	ENTBKN                          INT,
	ENTZEIKB                        VARCHAR(1),
	ENTZEI                          INT,
	ENTCMA                          VARCHAR(20),
	ENTCMB                          VARCHAR(20),
	SRTPO                           INT,
	SMADT                           VARCHAR(8),
	ZENENTDT                        VARCHAR(8),
	ZENKSBKB                        VARCHAR(8),
	ZENKIN                          VARCHAR(8),
	ZENPKIN                         VARCHAR(8),
	ETPPO                           INT,
	BIRTHMPO                        INT,
	BIRTHDPO                        INT,
	OUTPO                           INT,
	OUTFLG                          INT,
	INSDTM                          TIMESTAMP,
	UPDDTM                          TIMESTAMP,
        ZANAKN                          INT,
        ZANBKN                          INT,
        PREZANAKN                       INT,
        PREZANBKN                       INT,
        ZANAPO                          INT,
        ZANBPO                          INT,
	PRIMARY KEY(DATKB,ENTDT,ENTNO,INSDTM));

-- **********************************************************
-- ***   Create Table ENTTRB(入場トラン(無人用))
-- **********************************************************

DROP TABLE IF EXISTS ENTTRB;
CREATE TABLE ENTTRB (
	ENTDT                           VARCHAR(8)	NOT NULL,
	MANNO                           VARCHAR(8)      NOT NULL,
	KSBKB                           VARCHAR(2),
	RKNKB                           VARCHAR(2),
	TIMCD                           VARCHAR(2),
	TIMTM                           VARCHAR(4),
	POINT                           INT,
	POINTFLG                        INT,
	INSDTM                          TIMESTAMP,
	PRIMARY KEY(ENTDT,MANNO,INSDTM));

-- **********************************************************
-- ***   Create Table BALLTRN(ボールトラン)
-- **********************************************************

DROP TABLE IF EXISTS BALLTRN;
CREATE TABLE BALLTRN (
	UDNDT                           VARCHAR(8)	NOT NULL,
	NCSNO                           INT		NOT NULL,
	LENNO                           INT		NOT NULL,
	NKBNO                           INT,
        USEBALL                         INT,
        USEKIN                          INT,
        TAXKIN                          INT,
        TAX                             INT,
        PAYZANKN                        INT,
        PAYPREMKN                       INT,
        ZANAKN                          INT,
        ZANBKN                          INT,
        PREZANAKN                       INT,
        PREZANBKN                       INT,
        ZANAPO                          INT,
        ZANBPO                          INT,
	INSDTM                          TIMESTAMP               ,
	UPDDTM                          TIMESTAMP;

-- *******************************************************
-- ***   Create Table ZIPMTA(住所マスタ)【共通】
-- *******************************************************

DROP TABLE IF EXISTS ZIPMTA;
CREATE TABLE ZIPMTA (
    ZIPCD                          INT,
    ADDRESS1                       VARCHAR(20),
    ADDRESS2	         	   VARCHAR(100),
    ADDRESS3			   VARCHAR(100));


-- **********************************************************
-- ***   Create Table DCSTPTRN(打席カード停止トラン)
-- **********************************************************

DROP TABLE IF EXISTS DCSTPTRN;
CREATE TABLE DCSTPTRN (
        NCARDID                         INT             NOT NULL,
	NCSNO                           INT		NOT NULL,
	INSDTM                          TIMESTAMP               ,
	UPDDTM                          TIMESTAMP               ,
	PRIMARY KEY(NCARDID,NCSNO));

-- **********************************************************
-- ***   Create Table LKINTRA(金額クリアトラン)
-- **********************************************************

DROP TABLE IF EXISTS LKINTRA;
CREATE TABLE LKINTRA (
        LKINNO                          INT             NOT NULL,
	MANNO                           VARCHAR(8),
	MANNM                           VARCHAR(20),
	ZANKN                           INT,
	PREZANKN                        INT,
	SRTPO                           INT,
	CARDLIMIT                       VARCHAR(8),
	CLRKBN                          INT,
        ZANAKN                          INT,
        ZANBKN                          INT,
        PREZANAKN                       INT,
        PREZANBKN                       INT,
        ZANAPO                          INT,
        ZANBPO                          INT,
	STFCODE                         VARCHAR(4),
	STFNAME                         VARCHAR(20),
	INSDTMSTR                       VARCHAR(20),
	INSDTM                          TIMESTAMP,
	PRIMARY KEY(LKINNO));

-- *******************************************************
-- ***   Create Table DTELOP(テロップマスタ)【打席】
-- *******************************************************

DROP TABLE IF EXISTS DTELOP;
CREATE TABLE DTELOP (
    TELOP                          VARCHAR(100),
    COMMENT                        VARCHAR(40),
    FCOMMENT                       VARCHAR(40),
    UPDDTM			   TIMESTAMP);
INSERT INTO DTELOP VALUES(NULL,NULL,NULL,NOW());

-- **********************************************************
-- ***   Create Table SEATSMC(打席情報サマリC)
-- **********************************************************

DROP TABLE IF EXISTS SEATSMC;
CREATE TABLE SEATSMC (
	SEATDT                          VARCHAR(8)	NOT NULL,
	JIKAN                           VARCHAR(2)	NOT NULL,
        WEATHER                         VARCHAR(20),
        TEMPERATURE                     VARCHAR(3),
	SERVICECNT                      INT,
        ENTCNT                          INT,
        BALLSU                          INT,
	INSDTM                          TIMESTAMP               ,
	UPDDTM                          TIMESTAMP               ,
	PRIMARY KEY(SEATDT,JIKAN));

-- **********************************************************
-- ***   Create Table SEATRSV(打席予約)
-- **********************************************************

DROP TABLE IF EXISTS SEATRSV;
CREATE TABLE SEATRSV (
	SEATDT                          VARCHAR(8)	NOT NULL,
	SEATNO                          INT     	NOT NULL,
        CCSNAME                         VARCHAR(40),
        RSVKBN                          INT,
	INSDTM                          TIMESTAMP);

-- **********************************************************
-- ***   Create Table IKOTRN(残高移行トラン)
-- **********************************************************

DROP TABLE IF EXISTS IKOTRN;
CREATE TABLE IKOTRN (
	IKODT                           VARCHAR(8)	NOT NULL,
	IKOTIME                         VARCHAR(4)	NOT NULL,
        NCSNO                           INT             NOT NULL,
	ZANKN                           INT                     ,
        PREZANKN                        INT                     ,
        SRTPO                           INT                     ,
        MOTONCSNO                       INT                     ,
        MOTOCKBNAME                     VARCHAR(20)             ,
        MOTOCCSNAME                     VARCHAR(20)             ,
        MOTOCCSKANA                     VARCHAR(20)             ,
        MOTOZANKN                       INT                     ,
        MOTOPREZANKN                    INT                     ,
        MOTOSRTPO                       INT                     ,
        HAKKOKIN                        INT                     ,
        STFCODE                         VARCHAR(4)              ,
        STFNAME                         VARCHAR(20)             ,
	INSDTM                          TIMESTAMP               ,
	PRIMARY KEY(IKODT,IKOTIME,NCSNO,INSDTM));

-- **********************************************************
-- ***   Create Table REPOCHARGE_M(帳票用入金機履歴)
-- **********************************************************

DROP TABLE IF EXISTS REPOCHARGE_M;
CREATE TABLE REPOCHARGE_M (
	CHARGEDAY                       VARCHAR(8)	NOT NULL,
	HOSTNAME                        VARCHAR(20)	NOT NULL,
        NKNKBN                          INT                     ,
        HAKKOKAISU                      INT                     ,
        HAKKOGOKEIKIN                   INT                     ,
        CHARGE1KIN                      INT                     ,
        CHARGE1KAISU                    INT                     ,
        CHARGE1GOKEIKIN                 INT                     ,
        CHARGE2KIN                      INT                     ,
        CHARGE2KAISU                    INT                     ,
        CHARGE2GOKEIKIN                 INT                     ,
        CHARGE3KIN                      INT                     ,
        CHARGE3KAISU                    INT                     ,
        CHARGE3GOKEIKIN                 INT                     ,
        CHARGE4KIN                      INT                     ,
        CHARGE4KAISU                    INT                     ,
        CHARGE4GOKEIKIN                 INT                     ,
        CHARGE5KIN                      INT                     ,
        CHARGE5KAISU                    INT                     ,
        CHARGE5GOKEIKIN                 INT                     ,
        CHARGE6KIN                      INT                     ,
        CHARGE6KAISU                    INT                     ,
        CHARGE6GOKEIKIN                 INT                     ,
        SHITEIKAISU                     INT                     ,
        SHITEIGOKEIKIN                  INT                     ,
        SENENINKAISU                    INT                     ,
        NISENENINKAISU                  INT                     ,
        GOSENENINKAISU                  INT                     ,
        ICHIMANENINKAISU                INT                     ,
        SENENOUTKAISU                   INT                     ,
        GOSENENOUTKAISU                 INT                     ,
        JYUENINKAISU                    INT                     ,
        GOJYUENINKAISU                  INT                     ,
        HYAKUENINKAISU                  INT                     ,
        GOHYAKUENINKAISU                INT                     ,
        JYUENOUTKAISU                   INT                     ,
        GOJYUENOUTKAISU                 INT                     ,
        HYAKUENOUTKAISU                 INT                     ,
        GOHYAKUENOUTKAISU               INT                     ,
	INSDTM                          TIMESTAMP               ,
	UPDDTM                          TIMESTAMP               ,
	PRIMARY KEY(CHARGEDAY,HOSTNAME,NKNKBN));


-- **********************************************************
-- ***   Create Table CRTTRN(残高修正トラン)
-- **********************************************************

DROP TABLE IF EXISTS CRTTRN;
CREATE TABLE CRTTRN (
	CRTDT                           VARCHAR(8)	NOT NULL,
	CRTTIME                         VARCHAR(4)	NOT NULL,
        NCSNO                           INT             NOT NULL,
	ZANKN                           INT                     ,
        PREZANKN                        INT                     ,
        SRTPO                           INT                     ,
        MAEZANKN                        INT                     ,
        MAEPREZANKN                     INT                     ,
        MAESRTPO                        INT                     ,
        CRTKBN                          INT                     ,
        STFCODE                         VARCHAR(4)              ,
        STFNAME                         VARCHAR(20)             ,
	INSDTM                          TIMESTAMP               ,
	PRIMARY KEY(CRTDT,CRTTIME,NCSNO,INSDTM));


-- **********************************************************
-- ***   Create Table LESSCTRN(無記名ｶｰﾄﾞ作成ﾄﾗﾝ)
-- **********************************************************

DROP TABLE IF EXISTS LESSCTRN;
CREATE TABLE LESSCTRN (
	NCSNO                           INT      	NOT NULL,
        CARDNO                          INT             NOT NULL,
	NKBNO                           INT                     ,
        CKBNAME                         VARCHAR(20)             ,
	ZANKN                           INT                     ,
        PREZANKN                        INT                     ,
        SRTPO                           INT                     ,
        CARDKBN                         INT                     ,
        STFCODE                         VARCHAR(4)              ,
        STFNAME                         VARCHAR(20)             ,
	INSDTM                          TIMESTAMP               ,
	PRIMARY KEY(NCSNO,CARDNO));

-- **********************************************************
-- ***   Create Table VENMTRN(カード販売機売上トラン)
-- **********************************************************

DROP TABLE IF EXISTS VENMTRN;
CREATE TABLE VENMTRN (
	VENMDT                          VARCHAR(8)	NOT NULL,
	URIKBN1                         INT                     ,
	URIKBN2                         INT                     ,
	URIKBN3                         INT                     ,
	URIKBN4                         INT                     ,
	INSDTM                          TIMESTAMP               ,
	UPDDTM                          TIMESTAMP               ,
	PRIMARY KEY(VENMDT));

-- *******************************************************
-- ***   Create Table HINTRN(商品トラン)
-- *******************************************************

DROP TABLE IF EXISTS HINTRN;
CREATE TABLE HINTRN (
    DATKB                          VARCHAR(1)	NOT NULL,
    DENDT                          VARCHAR(8)	NOT NULL,
    DENNO                          INT		NOT NULL,
    MANNO                          VARCHAR(8),
    UDNAKN                         INT,
    UDNBKN                         INT,
    UDNZKN                         INT,
    POINT                          INT,
    ZANAKN                         INT,
    ZANBKN                         INT,
    PREZANAKN                      INT,
    PREZANBKN                      INT,
    ZANAPO                         INT,
    ZANBPO                         INT,
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(DATKB,DENDT,DENNO));

-- *******************************************************
-- ***   Create Table RECARDTRN(カード再発行トラン）
-- *******************************************************

DROP TABLE IF EXISTS RECARDTRN;
CREATE TABLE RECARDTRN (
    RECARDDT                       VARCHAR(8)    	 ,
    CARDNO                         INT           NOT NULL,
    MANNO                          VARCHAR(8)            ,
    OLDCARDNO                      INT                   ,
    STFCODE                        VARCHAR(4)            ,
    STFNAME                        VARCHAR(20)           ,
    ZANAKN                         INT,
    ZANBKN                         INT,
    PREZANAKN                      INT,
    PREZANBKN                      INT,
    ZANAPO                         INT,
    ZANBPO                         INT,
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(CARDNO));

-- *******************************************************
-- ***   Create Table GAMEPOTRN(ミニゲームポイントトラン）
-- *******************************************************

DROP TABLE IF EXISTS GAMEPOTRN;
CREATE TABLE GAMEPOTRN (
    GAMEDT                       VARCHAR(8)    	 NOT NULL,
    HOSTNAME                     VARCHAR(20)     NOT NULL,
    OUTCNT                       INT,
    HIT1CNT                      INT,
    HIT1PO                       INT,
    HIT2CNT                      INT,
    HIT2PO                       INT,
    HIT3CNT                      INT,
    HIT3PO                       INT,
    HIT4CNT                      INT,
    HIT4PO                       INT,
    INSDTM                       TIMESTAMP,
    UPDDTM                       TIMESTAMP,
    PRIMARY KEY(GAMEDT,HOSTNAME));

-- *******************************************************
-- ***   Create Table CLRSMA(カラーサマリ)
-- *******************************************************

DROP TABLE IF EXISTS CLRSMA;
CREATE TABLE CLRSMA (
	MANNO				VARCHAR(8)	NOT NULL,
	SCLMANNO			VARCHAR(6),
	CLRKBN1          		VARCHAR(20),
	CLRKBN2          		VARCHAR(20),
	CLRKBN3          		VARCHAR(20),
	CLRKBN4          		VARCHAR(20),
	CLRKBN5          		VARCHAR(20),
	UPDDTM				TIMESTAMP,
	INSDTM				TIMESTAMP,
    PRIMARY KEY(MANNO));

-- *******************************************************
-- ***   Create Table RCTTRN(領収書トラン)
-- *******************************************************

DROP TABLE IF EXISTS RCTTRN;
CREATE TABLE RCTTRN (
	RCTDT				VARCHAR(8)	NOT NULL,
	RCTNO    			INT             NOT NULL,
	ATENA            		VARCHAR(40),
	KINGAKU          		INT,
	TADASHI          		VARCHAR(40),
	STFCODE          		VARCHAR(4),
	STFNAME          		VARCHAR(20),
	UPDDTM				TIMESTAMP,
	INSDTM				TIMESTAMP,
    PRIMARY KEY(RCTDT,RCTNO));

-- *******************************************************
-- ***   Create Table BNDMTA(ベンダーマスタ)
-- *******************************************************

DROP TABLE IF EXISTS BNDMTA;
CREATE TABLE BNDMTA (
	FLOKB				VARCHAR(1)     	NOT NULL,
	BNDNO    			VARCHAR(2)      NOT NULL,
	FLONM            		VARCHAR(10),
	NODNO           		VARCHAR(8),
	IPADDRESS          		VARCHAR(15),
	SHOPNO          		VARCHAR(4),
	EIGKB            		VARCHAR(1),
        ENTBKB                          VARCHAR(1),
        GETDSU                          INT,
        TNKKB                           VARCHAR(1),
        CALLFLG                         VARCHAR(1),
	INSDTM				TIMESTAMP,
	UPDDTM				TIMESTAMP,
    PRIMARY KEY(FLOKB,BNDNO));
INSERT INTO BNDMTA VALUES('1','01','１号','1','','7000','2','1',10,'1','',NOW(),NOW());
INSERT INTO BNDMTA VALUES('1','02','２号','1','','7000','2','1',10,'1','',NOW(),NOW());
INSERT INTO BNDMTA VALUES('1','03','３号','1','','7000','2','1',10,'1','',NOW(),NOW());
INSERT INTO BNDMTA VALUES('2','04','４号','1','','7000','2','1',10,'1','',NOW(),NOW());
INSERT INTO BNDMTA VALUES('2','05','５号','1','','7000','2','1',10,'1','',NOW(),NOW());
INSERT INTO BNDMTA VALUES('2','06','６号','1','','7000','2','1',10,'1','',NOW(),NOW());

-- *******************************************************
-- ***   Create Table SRTTRA(打席トラン(ベンダー))
-- *******************************************************

DROP TABLE IF EXISTS SRTTRA;
CREATE TABLE SRTTRA (
	UDNDT                           VARCHAR(8)	NOT NULL,
	DATNO                           INT		NOT NULL,
	BNDNO                           VARCHAR(2)	NOT NULL,
	NODNO                           VARCHAR(8),
	IPADDRESS                       VARCHAR(15),
	KSBKB                           VARCHAR(2),
	MANNO                           VARCHAR(8),
	EIGKB                           VARCHAR(1),
	RKNKB                           VARCHAR(2),
	TIMCD                           VARCHAR(2),
	BALAKN                          INT,
	BALBKN                          INT,
	BALZEIKB                        VARCHAR(1),
	BALZEI                          INT,
	BALCMA                          VARCHAR(40),
	BALCMB                          VARCHAR(40),
	KAGOSU                          INT,
	BALLSU                          INT,
	KAGOAKN                         INT,
	KAGOBKN                         INT,
	KAGOZEIKB                       VARCHAR(1),
	KAGOZEI                         INT,
	KAGOCMA                         VARCHAR(40),
	KAGOCMB                         VARCHAR(40),
	SRTPO                           INT,
	SMADT                           VARCHAR(8),
	INSDTM				TIMESTAMP,
    PRIMARY KEY(UDNDT,DATNO,BNDNO));

-- *******************************************************
-- ***   Create Table TIMTRA(時間貸しトラン)
-- *******************************************************

DROP TABLE IF EXISTS TIMTRA;
CREATE TABLE TIMTRA (
	UDNDT				VARCHAR(8)     	NOT NULL,
	MANNO    			VARCHAR(8)      NOT NULL,
        UDNNO                           INT             NOT NULL,
        FLRKB                           INT,
	ENDTIME            		VARCHAR(4),
	MAXBALLSU                       INT,
        USEBALLSU                       INT,
	INSDTM				TIMESTAMP,
	UPDDTM				TIMESTAMP,
    PRIMARY KEY(UDNDT,MANNO,UDNNO));

-- *******************************************************
-- ***   Create Table SRTTRA(打席トラン)【ベンダー】
-- *******************************************************

DROP TABLE IF EXISTS SRTTRA;
CREATE TABLE SRTTRA (
	UDNDT                           VARCHAR(8)	NOT NULL,
	DATNO                           INT		NOT NULL,
	BNDNO                           VARCHAR(2)	NOT NULL,
	NODNO                           VARCHAR(8),
	IPADDRESS                       VARCHAR(15),
	KSBKB                           VARCHAR(2),
	MANNO                           VARCHAR(8),
	EIGKB                           VARCHAR(1),
	RKNKB                           VARCHAR(2),
	TIMCD                           VARCHAR(2),
	BALAKN                          INT,
	BALBKN                          INT,
	BALZEIKB                        VARCHAR(1),
	BALZEI                          INT,
	BALCMA                          VARCHAR(40),
	BALCMB                          VARCHAR(40),
	KAGOSU                          INT,
	BALLSU                          INT,
	KAGOAKN                         INT,
	KAGOBKN                         INT,
	KAGOZEIKB                       VARCHAR(1),
	KAGOZEI                         INT,
	KAGOCMA                         VARCHAR(40),
	KAGOCMB                         VARCHAR(40),
	SRTPO                           INT,
	SMADT                           VARCHAR(8),
	INSDTM                          TIMESTAMP,
	PRIMARY KEY(UDNDT,DATNO,BNDNO));


-- *******************************************************
-- ***   Create Table KOSUMTA(ｺｰｽ料金マスタ)
-- *******************************************************

DROP TABLE IF EXISTS KOSUMTA;
CREATE TABLE KOSUMTA (
	RKNKB				INT      	NOT NULL,
	NKBNO    			INT             NOT NULL,
        PRCKB                           INT             NOT NULL,
        PRCNM                           VARCHAR(20),
        H09KIN                          INT,
	H18KIN            		INT,
	H00KIN                          INT,
	HOKENKIN                        INT,
	CARTKIN                         INT,
        COMPEKIN                        INT,
        POINT                           INT,
        POINT2                          INT,
	USEKB                           INT,
	INSDTM				TIMESTAMP,
	UPDDTM				TIMESTAMP,
    PRIMARY KEY(RKNKB,NKBNO,PRCKB));

-- *******************************************************
-- ***   Create Table KOSUTRN(コース料金トラン)
-- *******************************************************

DROP TABLE IF EXISTS KOSUTRN;
CREATE TABLE KOSUTRN (
    DATKB                          VARCHAR(1)	NOT NULL,
    DENDT                          VARCHAR(8)	NOT NULL,
    DENNO                          INT		NOT NULL,
    MANNO                          VARCHAR(8),
    RKNKB                          INT,
    NKBNO                          INT,
    PRCKB                          INT,
    HOLESU                         INT,
    HOKENKIN                       INT,
    CARTKIN                        INT,
    COMPEKIN                       INT,
    UDNAKN                         INT,
    UDNBKN                         INT,
    UDNZKN                         INT,
    POINT                          INT,
    POINT2                         INT,
    PREMKN                         INT,
    ZANAKN                         INT,
    ZANBKN                         INT,
    PREZANAKN                      INT,
    PREZANBKN                      INT,
    ZANAPO                         INT,
    ZANBPO                         INT,
    INSDTM                         TIMESTAMP,
    PRIMARY KEY(DATKB,DENDT,DENNO));

-- *******************************************************
-- ***   Create Table PRTMST(アクティ帳票設定マスタ)
-- *******************************************************

DROP TABLE IF EXISTS PRTMST;
CREATE TABLE PRTMST (
    HINBUN1                        INT,
    HINCD1                         INT,
    HINBUN2                        INT,
    HINCD2                         INT,
    HINBUN3                        INT,
    HINCD3                         INT,
    HINBUN4                        INT,
    HINCD4                         INT,
    HINBUN5                        INT,
    HINCD5                         INT,
    HINBUN6                        INT,
    HINCD6                         INT,
    HINBUN7                        INT,
    HINCD7                         INT,
    HINBUN8                        INT,
    HINCD8                         INT,
    HINBUN9                        INT,
    HINCD9                         INT,
    HINBUN10                       INT,
    HINCD10                        INT,
    HINBUN11                       INT,
    HINCD11                        INT,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP);
INSERT INTO PRTMST VALUES(0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,NOW(),NOW());

-- *******************************************************
-- ***   Create Table NEWCARDTRN(新規カード発行トラン）
-- *******************************************************

DROP TABLE IF EXISTS NEWCARDTRN;
CREATE TABLE NEWCARDTRN (
    CARDDT                         VARCHAR(8)    	 ,
    CARDNO                         INT           NOT NULL,
    MANNO                          VARCHAR(8)            ,
    STFCODE                        VARCHAR(4)            ,
    STFNAME                        VARCHAR(20)           ,
    INSDTM                         TIMESTAMP,
    UPDDTM                         TIMESTAMP,
    PRIMARY KEY(CARDNO));
