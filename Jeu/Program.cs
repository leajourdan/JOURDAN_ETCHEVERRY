using System.Diagnostics;
using System.IO;
using System.Text;


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
    while (trouve==0 & k<100*n) // explication du k:  si le random ne trouve pas la case vide en n tentatives, le jeu s'arrete donc on augemnte le nombre de chiffre aleatoire generés pour etre certain de trouver une case vide. 
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
    /*if (numero==5){
        Console.WriteLine("Victoire");
        partieEnCours=0;
    }*/
}


int Score(int [][] tableauJeu) //fonction calculant le score
{    
    int score=0;
    for (int i=0; i<n; i++) // On parcourt les cases une à une en ajoutant à chaque fois au score la valeur des bonbons correspondante
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
                     /*if (tableauJeu[j+1+k][i]+tableauJeu[j+k][i] == 5){
                        Console.WriteLine("impossible, swipe autrement");
                        char u=Convert.ToChar(Console.ReadLine()!);
                        Mouvement(tableauFinal,u);
                    }*/
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

void AfficherLentement(string texte){
    foreach (char c in texte)
    {
        Console.Write(c);
        System.Threading.Thread.Sleep(30);
    }
}

void Main()
{
    Console.Write('\n');
    AfficherLentement("Pressez A pour jouer");
    Console.Write('\n');
    AfficherLentement("Pressez R pour voir les commandes et les règles ");
    Console.Write('\n');
    char A = Convert.ToChar(Console.ReadLine());
    if (A=='a')
    {
        Console.Write("\n");
        AfficherLentement("Choisir le mode de jeu:");
        Console.Write("\n");
        AfficherLentement("1: Facile (50 points en 35 déplacements)");
        Console.Write("\n");
        AfficherLentement("2: Moyen (50 points en 30 déplacements)");
        Console.Write("\n");
        AfficherLentement("3: Difficile (50 points en 20 déplacements)");
        Console.Write("\n");
        AfficherLentement("4: Record ");// libre
        Console.Write("\n");
        AfficherLentement("Saisir le chiffre souhaité:");
        Console.Write('\n');
        int B= Convert.ToInt32(Console.ReadLine());
        if(B==4)
        {
            StreamReader sr = new StreamReader("Score.txt");
            //Read the first line of text
            int meilleurScore= Convert.ToInt32(sr.ReadLine());
            sr.Close();
            AfficherLentement($"Record à battre: {meilleurScore} points");
            Console.Write('\n');
            AfficherLentement("Choisir la taille du tableau: exemple 3 pour 3x3");
            Console.Write('\n');
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
                Console.WriteLine($"Score={Score(tableauFinal)}");
                Console.WriteLine("Swiper dans une direction");
                char direction=Convert.ToChar(Console.ReadLine()!);
                Mouvement(tableauFinal,direction);
                ApresTour(tableauFinal);
                AfficherTableau(tableauFinal);
                Score(tableauFinal);
                scoreFinal=Score(tableauFinal);
                if (Lignes(tableauFinal)==-1)
                    partieEnCours=0;
            }
            Console.Write('\n');
            AfficherLentement ($"Votre score est de {scoreFinal} points.");
            if (scoreFinal>meilleurScore)
            {
                meilleurScore=scoreFinal;
                StreamWriter sw = new StreamWriter(new FileStream("Score.txt",FileMode.Create));
                sw.WriteLine(Score(tableauFinal));
                Console.Write('\n');
                AfficherLentement("Bravo, vous avez battu un nouveau record!");
                sw.Close();
            }
            
            else 
            {
                Console.Write('\n');
                AfficherLentement($"Le meilleur score est de {meilleurScore} points.");
            
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
                Console.Write('\n');
                AfficherLentement($"DOMMAGE.   Tu as atteint {scoreFinal} points en {compteurTour} déplacements...");
                Console.Write('\n');
                AfficherLentement("Réessaie :)");

            }
            else 
            {
                Console.Write('\n');
                AfficherLentement($"Félicitations ! Tu as atteint {scoreFinal} points en {compteurTour} déplacements! :)");
            }
        }
    }
    else
    {
        if(A=='r')
        {
            Console.Write("\n");
            AfficherLentement("COMMANDES:");
            AfficherLentement("Appuyer sur z pour swiper vers le haut");
            Console.Write("\n");
            AfficherLentement("Appuyer sur s pour swiper vers le bas");
            Console.Write("\n");
            AfficherLentement("Appuyer sur d pour swiper vers la droite");
            Console.Write("\n");
            AfficherLentement("Appuyer sur q pour swiper vers la gauche");            
            Console.Write("\n");
            Console.Write('\n');
            AfficherLentement("VALEUR DES ITEMS:");
            Console.Write("\n");
            AfficherLentement("Le bonbon ");
            Console.ForegroundColor = ConsoleColor.Red;
            AfficherLentement("¤ ");
            Console.ForegroundColor = ConsoleColor.Gray;
            AfficherLentement(": 1 point");
            Console.Write("\n");
            AfficherLentement("Le rouleau de réglisse ");
            Console.ForegroundColor = ConsoleColor.Blue;
            AfficherLentement("@ ");
            Console.ForegroundColor = ConsoleColor.Gray;
            AfficherLentement(": 3 points");
            Console.Write("\n");
            AfficherLentement("Le donut ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            AfficherLentement("o ");
            Console.ForegroundColor = ConsoleColor.Gray;
            AfficherLentement(": 7 points");
            Console.Write("\n");
            AfficherLentement("Le sucre d'orge ");
            Console.ForegroundColor = ConsoleColor.Green;
            AfficherLentement("J ");
            Console.ForegroundColor = ConsoleColor.Gray;
            AfficherLentement(": 15 points");
            Console.Write("\n");
            Console.Write("\n");
            AfficherLentement("Attention: dès que le tableau est plein vous avez perdu");
        }
    }
}


Main();
// efets sonores
// rapport
// matrice
