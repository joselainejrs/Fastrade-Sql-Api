/*DML*/

USE fastrade

INSERT INTO Tipo_Usuario (Tipo) VALUES ('Consumidor')

INSERT INTO Tipo_Usuario (Tipo) VALUES ('Fornecedor')

INSERT INTO Tipo_Usuario (Tipo) VALUES ('Adm')

INSERT INTO Endereco (Numero, Estado, Bairro, Rua, CEP) VALUES (67, 'SP', 'Jardim Madalena', 'Alfredo dos Andes', '12345678')

INSERT INTO Cat_Produto(Tipo) VALUES ('Conserva')

INSERT INTO Cat_Produto(Tipo) VALUES ('Bebidas')

INSERT INTO Produto(Id_Cat_Produto, Nome, Validade) VALUES (1, 'Feijão', '22/11/2022')

INSERT INTO Receita(Nome) VALUES ('Limão')

INSERT INTO Receita(Nome) VALUES ('Casca De Laranja')

INSERT INTO Produto_Receita(Id_Produto, Id_Receita) VALUES (1, 2)

INSERT INTO Usuario(Id_Endereco, Id_Tipo_Usuario, Nome_Razao_Social, Email, Senha, Celular, CPF_CNPJ,Foto_Url_Usuario) VALUES (1, 1, 'Consumidor Do Chiquinho', 'Consumidor@Live.com', '******', '(11)9777-6666', '12345678912345', 'foto')

INSERT INTO Usuario(Id_Endereco, Id_Tipo_Usuario, Nome_Razao_Social, Email, Senha, Celular, CPF_CNPJ,Foto_Url_Usuario) VALUES (1, 2, 'Fornecedor Do Chiquinho', 'Fornecedor@Live.com', '******', '(11)9777-6666', '12345678912345', 'foto')

INSERT INTO Usuario(Id_Endereco, Id_Tipo_Usuario, Nome_Razao_Social, Email, Senha, Celular, CPF_CNPJ,Foto_Url_Usuario) VALUES (1, 3, 'Adm Do Chiquinho', 'Adm@Live.com', '******', '(11)9777-6666', '12345678912345', 'foto')

INSERT INTO Pedido(Id_Produto, Id_Usuario, Quantidade) VALUES (1, 1, 20)

INSERT INTO Oferta(Id_Produto, Id_Usuario, Quantidade, Preco, Foto_Url_Oferta) VALUES (1, 1, 50, '4,99', 'Url_Imagens_Texto')
