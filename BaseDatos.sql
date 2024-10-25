-- Script para crear la base de datos
CREATE DATABASE IF NOT EXISTS devsupruebatecnica;

USE devsupruebatecnica;

-- Persona
CREATE TABLE Persona (
    PersonaId INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Genero VARCHAR(10) NOT NULL,
    Edad INT NOT NULL, -- Edad
    Identificacion VARCHAR(20) NOT NULL UNIQUE,
    Direccion VARCHAR(255),
    Telefono VARCHAR(15)
);

-- Cliente
CREATE TABLE Cliente (
    ClienteId INT AUTO_INCREMENT PRIMARY KEY,
    PersonaId INT,
    Contraseña VARCHAR(100) NOT NULL,
    Estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (PersonaId) REFERENCES Persona(PersonaId)
);

-- Cuenta
CREATE TABLE Cuenta (
    NumeroCuenta INT AUTO_INCREMENT PRIMARY KEY,
    ClienteId INT,
    TipoCuenta VARCHAR(50) NOT NULL,
    SaldoInicial DECIMAL(10, 2) NOT NULL,
    Estado BOOLEAN NOT NULL DEFAULT TRUE,
    FOREIGN KEY (ClienteId) REFERENCES Cliente(ClienteId)
);

-- Movimiento
CREATE TABLE Movimiento (
    MovimientoId INT AUTO_INCREMENT PRIMARY KEY,
    NumeroCuenta INT,
    Fecha DATE NOT NULL,
    TipoMovimiento VARCHAR(50) NOT NULL,
    Valor DECIMAL(10, 2) NOT NULL,
    Saldo DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (NumeroCuenta) REFERENCES Cuenta(NumeroCuenta)
);

SHOW TABLES;
