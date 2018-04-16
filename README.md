# Burger Delivery
Aplicativo web criado para um desafio e tamb�m para estudos.

## Instru��es para execu��o

### Pr�-requisitos

* Visual Studio 2017 (vers�o Community [aqui](https://www.visualstudio.com/vs/community/))
* .NET Framework 4.6.2 (instala junto com o Visual Studio)
* SQL Server 2012 ou mais recente
  * Pode ser a edi��o Express
  * Deve ter a op��o de LocalDB instalada tamb�m

### Configurando a solu��o

Para que todas as aplica��es, configurar a solu��o para iniciar ambas as
aplica��es web e api, entrando na tela de configura��es da solu��o e
selecionando a op��o "**Multiple startup projects**":
![Configura��o da solu��o](content/solution.png)

Agora � s� executar os projetos usando a op��o "**Start Debugging**"/"**Start
Without Debugging**", do menu **Debug**:
![Executar projetos](content/start-projects.png)


## Design do c�digo

O desafio solicitava que o *server-side* fosse desenvolvido usando-se **.NET**
e que o client-side fosse desenvolvido usando-se HTML, JavaScript com jQuery e
CSS.

Decidi seguir rigidamente as instru��es e esses s�o os artefatos desenvolvidos:

  * Projeto ASP.NET usando Web API;
  * Web site apenas com conte�do est�tico;
  * Projeto de testes.

### Projeto ASP.NET

Decidi criar apenas um projeto para o back-end por ser uma aplica��o com o
escopo bem simples. Separar a solu��o em v�rios projetos nesse caso apenas
adicionaria uma complexidade desnecess�ria.

De qualquer forma, as classes de diferentes "camadas" est�o separadas em
diferentes diret�rios e *namespaces*. Dessa forma fica mais f�cil uma poss�vel
altera��o da arquitetura da solu��o.

Apesar de n�o ser necess�rio o uso de banco de dados, decidi que seria mais
produtivo usar uma solu��o pronta e simples para persist�ncia de dados. Essa
solu��o foi a combina��o de **Entity Framework** e **SQL Server LocalDB**.

Junto com essa solu��o algumas opera��es s�o simplificadas e deixam o c�digo
mais limpo (salvar os dados e realizar consultas, e rela��o entre entidades, por
exemplo).

#### Separa��o das classes

##### Controllers

Apenas tr�s classes de *Controller* foram criadas no projeto. Duas delas s�o
muito simples, pois precisam apenas retornar uma lista de entidades cada uma.
Para essas duas opera��es n�o foi criada classe de **Service**, pois na
arquitetura atual do projeto essa classe serviria apenas como um passo a mais
e desnecess�rio para realizar uma consulta simples.

Essas duas controllers tamb�m utilizam a rota padr�o do Web API, por serem mais
simples.

A outra classe *Controller* possui rotas personalizadas, definidas por
atributos, por possuir mais de um m�todo para o mesmo verbo HTTP e pela
necessidade de ficar expl�cito o que cada *Action* deveria fazer.

Nessa controller tamb�m foi usada outra classe para executar a��es mais
complexas, de regras de neg�cio da aplica��o. Isso foi feito para que a
controller continuasse tendo apenas a fun��o de "ponte" entre o frontend e as
regras da aplica��o.

##### Data

Foi criada apenas uma classe de *Context* para o Entity Framework, que possui
todas as "tabelas" do sistema. N�o havia necessidade de separa��o de contextos,
pela simplicidade do modelo de dados.

Tamb�m h� uma classe customizada para a inicializa��o do Entity Framework, cuja
�nica fun��o � criar os dados iniciais (*Seed*).

##### Models

As classes de *Model* do projeto possuem os atributos (propriedades) que ser�o
persistidos em banco de dados

Al�m disso, tamb�m possuem algumas propriedades e m�todos de regras de neg�cio
que fazem parte das entidades, como por exemplo:
- O c�lculo de pre�o de um pedido, que � a soma dos pre�os de todos os itens.
- Verificar se a entidade possui itens de um determinado tipo.

##### Services

Foi criada apenas uma classe *Service*, para a parte de pedidos da aplica��o,
pois era a �nica funcionalidade que exigia regras de neg�cio complexas.

� nessa classe que ficam as regras de c�lculo dos pre�os de itens dos pedidos,
inclusive de itens que se encaixam em promo��es.


#### Edi��o de .NET

Foi usado o **.NET Framework** e n�o **.NET Core** por dois motivos:

1. N�o possuo ainda conhecimento suficiente em .NET Core para fazer um projeto
   completo. Ent�o eu precisaria estudar ainda uma para fazer esse projeto,
   reduzindo as chances de entregar algo funcionando bem.

2. Pelo que vi sobre o Entity Framework Core, ele ainda n�o possui todas as
   funcionalidades do EF 6, ent�o correria o risco de n�o fazer algo da forma
   como gostaria.


### Site web

Decidi criar apenas um web site est�tico na solu��o por ser mais simples e
port�vel. Ou seja, da forma como est� ele pode ser hospedado em qualquer
servidor HTTP (que suporte conte�do est�tico), incluside o pr�rio ISS Express.

H� apenas uma p�gina (um arquivo HTML) e a aplica��o funciona como um SPA. H�
somente uma folha de estilos (CSS) e alguns arquivos de JavaScript.

O arquivo "api.js" foi criado somente para executar m�todos da Web API. Ele
possui um objeto simples com algumas fun��es que chamam a API, sem mais
complexidades. N�o foi utilizada estrutura de classe nele pois n�o havia
necessidade desse n�vel de complexidade.

O arquivo "index.js" possui a l�gica que faz a p�gina da aplica��o funcionar.
Foi utilizado apenas um arquivo para uma melhor produtividade, sem usar, por
exemplo, componentes para encapsular funcionalidades.

Foi utilizado o jQuery para executar fun��es de AJAX, para obter elementos da
p�gina, atribuir e executar eventos e tamb�m para adicionar novos elementos �
p�gina (renderizar elementos). Dessa forma, muito c�digo que seria feito usando
apenas a biblioteca padr�o do JavaScript e o DOM foi evitado.
