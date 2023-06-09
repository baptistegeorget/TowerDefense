using System.IO;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // tableau de string contenant les chemins des niveaux ou chaque dimension correspond a une difficulte
    private static string[,] levels = new string [10, 10];
    // nom des variables de sauvegarde dans le playerPref
    public static string[] dificulteLevel =
        { "levelReachedDebutant", "levelReachedIntermediaire", "levelReachedExpert" };
    // index de la difficulte selectionné dans le menu
    public static int indexDificulty;
    // index du niveau max fini dans la difficulte selectionné
    public static int level;
    // index du niveau en cours
    public static int CurenteLevel;
    // nombre de niveau dans la difficulte selectionné
    public static int numberOfLevel;

    void Start()
    {
        CurenteLevel = 0;
        indexDificulty = 0;
        level = 0;
        numberOfLevel = 0;
        LoadDataLevals();
    }

    public static void LoadDataLevals()
    {
        // chemin du dossier contenant les niveaux
        string pathLevels = Application.dataPath + Path.AltDirectorySeparatorChar + "Scenes/Levels";
        // Je recupere tout les fichiers du dossier
        foreach (string worldDir in Directory.GetFiles(pathLevels, "*.unity"))
        {
            // je découpe le fichier pour recuperer le numero du niveau et la difficulte
            string[] temp = worldDir.Split(" ");
            foreach (string level in temp)
            {
                // je sépare la difficulte du numero du niveau
                string[] levelDecoupe = level.Split(".");
                // je verifie que le fichier est bien un niveau et que la difficulte est bien renseigné sinon c'est qu'il est un niveau avec la dificulte debutant
                if (levelDecoupe.Length > 1 && !Equals(levelDecoupe[1], "unity"))
                {
                    // je recupere le numero du niveau et je le met dans le tableau de niveau avec l'index correspondant a la difficulte
                    levels[int.Parse(levelDecoupe[0]) - 1, int.Parse(levelDecoupe[1])] =
                        "Assets/Scenes/Levels/Level " + levelDecoupe[0] + "." + levelDecoupe[1] + ".unity";
                }
                // si la difficulte n'est pas renseigné je le met dans la difficulte debutant
                else if (levelDecoupe.Length > 1)
                {
                    levels[int.Parse(levelDecoupe[0]) - 1, 0] =
                        "Assets/Scenes/Levels/Level " + levelDecoupe[0] + ".unity";
                }
            }
        }
    }

    public static string NextLevel()
    {
        // je vérifie que le niveau suivant est le dernier niveau déjà fini si c'est le cas je l'enregistre dans le playerPref
        if (CurenteLevel >= level)
        {
            PlayerPrefs.SetInt(dificulteLevel[indexDificulty], level + 1);
        }

        return levels != null ? levels[level, indexDificulty] : null;
    }

    public static void SetDIficulty(int index)
    {
        // je recupere l'index de la difficulte selectionné et le niveau max fini dans cette difficulte avec le playerPref 
        indexDificulty = index;
        level = PlayerPrefs.GetInt(dificulteLevel[indexDificulty], 1);
        NumberOfLevel();
    }

    public static void NumberOfLevel()
    {
        // Permet de savoir conbien de niveau il y a dans la difficulte selectionné pour les diférentes boucle dans ce script et dans levelSelector
        int i = 0;
        while (levels[i, indexDificulty] != null)
        {
            i++;
        }

        numberOfLevel = i;
    }

    public static string[] GetLevels()
    {
        // permet de récuperer tout les niveaux dans la difficulte selectionné
        string[] levelsString = new string[numberOfLevel];
        for (int i = 0; i < numberOfLevel; i++)
        {
            levelsString[i] = levels[i, indexDificulty];
        }

        return levelsString;
    }

    public static string GetLevel()
    {
        // permet de récuperer le niveau en cours (pour le rejouer)
        return levels[CurenteLevel, indexDificulty];
    }
}