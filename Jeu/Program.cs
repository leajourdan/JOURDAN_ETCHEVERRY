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
    int indice=Lignes(tableauDeTableaux);
    int n= tableauJeu.Length;
    Random aleatoire= new Random();
    int nb= aleatoire.Next(0,n);
    if (indice==-1){
        Console.WriteLine("Tu as perdu! Gros nul");
    
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
coucou
