# Instructions pour Créer la Version sur GitHub

## 🎯 Étapes pour Créer la Version v1.0.0

### 1. Aller sur GitHub
1. Ouvrez votre navigateur et allez sur : https://github.com/carlcgb/time-tracker
2. Cliquez sur l'onglet **"Releases"** (à droite de "Code")
3. Cliquez sur **"Create a new release"**

### 2. Configurer la Version
- **Tag version** : `v1.0.0` (sélectionnez dans la liste déroulante)
- **Release title** : `🕐 Chronomètre v1.0.0 - Première Version Stable`
- **Description** : Copiez le contenu du fichier `RELEASE_NOTES_v1.0.0.md`

### 3. Ajouter le Fichier Exécutable
1. Dans la section **"Attach binaries"**
2. Glissez-déposez le fichier : `bin\Release\net8.0-windows\win-x64\publish\Chronometre.exe`
3. Ou cliquez sur **"selecting them"** et naviguez vers le fichier

### 4. Publier la Version
1. Cochez **"Set as the latest release"**
2. Cliquez sur **"Publish release"**

## 📋 Informations de la Version

**Nom de la Version :** 🕐 Chronomètre v1.0.0 - Première Version Stable

**Description :**
```
Chronomètre est un chronomètre léger et sans fioritures pour Windows, construit avec C# .NET 8 et Windows Forms. Il offre un suivi du temps simple et efficace avec des raccourcis clavier globaux et une intégration dans la barre des tâches.

## ✨ Fonctionnalités Principales

### 🎯 Raccourcis Clavier Globaux
- Ctrl+Alt+F1 - Démarrer une nouvelle session
- Ctrl+Alt+F2 - Suspendre/Reprendre la session actuelle
- Ctrl+Alt+F3 - Arrêter la session actuelle
- Ctrl+Alt+F4 - Ajouter une note à la session actuelle
- Ctrl+Alt+F5 - Aperçu de l'indicateur visuel

### 🖥️ Intégration Barre des Tâches
- Icône dans la zone de notification
- Menu contextuel complet
- Fonctionne en arrière-plan
- Instance unique (empêche les doublons)

### 👁️ Indicateur Visuel
- Cercle rouge : Chronomètre en cours
- Cercle orange : Chronomètre en pause
- Masqué : Chronomètre arrêté
- Auto-masquage configurable (5, 10, 30 minutes, jamais)
- Aperçu temporaire avec Ctrl+Alt+F5

### 📝 Gestion des Sessions
- Notes de session au démarrage et pendant l'exécution
- Interface simple et intuitive
- Focus automatique sur les champs de saisie

### 📊 Journalisation Automatique
- Fichier de journal sur le bureau : Chrono-log.txt
- Horodatage complet avec durée et total quotidien
- Emplacements de secours : Documents/AppData

## 🚀 Installation

1. Téléchargez Chronometre.exe depuis cette version
2. Placez-le dans n'importe quel dossier
3. Double-cliquez pour lancer

## 📋 Prérequis Système

- Windows 10/11 (64-bit)
- .NET 8.0 (inclus dans la version autonome)
- 50 MB de RAM disponible
- 70 MB pour l'exécutable

Créé avec ❤️ par CGB
```

**Fichier à Attacher :**
- `bin\Release\net8.0-windows\win-x64\publish\Chronometre.exe` (68.5 MB)

## ✅ Vérifications Finales

Avant de publier, vérifiez que :
- [ ] Le tag v1.0.0 est sélectionné
- [ ] Le titre contient l'emoji et le nom en français
- [ ] La description est complète et en français
- [ ] Le fichier Chronometre.exe est attaché
- [ ] "Set as the latest release" est coché

## 🎉 Après Publication

Une fois la version publiée :
1. Les utilisateurs pourront télécharger directement l'exécutable
2. La version apparaîtra sur la page principale du dépôt
3. Vous pourrez partager le lien de téléchargement

**Lien de la Version :** https://github.com/carlcgb/time-tracker/releases/tag/v1.0.0
