# FreeGameCore
game core affachi de tout moteur de jeu spécifique pour **pong**


## GameRule
2 joueurs \
1 balle
### Joueur Input
\<Espace> ->  changement de direction \
contrôle une barre sur laquelle la balle rebondi
### Game
quand la balle sort du terrain par le côté d'un joueur, ce joueur perd \
les bord autres (pas côté d'un joueur -> haut/bas) font rebondit la balle

### Init
La balle est initisé au centre dnas une direction aléatoire avec une vitesse fixe.