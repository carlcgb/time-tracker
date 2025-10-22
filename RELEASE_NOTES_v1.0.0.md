# 🕐 Chronomètre v1.0.0 - Première Version Stable

## 🎉 Nouveautés

**Chronomètre** est un chronomètre léger et sans fioritures pour Windows, construit avec C# .NET 8 et Windows Forms. Il offre un suivi du temps simple et efficace avec des raccourcis clavier globaux et une intégration dans la barre des tâches.

## ✨ Fonctionnalités Principales

### 🎯 **Raccourcis Clavier Globaux**
- `Ctrl+Alt+F1` - Démarrer une nouvelle session
- `Ctrl+Alt+F2` - Suspendre/Reprendre la session actuelle
- `Ctrl+Alt+F3` - Arrêter la session actuelle
- `Ctrl+Alt+F4` - Ajouter une note à la session actuelle
- `Ctrl+Alt+F5` - Aperçu de l'indicateur visuel

### 🖥️ **Intégration Barre des Tâches**
- Icône dans la zone de notification
- Menu contextuel complet
- Fonctionne en arrière-plan
- Instance unique (empêche les doublons)

### 👁️ **Indicateur Visuel**
- Cercle rouge : Chronomètre en cours
- Cercle orange : Chronomètre en pause
- Masqué : Chronomètre arrêté
- **Auto-masquage** configurable (5, 10, 30 minutes, jamais)
- **Aperçu** temporaire avec `Ctrl+Alt+F5`

### 📝 **Gestion des Sessions**
- Notes de session au démarrage et pendant l'exécution
- Interface simple et intuitive
- Focus automatique sur les champs de saisie

### 📊 **Journalisation Automatique**
- Fichier de journal sur le bureau : `Chrono-log.txt`
- Horodatage complet avec durée et total quotidien
- Format : `YYYY-MM-DD HH:mm:ss zzz Duration=HH:mm:ss DayTotal=HH:mm:ss State=Stopped Notes="..." AppVersion=X.Y.Z`
- Emplacements de secours : Documents/AppData

### ⚙️ **Configuration**
- Paramètres sauvegardés dans `settings.json`
- Configuration de l'indicateur visuel
- Activation/désactivation des raccourcis
- Gestion des paramètres d'auto-masquage

## 🚀 Installation

### Option 1 : Téléchargement Direct (Recommandé)
1. Téléchargez `Chronometre.exe` depuis cette version
2. Placez-le dans n'importe quel dossier
3. Double-cliquez pour lancer

### Option 2 : Compilation depuis les Sources
1. Installez le [SDK .NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Clonez le dépôt :
   ```bash
   git clone https://github.com/carlcgb/time-tracker.git
   cd time-tracker
   ```
3. Compilez l'application :
   ```bash
   # Windows PowerShell
   .\build.ps1
   
   # Windows Command Prompt
   build.bat
   ```

## 📋 Prérequis Système

- **Système d'exploitation** : Windows 10/11 (64-bit)
- **Runtime** : .NET 8.0 (inclus dans la version autonome)
- **Mémoire** : 50 MB de RAM disponible
- **Espace disque** : 70 MB pour l'exécutable

## 🎮 Utilisation

### Premier Lancement
1. **Lancez l'application** - Double-cliquez sur `Chronometre.exe`
2. **Vérifiez la barre des tâches** - Cherchez l'icône de chronomètre
3. **Testez les raccourcis** - Appuyez sur `Ctrl+Alt+F1` pour démarrer
4. **Vérifiez le fichier de journal** - Un fichier `Chrono-log.txt` sera créé sur votre bureau

### Pendant une Session
- **Suspendre/Reprendre** : `Ctrl+Alt+F2` ou menu de la barre des tâches
- **Ajouter des Notes** : `Ctrl+Alt+F4` ou menu de la barre des tâches
- **Arrêter la Session** : `Ctrl+Alt+F3` ou menu de la barre des tâches

### Indicateur Visuel
- **Cercle Rouge** : Chronomètre en cours
- **Cercle Orange** : Chronomètre en pause
- **Masqué** : Chronomètre arrêté
- **Aperçu** : `Ctrl+Alt+F5` pour affichage temporaire

## 🔧 Dépannage

### Les Raccourcis Ne Fonctionnent Pas
- Assurez-vous qu'aucune autre application n'utilise les mêmes combinaisons
- Essayez de lancer en tant qu'administrateur
- Vérifiez les paramètres de PowerToys pour les conflits

### L'Icône de la Barre des Tâches N'est Pas Visible
- Vérifiez si l'icône est masquée dans les paramètres de la zone de notification
- Clic droit sur la barre des tâches et sélectionnez "Afficher les icônes masquées"
- Cherchez une icône de chronomètre dans la barre des tâches

### Problèmes de Fichier de Journal
- Le fichier de journal est créé sur votre bureau sous `Chrono-log.txt`
- Si le bureau n'est pas accessible, il utilise le dossier Documents
- Vérifiez les permissions de fichier si l'enregistrement échoue

### L'Indicateur Visuel Ne S'Affiche Pas
- L'indicateur apparaît dans le coin supérieur droit par défaut
- Vérifiez si "Afficher le temps écoulé dans l'indicateur" est activé dans le menu
- Essayez l'option "Aperçu maintenant" pour tester la visibilité

## 📁 Structure du Projet

```
Chronometre/
├── Forms/                    # Formulaires UI
├── Services/                 # Services principaux
├── Program.cs               # Point d'entrée de l'application
├── Chronometre.csproj       # Fichier de projet
├── timer-icon.ico          # Icône de l'application
├── build.ps1               # Script de compilation PowerShell
├── build.bat               # Script de compilation Batch
└── README.md               # Documentation principale
```

## 🏗️ Architecture Technique

- **Framework** : .NET 8.0 avec Windows Forms
- **Raccourcis Globaux** : API Windows RegisterHotKey
- **Barre des Tâches** : NotifyIcon avec menu contextuel
- **Indicateur Visuel** : Form transparent avec styles Win32
- **Journalisation** : Écriture de fichiers avec calcul de totaux quotidiens
- **Configuration** : Sérialisation JSON

## 🔒 Sécurité et Confidentialité

Cette application :
- Ne nécessite pas d'accès internet
- Ne collecte ni ne transmet de données personnelles
- Stocke toutes les données localement sur votre ordinateur
- Utilise uniquement les API Windows standard pour les raccourcis et l'intégration de la barre des tâches

## 🤝 Contribution

Les contributions sont les bienvenues ! N'hésitez pas à :
1. Signaler des bugs via les [Issues](../../issues)
2. Proposer des améliorations
3. Soumettre des pull requests

## 📄 Licence

Ce projet est sous licence MIT - voir le fichier [LICENSE](LICENSE) pour plus de détails.

## 🙏 Remerciements

- Construit avec .NET 8 et Windows Forms
- Icônes de System.Drawing.SystemIcons
- Implémentation des raccourcis globaux avec l'API Windows

---

**Créé avec ❤️ par CGB**

*Version 1.0.0 - 22 octobre 2025*
