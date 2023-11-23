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
        Console.WriteLine("");
    if (numero==1)
        Console.WriteLine("¤");
    if (numero==2)
        Console.WriteLine("@");
    if (numero==3)
        Console.WriteLine("o");
    if (numero==4)
        Console.WriteLine("J");
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