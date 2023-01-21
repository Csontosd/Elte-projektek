///  Név: Csontos Dávid
/// Neptun-kód: Elaip4
/// E-mail: Csontos.david01@gmail.com

using System;
using System.Collections.Generic;
using System.Linq;


namespace Programozasi_alapismeretek_Komplex_Beadando
{

    class Program
    {
        static void Main(string[] args)
        {
            // Versenyző kutyák és a versenyszempontok bekérése, egy sorban szóközzel elválasztva.
            Console.WriteLine("Adja meg a kutyaszépség versenyen résztvevő kutyák számát, majd az értékelési szempontok számát egy sorban, szóközzel elválasztva!");
            string[] elsosor = Console.ReadLine().Split(' ');
            int N = 0;
            int M = 0;
            int min = 0;
            int max = 100;
            // Függvény, amely ellenőrzi, hogy a beírt adatokat át lehet-e int-té konvertálni, valamint, hogy 1 és 100 között vannak-e. 
            // Ha hibás, valamelyik adat, akkor a program jelzi a felhasználónak és új adatot kér be.
            N = Nvizsgalat(elsosor, min, max);
            // Függvény, amely ellenőrzi, hogy a beírt adatokat átlehet-e int-té konvertálni, valamint, hogy 1 és 100 között vannak-e. 
            // Ha hibás valamelyik adat, akkor a program jelzi a felhasználónak és új adatot kér be.
            M = Mvizsgalat(elsosor, min, max);
            // A versenyszempontok maximális értékének bekérése.
            Console.WriteLine("Adja meg az egyes versenyszempontok maximális értékét.");
            int[] maxpontszamok = new int[M];
            string uzenet = $"Adjon meg annyi számot, mint amennyi a versenyszempontok száma, {M}, az értékeket szóközzel kell elválasztani.\n";
            Console.Write(uzenet);
            // Függvény, amely ellenőrzi, hogy a beírt adatokat átlehet-e int-té konvertálni valamint hogy 1 és 100 között vannak-e. 
            // Ha hibás valamelyik adat akkor a program jelzi a felhasználónak és új adatot kér be.
            while (!TryParseMaxok(Console.ReadLine(), M, out maxpontszamok))
            {
                Console.WriteLine("Helytelen bemenet próbálja újra...");
            }
            // A versenyszempontok minimális értékének bekérése.
            Console.WriteLine("Adja meg az egyes versenyszempontok minimális értékét.");
            int[] minpontszamok = new int[M];
            string uzenet2 = $"Adjon meg annyi számot, mint amennyi a versenyszempontok száma, azaz {M}, az értékeket szóközzel kell elválasztani.\nÜgyeljen arra, hogy az egyes szempontok maxpontszámánál ne adjon többet.\n";
            Console.Write(uzenet2);
            // Függvény, amely ellenőrzi, hogy a beírt adatokat át lehet-e int-té konvertálni, valamint, hogy 1 és 100 között vannak-e. 
            // Ha hibás valamelyik adat, akkor a program jelzi a felhasználónak és új adatot kér be.
            while (!TryParseMin(Console.ReadLine(), M, maxpontszamok, out minpontszamok))
            {
                Console.WriteLine("Helytelen bemenet próbáld újra...");
            }

            // Kutyák mátrixának és a szempontok listájának létrehozása.
            int[,] kutyaMatrix;
            kutyaMatrix = new int[N, M];
            List<int> SzempontokSzamaÉsSorszáma = new List<int>();
            // Kutya mátrix adatainak bekérése.
            Console.WriteLine("Adja meg a kutyák és a pontszámok mátrixát, a sorok kutyákat, míg az oszlopok a pontszámokat jelölik");
            kutyaMatrixLetrehozas(N, M, kutyaMatrix, maxpontszamok, minpontszamok);
            // A program végig megy a mátrix sorain és oszlopain, és ha a maxpontszámok tömb valamelyik indexe megegyezik a mátrix azon indexedik elemével, 
            // akkor azt az indexet hozzáadja a SzempontokSzamaÉsSorszáma listához. Mivel az index 0-tól indul, ezért hozzá kell adnunk plusz 1-et, hogy a tényleges sorszámot kapjuk. 
            SzempontlistaFeltoltes(maxpontszamok, kutyaMatrix, SzempontokSzamaÉsSorszáma);
            // Mivel az előző ciklus kétszer adja hozzá ugyanazt az indexet, ezért a Distinct metódussal kiválasszuk a lista csak azon tagjait, amelyek különbözőek.
            List<int> DistinctSzempontokSzamaÉsSorszáma = new List<int>();
            DistinctSzempontokSzamaÉsSorszáma = SzempontokSzamaÉsSorszáma.Distinct().ToList();
            int SzempontokSzama = DistinctSzempontokSzamaÉsSorszáma.Count;

            // A különböző versenyszempontok számának és a különböző versenyszempontok listájának tagjainak kiírása.
            kiíras(DistinctSzempontokSzamaÉsSorszáma, SzempontokSzama);

            
        }



