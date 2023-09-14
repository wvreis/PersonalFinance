# Controle de Finanças Pessoais

## Visão Geral

**Nota: Este projeto está em construção e ainda não está pronto para uso.**

O objetivo deste projeto é fornecer aos usuários uma plataforma intuitiva para acompanhar suas receitas, despesas e investimentos. Com o Controle de Finanças Pessoais, você poderá:

- Registrar e categorizar suas transações financeiras diárias.
- Visualizar um resumo detalhado de suas despesas e receitas em gráficos e tabelas.
- Acompanhar o saldo atual de suas contas bancárias e carteiras de investimento.

### Funcionalidades Principais

1. **Painel de Controle**

O painel de controle oferece uma visão geral rápida e abrangente das suas finanças...

2. **Registros de Transações**

Registre suas transações de forma simples e organizada...

# Executando o Projeto com Docker

Este guia fornece instruções passo a passo sobre como executar este projeto usando o Docker.

## Pré-requisitos

Antes de começar, certifique-se de ter o Docker instalado em sua máquina. Se você ainda não tem o Docker instalado, siga as instruções no [site oficial do Docker](https://www.docker.com/get-started) para instalar a versão apropriada para o seu sistema operacional.

## Passos

1. **Clonar o Repositório**

   Clone este repositório para a sua máquina local:

   ```bash
   git clone https://github.com/wvreis/PersonalFinance
   cd PersonalFinance

2. **Localize o Arquivo `docker.env`**

   No diretório raiz do seu projeto, você encontrará o arquivo `docker.env`.

3. **Edite as Variáveis de Ambiente**

   Abra o arquivo `docker.env` em um editor de texto.

4. **Defina as Variáveis de Ambiente**

   Defina as variáveis de ambiente no arquivo `docker.env` de acordo com suas configurações. Aqui estão as variáveis disponíveis:

   ```bash
   DATABASEHOST=host.docker.internal
   DATABASEPORT=5433
   DATABASENAME=NomeDoBancoDeDados
   DATABASEUSER=NomeDoUsuárioDoBanco
   POSTGRES_PASSWORD=SenhaDoPostgres

## Executando o Projeto

Agora que você editou o arquivo `docker.env`, você está pronto para executar o projeto:

1. **Execute o Projeto com Docker**

   Execute o seguinte comando para iniciar os containers Docker:

   ```bash
   docker-compose up -d