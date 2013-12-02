Le Proto1 est organisé de la manière suivante :

 - Dans le dossier "View", les deux élements graphiques principaux en XAML et C# (rappel pour afficher le C#, dérouler le XAML en cliquant sur la petite flèche à sa gauche dans l'explorateur de solutions):
		-> "SurfaceWindow1" qui définit la vue principale ainsi que l'enchainement des différents écrans
		-> "Tab" qui définit la vue déplaçable d'un seul joueur
		
 - Dans le dossier "ViewModel", l'affichage et l'animation d'une seule note de musique.
 
 - Dans le dossier "Model", la partie qui reprend le plus du code de l'an dernier, là on s'occupe de la logique de la lecture de la musique
		-> "Melody" qui possède la liste des notes (classe "Note" et "NotesView pour la représentation) à jouer et avec quel instrument (class "Instrument")
		-> "AudioController", très dépendant de ce qui a été fait l'an dernier, lit ce qui est dans les banque de sons (utilise XNA Audio)

 - Le dossier "Resources" enfin contient uniquement les différentes textures et les banques de sons lue par XNA


Tout ça est un mix (relativement propre au final) entre le projet de l'an dernier et ma petite sauce à moi... Mais la route est encore longue ;).