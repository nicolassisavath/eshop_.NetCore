# eshop_.NetCore
Projet réalisé en .net Core 2.2 avec Entity Framework DB First

Il est preferable que la bdd fournie (eshop.bak) porte le nom eshop, sinon, modifier le nom réel de la bdd dans appsettings.json:
"ConnectionString": "Server=.;Database=eshop;Trusted_Connection=True;"



L'application gère:
  -l'authentification des users: création/connection (hashage & salage des passwords)
  -la sauvegarde des images en bdd sous forme de byte[]
  -l'implémentation du Repository Pattern avec UnitOfWork
  -la journalisation avec NLog
  -l'injection de dépendances
  -l'authentification du front office pour consommer les API via la génération de jwt
  
La partie front office sert seulement à tester la partie connection/création de compte.
Il est préférable d'utiliser Postman pour tester le bask office.

Afin de consommer les API autres que pour la connection et la création de compte, il faudra renvoyer dans les headers le jwt retourné en response lors la connection:

"Authorization" : "Bearer " + jwt_reçu

Il est sinon possible de mettre en commentaire l'attribut Authorize au dessus des controllers.

Les utilisateurs Nicolas/nicolas & Zidane/zidane sont déjà enregistrés en tant qu'utilisateurs.

