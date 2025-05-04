# Projeto de Testes Unitários (Separado - Estrutura Básica)

Este projeto contém a estrutura básica para testes unitários da API.

## Pré-requisitos

* .NET SDK 8.0 ou superior instalado.

## Para executar os testes:

1.  Navegue até a pasta raiz deste projeto (`Projeto-api.Tests`) no terminal.
2.  Execute o comando: `dotnet restore` para restaurar as dependências.
3.  Execute o comando: `dotnet build` para construir o projeto de testes.
4.  Execute o comando: `dotnet test` para executar os testes unitários de exemplo.

## Para referenciar o projeto da API e escrever testes para ela:

1.  Certifique-se de que a pasta `Projeto-api` (contendo o projeto da API) esteja localizada no mesmo diretório pai desta pasta `Projeto-api.Tests`.
2.  Edite o arquivo `Projeto-api.Tests.csproj` e adicione a seguinte linha dentro de um elemento `<ItemGroup>`:

    ```xml
    <ProjectReference Include="..\Projeto-api\Projeto-api.csproj" />
    ```

3.  Reconstrua a solução com `dotnet build`.
4.  Crie pastas (ex: `Controllers`) e arquivos `.cs` dentro deste projeto para seus testes (ex: `ProdutosControllerTests.cs`).
5.  Escreva seus testes unitários, referenciando as classes da sua API (lembre-se dos namespaces corretos).
6.  Use Moq para simular dependências externas.

## Observações

* Este projeto fornece uma estrutura básica funcional para testes unitários. A referência ao projeto da API precisará ser adicionada manualmente no ambiente de destino.