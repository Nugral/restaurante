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

CREATE TABLE `restaurante`.`api_usuario`(
  `id` INT NOT NULL AUTO_INCREMENT,
  `usuario` VARCHAR(50) NOT NULL DEFAULT NULL,
  `senha` CHAR(32) NOT NULL DEFAULT NULL,
  `ativo` TINYINT(1) DEFAULT 1,
  `dataCadastro` DATETIME,
  `dataModificacao` DATETIME,
  PRIMARY KEY (`id`)
);