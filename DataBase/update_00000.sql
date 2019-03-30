ALTER TABLE `restaurante`.`funcionario`   
  ADD COLUMN `nivel` VARCHAR(30) NULL AFTER `cpf`,
  ADD COLUMN `senha` CHAR(32) NULL AFTER `nivel`;