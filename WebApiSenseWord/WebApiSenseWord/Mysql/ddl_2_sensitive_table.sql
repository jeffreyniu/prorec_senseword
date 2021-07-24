USE prorec_SenseWord;

#Create table Sense_Table
CREATE TABLE IF NOT EXISTS Sense_Table (
    ID BIGINT(8) UNSIGNED PRIMARY KEY  AUTO_INCREMENT,
    UserId BIGINT(8) UNSIGNED,
    TableName NVARCHAR(50) NOT NULL,
    Enabled BOOLEAN,
    CreatedDate TIMESTAMP,
    LastUpdatedDate TIMESTAMP
);

#Create procedure sp_add_sense_table
DROP PROCEDURE IF EXISTS sp_add_sense_table;
DELIMITER $$
CREATE PROCEDURE sp_add_sense_table(
IN iUserId BIGINT (8),
IN iTableName NVARCHAR(50),
OUT oID BIGINT(8)
)
BEGIN
START TRANSACTION;

SET @newFullTableName := genSenseWordTableName(iUserId,iTableName);
SET @sql := CONCAT('CREATE TABLE IF NOT EXISTS ', @newFullTableName,' LIKE Sense_Word');

PREPARE stmt from @sql;  
EXECUTE stmt;

INSERT INTO Sense_Table(UserId,TableName,Enabled,CreatedDate,LastUpdatedDate) values(
iUserId,iTableName,TRUE,current_timestamp(),current_timestamp());

set oID = last_insert_id();
COMMIT;
END$$
DELIMITER ;

-- Update procedure sp_update_sense_table
DROP PROCEDURE IF EXISTS sp_update_sense_table;
DELIMITER $$
CREATE PROCEDURE sp_update_sense_table(
IN iTableName NVARCHAR(20),
IN iID BIGINT(8)
)
BEGIN
DECLARE v_oldUserId BIGINT(8);
DECLARE v_oldTableName NVARCHAR(20);

START TRANSACTION;

SELECT UserId,TableName INTO v_oldUserId, v_oldTableName FROM Sense_Table WHERE ID=iID;

SET @oldFullTableName := genSenseWordTableName(v_oldUserId,v_oldTableName);
SET @newFullTableName := genSenseWordTableName(v_oldUserId,iTableName);

SET @sql := CONCAT('ALTER TABLE ',@oldFullTableName,' RENAME TO ', @newFullTableName);

PREPARE stmt from @sql;  
EXECUTE stmt;

UPDATE Sense_Table SET TableName=iTableName,LastUpdatedDate=current_timestamp() WHERE ID=iID;

COMMIT;
END$$
DELIMITER ;

#Delete procedure sp_delete_sense_table
DROP PROCEDURE IF EXISTS sp_delete_sense_table;
DELIMITER $$
CREATE PROCEDURE sp_delete_sense_table(
IN iID BIGINT(8)
)
BEGIN

UPDATE Sense_Table SET Enabled=FALSE,LastUpdatedDate=current_timestamp() WHERE ID=pID;

END$$

DELIMITER ;