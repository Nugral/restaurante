CREATE DATABASE `restaurante`;

CREATE TABLE `restaurante`.`funcionario`(
  `id` INT NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100),
  `dataNascimento` DATE,
  `cpf` CHAR(11),
  `dataCadastro` DATETIME,
  `dataModificacao` DATETIME,
  PRIMARY KEY (`id`)
);
