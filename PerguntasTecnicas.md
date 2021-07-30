Frameworks utilizados



1. Quanto tempo você usou para completar a solução apresentada? O que você faria se tivesse mais tempo?

Em torno de 7 horas. Em caso de continuidade, faria tratamentos de erro mais complexos, como
por exemplo na query string, impedindo caracteres especiais, e também verificando por comandos SQL,
por segurança, evitando ataques de SQL injection.

Faria, por fim, tratamentos de estética no json retornado e detalharia mais mensagens de erro.


2. Se usou algum framework, qual foi o motivo de ter usado este? Caso contrário, por que não utilizou nenhum?

- NewtonSoft.Json
- EntityFrameworkCore
- EntityFrameworkCore.MySql
- EntityFrameworkCore.Tools
- Xunit
- Dynamitey

Os frameworks foram utilizados por questões de performance, agilidade no desenvolvimento, maior facilidade 
na manutenção do código. Esses frameworks provém métodos robustos para Serialização de json (NewtonSoft.Json) obtido de api's externas; maior facilidade
de integração com banco de dados SQL (EntityFrameworkCore); disponibilização de métodos convenientes para 
tratamento de objetos dinamicos e testes unitários.