        static int Nvizsgalat(string[] elsosor, int min, int max)
        {
            int N;

            while (!int.TryParse(elsosor[0], out N) || N <= min || max <= N)
            {
                Console.WriteLine("Az első adat helytelen, írja be csak az első adatot...");
                elsosor[0] = Console.ReadLine();
            }

            return N;
        }

        static int Mvizsgalat(string[] elsosor, int min, int max)
        {
            int M;
            while (!int.TryParse(elsosor[1], out M) || M <= min || max <= M)
            {
                Console.WriteLine("A második adat helytelen, írja be csak a második adatot...");
                elsosor[1] = Console.ReadLine();
            }

            return M;
        }
        public static bool TryParseMaxok(string input, int M, out int[] maxpontszamok)
        {
            maxpontszamok = default;
            string[] sor = input.Split(" ");
            int[] eredmeny = new int[M];
            for (int i = 0; i < M; i++)
            {
                if (sor.Length < M || !int.TryParse(sor[i], out eredmeny[i]) || (eredmeny[i] <= 1 || eredmeny[i] > 100))
                {
                    return false;
                }
            }
            maxpontszamok = eredmeny;
            return true;
        }

        public static bool TryParseMin(string input2, int M, int[] maxpontszamok, out int[] minpontszamok)
        {
            minpontszamok = default;
            string[] sorok = input2.Split(" ");
            int[] eredmeny2 = new int[M];
            for (int i = 0; i < M; i++)
            {
                if (sorok.Length < M || !int.TryParse(sorok[i], out eredmeny2[i]) || (eredmeny2[i] < 1 || eredmeny2[i] >= maxpontszamok[i]))
                {
                    return false;
                }
            }
            minpontszamok = eredmeny2;
            return true;
        }

        static void kutyaMatrixLetrehozas(int N, int M, int[,] kutyaMatrix, int[] maxpontszamok, int[] minpontszamok)
        {

            for (int i = 0; i < N; i++)
            {
                string[] bemenetString = Console.ReadLine().Split(' ');
                for (int j = 0; j < M; j++)
                {
                    kutyaMatrix[i, j] = int.Parse(bemenetString[j]);
                    if (kutyaMatrix[i, j] > maxpontszamok[j] || kutyaMatrix[i, j] < minpontszamok[j])
                    {
                        Console.WriteLine("Helytelen bemenet próbálja újra...");
                        kutyaMatrixLetrehozas(N, M, kutyaMatrix, maxpontszamok, minpontszamok);
                    }

                }
            }

        }

        static void SzempontlistaFeltoltes(int[] maxpontszamok, int[,] kutyaMatrix, List<int> SzempontokSzamaÉsSorszáma)
        {
            for (int i = 0; i < kutyaMatrix.GetLength(1); i++)
            {
                for (int j = 0; j < kutyaMatrix.GetLength(0); j++)
                {
                    if (kutyaMatrix[j, i] == maxpontszamok[i])
                    {
                        SzempontokSzamaÉsSorszáma.Add(i + 1);
                    }
                }

            }
        }

        static void kiíras(List<int> DistinctSzempontokSzamaÉsSorszáma, int SzempontokSzama)
        {
            Console.Write(SzempontokSzama + " ");
            for (int i = 0; i < DistinctSzempontokSzamaÉsSorszáma.Count; i++)
            {
                Console.Write(DistinctSzempontokSzamaÉsSorszáma[i] + " ");
            }
        }
    }
}


