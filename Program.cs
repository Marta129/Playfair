using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        char[,] matrix = new char[5, 5];
        Console.WriteLine("Enter Text:");
        string input=Console.ReadLine();
        Console.WriteLine("Enter Key:");
        string key=Console.ReadLine();
        matrix = Matrix(key,matrix);
        input=Transform(input);
        Console.Write("Encrypted:");
        //Encrypt
        input = Playfair(input, matrix, true);
        Console.Write(input);
        Console.WriteLine();
        Console.Write("Decrypted:");
        //Decrypt
        input=Playfair(input, matrix, false);
        Console.Write(input);
    }
    static string Playfair(string input,char[,]matrix,bool okay)
    {
        string aux = "";
        for(int i=0;i<input.Length-1;i++)
        {
            int a = 0, b = 0, c = 0, d = 0;
            for(int j=0;j<matrix.GetLength(0);j++)
            {
                for(int k=0;k<matrix.GetLength(1);k++)
                {
                    if (input[i] == matrix[j,k])
                    {
                        a = j;
                        b = k;
                    }
                    else
                    {
                        if (input[i + 1] == matrix[j,k])
                        {
                            c = j;
                            d = k;
                        }
                    }
                }
            }
            if(a==c)
            {
                switch (okay)
                {
                    //Encrypt
                    case true:
                        b = (b + 1) % 5;
                        d = (d + 1) % 5;
                        break;
                    //Decrypt
                    case false:
                        b = (b + 4) % 5;
                        d = (d + 4) % 5;
                        break;
                }
                aux =aux+ matrix[a, b] + matrix[c, d];
            }
            else
            {
                if(b==d)
                {
                    switch (okay)
                    {
                        case true:
                            a= (a + 1) % 5;
                            c= (c + 1) % 5;
                            break;
                        case false:
                            a=(a + 4) % 5;
                            c=(c + 4) % 5;
                            break;
                    }
                    aux =aux+ matrix[a, b] + matrix[c, d];
                }
                else
                {
                    aux =aux+ matrix[a, d] + matrix[c, b];
                }
            }
            i++;
        }
        input=aux;
        return input;
    }
    static string Transform(string input)
    {
        input=input.ToUpper();
        string a = "";
        for(int i=0;i<input.Length;i++)
        {
            int aux = (int)input[i];
            if(65<=aux && aux<=90)
            {
                if(a.Length==0)
                {
                    a += input[i];
                }
                else
                {
                    if (input[i] == a[a.Length-1])
                    {
                        a += 'X';
                    }
                    a +=input[i];
                }
            }
        }
        if(a.Length%2==1)
        {
            a += 'X';
        }
        input = a;
        return input;
    }
    static char[,] Matrix(string key, char[,]matrix)
    {
        key = key.ToUpper();
        int[] v = new int[91];
        int j = 0, k = 0;
        for(int i=0;i<key.Length;i++)
        {
            int aux = (int)key[i];
            char letter;
            if(65<=aux && aux<=90)
            {
                if(!(k<matrix.GetLength(1)))
                {
                    j++;
                    k = 0;
                }
                switch ((char)aux)
                {
                    case 'J':
                        letter = 'I';
                        break;
                    default:
                        letter = (char)aux;
                        break;
                }
                if (v[(int)letter]==0)
                {
                    matrix[j, k] = letter;
                    v[(int)letter]++;
                    k++;
                }
            }
        }
        for(int i=65;i<=90;i++)
        {
            if (v[i]==0 && i!=74)
            {
                if(!(k<matrix.GetLength(1)))
                {
                    j++;
                    k = 0;
                }
                matrix[j, k] = (char)i;
                k++;
            }
        }
        return matrix;
    }
}

