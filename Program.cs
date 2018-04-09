using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ConsoleApp1
{   public class Suduku
    {
        int[,] numbers = new int[9,9];
        /*public int[] Get_sample()
        {
            int[] sample = new int[9]; 
            return sample;
        }*/
        public int Check()/*检查该矩阵是否满足数独规则，0-满足规则*/
        {                   /*1--9-该0行出错；10--18-该数字-9列出错；19--27- -18个宫格出错*/
            for(int i = 0; i < 9; i++)/*检查行的重复*/
            {
                bool[] line = new bool[10];
                for (int j = 0; j < 9; j++)
                {
                    if (line[numbers[i,j]] && numbers[i, j] != 0) return i;/*出错行为第i行*/
                    else line[numbers[i,j]] = true;
                }
            }
            for (int i = 0; i < 9; i++)/*检查列的重复*/
            {
                bool[] row = new bool[10];
                for (int j = 0; j < 9; j++)
                {
                    if (row[numbers[j, i]]&&numbers[j, i]!=0) return i+9;/*出错在第i列*/
                    else row[numbers[j, i]] = true;
                }
            }
            int p = 3, q=3;
            while(p <= 9 && q <= 9)
            {
                bool[] pane = new bool[10];
                for (int i = 0; i < p; i++)
                {
                    for (int j = 0; j < q; j++)
                    {
                        if (pane[numbers[i, j]] && numbers[i, j] != 0) return i+18;/*出错行为第i个宫格*/
                        else pane[numbers[i, j]] = true;
                    }
                }
                p += 3;
                q += 3;
            }
            
            return 0;/*未发现错误*/
        }
        public static void Write_txt(int[,] ans,int k)
        {
            /*FileStream fw = new FileStream("out.txt", FileMode.Create,FileAccess.Write);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fw, ans);
            fw.Dispose();
            for(int i=0;i<9;i++)
            {
                
                for (int j = 0; j < 9; j++)
                {
                    fw.Write(ans[i, j].ToString() + " ");sw.Flush();
                }
                fw.WriteLine();
            }*/
            File.sw.WriteLine("{0}", k);
            for (int i = 0; i < 9; i++)
            {

                for (int j = 0; j < 9; j++)
                {
                    File.sw.Write(ans[i, j].ToString() + " ");
                    //sw.Dispose();
                }
                File.sw.WriteLine();
            }
            //sw.Close();

        }
        public static void Exchange(int[,] ans,int before,int after,bool Is_line)
        {
            if(Is_line)
            for (int i = 0; i < 9; i++) 
            {
                int temp = ans[before, i];
                ans[before, i] = ans[after,i];
                ans[after, i] = temp;
            }
            else
                for (int i = 0; i < 9; i++)
                {
                    int temp = ans[i, before];
                    ans[i, before] = ans[i, after];
                    ans[i, after] = temp;
                }
        }
        public static void Dfs(int n, int[] sample)
        {
            if (n >= 9)
            {
                int[,] ans = new int[9, 9];
                int[] shift = { 0, 3, 6, 7, 4, 1, 2, 5, 8 };
                for (int i=0;i<9;i++)
                {
                    for(int j=0;j<9;j++)
                    {
                        ans[i, j] = sample[(j + shift[i]) % 9];
                    }
                }
                int[,] line = { { 3, 4, 5, 6, 7, 8 }, { 5, 3, 4, 8, 6, 7 } };
                int[,] row = { { 0, 3, 4, 5, 6, 7, 8 }, { 0, 5, 3, 4, 8, 6, 7 } };
                for (int i=0;i<6;i++)
                {
                    if (Count.count > 1000000) return;
                    Suduku.Exchange(ans,line[0,i],line[1,i],true);
                    for (int j = 0; j < 6; j++)
                    {
                        Suduku.Exchange(ans, row[0, j], row[1, j], false);
                        //Write_txt(ans,count);
                        Count.count++;
                       // Console.WriteLine("{0}",Count.count);
                    }
                }
                

            }
            for (int i = 1; i <= 9; i++)
            {
                int j = 0, flag = 0;
                while (j < n)
                {
                    if (sample[j] == i)
                    {
                        flag = 1;
                        break;
                    }
                    j++;
                }
                if (flag == 0)
                {
                    sample[n] = i;
                    //Console.WriteLine("{0}",i);
                    Dfs(n + 1, sample);
                }
            }
        }
        public static void Swap(int[] sample,int i,int j)
        {
            int temp = sample[i];
            sample[i] = sample[j];
            sample[j] = temp;
        }
        public static void Create_suduku(int k)
        {
            int[,] ans = new int[9, 9];//ans是当前的数独方案
            int count = 0;
            ans[0, 0] = 4;

                int[] sample = { 4, 1, 2, 3, 5, 6, 7, 8, 9 };
                sample[0] = 4;
                for(int i=1;i<9;i++)
                {
                    for(int j=i;j<9;j++)
                    {
                        Suduku.Swap(sample, i, j);
                        foreach (int temp in sample)
                            Console.Write("{0} ",temp);
                        Console.WriteLine();
                        //Console.ReadKey();
                        int[] shift = { 0, 3, 6, 7, 4, 1, 2, 5, 8 };
                        for (int p = 0; p < 9; p++)
                        {
                            for (int q = 0; q < 9; q++)
                            {
                                ans[p, q] = sample[(q + shift[p]) % 9];
                            }
                        }
                        int[,] line = { { 3, 4, 5, 6, 7, 8 }, { 5, 3, 4, 8, 6, 7 } };
                        int[,] row = { { 0, 3, 4, 5, 6, 7, 8 }, { 0, 5, 3, 4, 8, 6, 7 } };
                        for (int p = 0; p < 6; p++)
                        {
                            if (count > k) return;
                            Suduku.Exchange(ans, line[0, p], line[1, p], true);
                            for (int q = 0; q < 6; q++)
                            {
                                Suduku.Exchange(ans, row[0, q], row[1, q], false);
                                Write_txt(ans,count);
                                count++;
                                // Console.WriteLine("{0}",Count.count);
                            }
                        }
                    }
                }
               
            
            
            
        }
   }
    public class  File
    {
        public static StreamWriter sw = new StreamWriter("out.txt",false, Encoding.Default);
    }
    public class Count
    {
        public static int count = 0;
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] Line = new int[9];
            
            Line[0] = 4;
            //Suduku.Create_suduku(1000000);
            Suduku.Dfs(1,Line);
        }
    }
}
