-- *************************************************
-- ***   Create view STPTRN_VIEW
-- *************************************************
   DROP VIEW IF EXISTS STPTRN_VIEW;
CREATE VIEW STPTRN_VIEW AS 
    SELECT DENDT,
           DENNO,
           SCLMANNO,
           SCLKBN,
           STPDT,
           RETNDT,
           RIYU,
           SUBSTR(RIYU,1,1)  AS RIYU1,
           SUBSTR(RIYU,2,1)  AS RIYU2,
           SUBSTR(RIYU,3,1)  AS RIYU3,
           SUBSTR(RIYU,4,1)  AS RIYU4,
           SUBSTR(RIYU,5,1)  AS RIYU5,
           SUBSTR(RIYU,6,1)  AS RIYU6,
           SUBSTR(RIYU,7,1)  AS RIYU7,
           SUBSTR(RIYU,8,1)  AS RIYU8,
           SUBSTR(RIYU,9,1)  AS RIYU9,
           SUBSTR(RIYU,10,1) AS RIYU10,
           SONOTA,
           BIKO,
           MAILKBN,
           RETNKBN,
           STSFLG,
           INSDTM,
           RETNKDT,
           RSTSFLG
      FROM (
          SELECT ROW_NUMBER() OVER (PARTITION BY SCLMANNO ORDER BY DENDT || TO_CHAR(DENNO, '0000') desc) RNO, 
                 DENDT,
                 DENNO,
                 SCLMANNO,
                 SCLKBN,
                 STPDT,
                 RETNDT,
                 RIYU,
                 SONOTA,
                 BIKO,
                 MAILKBN,
                 RETNKBN,
                 STSFLG,
                 INSDTM,
                 RETNKDT,
                 RSTSFLG                
            FROM STPTRN
           ) AS T
     WHERE RNO = 1;


