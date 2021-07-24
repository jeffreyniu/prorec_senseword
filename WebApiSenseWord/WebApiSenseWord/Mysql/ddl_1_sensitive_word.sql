#Create database
CREATE DATABASE IF NOT EXISTS prorec_SenseWord CHARACTER SET UTF8;

USE prorec_SenseWord;
SET GLOBAL log_bin_trust_function_creators = TRUE;

#Create table Sense_Word
CREATE TABLE IF NOT EXISTS Sense_Word (
    ID BIGINT(8) UNSIGNED PRIMARY KEY  AUTO_INCREMENT,
    Word NVARCHAR(50),
    Enabled BOOLEAN,
    CreatedDate TIMESTAMP,
    LastUpdatedDate TIMESTAMP
);

#Create function genSenseWordTableName
DELIMITER $$

DROP FUNCTION IF EXISTS genSenseWordTableName;
CREATE FUNCTION genSenseWordTableName(userId BIGINT (8), tableName NVARCHAR(50)) RETURNS NVARCHAR(70)
BEGIN
    RETURN CONCAT_WS('_','SW','UID',userId,tableName);
END$$
DELIMITER ;

DELIMITER $$

#Create procedure sp_get_sense_word
DROP PROCEDURE IF EXISTS sp_get_sense_word;
CREATE PROCEDURE sp_get_sense_word(
IN iUserId BIGINT (8),
IN iTableName NVARCHAR(50),
IN iID BIGINT(8)
)
BEGIN

SET @tableName := genSenseWordTableName(iUserId,iTableName);

SET @sql := CONCAT('SELECT * FROM ', @tableName,' WHERE ID =', 'iID');
PREPARE stmt from @sql;  
EXECUTE stmt;

END$$
DELIMITER ;

DELIMITER $$

#Create procedure sp_add_sense_word
DROP PROCEDURE IF EXISTS sp_add_sense_word;
CREATE PROCEDURE sp_add_sense_word(
IN iUserId BIGINT (8),
IN iTableName NVARCHAR(50),
IN iWord NVARCHAR(50),
OUT oID BIGINT(8)
)
BEGIN

SET @tableName := genSenseWordTableName(iUserId,iTableName);

SET @sql := CONCAT('INSERT INTO ', @tableName,'(Word,Enabled,CreatedDate,LastUpdatedDate) values('
,iWord,',TRUE,current_timestamp(),current_timestamp())');

PREPARE stmt from @sql;  
EXECUTE stmt;

set oID = last_insert_id();

END$$
DELIMITER ;

#Update procedure sp_update_sense_word
DROP PROCEDURE IF EXISTS sp_update_sense_word;

DELIMITER $$
CREATE PROCEDURE sp_update_sense_word(
IN iUserId BIGINT (8),
IN iTableName NVARCHAR(50),
IN iWord NVARCHAR(20),
IN iID BIGINT(8)
)
BEGIN

SET @tableName := genSenseWordTableName(iUserId,iTableName);

SET @sql = CONCAT('UPDATE ', @tableName,' SET Word=', iWord,',LastUpdatedDate=current_timestamp() WHERE ID=',iID);

PREPARE stmt from @sql;  
EXECUTE stmt;

END$$
DELIMITER ;

#Delete procedure sp_delete_sense_word
DROP PROCEDURE IF EXISTS sp_delete_sense_word;
DELIMITER $$
CREATE PROCEDURE sp_delete_sense_word(
IN iUserId BIGINT (8),
IN iTableName NVARCHAR(50),
IN iID BIGINT(8)
)
BEGIN

SET @tableName := genSenseWordTableName(iUserId,iTableName);

SET @sql := CONCAT('UPDATE ', @tableName, 'SET Enabled=FALSE,LastUpdatedDate=current_timestamp() WHERE ID=', pID);

PREPARE stmt from @sql;  
EXECUTE stmt;

END$$
DELIMITER ;
