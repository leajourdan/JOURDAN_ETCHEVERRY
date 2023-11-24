using System.Diagnostics;

int CountZeros(int[] tableau)
{
    int count = 0;
    foreach (int element  in tableau)
    {

        if (element == 0)
        {
            count++;
        }
    }
    return count;
}
int Lignes(int [][] tableauJeu){
    int n= tableauJeu.Length;
    Random indice= new Random();
    int trouve=0;
    int k=0;
    while (trouve==0 & k<n){
        int a=indice.Next(0,n);
        if(CountZeros(tableauJeu[a])!=0){
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

int [][] ApresTour(int[][] tableauJeu){
    int indice=Lignes(tableauJeu);
    int n= tableauJeu.Length;
    Random aleatoire= new Random();
    int nb= aleatoire.Next(0,n);
    if (indice==-1){
        Console.WriteLine("Tu as perdu! Gros nul");     //Si la grille est pleine , le joueur a perdu
    
    }
    else{
        do {
            nb= aleatoire.Next(0,n);
        }
        while(tableauJeu[indice][nb]!=0);
    }
    tableauJeu[indice][nb]=1;
    return tableauJeu;
}

void AfficherItem(int numero){
    if (numero==0)
        Console.Write("   ");
    if (numero==1){
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(" ¤ ");
        Console.ForegroundColor = ConsoleColor.Gray;    }
    if (numero==2){
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(" @ ");
        Console.ForegroundColor = ConsoleColor.Gray;  
    }
    if (numero==3){
        Console.ForegroundColor = ConsoleColor.DarkRed; // Rouge foncé
        Console.Write(" o ");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
    if (numero==4){
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(" J ");
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}

int Score(int [][] tableauJeu){
    int score=0;
    int n= tableauJeu.Length;
    for (int i=0; i<n; i++){
        for (int j=0; j<n; j++){
            if (tableauJeu[i][j]==1)
                score+=1;
            if (tableauJeu[i][j]==2)
                score+=3;
            if (tableauJeu[i][j]==3)
                score+=7;
            if (tableauJeu[i][j]==4)
                score+=15;
        }
    }
    return score;
}



void AfficherTableau(int[][] tableauJeu){
    int n=tableauJeu.Length;
    Console.BackgroundColor = ConsoleColor.White;
    for (int i=0;i<n;i++){
        Console.WriteLine(string.Concat(Enumerable.Repeat("",n)));
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write("|");
        Console.ForegroundColor = ConsoleColor.Gray;
        for(int j=0; j<n;j++){
            AfficherItem(tableauJeu[i][j]);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("|");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
Console.Write('\n');
Console.BackgroundColor = ConsoleColor.Black;
}   
 


        // Déclaration et initialisation d'un tableau de tableaux
int[][] tableauDeTableaux = new int[3][];

        // Initialisation des sous-tableaux
        tableauDeTableaux[0] = new int[] {1,0,0};
        tableauDeTableaux[1] = new int[] {0,1,2};
        tableauDeTableaux[2] = new int[] {1,1,2};

int [][] Mouvement (int [][] tableauJeu, char direction){
    int n=tableauJeu.Length;

   if (direction == 'z'){
    for (int i=0; i<n ; i++){
        int fusion=0;
        for (int j=1; j<n;j++){
            int k=0;
            while ( j-1-k>=0 && tableauJeu[j-1-k][i]==0){
                tableauJeu[j-1-k][i]=tableauJeu[j-k][i];
                tableauJeu[j-k][i]=0;
                k++;
            }
            if (j-k!=0){
                if (tableauJeu[j-1-k][i]==tableauJeu[j-k][i] && fusion == 0){
                    tableauJeu[j-1-k][i]++;
                    tableauJeu[j-k][i]=0;
                    fusion=1;
            }   }
        }
    }
    }
    if (direction == 'q'){
        for (int i=0; i<n ; i++){
            int fusion=0;
            for (int j=1;j<n;j++){
                int k=0;
                while( j-1-k>=0 &&tableauJeu[i][j-1-k]==0 ){
                    tableauJeu[i][j-1-k]=tableauJeu[i][j-k];
                    tableauJeu[i][j-k]=0;
                    k++;
                }
                if (j-k!=0){
                    if (tableauJeu[i][j-1-k]==tableauJeu[i][j-k] && fusion==0){
                        tableauJeu[i][j-1-k]++;
                        fusion=1;
                        tableauJeu[i][j-k]=0;
                }}
            }
        }
    }
    
 if (direction == 'd'){
        for (int i=0; i<n ; i++){
            int fusion=0;
            for (int j=n-2;j>=0;j--){
                int k=0;
                while( j+1+k<=n-1 &&tableauJeu[i][j+1+k]==0 ){
                    tableauJeu[i][j+1+k]=tableauJeu[i][j+k];
                    tableauJeu[i][j+k]=0;
                    k++;
                }
                if (j+k!=n-1){
                    if (tableauJeu[i][j+1+k]==tableauJeu[i][j+k] && fusion==0){
                        tableauJeu[i][j+1+k]++;
                        fusion=1;
                        tableauJeu[i][j+k]=0;
                }}
            }
        }
    }
    if (direction == 's'){
        for (int i=0; i<n ; i++){
            int fusion=0;
            for (int j=n-1;j>=0;j--){
                int k=0;
                while( j+1+k<=n-1 &&tableauJeu[j+1+k][i]==0 ){
                    tableauJeu [j+1+k][i] =tableauJeu[j+k][i];
                    tableauJeu[j+k][i]=0;
                    k++;
                }
                if (j+k!=n-1){
                    if (tableauJeu[j+1+k][i]==tableauJeu[j+k][i] && fusion==0){
                        tableauJeu[j+1+k][i]++;
                        fusion=1;
                        tableauJeu[j+k][i]=0;
                }}
            }
        }
    }

return tableauJeu;
}


 static void AfficherTableauDeTableaux(int[][] tableauDeTableaux)
    {
        for (int i = 0; i < tableauDeTableaux.Length; i++)
        {
            for (int j = 0; j < tableauDeTableaux[i].Length; j++)
            {
                Console.Write(tableauDeTableaux[i][j] + " ");
            }
            Console.WriteLine(); // Passer à la ligne après chaque sous-tableau
        }
    }
 int [][] a = Mouvement(tableauDeTableaux,'s');
AfficherTableauDeTableaux(a);