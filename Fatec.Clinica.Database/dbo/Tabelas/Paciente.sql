﻿CREATE TABLE Paciente (
[Id] INTEGER IDENTITY PRIMARY KEY NOT NULL,
[Nome] VARCHAR(50) NOT NULL,
[Cpf] VARCHAR(11) NOT NULL,
[Email] VARCHAR(50) NOT NULL,
[Senha] VARCHAR(20) NOT NULL,
[DataNasc] DATE NOT NULL,
[Genero] CHAR(1) NOT NULL,
[Celular] VARCHAR(11) NOT NULL,
[TelefoneRes] VARCHAR(10)
)