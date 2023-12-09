using System.Diagnostics;
using System.IO;
using System.Text;
using System.Media;

 // Spécifiez le chemin du fichier audio
        string cheminFichierAudio = "chemin/vers/votre/musique.wav";

        // Créez une instance de SoundPlayer
        SoundPlayer lecteurAudio = new SoundPlayer(cheminFichierAudio);

        // Jouez la musique
        lecteurAudio.Play();

        // Attendez la fin de la musique
        Console.WriteLine("Appuyez sur une touche pour arrêter la musique.");
        Console.ReadKey();

        // Arrêtez la lecture
        lecteurAudio.Stop();

/*
int n=0;
int partieEnCours=1;


int CountZeros(int[] tableau) // Fonction qui compte le nombre de zeros dans un tableau 1D
{
    int count = 0;
    for(int i=0;i<tableau.Length;i++)
    {
        if (tableau[i] == 0)
        {
            count++;
        }
    }
    return count;
}


int Lignes(int [][] tableauJeu) //fonction trouvant une ligne au hasard contenant au moins 1 case vide 
{
    Random indice= new Random();
    int trouve=0; //condition d'arret pour la boucle while: tant qu'une ligne n'est pas trouvée, le prg continue de chercher
    int k=0; 
    while (trouve==0 & k<100*n) // explication du k:  si le random ne trouve pas la case vide en n tentatives, le jeu s'arrete donc on augemnte le nombre de chiffre aleatoire generés pour etre sûr. 
    {    
        int a=indice.Next(0,n);
        if(CountZeros(tableauJeu[a])!=0)
        {
            trouve=1;
            return a;
        }
        else
        {
            k++;
        }
    
    }
    return -1;     
}   


int [][] ApresTour(int[][] tableauJeu) // fonction rajoutant 1 bonbon dans une case aléatoire vide
{    
    int indice=Lignes(tableauJeu); // on a deja choisi sur quelle ligne serait le bonbon, choisissons  à présent la colonne
    Random aleatoire= new Random();
    int nb= aleatoire.Next(0,n);
    if (indice==-1)
    {
        partieEnCours=0;
    }
    else
    {
        do
        {
            nb= aleatoire.Next(0,n);
        }
        while(tableauJeu[indice][nb]!=0); // tant que la case aléatoire choisie n'est pas vide (=0)  alors en chercher une autre
        tableauJeu[indice][nb]=1; //lorsqu'on a trouvé notre case, la remplir avec un bonbon(=1)
    }
    return tableauJeu;
}


void AfficherItem(int numero)
{
    if (numero==0)
        Console.Write("   "); // La case s'affiche vide si elle vaut 0
    if (numero==1)
    {
        Console.ForegroundColor = ConsoleColor.Red;//
        Console.Write(" ¤ ");// la case est remplie avec un bonbon si elle vaut 1
        Console.ForegroundColor = ConsoleColor.Gray; //    
    }
    if (numero==2)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(" @ "); // la case est remplie avec un rouleau réglisse si elle vaut 2
        Console.ForegroundColor = ConsoleColor.Gray;  
    }
    if (numero==3)
    {
        Console.ForegroundColor = ConsoleColor.DarkRed; 
        Console.Write(" o "); // la case est remplie avec un cookie si elle vaut 3
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    if (numero==4)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" J "); //  la case est remplie avec un sucre d'orge si elle vaut 4
        Console.ForegroundColor = ConsoleColor.Gray;
    }
   
}


int Score(int [][] tableauJeu) //fonction calculant le score
{    
    int score=0;
    for (int i=0; i<n; i++) // On parcours les cases une à une en ajoutant a chaque fois au score la valeur des bonbons correspondante
    {
        for (int j=0; j<n; j++)
        {
            if (tableauJeu[i][j]==1)
                score+=1;
            if (tableauJeu[i][j]==2)
                score+=3;
            if (tableauJeu[i][j]==3)
                score+=7;
            if (tableauJeu[i][j]==4)
                score+=15;
            if (tableauJeu[i][j]==5)
                Console.WriteLine(score);
        }
    }
    return score;
}


void AfficherTableau(int[][] tableauJeu) // Fonction s'occupant de la mise en forme dans la console
{
    for (int i=0;i<n;i++)
    {
        Console.Write("+");
        Console.WriteLine(string.Concat(Enumerable.Repeat("---+",n)));
        Console.Write(string.Concat(Enumerable.Repeat("",n)));
        Console.Write($"|");
        for(int j=0; j<n;j++)
        {
            AfficherItem(tableauJeu[i][j]);
            Console.Write($"|");
        }
        Console.Write('\n');
    }
    Console.Write("+");
    Console.WriteLine(string.Concat(Enumerable.Repeat("---+",n)));
    Console.Write('\n');
}   
 

int [][] Mouvement (int [][] tableauJeu, char direction) // fonction réalisant les déplacements et fusions des bonbons
// rappel: un seul mouvement possible par tour
{   
    if (direction == 'z') // en voulant tout swiper vers le haut
    {    
        for (int i=0; i<n ; i++) //on parcourt les colonnes
        {    
            int fusion=0; // variable vérifiant qu'une seule fusion est effectuée par colonne par tour
            for (int j=1; j<n;j++) // boucle permettant de parcourir toutes les cases de la colonne (on parcourt les lignes)
            {
                int k=0; //variable permettant l'incrémentation de la case observée
                while ( j-1-k>=0 && tableauJeu[j-1-k][i]==0) // tant que la case observée n'est pas celle tout en haut du tableau et qu'elle est vide
                {    
                    tableauJeu[j-1-k][i]=tableauJeu[j-k][i]; // cette case prend la valeur de celle d'en dessous 
                    tableauJeu[j-k][i]=0;// la case d'en dessous de celle observée initialement devient vide
                    k++;// on observe ensuite la case d'en dessous
                }
                if (j-k!=0)// on verifie que la case observée n'est pas sur la ligne tout en haut sinon out of range
                { 
                    if (tableauJeu[j-1-k][i]==tableauJeu[j-k][i] && fusion == 0 && tableauJeu[j-1-k][i]!= 4) // si la case observée et celle d'en dessous ont la meme valeur et qu'aucune fusion n'a été effectuée sur la colonne
                    {
                        tableauJeu[j-1-k][i]++;// les deux cases fusionnent et la valeur de la plus haute des deux s'incrémente de 1
                        tableauJeu[j-k][i]=0;// la case la plus basse des deux se vide
                        fusion=1;// la fusion a été effectuée
                    } 
                

                }

            }// on passe maintenent à la case suivante de la colonne 
        }// on passe à la colonne suivante
    }
    if (direction == 'q') // en voulant tout swiper vers la gauche
    {
        for (int i=0; i<n ; i++) // on parcourt les lignes
        {
            int fusion=0; //variable vérifiant qu'une seule fusion est effectuée par colonne par tour
            for (int j=1;j<n;j++)// on parcourt les cases sur une meme lignes (on parcourt les colonnes)
            {    
                int k=0;
                while( j-1-k>=0 &&tableauJeu[i][j-1-k]==0 )
                {
                    tableauJeu[i][j-1-k]=tableauJeu[i][j-k];
                    tableauJeu[i][j-k]=0;
                    k++;
                }
                if (j-k!=0)
                {
                    if (tableauJeu[i][j-1-k]==tableauJeu[i][j-k] && fusion==0 && tableauJeu[i][j-1-k] != 4)
                    {
                        tableauJeu[i][j-1-k]++;
                        fusion=1;
                        tableauJeu[i][j-k]=0;
                    }
                
                }
            }
        }
    }
    
    if (direction == 'd')// en voulant tout swiper vers la droite
    {
        for (int i=0; i<n ; i++)
        {
            int fusion=0;
            for (int j=n-2;j>=0;j--)// on parcourt à l'envers 
                {
                int k=0;
                while( j+1+k<=n-1 &&tableauJeu[i][j+1+k]==0 )
                {
                    tableauJeu[i][j+1+k]=tableauJeu[i][j+k];
                    tableauJeu[i][j+k]=0;
                    k++;
                }
                if (j+k!=n-1)
                {
                    if (tableauJeu[i][j+1+k]==tableauJeu[i][j+k] && fusion==0 && tableauJeu[i][j+1+k] !=4 )
                    {
                        tableauJeu[i][j+1+k]++;
                        fusion=1;
                        tableauJeu[i][j+k]=0;
                    }
                
                    
                    
                }

            }
        }
    }
    if (direction == 's') // En voulant tout swiper vers le bas
    {
        for (int i=0; i<n ; i++)
        {
            int fusion=0;
            for (int j=n-1;j>=0;j--)
            {
                int k=0;
                while( j+1+k<=n-1 &&tableauJeu[j+1+k][i]==0 )
                {
                    tableauJeu [j+1+k][i] =tableauJeu[j+k][i];
                    tableauJeu[j+k][i]=0;
                    k++;
                }
                if (j+k!=n-1)
                {
                    if (tableauJeu[j+1+k][i]==tableauJeu[j+k][i] && fusion==0 && tableauJeu[j+1+k][i] !=4 )
                    {
                        tableauJeu[j+1+k][i]++;
                        fusion=1;
                        tableauJeu[j+k][i]=0;
                    }
                    
                }
            }
        }
    }

    return tableauJeu;
}


int [][] InitTableauZeros(int [][]tableauFinal)
{
    
    for(int i=0;i<n;i++)
    {
        tableauFinal[i] = new int[n];
        for(int j=0;j<n;j++){
            tableauFinal[i][j]=0;
        }
    }
    return tableauFinal;
}


void Main()
{
    Console.WriteLine("Press A to play. Press R to see the commands");
    char A = Convert.ToChar(Console.ReadLine());
    if (A=='a')
    {
        Console.WriteLine("Choisir le mode de jeu:");
        Console.WriteLine("1:Facile (50 points en 35 déplacements)");
        Console.WriteLine("2:Moyen (50 points en 30 déplacements)");
        Console.WriteLine("3:Difficile (50 points en 20 déplacements)");
        Console.WriteLine("4: Record ");// libre
        Console.WriteLine("Appuyer sur le chiffre souhaité:");
        int B= Convert.ToInt32(Console.ReadLine());
        if(B==4)
        {
            StreamReader sr = new StreamReader("Score.txt");
            //Read the first line of text
            int meilleurScore= Convert.ToInt32(sr.ReadLine());
            sr.Close();
            Console.WriteLine($"Record à battre: {meilleurScore} points");
            Console.WriteLine("Choisir la taille du tableau: saisissez la longeur du coté exemple: 3 pour 3x3");
            n= Convert.ToInt32(Console.ReadLine()!);
            int [][] tableauFinal= new int [n][];
            InitTableauZeros(tableauFinal);
            ApresTour(tableauFinal);
            ApresTour(tableauFinal);
            AfficherTableau(tableauFinal);
            Score(tableauFinal);
            int scoreFinal=0;
            while(partieEnCours==1)
            {
                Console.WriteLine ($"Score={Score(tableauFinal)}");
                Console.WriteLine("Swiper dans une direction");
                char direction=Convert.ToChar(Console.ReadLine()!);
                Mouvement(tableauFinal,direction);
                Lignes(tableauFinal);
                ApresTour(tableauFinal);
                AfficherTableau(tableauFinal);
                Score(tableauFinal);
                scoreFinal=Score(tableauFinal);
                if (Lignes(tableauFinal)==-1)
                    partieEnCours=0;
            }
        
            Console.WriteLine ($"Votre score est de {scoreFinal} points.");
            
            if (scoreFinal>meilleurScore)
            {
                meilleurScore=scoreFinal;
                StreamWriter sw = new StreamWriter(new FileStream("Score.txt",FileMode.Create));
                //Write a line of text
                sw.WriteLine(Score(tableauFinal));
                Console.WriteLine("Bravo, vous avez battu un nouveau record!");
                sw.Close();
            }
            
            else 
            {
                Console.WriteLine($"Le meilleur score est de {meilleurScore} points.");
            
            }
        }
        else 
        {
            int tour=0;
            int compteurTour=0;
            if(B==1)
                tour=35;
            if (B==2)
                tour=30;
            if (B==3)
                tour=20;
            n=3;
            int [][] tableauFinal= new int [n][];
            InitTableauZeros(tableauFinal);
            ApresTour(tableauFinal);
            ApresTour(tableauFinal);
            AfficherTableau(tableauFinal);
            Score(tableauFinal);
            int scoreFinal=0;
            while(partieEnCours==1 && compteurTour<= tour && scoreFinal< 50)
            {

                Console.WriteLine ($"Score={Score(tableauFinal)}");
                Console.WriteLine($"Nombre de déplacements restants: {tour-compteurTour} ");
                Console.WriteLine("Swiper dans une direction");
                char direction=Convert.ToChar(Console.ReadLine()!);
                Mouvement(tableauFinal,direction);
                Lignes(tableauFinal);
                ApresTour(tableauFinal);
                AfficherTableau(tableauFinal);
                Score(tableauFinal);
                scoreFinal=Score(tableauFinal);
                compteurTour++;
                if (Lignes(tableauFinal)==-1)
                    partieEnCours=0;
                
            }
            if ( scoreFinal<50)
            {
                Console.WriteLine($"DOMMAGE.   Tu as atteint {scoreFinal} points en {compteurTour} déplacements...");
                Console.WriteLine("Réessaie :)");

            }
            else 
            {
                Console.WriteLine($"Félicitations ! Tu as atteint {scoreFinal} points en {compteurTour} déplacements! :)");
            }
        }
    }
    else
    {
        if(A=='r')
        {
            Console.WriteLine("Appuyer sur z pour swiper vers le haut");
            Console.WriteLine("Appuyer sur s pour swiper vers le bas");
            Console.WriteLine("Appuyer sur d pour swiper vers la droite");
            Console.WriteLine("Appuyer sur q pour swiper vers la gauche");            
            Console.Write("\n");

            Console.WriteLine("Attention: dès que le tableau est plein vous avez perdu");
            Console.Write("\n");
            Console.WriteLine("Valeur des items:");
            Console.Write("Le bonbon ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("¤ ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(": 1 point");
            Console.Write("\n");
            Console.Write("Le rouleau de réglisse ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("@ ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(": 3 points");
            Console.Write("\n");
            Console.Write("Le donut ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("o ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(": 7 points");
            Console.Write("\n");
            Console.Write("Le sucre d'orge ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("J ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(": 15 points");
            Console.Write("\n");

        }
    }
}


Main();
// efets sonores
// rapport
// matrice

*/