﻿CREATE TABLE ArquivoMorto (
[Id] INTEGER PRIMARY KEY NOT NULL,
[Nome] VARCHAR(50) NOT NULL,
[Cpf] VARCHAR(11) UNIQUE NOT NULL,
[Email] VARCHAR(50) UNIQUE NOT NULL,
[DataNasc] DATE NOT NULL,
[Genero] CHAR(1) CHECK ([GENERO] IN ('M', 'F')) NOT NULL,
[Celular] VARCHAR(11) NOT NULL
)